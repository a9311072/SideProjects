from locust import HttpLocust, TaskSet, task
import json
import csv
import random

users_info =None
class UserBehavior(TaskSet):
    def get_user(self):
        user = random.choice(users_info)
        return user

    @task(1)
    def get_siparislerim(self):
        # user = self.get_user()
        # print(user)
        headers = {'Content-Type': 'application/json', 'X-Auth-Token': ''}
        self.client.get('/api/widgets', headers = headers)

class WebsiteUser(HttpLocust):
    task_set = UserBehavior

    def __init__(self):
        global users_info
        with open('qa.csv') as csvfile:
            reader = csv.reader(csvfile, delimiter="\n")
            users_info = list(reader)