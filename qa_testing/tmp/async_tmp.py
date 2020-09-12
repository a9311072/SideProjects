import json
import time
import aiohttp
import asyncio
from pydoc import locate
from service.util import UtilService

headers = { 'X-Auth-Token': "" ,
            'Accept': "application/json",
            'Content-Type': "application/json"}

async def download_site(session, url):
    async with session.get(url, headers=headers) as response:
        return await response.json()


async def download_all_sites(sites):
    async with aiohttp.ClientSession() as session:
        print('start to async getting data')
        tasks = []
        for url in sites:
            task = asyncio.ensure_future(download_site(session, url))
            tasks.append(task)
        results = await asyncio.gather(*tasks, return_exceptions=True)
        
        for result in results:
            print(result['token'])


if __name__ == "__main__":

    sites = []
    name = 'qa'

    host = locate('service.host.' + name.replace('-','_'))
    util = UtilService(host)
    login_list = util.get_channel_login_list('branch_177')
    for login in login_list:
        url = f"https://{name}.example-.com/api/users/{login}%40{name}/token"
        sites.append(url)
    
    start_time = time.time()
    asyncio.get_event_loop().run_until_complete(download_all_sites(sites))
    duration = time.time() - start_time
    print(f"Get {len(sites)} sites in {duration} seconds")