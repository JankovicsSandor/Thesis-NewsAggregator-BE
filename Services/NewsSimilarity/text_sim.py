import gensim.models as g


class TextSimilarity(object):

    model=None

    def loadModelFromFile(self,path):
       self.model = g.Doc2Vec.load(self.model)
    
    def getMostSimilarNews(self, article):
       if self.model is None:
           self.loadModelFromFile("/var/similarity/doc2vec.bin")