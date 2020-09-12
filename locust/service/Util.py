import requests
import json
import os
import multiprocessing
from multiprocessing.pool import ThreadPool
import logging
import threading
from datetime import datetime
from datetime import timedelta
import time
import inspect
import sys
import company_configs
# import Announcement
import random


class Util(object):
    def __init__(self, obj):
        self._obj = obj
    
    def request(self, action, url, token=None, data=None, headers=None, perf=False):
        url = self._obj.url + url
        if headers==None:   
            headers = self.getHeaders(token)

        start_time = datetime.now()
        res = requests.request(action, url, data=data, headers=headers)
        spent_time = datetime.now() - start_time
        time.sleep(self._obj.think_time)
        caller = token.split('@')

        notation = ", "
        class_func_name = inspect.stack()[1][0].f_locals['self'].__class__.__name__ + "." + inspect.stack()[1][3]
        msg = notation.join((" "+ str(res.status_code), str(spent_time), class_func_name, caller[0]))

        if res.status_code >= 300: 
            logging.error(msg + ', ' + res.text + ', ')
        else:                      
            logging.info(msg)
        return json.loads(res.text)

    def getUserToken(self, username):
        url = "/api/users/user_id%40company_id/token"
        url = url.replace("user_id", username)
        url = url.replace("company_id", self._obj.name)
        return Util(self._obj).request("GET", url, self._obj.token)

    def get_token_by_thread(self, username):
        url = "/api/users/user_id%40company_id/token"
        url = url.replace("user_id", username)
        url = url.replace("company_id", self._obj.name)
        url = self._obj.url + url
        headers = self.getHeaders(self._obj.token)
        res = requests.request("GET", url, headers= headers)
        res = json.loads(res.text)
        # logging.INFO(res['token'])
        print(res['token'])
        return str(res['token'])
    
    def getPackageVersion(self):
        url = '/api/packageVersion'
        return Util(self._obj).request("GET", url, self._obj.token)

    def getHeaders(self, token):
        return { 'X-Auth-Token': token , 
                 'Accept': "application/json",
                 'Content-Type': "application/json"}


    def clearCache(self):
        url = "/api/cache/clear/all"
        return Util(self._obj).request("DELETE", url, self._obj.token)

    def getDate(self, days=None):
        if days is None:    now = datetime.now() # current date and time
        else:               now = datetime.now() + timedelta(days=days) 
        return now.strftime("%Y-%m-%d")

    def getDateTime(self, days=None):
        if days is None:    now = datetime.now() # current date and time
        else:               now = datetime.now() + timedelta(days=days) 
        return now.strftime("%Y-%m-%dT%H:%M:%S.%f")[:-3] + 'Z'
    
    def announcement_processing(self, method, channel, thread_id, username):
        pool = multiprocessing.Pool(multiprocessing.cpu_count())  
        for index in range(username):
            userid = username + str(index)
            pool.apply_async(method, args=(channel, thread_id, userid)) 
        pool.close()
        pool.join()

    def announcement_threading(self, pool_size, method, company, channel, thread_id, username):
        pool = ThreadPool(pool_size)
        for index in range(len(username)):
            pool.apply_async(method, args=(company, channel, thread_id, username[index])) 
        pool.close()
        pool.join()
    
    def func_threading(self, pool_size, method, *args):
        token_list = []
        pool = ThreadPool(pool_size)
        for index in range(len(args.user_list)):
            user_token = pool.apply_async(method, args=(*args,)) 
            token_list.append(user_token)
        pool.close()
        pool.join()

    def token_threading(self, pool_size, method, data_list):
        result = []
        pool = ThreadPool(pool_size)
        # for index in range(len(data_list)):
        #     row = pool.apply_async(method, args=(data_list[index], )) 
        #     # result.append(row.get())
        #     result.append(row)

        for index in range(0,1000):
            row = pool.apply_async(method, args=(data_list[index], )) 
            # result.append(row.get())
            result.append(row)
        pool.close()
        pool.join()
        return result
        
    def to_csv(self, file_path, data_list):
        with open(file_path, 'w') as wfile:
            for line in data_list:
                wfile.write(line + "\n")       
        wfile.close()
        print("Write file successfully.")
        # writeFile.close()
    
    def getChannelLoginIds(self, channel):
        url = "/api/announcementChannel/channel/members?active=true"
        url = url.replace("channel",channel)
        # response = requests.request("GET", url, headers=self._obj.headers)
        res = self.request('GET', url, self._obj.token)
        # res_list = json.loads(res.text)
        login = []
        for i in range(len(res)):  login.append(res[i]["login"])
        return login

    def get_channel_tokens(self, channel):
        csv_list = []
        login_list = self.getChannelLoginIds(channel)
        for index in range(len(login_list)):
            res = Util(self._obj).getUserToken(login_list[index])
            csv_list.append(res["token"])   

        return csv_list
    
    def get_channel_csv_tokens(self, channel):
        csv_list = []
        login_list = self.getChannelLoginIds(channel)
        for index in range(len(login_list)):
            res = self.getUserToken(login_list[index])
            csv_list.append(res["token"])   
        self.to_csv('.\\token\\'+ self._obj +'.csv',csv_list)
        print('Export csv sccussfully!')
    
    def get_context_count(self, file_name, context):
        f = open(file_name, "r")
        if f.mode == 'r':
            contents =f.read()
            temp= contents.count(context)
            print(temp)
    
 