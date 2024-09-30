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
-- Table structure for table `administrador`
--

DROP TABLE IF EXISTS `administrador`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `administrador` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OrganizacaoId` int NOT NULL,
  `Nome` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `Login` longtext NOT NULL,
  `Senha` longtext NOT NULL,
  `Telefone` longtext NOT NULL,
  `Cpf` longtext NOT NULL,
  `DataNascimento` datetime(6) NOT NULL,
  `DataCadastro` datetime(6) NOT NULL,
  `Ativo` tinyint(1) NOT NULL,
  `Role` longtext NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Administrador_OrganizacaoId` (`OrganizacaoId`),
  CONSTRAINT `FK_Administrador_Organizacao_OrganizacaoId` FOREIGN KEY (`OrganizacaoId`) REFERENCES `organizacao` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrador`
--

LOCK TABLES `administrador` WRITE;
/*!40000 ALTER TABLE `administrador` DISABLE KEYS */;
INSERT INTO `administrador` VALUES (1,1,'admin','admin@gmail.com','admin','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32)99999-9999','600.026.180-28','2024-05-18 17:43:01.545000','2024-05-18 17:43:01.545000',1,'Admin'),(2,1,'admin','admin@gmail.com','admin','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32)99999-9999','600.026.180-28','2024-05-18 17:43:01.545000','2024-05-18 17:43:01.545000',1,'Admin'),(4,1,'Admin','admin@email.com','admin2','40bd001563085fc35165329ea1ff5c5ecbdbbeef','123456789','123.456.789-01','0001-01-01 00:00:00.000000','2024-08-12 21:13:45.644232',1,'0');
/*!40000 ALTER TABLE `administrador` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-09-30 19:51:15
