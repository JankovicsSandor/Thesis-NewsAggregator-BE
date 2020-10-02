#!flask/bin/python
from flask import Flask,jsonify,abort,request
from textToId import TextToId
from database import DatabaseConnection

app = Flask(__name__)
PREFIX = "/api/similarity"
database=DatabaseConnection()
databaseSession=database.getDatabaseConnection()

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


@app.teardown_request
def checkin_db(exc):
    if databaseSession:
        database.closeConnection()
    
if __name__ == '__main__':
    app.run()
