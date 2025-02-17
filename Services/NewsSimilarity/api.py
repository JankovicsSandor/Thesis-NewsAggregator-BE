#!flask/bin/python
from flask import Flask,jsonify,abort,request
from textToId import TextToId
from message import RabbitMqConnectionManager
from text_sim import TextSimilarity
app = Flask(__name__)
PREFIX = "/api/similarity"
eventBroker = RabbitMqConnectionManager()
similarityManager=TextSimilarity()


@app.route(PREFIX+'/')
def index():
    return "Hello, World!"

@app.route(PREFIX+'/mostSimilar',methods=["POST"])
def getMostSimilarArticles():
    if not request.json:
        abort(400)
    converter=TextToId()
    convertedId=converter.createIdFromText(request.json['summary'])
    return jsonify({'id':convertedId}),201

def newMessage(ch, method, properties, body):   
    print(" [x] %r" % body)
    print(" [x] %r" % similarityManager.getMostSimilarNews(body.summary))

@app.teardown_request
def checkin_db(exc):
    if eventBroker:
        eventBroker.closeConnection()

eventBroker.registerToEvent('AddNewArticleEvent', newMessage)  
if __name__ == '__main__':
    app.run()


