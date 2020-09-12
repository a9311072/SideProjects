import random
import sys
sys.path.append('./service')
from get_data import get_json_header, get_config
from locust import HttpLocust, TaskSet, task


company, users_info = get_config()
surveyId = company.surveyId
url= f'/api/me/survey-assignments/{surveyId}'
payload = '{"id":null,"startDate":"2020-02-18T14:28:53.604Z","finishDate":"2020-02-18T14:29:04.015Z","duration":10409,"result":[\
    {"questionId":8,"title":"likert","answer":[{"value":1}],"type":"likert","metaData":{"originalType":"likert"},"answerText":[{"text":"1"}],"duration":2305,"score":1,"version":2},\
    {"questionId":9,"title":"checkbox","answer":[{"value":0}],"type":"checkbox","metaData":{"originalType":"checkbox"},"answerText":[{"text":"A"}],"duration":2239,"score":null,"version":2},\
    {"questionId":10,"title":"radio","answer":[{"value":0}],"type":"radio","metaData":{"originalType":"radio"},"answerText":[{"text":"A"}],"duration":1768,"version":2},\
    {"questionId":11,"title":"scale","answer":[{"value":1}],"type":"scale","metaData":{"originalType":"scale"},"answerText":[{"text":"1"}],"duration":1968,"score":1,"version":2},\
    {"questionId":12,"title":"freetext","answer":[],"type":"freetext","metaData":{"originalType":"freetext"},"answerText":[{"text":"s"}],"duration":2129,"score":null,"version":2}]}'
   
    
class UserTasks(TaskSet):
    def get_headers(self):
        user = random.choice(users_info)
        return get_json_header(user[0])

    @task
    def task1(self):
        headers = self.get_headers()
        self.client.post(url , headers = headers, data=payload)

class WebsiteUser(HttpLocust):
    min_wait = company.min_time * 1000
    max_wait = company.max_time * 1000
    host = company.url
    task_set = UserTasks