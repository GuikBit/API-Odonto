-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: dentistabanco
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `anamnese`
--

DROP TABLE IF EXISTS `anamnese`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `anamnese` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ProblemaSaude` longtext NOT NULL,
  `Tratamento` longtext NOT NULL,
  `Remedio` longtext NOT NULL,
  `Alergia` longtext NOT NULL,
  `SangramentoExcessivo` tinyint(1) NOT NULL,
  `Hipertensao` tinyint(1) NOT NULL,
  `Gravida` tinyint(1) NOT NULL,
  `TraumatismoFace` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `anamnese`
--

LOCK TABLES `anamnese` WRITE;
/*!40000 ALTER TABLE `anamnese` DISABLE KEYS */;
INSERT INTO `anamnese` VALUES (2,'nao','nao','nao','nao',0,0,0,0),(3,'Nao','Nao','Nao','Nao',0,0,0,0),(4,'nao','nao','nao','nao',0,0,0,0),(5,'nao','nao','nao','nao',0,0,0,0),(6,'nao','nao','nao','nao',0,0,0,0),(7,'nao','nao','nao','nao',0,0,0,0),(8,'nao','nao','nao','nao',0,0,0,0),(9,'nao','nao','nao','nao',0,0,0,0),(10,'nao','nao','nao','nao',0,0,0,0),(11,'nao','nao','nao','nao',0,0,0,0),(12,'nao','nao','nao','nao',0,0,0,0),(13,'nao','nao','nao','nao',0,0,0,0),(14,'nao','nao','nao','nao',0,0,0,0),(15,'nao','nao','nao','nao',0,0,0,0),(16,'nao','nao','nao','nao',0,0,0,0),(17,'nao','nao','nao','nao',0,0,0,0),(18,'nao','nao','nao','nao',0,0,0,0);
/*!40000 ALTER TABLE `anamnese` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-09-30 19:51:16
