class TextToId(object):

    def createIdFromText(self,text):
        encoded=""
        for char in text:
            encoded+=(str(ord(char))+"a")
        return encoded