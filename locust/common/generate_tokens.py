import json
import logging
import argparse
import company_configs
import os.path
from Util import Util
from pydoc import locate


def get_tokens(host, paras):
    util = Util(host)
    login_list = util.getChannelLoginIds(paras.channel)
    res = util.token_threading(paras.pool, util.get_token_by_thread, login_list)

    data = []
    for res_data in res:
        data.append(res_data.get())

    file_path = os.path.join('../token/', host.name +'8.csv' )
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
        parser.add_argument('-c','--company', default='qa-cn', help='Setup the environment')
        parser.add_argument('-n','--channel', default='channel', help='Setup the ann/mm channel')
        parser.add_argument('-p','--pool', default=16, help='Setup the threading pools')
        parser.add_argument('-t','--think_time', default=0.5, help='Setup the think time')
        paras = parser.parse_args()

        if '-' in paras.company:
            paras.company = paras.company.replace('-','_')
        host_configs = locate('company_configs.' + paras.company)     
        host_configs.think_time = int(paras.think_time)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Main function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        get_tokens(host_configs, paras)

    except Exception as e:   logging.error(e)
    