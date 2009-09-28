-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.1.33-community-log


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema dpx
--

CREATE DATABASE IF NOT EXISTS dpx;
USE dpx;

--
-- Definition of table `classes`
--

DROP TABLE IF EXISTS `classes`;
CREATE TABLE `classes` (
  `class` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Semester` varchar(45) NOT NULL,
  `Number` varchar(45) NOT NULL,
  `Sections` varchar(45) NOT NULL,
  `Professor` varchar(45) NOT NULL,
  PRIMARY KEY (`class`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `classes`
--

/*!40000 ALTER TABLE `classes` DISABLE KEYS */;
INSERT INTO `classes` (`class`,`Semester`,`Number`,`Sections`,`Professor`) VALUES 
 (1,'Fall 2009','101','1,2','Ralston');
/*!40000 ALTER TABLE `classes` ENABLE KEYS */;


--
-- Definition of table `deob`
--

DROP TABLE IF EXISTS `deob`;
CREATE TABLE `deob` (
  `deob` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `page` int(10) unsigned NOT NULL,
  `ut` tinyint(1) NOT NULL,
  `uid` varchar(45) NOT NULL,
  `ig` varchar(45) NOT NULL,
  PRIMARY KEY (`deob`),
  KEY `FK_deob_1` (`page`),
  CONSTRAINT `FK_deob_1` FOREIGN KEY (`page`) REFERENCES `pages` (`page`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `deob`
--

/*!40000 ALTER TABLE `deob` DISABLE KEYS */;
/*!40000 ALTER TABLE `deob` ENABLE KEYS */;


--
-- Definition of table `edde`
--

DROP TABLE IF EXISTS `edde`;
CREATE TABLE `edde` (
  `edde` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `deob` int(10) unsigned NOT NULL,
  `sti` int(10) unsigned NOT NULL,
  `objid` varchar(45) NOT NULL,
  PRIMARY KEY (`edde`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `edde`
--

/*!40000 ALTER TABLE `edde` DISABLE KEYS */;
/*!40000 ALTER TABLE `edde` ENABLE KEYS */;


--
-- Definition of table `files`
--

DROP TABLE IF EXISTS `files`;
CREATE TABLE `files` (
  `file` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `class` int(10) unsigned NOT NULL,
  `filename` varchar(45) NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`file`),
  KEY `FK_files_1` (`class`),
  CONSTRAINT `FK_files_1` FOREIGN KEY (`class`) REFERENCES `classes` (`class`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `files`
--

/*!40000 ALTER TABLE `files` DISABLE KEYS */;
/*!40000 ALTER TABLE `files` ENABLE KEYS */;


--
-- Definition of table `pages`
--

DROP TABLE IF EXISTS `pages`;
CREATE TABLE `pages` (
  `page` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `file` int(10) unsigned NOT NULL,
  `uid` varchar(45) NOT NULL,
  `oner` varchar(45) NOT NULL,
  `onern` varchar(45) NOT NULL,
  PRIMARY KEY (`page`),
  KEY `FK_pages_1` (`file`),
  CONSTRAINT `FK_pages_1` FOREIGN KEY (`file`) REFERENCES `files` (`file`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pages`
--

/*!40000 ALTER TABLE `pages` DISABLE KEYS */;
/*!40000 ALTER TABLE `pages` ENABLE KEYS */;


--
-- Definition of table `pens`
--

DROP TABLE IF EXISTS `pens`;
CREATE TABLE `pens` (
  `pen` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `page` int(10) unsigned NOT NULL,
  `ut` tinyint(1) NOT NULL,
  `pw` int(10) unsigned NOT NULL,
  `ph` int(10) unsigned NOT NULL,
  `uid` varchar(45) NOT NULL,
  `data` longtext NOT NULL,
  PRIMARY KEY (`pen`),
  KEY `FK_pens_1` (`page`),
  CONSTRAINT `FK_pens_1` FOREIGN KEY (`page`) REFERENCES `pages` (`page`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pens`
--

/*!40000 ALTER TABLE `pens` DISABLE KEYS */;
/*!40000 ALTER TABLE `pens` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
