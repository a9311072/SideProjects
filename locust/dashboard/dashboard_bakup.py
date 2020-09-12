import csv
import random
import argparse
import os.path
import company_configs
from pydoc import locate
from locust import HttpLocust, TaskSet, task

users_info =None

def company_setting():
    company = 'dev-cn'
    if '-' in company:
        company = company.replace('-','_')
    host_configs = locate('company_configs.' + company)
    return host_configs

def get_user_list(host):
    global users_info
    with open(os.path.join('../token/', host +'.csv' )) as csvfile:
        reader = csv.reader(csvfile, delimiter="\n")
        users_info = list(reader)
    

class UserTasks(TaskSet):
    def get_headers_json(self, user):
        return {'Accept': 'application/json', 'Content-Type': 'application/json', 'X-Auth-Token': user}

    def get_headers(self):
        user = random.choice(users_info)
        return self.get_headers_json(user[0])

    @task(1)
    def get_widgets(self):
        headers = self.get_headers()
        self.client.get('/api/me/dashboard?size=200', headers = headers)

    # @task(2)
    # def get_highlights(self):
    #     headers = self.get_headers()
    #     self.client.get('/api/me/highlights?size=16', headers = headers)

    # @task(3)
    # def get_account(self):
    #     headers = self.get_headers()
    #     self.client.get('/api/account', headers = headers)   
    

class WebsiteUser(HttpLocust):
    min_wait = 2000
    max_wait = 20000
    company = company_setting()
    host = company.url

    get_user_list(company.name)
    task_set = UserTasks