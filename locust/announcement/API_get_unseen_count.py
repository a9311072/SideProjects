import csv
import random
import os.path
from locust import HttpLocust, TaskSet, task

users_info =None

def get_user_list(host):
    global users_info
    with open(os.path.join('./token/', host +'.csv' )) as csvfile:
        reader = csv.reader(csvfile, delimiter="\n")
        users_info = list(reader)
    
class UserTasks(TaskSet):
    def get_headers_json(self, user):
        return {'Accept': 'application/json', 'Content-Type': 'application/json', 'X-Auth-Token': user}

    def get_headers(self):
        user = random.choice(users_info)
        return self.get_headers_json(user[0])

    @task(1)
    def task1(self):
        headers = self.get_headers()
        self.client.get('/api/me/notification-items/unseen-count', headers = headers)

    
class WebsiteUser(HttpLocust):
    min_wait = 2000
    max_wait = 20000
    name = ""
    host = ""

    get_user_list(name)
    task_set = UserTasks