import csv
import os.path

def user_list(host):
    with open(os.path.join('../token/', host +'.csv' )) as csvfile:
        reader = csv.reader(csvfile, delimiter="\n")
        users_info = list(reader)
    return users_info