import json
import logging
import argparse
import os.path
from pydoc import locate

from service import host
from service.util import UtilService
from service.announcement import AnnouncementService


def query_data(host, paras):
    util = UtilService(_host)
    context = "SELECT * FROM qa.jhi_user where id < 100"
    data = util.sql_query(context)

    for i in range(len(data)):
        print(data[i][1])   

    
if __name__ == "__main__":
    try: 
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Company Settings
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        logging.basicConfig(level=logging.INFO)

        parser = argparse.ArgumentParser()
        parser.add_argument('-c','--company', default='qa', help='Setup the environment')
        parser.add_argument('-n','--channel', default='channel7', help='Setup the ann/mm channel')
        parser.add_argument('-p','--pool', default=16, help='Setup the threading pools')
        parser.add_argument('-t','--think_time', default=1, help='Setup the think time')
        paras = parser.parse_args()

        if '-' in paras.company:
            paras.company = paras.company.replace('-','_')
        _host = locate('service.host.' + paras.company)     
        _host.think_time = int(paras.think_time)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Main function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        query_data(_host, paras)

    except Exception as e:   
        logging.error(e)
    