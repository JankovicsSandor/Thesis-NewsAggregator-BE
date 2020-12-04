class NewsGroupDoneEvent(object):

    newsItem=""
    similarity = []
   
    
    def __init__(self, newsItem,similarity):
        self.newsItem=newsItem
        self.similarity = similarity