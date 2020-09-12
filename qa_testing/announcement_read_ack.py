import json
import logging
import argparse
from pydoc import locate

from service import host
from service.announcement import AnnouncementService
from service.util import UtilService


def announcement(host, paras):
    util = UtilService(host)
    ann = AnnouncementService(host)

    thread_id=""
    login_list = util.get_channel_login_list(paras.channel)

    for i in range(30):
        res = util.get_user_token(login_list[i])
        _token=res
        ann.put_read(_token, paras.channel, thread_id)
        ann.put_read(_token, paras.channel, thread_id)
        ann.put_read(_token, paras.channel, thread_id)
        ann.put_ack(_token, paras.channel, thread_id)
        ann.put_read(_token, paras.channel, thread_id)
        ann.put_read(_token, paras.channel, thread_id)
        ann.put_ack(_token, paras.channel, thread_id)
        ann.put_read(_token, paras.channel, thread_id)
        ann.put_read(_token, paras.channel, thread_id)

    
if __name__ == "__main__":
    try: 
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Company Settings
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        logging.basicConfig(level=logging.INFO)

        parser = argparse.ArgumentParser()
        parser.add_argument('-c','--company', default='qa', help='Setup the environment')
        parser.add_argument('-f','--feature', default='ann', help='Select all or one feature')
        parser.add_argument('-n','--channel', default='channel', help='Setup the ann/mm channel')
        parser.add_argument('-l','--loop', default=1, help='Setup loop count')
        parser.add_argument('-p','--pool', default=50, help='Setup the threading pools')
        parser.add_argument('-t','--think_time', default=1, help='Setup the think time')
        paras = parser.parse_args()

        host = locate('service.host.' + paras.company.replace('-','_'))     
        host.think_time = int(paras.think_time)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Main function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        announcement(host, paras)


    except Exception as e:   
        logging.error(e)
    