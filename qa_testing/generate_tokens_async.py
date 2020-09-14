import time
import json
import logging
import argparse
import os.path
import asyncio
import concurrent.futures
from pydoc import locate

from service import host
from service.util import UtilService
from service.announcement import AnnouncementService


def get_tokens_async(host, paras):
    data = []
    sites = []
    util = UtilService(host)

    login_list = util.get_channel_login_list(paras.channel)
    for login in login_list:
        url = f"https://api-{host.name}.com/api/users/{login}%40{host.name}/token"
        sites.append(url)
       
    start = time.time()
    results = asyncio.get_event_loop().run_until_complete(util.get_sites_async(sites))
    print(' TimeSpent:', time.time()-start)
    for result in results:
        data.append(result['token'])

    file_path = os.path.join('token', host.name +'.csv' )
    util.to_csv(file_path, data)


if __name__ == "__main__":
    try: 
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Company Settings
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        logging.basicConfig(level=logging.INFO)
        # logging.basicConfig(filename='debug.log',
        #                     filemode='w',
        #                     format='%(asctime)s,%(msecs)d %(name)s %(levelname)s %(message)s',
        #                     datefmt='%H:%M:%S',
        #                     level=logging.DEBUG)

        parser = argparse.ArgumentParser()
        parser.add_argument('-c','--company', default='qa', help='Setup the environment')
        parser.add_argument('-n','--channel', default='channel', help='Setup the ann/mm channel')
        parser.add_argument('-t','--think_time', default=1, help='Setup the think time')
        paras = parser.parse_args()

        host = locate('service.host.' + paras.company.replace('-','_'))     
        host.think_time = int(paras.think_time)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Main function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        get_tokens_async(host, paras)


    except Exception as e:   
        logging.error(e)
    
