class NewsItemVector(object):

    newsItem=""
    vector = ""
   
    
    def __init__(self, originalDescription,newsItem,vector):
        self.newsItem = newsItem
        self.originalDescription=originalDescription
        self.vector = vector