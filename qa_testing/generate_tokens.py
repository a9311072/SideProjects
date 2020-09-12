import time
import json
import logging
import argparse
import os.path
from pydoc import locate

from service import host
from service.util import UtilService


def get_tokens(host, paras):
    data = []
    util = UtilService(host)

    login_list = util.get_channel_login_list(paras.channel)
    start = time.time()
    res = util.func_threading(paras.pool, util.get_token_by_thread, login_list)
    end = time.time()
    print(end-start)

    for res_data in res:
        data.append(res_data.get())
        
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
        # parser.add_argument('-n','--channel', default='branch_2315', help='Setup the ann/mm channel')
        parser.add_argument('-n','--channel', default='channel', help='Setup the ann/mm channel')
        parser.add_argument('-p','--pool', default=16, help='Setup the threading pools')
        parser.add_argument('-t','--think_time', default=1, help='Setup the think time')
        paras = parser.parse_args()

        host = locate('service.host.' + paras.company.replace('-','_'))     
        host.think_time = int(paras.think_time)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Main function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        get_tokens(host, paras)


    except Exception as e:   
        logging.error(e)
    