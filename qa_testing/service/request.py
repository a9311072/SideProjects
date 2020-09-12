import requests
import json
import logging
from datetime import datetime
import time
import inspect
import asyncio
# import host

class RequestService(object):
    def __init__(self, obj):
        self.obj = obj
    
    def request(self, method, url, token=None, data=None, headers=None, files= None, params=None):
        url = self.obj.url + url
        if headers==None:   
            headers = self.get_headers(token)

        start_time = datetime.now()
        res = requests.request(method, url, data=data, headers=headers, files=files, params=params)
        spent_time = datetime.now() - start_time
        time.sleep(self.obj.think_time)
        
        caller = token.split('@')
        comma = ', '
        class_func_name = inspect.stack()[1][0].f_locals['self'].__class__.__name__ + "." + inspect.stack()[1][3]
        msg = comma.join((' ' + str(res.status_code), str(spent_time)[:-3], class_func_name, caller[0]))

        if res.status_code in range(200,299): 
            logging.info(msg)
        else:                    
            logging.error(msg + comma + res.text)

        return json.loads(res.text)

    def get_headers(self, token):
        return { 'X-Auth-Token': token , 
                 'Accept': "application/json",
                 'Content-Type': "application/json"}

