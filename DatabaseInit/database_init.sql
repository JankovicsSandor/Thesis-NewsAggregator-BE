CREATE DATABASE `news_aggregator_data`;

CREATE TABLE `article` (
  `id` int NOT NULL AUTO_INCREMENT,
  `feedId` int NOT NULL,
  `title` longtext,
  `description` longtext,
  `link` longtext ,
  `publishDate` datetime NOT NULL,
  `picture` longtext ,
  `guid` varchar(250)  DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_ARTICLE_FEED_ID_idx` (`feedId`),
  FULLTEXT KEY `FULLTEXT_DESCRIPTION` (`description`),
  CONSTRAINT `FK_ARTICLE_FEED_ID` FOREIGN KEY (`feedId`) REFERENCES `feed` (`id`)
) ENGINE=InnoDB;

CREATE TABLE `feed` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Active` tinyint(1) DEFAULT NULL,
  `picture` longtext,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB;

CREATE DATABASE `news_aggregator_resource`;
CREATE TABLE `lastsynchronizedresource` (
  `id` int NOT NULL AUTO_INCREMENT,
  `resourceId` int NOT NULL,
  `title` longtext,
  `description` longtext,
  PRIMARY KEY (`id`),
  KEY `FK_RESOURCE_SYNC_ID_idx` (`resourceId`),
  CONSTRAINT `FK_RESOURCE_SYNC_ID` FOREIGN KEY (`resourceId`) REFERENCES `resource` (`id`)
) ENGINE=InnoDB;

CREATE TABLE `resource` (
  `id` int NOT NULL AUTO_INCREMENT,
  `url` longtext NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT '1',
  `feedId` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB;

CREATE DATABASE `news_aggregator_user`;
CREATE TABLE `user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `uuid` longtext,
  `firstname` mediumtext NOT NULL,
  `lastname` mediumtext NOT NULL,
  `email` mediumtext NOT NULL,
  `username` varchar(45)  NOT NULL,
  `password` varchar(45) NOT NULL,
  `birthDate` datetime DEFAULT NULL,
  `passwordSalt` blob,
  `registerDate` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username_UNIQUE` (`username`)
) ENGINE=InnoDB ;





