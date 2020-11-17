import pika
import os
import json

class RabbitMqConnectionManager(object):

    connection = None
    def registerToEvent(self,eventName,callback):
        credentials = pika.PlainCredentials(os.environ['RABBITMQ_USER'], os.environ['RABBITMQ_PASS'])
        self.connection = pika.BlockingConnection(pika.ConnectionParameters(os.environ['RABBITMQ_HOST'],5672,"/",credentials,connection_attempts=4,retry_delay=5.0))
        channel = self.connection.channel()

       # channel.exchange_declare(exchange='news_aggregator_bus', exchange_type='direct')
        channel.queue_declare(queue=eventName, exclusive=True)
        channel.queue_bind(exchange='news_aggregator_bus', queue=eventName)
        print(' [*] Waiting for messages. To exit press CTRL+C')

        channel.basic_consume(queue=eventName, on_message_callback=callback, auto_ack=True)     
        channel.start_consuming()
    
    def closeConnection(self):
        if (self.connection):
            self.connection.close()
    
    def sendEvent(self, eventName, eventBody):
        if (self.connection):
            channel = self.connection.channel()
            message = json.dumps(eventBody)
            channel.basic_publish(exchange="news_aggregator_bus",routing_key=eventName,body=message)
