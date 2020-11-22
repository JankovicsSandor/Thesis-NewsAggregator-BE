Delete from news_aggregator_data.article;
Delete from news_aggregator_data.feed;
Delete from news_aggregator_resource.lastsynchronizedresource;
Delete from news_aggregator_resource.resource;

ALTER TABLE  news_aggregator_data.article AUTO_INCREMENT  = 1;
ALTER TABLE  news_aggregator_data.feed AUTO_INCREMENT  = 1;
ALTER TABLE  news_aggregator_resource.lastsynchronizedresource AUTO_INCREMENT  = 1;
ALTER TABLE  news_aggregator_resource.resource AUTO_INCREMENT = 1;
