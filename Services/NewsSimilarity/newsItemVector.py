class NewsItemVector(object):

    originalDescription=""
    newsItem=""
    vector = ""
    origText="" 
   
    
    def __init__(self, guid,originalText,processedText,vector):
        self.guid = guid
        self.processedText = processedText
        self.origText=originalText
        self.vector = vector