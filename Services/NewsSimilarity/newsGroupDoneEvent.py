class NewsGroupDoneEvent(object):

    guid = "",
    title = "",
    description = ""
    link = "",
    publishDate = ""
    picture=""
    similarities = []
   
    
    def __init__(self, guid,title,description,link,publishDate,picture,similarities):
        self.guid = guid
        self.title = title
        self.description = description
        self.link = link
        self.publishDate = publishDate
        self.picture = picture
        self.similarities = similarities