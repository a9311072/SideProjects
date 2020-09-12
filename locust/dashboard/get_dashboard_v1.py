import random
import sys
sys.path.append('./service')
from get_data import get_json_header, get_config
from locust import HttpLocust, TaskSet, task


company, users_info = get_config()
url = '/api/me/dashboard?size=200'

class UserTasks(TaskSet):
    def get_headers(self):
        user = random.choice(users_info)
        return get_json_header(user[0])

    @task
    def get_widgets(self):
        headers = self.get_headers()
        self.client.get(url, headers = headers)

    # @task
    # def get_account(self):
    #     headers = self.get_headers()
    #     self.client.get('/api/account', headers = headers)   
    

class WebsiteUser(HttpLocust):
    min_wait = company.min_time * 1000
    max_wait = company.max_time * 1000
    host = company.url
    task_set = UserTasks