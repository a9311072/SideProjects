
def Solution(a,b):  
    result = []
    result.append(findSquareByTwoNumbers(a, b))
    result.append(findSquareByOneNumber(a, b))
    return max(result)
  
def findSquareByTwoNumbers(a,b):
    count = 1
    while True:
        if (a//count) + (b//count) >=4:
            count +=  1
        else:
            return count - 1
            
def findSquareByOneNumber(a,b):
    arr = []
    arr.append(a)
    arr.append(b)
    maxVal = max(arr)
    count = 1
    while True:
        if (maxVal//count) >=4:
            count +=  1
        else:
            return count - 1

# Driver code  
if __name__ == "__main__" :  
    a=10
    b=21
    print(Solution(a,b))
    a = 1
    b = 8
    print(Solution(a,b))
    a =13
    b = 11
    print(Solution(a,b))
    a =2 
    b = 1
    print(Solution(a,b))