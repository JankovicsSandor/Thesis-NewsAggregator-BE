import pika

class RabbitMqConnectionManager(object):

    connection= None

    def registerToEvent(self,eventName,callback):
        self.connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost'))
        channel = self.connection.channel()

        channel.exchange_declare(exchange='eventName', exchange_type='fanout')
        result = channel.queue_declare(queue='', exclusive=True)
        queue_name = result.method.queue
        channel.queue_bind(exchange='eventName', queue=queue_name)
        print(' [*] Waiting for messages. To exit press CTRL+C')

        channel.basic_consume(queue='hello', on_message_callback=callback, auto_ack=True)     
        channel.start_consuming()
    
    def closeConnection(self):
        if (self.connection):
            self.connection.close()
