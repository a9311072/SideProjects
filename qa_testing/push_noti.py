import json
import logging
import csv
import argparse
from pydoc import locate

from service import host
from service.util import UtilService
from service.dashboard import DashboardService
from service.announcement import AnnouncementService

def push_noti(host, paras):
    channel = paras.channel
    sender = paras.sender
    receiver = paras.receiver
    feature = str(paras.feature).lower()

    util = UtilService(host)
    dashboard = DashboardService(host)
    
    token = util.get_user_token(sender)

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    # Start
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    if feature == 'all' or 'ann' in feature:
        dashboard.create_anncouncement(token, channel)                # create announcement

    if feature == 'all' or 'survey' in feature:
        res = dashboard.create_survey(token)                          # create survey
        dashboard.create_survey_question(token, res["id"])
        dashboard.publish_survey(token, res["id"])  

    if feature == ('all' or 'star' or 'task' or '1on1' or 'assign') in feature:
        res = dashboard.get_friend_list(sender)
        sender_id = str(res[0]['id'])
        res = dashboard.get_friend_list(receiver)
        receiver_id = str(res[0]['id'])
 
    if feature == 'all' or 'star' in feature or 'reward' in feature:
        dashboard.create_sendStars(token, receiver_id)                # create send star
    if feature == 'all' or 'task' in feature:
        dashboard.create_taskAssignment(token, receiver_id)           # create task assignment

    if feature == 'all' or '1on1' in feature:
        res = dashboard.get_1on1_today(token)
        res_1on1 = remove_1on1_with_recevier(token, host, res, receiver_id)
        if res_1on1 == False:
            res = dashboard.get_1on1_upcoming(token)
            res_1on1 = remove_1on1_with_recevier(token, host, res, receiver_id)   
        if res_1on1 == False:
            res = dashboard.get_1on1_missing(token)
            remove_1on1_with_recevier(token, host, res, receiver_id)   
        res = dashboard.create_1on1(token, receiver_id)                       # create 1on1
  
        
    if feature == 'all' or 'assign' in feature:
        dashboard.create_course_assignment(token,  receiver_id)

    if feature == 'all' or 'course' in feature:
        res = dashboard.create_course(token)
        course_id = res["id"] 
        dashboard.create_scheduled_predefined_group(token, course_id)
        dashboard.publish_course(token, course_id)
        dashboard.publish_course_push_noti()

 

def remove_1on1_with_recevier(token, company, res, receiver):
    if len(res) > 0:  
        for i in range(0, len(res)):
            if res[i]['receiver']['id'] == int(receiver):
                DashboardService(company).remove_1on1(token, res[i]["id"]) 
                return True
    return False


if __name__ == "__main__":
    try: 
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Company Settings
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        logging.basicConfig(level=logging.INFO)

        parser = argparse.ArgumentParser()
        parser.add_argument('-c','--company', default='qa', help='Setup the environment')
        parser.add_argument('-f','--feature', default='ann', help='Select all or one feature')
        parser.add_argument('-s','--sender', default='user1', help='Setup the sender')
        parser.add_argument('-r','--receiver', default='user2', help='Setup the receiver')
        parser.add_argument('-n','--channel', default='channel1', help='Setup the ann/mm channel')
        parser.add_argument('-l','--loop', default=1, help='Setup loop count')
        parser.add_argument('-p','--pool', default=50, help='Setup the threading pools')
        parser.add_argument('-t','--think_time', default=1.5, help='Setup the think time')
        paras = parser.parse_args()

        host = locate('service.host.' + paras.company.replace('-','_'))     
        host.think_time = int(paras.think_time)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        # Main function
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        for i in range(0, int(paras.loop)):
            push_noti(host, paras)


    except Exception as e:   logging.error(e)
    