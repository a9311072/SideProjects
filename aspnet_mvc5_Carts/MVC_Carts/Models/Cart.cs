﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Carts.Models
{
    [Serializable]
    public class Cart:IEnumerable<CartItem>
    {
        private List<CartItem> cartItems;
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }
        /// <summary>
        /// 取得購物車內商品的總數量
        /// </summary>
        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }


        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0.0m;
                foreach(var cartItem in this.cartItems)
                {
                    totalAmount = totalAmount + cartItem.Amount;
                }
                return totalAmount;
            }
        }


        //新增一筆Product，使用ProductId
        public bool AddProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            //判斷相同Id的CartItem是否已經存在購物車內
            if (findItem == default(CartItem))
            {   //不存在購物車內，則新增一筆
                using (Models.CartsEntities db = new CartsEntities())
                {
                    var product = (from s in db.Products
                                   where s.Id == ProductId
                                   select s).FirstOrDefault();
                    if (product != default(Models.Product))
                    {
                        this.AddProduct(product);
                    }
                }
            }
            else
            {   //存在購物車內，則將商品數量累加
                findItem.Quantity += 1;
            }
            return true;
        }

        //新增一筆Product，使用Product物件
        private bool AddProduct(Product product)
        {
            //將Product轉為CartItem
            var cartItem = new CartItem()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                DefaultImageURL = product.DefaultImageURL,
                Quantity = 1
            };

            //加入CartItem至購物車
            this.cartItems.Add(cartItem);
            return true;
        }

        //移除一筆Product，使用ProductId
        public bool RemoveProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            //判斷相同Id的CartItem是否已經存在購物車內
            if (findItem == default(Models.CartItem))
            {
                //不存在購物車內，不需做任何動作
            }
            else
            {   //存在購物車內，將商品移除
                this.cartItems.Remove(findItem);
            }
            return true;
        }

        //清空購物車
        public bool ClearCart()
        {
            this.cartItems.Clear();
            return true;
        }

        //將購物車商品轉成OrderDetail的List
        public List<Models.OrderDetail> ToOrderDetailList(int orderId)
        {
            var result = new List<Models.OrderDetail>();
            foreach (var cartItem in this.cartItems)
            {
                result.Add(new Models.OrderDetail()
                {
                    Id = cartItem.Id,
                    Name = cartItem.Name,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                    OrderId = orderId
                });
            }
            return result;
        }


        #region IEnumerator

        IEnumerator<CartItem> IEnumerable<CartItem>.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        #endregion

    }
}