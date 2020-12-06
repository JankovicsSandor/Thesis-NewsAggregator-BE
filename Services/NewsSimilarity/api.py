#!flask/bin/python
from flask import Flask,jsonify,abort,request
from message import RabbitMqConnectionManager
from newsGroupDoneEvent import NewsGroupDoneEvent
from text_sim import TextSimilarity
app = Flask(__name__)
PREFIX = "/api/similarity"
eventBroker = RabbitMqConnectionManager()
similarityManager=TextSimilarity()
import json


@app.route(PREFIX+'/')
def index():
    return "Hello, World!"

counter=1
def newMessage(ch, method, properties, body):
    global counter
    print(counter)
    counter = counter + 1
    eventBody=json.loads(body.decode())
    print(" [x] %r" % eventBody)
    #print(" [x] %r" % similarityManager.getMostSimilarNews(body.summary))
    # TODO get most similar articles
    todaySimilarity = similarityManager.getMostSimilarNews(eventBody["Id"],eventBody["Description"])
    if (todaySimilarity == ""):
        eventBroker.sendEvent('NewsGroupDoneEvent',NewsGroupDoneEvent(json.loads(body.decode()),"")) 
    else:
        eventBroker.sendEvent('NewsGroupDoneEvent',NewsGroupDoneEvent(json.loads(body.decode()),todaySimilarity))
    

@app.teardown_request
def checkin_db(exc):
    if eventBroker:
        eventBroker.closeConnection()

eventBroker.registerToEvent('NewsArticlesSim','AddNewArticleEvent', newMessage)  
if __name__ == '__main__':
    app.run()


