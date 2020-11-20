class NewsGroupDoneEvent(object):

    newsItem=""
    similarities = []
   
    
    def __init__(self, newsItem,similarities):
        self.newsItem=newsItem
        self.similarities = similarities