import csv
import os.path

import sys
sys.path.append('./service')
from host import host


def user_list(host):
    with open(os.path.join('./token/', host +'.csv' )) as csvfile:
        reader = csv.reader(csvfile, delimiter="\n")
        users_info = list(reader)
    return users_info

def get_json_header(user):
    return {'X-Auth-Token': user, 'Accept': 'application/json', 'Content-Type': 'application/json'}

def get_config():
    company = host()
    users_info = user_list(company.name)
    return company, users_info
