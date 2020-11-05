import gensim.models as g
import re
from nltk.corpus import stopwords 
from nltk.tokenize import word_tokenize
from nltk.stem import PorterStemmer,WordNetLemmatizer


class TextSimilarity(object):
    
   def getMostSimilarNews(self, article):
      model = g.Doc2Vec.load("/var/similarity/doc2vec.bin")

      processesedText=self.preprocessText(article)
      infer_vector = model.infer_vector(processesedText)
      return model.docvecs.most_similar([infer_vector], topn=5)
   
   def preprocessText(self, text):
      # convert the text to lowercase
      if (text is None):
         raise TypeError("Input text is empty")
      if (not isinstance(text, str)):
         raise TypeError("Input text is not a string")
      convertedString = text.lower()

      # remove numbers from text
      convertedString = self.removeNumbers(convertedString)

      # trip the text
      convertedString = convertedString.strip()
      
      # remove stop words
      convertedString = self.removeStopwords(convertedString)

      # stemming text
      convertedString = self.stemText(convertedString)

      # lemmanize text
      convertedString = self.lemmanizeText(convertedString)

      return convertedString
   
   def removeNumbers(self, text):
      if (text is None):
         raise TypeError("Input text is empty")
      if (not isinstance(text, str)):
         raise TypeError("Input text is not a string")
      return re.sub(r"\d+", "", text)

   def removeStopwords(self, text):
      if (text is None):
         raise TypeError("Input text is empty")
      if (not isinstance(text, str)):
         raise TypeError("Input text is not a string")

      stop_words = set(stopwords.words("english"))
      tokens = word_tokenize(text)
      return ' '.join([i for i in tokens if not i in stop_words])

   def stemText(self,text):
      if (text is None):
         raise TypeError("Input text is empty")
      if (not isinstance(text, str)):
         raise TypeError("Input text is not a string")
      stemmer= PorterStemmer()
      tokenized=word_tokenize(text)
      return ' '.join([stemmer.stem(word) for word in tokenized])

   def lemmanizeText(self,text):
      if (text is None):
         raise TypeError("Input text is empty")
      if (not isinstance(text, str)):
         raise TypeError("Input text is not a string")
      lemmatizer=WordNetLemmatizer()
      tokenized=word_tokenize(text)
      return ' '.join([lemmatizer.lemmatize(word) for word in tokenized])