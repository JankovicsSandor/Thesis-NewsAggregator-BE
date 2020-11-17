#!flask/bin/python
from flask import Flask,jsonify,abort,request
from newGroupDoneEvent import NewsGroupDoneEvent
from message import RabbitMqConnectionManager
from text_sim import TextSimilarity
app = Flask(__name__)
PREFIX = "/api/similarity"
eventBroker = RabbitMqConnectionManager()
similarityManager=TextSimilarity()


@app.route(PREFIX+'/')
def index():
    return "Hello, World!"

#@app.route(PREFIX+'/mostSimilar',methods=["POST"])
#def getMostSimilarArticles():
#    if not request.json:
#        abort(400)
#    return jsonify({'id':convertedId}),201

def newMessage(ch, method, properties, body):   
    print(" [x] %r" % body)
    #print(" [x] %r" % similarityManager.getMostSimilarNews(body.summary))
    eventBroker.sendEvent('NewsGroupDoneEvent',NewsGroupDoneEvent([]))

@app.teardown_request
def checkin_db(exc):
    if eventBroker:
        eventBroker.closeConnection()

eventBroker.registerToEvent('AddNewArticleEvent', newMessage)  
if __name__ == '__main__':
    app.run()


