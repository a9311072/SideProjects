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
        for index in range(2500,3001):
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
    
    def specific_string_count(self, file_name, context):
        f = open(file_name, "r")
        if f.mode == 'r':
            contents =f.read()
            temp= contents.count(context)
            print(temp)
    
    def create_users(self,loginId):
        url = "/api/users"
        payload = '{"activated":true,"authorities":["ROLE_USER"],"login":"username","employeeId":"username","firstName":"username", \
            "lastName":"chen","mobilePhone":null,"phoneCountryCode":null,"userProfile":{"jobTitle":{"id":1,"name":"Dev L1","department":{}}, \
            "department":{"id":1,"name":"Dev","companyId":1},"branches":[{"id":2}],"branchesEntity":{"_collection":[{"id":2}]}, \
            "country":{},"userGroupsEntity":{"_collection":[]},"userGroups":[],"company":{"id":1},"supervisor":null},"langKey":"en-US", \
            "password":"kitchen123","displayName":"username","tags":[]}'
        payload = payload.replace('username', loginId)
        return Util(self._obj).request("POST", url, self._obj.token, data=payload)

    def submit_survey(self, token):
        url = "/api/me/survey-assignments/2915"
        payload = '{"id":null,"startDate":"2020-02-12T21:41:19.938Z","finishDate":"2020-02-12T21:43:38.758Z","duration":137358,"result":[\
        {"questionId":7758,"title":"New Question","answer":[_value],"type":"likert","metaData":{"originalType":"likert"},"answerText":[_text],"duration":2733,"score": _index,"version":2},\
        {"questionId":7759,"title":"New Question","answer":[_value],"type":"checkbox","metaData":{"originalType":"checkbox"},"answerText":[_text],"duration":2622,"score":null,"version":2},\
        {"questionId":7760,"title":"New Question","answer":[_value],"type":"radio","metaData":{"originalType":"radio"},"answerText":[_text],"duration":2864,"score":4,"version":2},\
        {"questionId":7761,"title":"New Question","answer":[_value],"type":"scale","metaData":{"originalType":"scale"},"answerText":[_text],"duration":3997,"score":1,"version":2},\
        {"questionId":7763,"title":"New Question","answer":[],"type":"freetext","metaData":{"originalType":"freetext"},"answerText":[_text],"duration":125142,"score":null,"version":2}]}'
                
        _index, _value, _text = self.answer_survey_question()
        payload = payload.replace('_value', _value)
        payload = payload.replace('_text', _text)
        payload = payload.replace('_index', str(_index))
        return Util(self._obj).request("POST", url, token, data=payload)
    
    def answer_survey_question(self):
        answer_value=['{"value":0}','{"value":1}','{"value":2}','{"value":3}','{"value":4}']
        answer_text=['{"text":"A"}','{"text\":"B"}','{"text":"C"}','{"text":"D"}','{"text":"E"}']
        answer=[0,1,2,3,4]

        index = random.choice(answer)
        value = answer_value[index]
        text = answer_text[index]

        return index, value, text


    # def get_user_list(self,host):
    #     import csv
    #     import os.path
    #     with open(os.path.join('../token/', host +'.csv' )) as csvfile:
    #         reader = csv.reader(csvfile, delimiter="\n")
    #         users_info = list(reader)
    #     return users_info

# def get_user_list(self,host):
#     import csv
#     import os.path
#     with open(os.path.join('../token/', host +'.csv' )) as csvfile:
#         reader = csv.reader(csvfile, delimiter="\n")
#         users_info = list(reader)
#     return users_info