import json
import logging
import argparse
import os.path

from pydoc import locate
from service import host
from service.dashboard import DashboardService


def create_users(host, paras):
    dashboard = DashboardService(host)
    for i in range(20000):
        dashboard.create_users('system_created_' + str(i))
        print(str(i))


if __name__ == "__main__":
    try: 
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Company Settings
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        logging.basicConfig(level=logging.INFO)

        parser = argparse.ArgumentParser()
        parser.add_argument('-c','--company', default='qa', help='Setup the environment')
        parser.add_argument('-n','--channel', default='channel', help='Setup the ann/mm channel')
        parser.add_argument('-p','--pool', default=26, help='Setup the threading pools')
        parser.add_argument('-t','--think_time', default=1, help='Setup the think time')
        paras = parser.parse_args()

        host = locate('service.host.' + paras.company.replace('-','_'))     
        host.think_time = int(paras.think_time)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Main function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        create_users(host, paras)

    except Exception as e:   
        logging.error(e)
    