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
-- Table structure for table `dentistas`
--

DROP TABLE IF EXISTS `dentistas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dentistas` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `EspecialidadeId` int DEFAULT NULL,
  `CargoId` int DEFAULT NULL,
  `CorDentista` longtext NOT NULL,
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
  `OrganizacaoId` int NOT NULL,
  `CRO` longtext NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Dentistas_CargoId` (`CargoId`),
  KEY `IX_Dentistas_EspecialidadeId` (`EspecialidadeId`),
  KEY `IX_Dentistas_OrganizacaoId` (`OrganizacaoId`),
  CONSTRAINT `FK_Dentistas_Cargos_CargoId` FOREIGN KEY (`CargoId`) REFERENCES `cargos` (`Id`),
  CONSTRAINT `FK_Dentistas_Especialidades_EspecialidadeId` FOREIGN KEY (`EspecialidadeId`) REFERENCES `especialidades` (`Id`),
  CONSTRAINT `FK_Dentistas_Organizacao_OrganizacaoId` FOREIGN KEY (`OrganizacaoId`) REFERENCES `organizacao` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dentistas`
--

LOCK TABLES `dentistas` WRITE;
/*!40000 ALTER TABLE `dentistas` DISABLE KEYS */;
INSERT INTO `dentistas` VALUES (4,7,NULL,'#EC2ABA','Maria Clara Souza','maria.clara@teste.com','dentista1','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 98765-4321','123.456.789-01','1985-03-12 02:00:00.000000','2024-05-18 23:53:42.579701',1,'Dentista',1,'43413'),(5,7,NULL,'#4FA718','Pedro Henrique Silva','pedro.henrique@teste.com','dentista2','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 87654-3210','234.567.890-12','1990-07-25 02:00:00.000000','2024-05-18 23:53:51.495659',1,'Dentista',1,'54241'),(6,3,NULL,'#DAC0AD','Ana Beatriz Lima','ana.beatriz@teste.com','dentista3','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 76543-2109','345.678.901-23','1978-11-02 02:00:00.000000','2024-05-18 23:53:59.070499',1,'Dentista',1,'65624'),(7,4,NULL,'#4D5E6F','João Gabriel Pereira','joao.gabriel@teste.com','dentista4','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 65432-1098','456.789.012-34','1989-05-15 02:00:00.000000','2024-05-18 23:54:06.126053',1,'Dentista',1,'76432'),(8,5,NULL,'#D6CE63','Isabela Dias','isabela.dias@teste.com','dentista5','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 54321-0987','567.890.123-45','1992-12-05 02:00:00.000000','2024-05-18 23:54:12.568639',1,'Dentista',1,'21565');
/*!40000 ALTER TABLE `dentistas` ENABLE KEYS */;
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
