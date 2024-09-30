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
-- Table structure for table `consultaespecialidades`
--

DROP TABLE IF EXISTS `consultaespecialidades`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `consultaespecialidades` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Tipo` longtext NOT NULL,
  `Descricao` longtext NOT NULL,
  `ValorBase` double NOT NULL,
  `DataCadastro` longtext NOT NULL,
  `DataUpdade` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `consultaespecialidades`
--

LOCK TABLES `consultaespecialidades` WRITE;
/*!40000 ALTER TABLE `consultaespecialidades` DISABLE KEYS */;
INSERT INTO `consultaespecialidades` VALUES (1,'Avaliação','Avaliação do paciente, para análise de procedimento a ser tratado.',0,'18/05/2024 21:01',NULL),(2,'Controle invisalign','Controle de aparelho invisalign.',190,'18/05/2024 21:09',NULL),(3,'Extração de siso','Cirurgia de extracao de sisos.',299.99,'18/05/2024 21:34','06/06/2024 22:02'),(4,'Inxerto Osseo','testes',500,'20/05/2024 12:05',NULL),(5,'Invisalign 14pl','Tratamento com invisalign contendo 14 placas',10700,'05/06/2024 22:43',NULL),(6,'Invisalign 26pl','Tratamento com invisalign contendo 14 placas',12800,'05/06/2024 22:44',NULL),(7,'Invisalign ထ','Tratamento com invisalign contendo placas ilimitadas',14800,'05/06/2024 22:47',NULL);
/*!40000 ALTER TABLE `consultaespecialidades` ENABLE KEYS */;
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
