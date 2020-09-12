import random
import sys
sys.path.append('./service')
from get_data import get_json_header, get_config
from locust import HttpLocust, TaskSet, task


company, users_info = get_config()
url= '/api/rewards/multiple-receivers?size=200'
dataPayload = '{\
  "message": "Send Star Performance Testing",\
  "pointCount": 1,\
  "reasonId": 1,\
  "receiverUsers": [\
    {\
      "activated": false,\
      "id": receiverId,\
      "favorite": false,\
      "isFavorite": false,\
      "isNewUser": false,\
      "isSupervisor": false,\
      "newUser": false,\
      "requireResetPassword": false\
     }]}'

class UserTasks(TaskSet):
    def get_headers(self):
        user = random.choice(users_info)
        return get_json_header(user[0])

    @task
    def task1(self):
        headers = self.get_headers()
        receiverId = str(random.randrange(6,50000))
        data = dataPayload.replace('receiverId', receiverId)
        self.client.post(url , headers=headers, data= data)

class WebsiteUser(HttpLocust):
    min_wait = company.min_time * 1000
    max_wait = company.max_time * 1000
    host = company.url
    task_set = UserTasks