import requests
import json

from service.request import RequestService


class AnnouncementService(object):
    def __init__(self, obj):
        self._obj = obj

    def attachmentUpload(self, channel):
        url = f"/api/announcementplus/{channel}/attachments"
        files = {"file": ("DigitalTranfromation(5+BJC).jpg", open("DigitalTranfromation(5+BJC).jpg", 'rb'))}
        header = {'X-Auth-Token': ""}
        res =  RequestService(self._obj).request('POST', url, headers=header, files=files)
        uuid = res["uuid"]
        print(uuid)

    def create_announcement(self, channel, uuid):
        url = f"/api/announcementplus/{channel}/messages"
        payload = '{"subject":"Business Transformation Survey-my_count","message":"MyMessage","channel":"_channel_",\
            "attachments":[{"fileName":"DigitalTranfromation.jpg","contentType":"image/jpeg","progres":100,"extension":"jpg","completed":true,\
            "uuid":"_uuid_"}],"author":{},"enableAck":true}'
        payload = payload.replace("_uuid_", uuid)
        payload = payload.replace("_channel_", channel)
        return RequestService(self._obj).request("POST", url, data=payload, headers=self._obj.headers)

    def update_predefined_group(self, user_id):
        url = "/api/predefined-groups"
        payload = '{"id": 451, "name": "Taipei QA 001", "members": [ member_id ], "subGroupIds": [], "criteria": [], "privateGroup": false}'
        payload = payload.replace("member_id", user_id)
        return RequestService(self._obj).request("PUT", url, data=payload, headers=self._obj.headers)
    
    def get_ack_count(self, token, channel, thread_id):
        url = f"/api/announcementplus/{channel}/messages/{thread_id}/ack/count"
        return RequestService(self._obj).request("GET", url, token)

    def put_ack(self, token,channel,thread_id):
        url = f"/api/announcementplus/{channel}/messages/{thread_id}/ack"
        return RequestService(self._obj).request("PUT", url, token)

    def put_read(self, token, channel, thread_id):
        url = f"/api/announcementplus/{channel}/messages/{thread_id}/read"

        return RequestService(self._obj).request("PUT", url, token)
    
    def get_ack_report(self, token, channel, thread_id):
        url = f"/api/announcementplus/{channel}/messages/{thread_id}/ack/report"
        return RequestService(self._obj).request("GET", url, token)
