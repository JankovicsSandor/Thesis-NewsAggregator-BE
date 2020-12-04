import gensim.models as g
import re
import os
import nltk
nltk.download('wordnet')
from nltk.corpus import stopwords 
from nltk.tokenize import word_tokenize
from nltk.stem import PorterStemmer, WordNetLemmatizer
import requests
from newsItemVector import NewsItemVector
from scipy.spatial import distance
import json

class TextSimilarity(object):

   newsDict=[]

   def __init__(self):
      self.getTodayArticles()

   def getTodayArticles(self):
      todayArticles = requests.get(os.environ['NEWS_API'] + "article/today").json()
      for articleItem in todayArticles:
         processedText = self.preprocessText(articleItem["description"])

         embeddingResult=self.createWordVectorFromText(processedText)
         
         self.newsDict.append(NewsItemVector(articleItem["guid"],articleItem["description"],processedText,embeddingResult))
       


   def getMostSimilarNews(self, guid,articleDescription):
      maxSimilarity = -45
      similarArticleDescription = ""
      originalDescription=""
      newItemProcessedText = self.preprocessText(articleDescription)
      newArticleVector=self.createWordVectorFromText(newItemProcessedText)
      for todayArticle in self.newsDict:    
         cosine_similarity = 1 - distance.cosine(newArticleVector, todayArticle.vector)
         if cosine_similarity > maxSimilarity:
            maxSimilarity = cosine_similarity
            similarArticleDescription = todayArticle.guid
            originalDescription=todayArticle.origText
      self.newsDict.append(NewsItemVector(guid,articleDescription,newItemProcessedText,newArticleVector))
      print("Article: "+ articleDescription)
      print("Similarity: " + str(maxSimilarity))
      print("Similar: "+ originalDescription)
      return similarArticleDescription
      
            
   def createWordVectorFromText(self, processedText):
      params = {"q": processedText, "lang": "en"}
      requestResult = requests.get(os.environ['LASER_API'] + "vectorize", params=params).json()
      
      return requestResult["embedding"][0]
   
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
      #convertedString = self.stemText(convertedString)

      # lemmanize text
     # convertedString = self.lemmanizeText(convertedString)

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