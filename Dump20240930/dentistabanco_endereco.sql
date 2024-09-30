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
-- Table structure for table `endereco`
--

DROP TABLE IF EXISTS `endereco`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `endereco` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Logradouro` longtext NOT NULL,
  `Bairro` longtext NOT NULL,
  `Cidade` longtext NOT NULL,
  `Cep` longtext NOT NULL,
  `Numero` longtext NOT NULL,
  `Complemento` longtext NOT NULL,
  `Referencia` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `endereco`
--

LOCK TABLES `endereco` WRITE;
/*!40000 ALTER TABLE `endereco` DISABLE KEYS */;
INSERT INTO `endereco` VALUES (1,'Avenida Tanus Saliba 412 Loja 2','Centro','Juatuba','35675-975','409','Apartamento',''),(3,'Rua Palmyra da Silva Frizero','Grama','Juiz de Fora','36048-502','27','(Vl Sta Clara)',NULL),(4,'Rua Palmyra da Silva Frizero','Grama','Juiz de Fora','36048-502','27','(Vl Sta Clara)',NULL),(5,'Rua João Valdir Borges','Efapi','Chapecó','89809-878','432','casa',NULL),(6,'Rua Monte Líbano Sul','Residencial Califórnia','Imperatriz','65908-848','32','Casa',NULL),(7,'Rua Principal','Centro','Juiz de Fora','36010-000','10','',NULL),(8,'Rua Secundária','São Mateus','Juiz de Fora','36020-000','20','',NULL),(9,'Rua Terciária','Granbery','Juiz de Fora','36030-000','30','',NULL),(10,'Rua Quartenária','Morro da Glória','Juiz de Fora','36040-000','40','',NULL),(11,'Rua Quinária','Vitorino Braga','Juiz de Fora','36050-000','50','',NULL),(12,'Rua Senária','Santa Luzia','Juiz de Fora','36060-000','60','',NULL),(13,'Rua Septuária','Santa Catarina','Juiz de Fora','36070-000','70','',NULL),(14,'Rua Octonária','Bandeirantes','Juiz de Fora','36080-000','80','',NULL),(15,'Rua Nona','São Pedro','Juiz de Fora','36090-000','90','',NULL),(16,'Rua Decanária','Santos Dumont','Juiz de Fora','36100-000','100','',NULL),(17,'Rua Undecanária','Santa Terezinha','Juiz de Fora','36110-000','110','',NULL),(18,'Rua Duodecanária','Sagrado Coração','Juiz de Fora','36120-000','120','',NULL),(19,'Rua Palmyra da Silva Frizero','Grama','Juiz de Fora','36048-502','27','(Vl Sta Clara)',NULL);
/*!40000 ALTER TABLE `endereco` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-09-30 19:51:14
