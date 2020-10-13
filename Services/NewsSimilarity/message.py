import pika
import os

class RabbitMqConnectionManager(object):

    connection= None

    def registerToEvent(self,eventName,callback):
        credentials = pika.PlainCredentials(os.environ['RABBITMQ_USER'], os.environ['RABBITMQ_PASS'])
        self.connection = pika.BlockingConnection(pika.ConnectionParameters(os.environ['RABBITMQ_HOST'],5672,"/",credentials))
        channel = self.connection.channel()

        channel.exchange_declare(exchange='news_aggregator_bus', exchange_type='fanout')
        result = channel.queue_declare(queue='', exclusive=True)
        queue_name = result.method.queue
        channel.queue_bind(exchange='news_aggregator_bus', queue=queue_name)
        print(' [*] Waiting for messages. To exit press CTRL+C')

        channel.basic_consume(queue=queue_name, on_message_callback=callback, auto_ack=True)     
        channel.start_consuming()
    
    def closeConnection(self):
        if (self.connection):
            self.connection.close()
    
    def sendEvent(self, eventName, eventBody):
        print("almafa")
