import os
from locust_plugins.csvreader import CSVReader

host='dev-cn'
print(os.path.join('../token/', host +'.csv' ))
data = CSVReader(os.path.join('../token/', host +'.csv' ))
print('teat')
