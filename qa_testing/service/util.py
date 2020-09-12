import requests
import json
import os
import time
import inspect
import sys
import random
import aiohttp
import asyncio
import logging
import threading
import multiprocessing
import mysql.connector
from multiprocessing.pool import ThreadPool

from datetime import datetime
from datetime import timedelta
from service.request import RequestService


class UtilService(object):
    def __init__(self, obj):
        self._obj = obj 

    def get_user_token(self, username):
        url = f"/api/users/{username}%40{self._obj.name}/token"
        res = RequestService(self._obj).request("GET", url, self._obj.token)
        return str(res['token'])

    def get_token_by_thread(self, user_id, *args):
        url = f"/api/users/{user_id}%40{self._obj.name}/token"
        url = self._obj.url + url
        headers = self.getHeaders(self._obj.token)
        res = requests.request("GET", url, headers= headers)
        res = json.loads(res.text)
        # logging.info(str(res['token']))
        return str(res['token'])
    
    def getPackageVersion(self):
        url = '/api/packageVersion'
        return RequestService(self._obj).request("GET", url, self._obj.token)

    def getHeaders(self, token):
        return { 'X-Auth-Token': token , 
                 'Accept': "application/json",
                 'Content-Type': "application/json"}

    def clearCache(self):
        url = "/api/cache/clear/all"
        return RequestService(self._obj).request("DELETE", url, self._obj.token)

    def getDate(self, days=None):
        if days is None:    now = datetime.now() # current date and time
        else:               now = datetime.now() + timedelta(days=days) 
        return now.strftime("%Y-%m-%d")

    def getDateTime(self, days=None):
        if days is None:    now = datetime.now() # current date and time
        else:               now = datetime.now() + timedelta(days=days) 
        return now.strftime("%Y-%m-%dT%H:%M:%S.%f")[:-3] + 'Z'
    
    async def get_url_async(self, session, url):
        async with session.get(url, headers=self.getHeaders(self._obj.token)) as response:
            return await response.json()

    async def get_sites_async(self, sites):    
        async with aiohttp.ClientSession() as session:
            logging.info('start to get data async')
            tasks = []
            for url in sites:
                task = asyncio.ensure_future(self.get_url_async(session, url))
                tasks.append(task)
            results = await asyncio.gather(*tasks, return_exceptions=True)
            return results

    def func_processing(self, pool_size, method, data_list, *args):
        result = []
        # pool = multiprocessing.Pool(multiprocessing.cpu_count())
        pool = multiprocessing.Pool(pool_size)
        for index in range(len(data_list)):
            row = pool.apply_async(method, args=(data_list[index], *args, )) 
            result.append(row)
        pool.close()
        pool.join()
        return result

    def func_threading(self, pool_size, method, data_list, *args):
        result = []
        pool = ThreadPool(pool_size)
        for index in range(len(data_list)):
            row = pool.apply_async(method, args=(data_list[index], *args, )) 
            result.append(row)
        pool.close()
        pool.join()
        return result
        
    def sql_query(self, sqlcontext):
        try:
            conn = mysql.connector.connect(
                user=self._obj.db_username,
                password=self._obj.db_password,
                host=self._obj.db_endpoint,
                database=self._obj.db_schema
            )
            logging.info('Connecting to database')
            logging.info('Sqlcontext:' + sqlcontext)
            cursor = conn.cursor()
            cursor.execute(sqlcontext)
            result = cursor.fetchall()

        except mysql.connector.Error as e:
            logging.error('Connecting to MySQL failed: %s' % e)
        finally:
            if (conn.is_connected()):
                cursor.close()
                conn.close()
                logging.info("MySQL connection is closed")  
        return result

    def to_csv(self, file_path, data_list):
        with open(file_path, 'w') as wfile:
            for line in data_list:
                wfile.write(line + "\n")       
        logging.info("Write file successfully.")

    def read_csv(self, file_path):
        import csv
        with open(file_path, 'r') as rfile:
            data = []
            reader = csv.reader(rfile)
            for row in reader:
                data.append(row)
        return data

    def get_channel_login_list(self, channel):
        url = f"/api/announcementChannel/{channel}/members?active=true"
        res = RequestService(self._obj).request('GET', url, self._obj.token)      
        login = []
        for i in range(len(res)):  login.append(res[i]["login"])
        return login
    
    def get_channel_user_ids(self, channel):
        url = f"/api/announcementChannel/{channel}/members?active=true"
        res = RequestService(self._obj).request('GET', url, self._obj.token)
        user_ids = []
        for i in range(len(res)):  user_ids.append(res[i]["id"])
        return user_ids

    def get_channel_tokens(self, channel):
        csv_list = []
        login_list = self.get_channel_login_list(channel)
        for index in range(len(login_list)):
            res = self.get_user_token(login_list[index])
            csv_list.append(res)   
        return csv_list
    
    def get_channel_tokens_to_csv(self, channel):
        csv_list = []
        login_list = self.get_channel_login_list(channel)
        for index in range(len(login_list)):
            res = self.get_user_token(login_list[index])
            csv_list.append(res)    
        self.to_csv('.\\token\\'+ self._obj.name +'.csv',csv_list)
        logging.info('Export csv sccussfully!')
    
    def get_context_count(self, file_name, context):
        f = open(file_name, "r")
        if f.mode == 'r':
            contents =f.read()
            result= contents.count(context)
            logging.info(result)
    
