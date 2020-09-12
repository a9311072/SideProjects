import json
import logging
import argparse
import os.path
import random
from pydoc import locate

from service import host
from service.util import UtilService
from service.announcement import AnnouncementService
from service.dashboard import DashboardService

def get_tokens(host, paras):
    util = UtilService(host)
    dashboard = DashboardService(host)

    file_path = os.path.join('token', host.name +'.csv' )
    data = util.read_csv(file_path)
    lesson_list = ['abcd','defg']
    lesson_status = ['LESSON_VIEWED', 'LESSON_COMPLETED']

    for i in data:
        login = random.choice(data)
        lesson = random.choice(lesson_list)
        status = random.choice(lesson_status)
        dashboard.update_lesson_progress(login[0], lesson, status)


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
        parser.add_argument('-t','--think_time', default=0.8, help='Setup the think time')
        paras = parser.parse_args()

        host = locate('service.host.' + paras.company.replace('-','_'))       
        host.think_time = int(paras.think_time)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Main function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        get_tokens(host, paras)

    except Exception as e:   
        logging.error(e)
    