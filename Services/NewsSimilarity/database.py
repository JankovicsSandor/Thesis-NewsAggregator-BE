import os
import mysql.connector
from mysql.connector import errorcode


class DatabaseConnection(object):

    connection= None
    def getDatabaseConnection(self):
        try:
            connection = mysql.connector.connect(host=os.environ['DATABASE_HOST'],user=os.environ['DATABASE_USER'],
                                        password=os.environ['DATABASE_PASSWORD'], database=os.environ['DATABASE_NAME'],auth_plugin='mysql_native_password')
            return connection
        except mysql.connector.Error as err:
            if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
                print("Something is wrong with your user name or password")
            elif err.errno == errorcode.ER_BAD_DB_ERROR:
                print("Database does not exist")
            else:
                print(err)
        else:
            connection.close()

    def closeConnection(self):
        self.connection.close()
    