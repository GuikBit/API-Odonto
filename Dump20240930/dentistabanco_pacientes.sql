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
-- Table structure for table `pacientes`
--

DROP TABLE IF EXISTS `pacientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pacientes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `NumPasta` longtext,
  `EnderecoId` int NOT NULL,
  `AnamneseId` int NOT NULL,
  `ResponsavelId` int NOT NULL,
  `FotoPerfil` longtext,
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
  PRIMARY KEY (`Id`),
  KEY `IX_Pacientes_AnamneseId` (`AnamneseId`),
  KEY `IX_Pacientes_EnderecoId` (`EnderecoId`),
  KEY `IX_Pacientes_OrganizacaoId` (`OrganizacaoId`),
  KEY `IX_Pacientes_ResponsavelId` (`ResponsavelId`),
  CONSTRAINT `FK_Pacientes_Anamnese_AnamneseId` FOREIGN KEY (`AnamneseId`) REFERENCES `anamnese` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Pacientes_Endereco_EnderecoId` FOREIGN KEY (`EnderecoId`) REFERENCES `endereco` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Pacientes_Organizacao_OrganizacaoId` FOREIGN KEY (`OrganizacaoId`) REFERENCES `organizacao` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Pacientes_Responsavel_ResponsavelId` FOREIGN KEY (`ResponsavelId`) REFERENCES `responsavel` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pacientes`
--

LOCK TABLES `pacientes` WRITE;
/*!40000 ALTER TABLE `pacientes` DISABLE KEYS */;
INSERT INTO `pacientes` VALUES (10,'99999',3,2,2,NULL,'teste da silva','teste@gmail.com','teste','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 99999-9999','600.026.180-28','2024-05-01 03:00:00.000000','2024-04-18 17:14:01.174714',0,'Paciente',1),(11,'87623',4,3,3,NULL,'Guilherme Oliveira','guilhermeoliveira@gmail.com','Gui','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 99822-0082','120.981.336-00','1998-11-18 02:00:00.000000','2024-04-18 18:04:45.459945',1,'Paciente',1),(12,'23231',5,4,4,NULL,'Bianca Cristina Machado','bianca@gmail.com','Bianca','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 55555-5555','998.914.860-01','1998-03-29 03:00:00.000000','2024-04-18 18:15:36.984089',1,'Paciente',1),(13,'23234',6,5,5,NULL,'Pedro Augusto Perreira','pedro@gmail.com','pedro','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 11111-1111','966.892.350-25','2012-05-14 03:00:00.000000','2024-04-18 18:17:38.033326',1,'Paciente',1),(14,'99999',7,6,6,NULL,'Ana Maria','teste1@gmail.com','login1','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 98888-8888','123.456.789-01','1990-01-01 03:00:00.000000','2024-04-18 23:48:22.999114',1,'Paciente',1),(15,'99998',8,7,7,NULL,'Bruno Silva','teste2@gmail.com','login2','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 97777-7777','234.567.890-12','1985-05-05 03:00:00.000000','2024-05-18 23:48:58.571308',1,'Paciente',1),(16,'99997',9,8,8,NULL,'Carlos Alberto','teste3@gmail.com','login3','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 96666-6666','345.678.901-23','1995-03-15 03:00:00.000000','2024-05-18 23:49:08.670299',1,'Paciente',1),(17,'99996',10,9,9,NULL,'Daniela Ferreira','teste4@gmail.com','login4','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 95555-5555','456.789.012-34','1982-11-30 03:00:00.000000','2024-04-18 23:49:16.078974',1,'Paciente',1),(18,'99995',11,10,10,NULL,'Eduardo Souza','teste5@gmail.com','login5','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 94444-4444','567.890.123-45','1993-07-20 03:00:00.000000','2024-03-18 23:49:24.795429',1,'Paciente',1),(19,'99994',12,11,11,NULL,'Fernanda Oliveira','teste6@gmail.com','login6','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 93333-3333','678.901.234-56','1988-09-15 03:00:00.000000','2024-03-18 23:49:33.133776',1,'Paciente',1),(20,'99993',13,12,12,NULL,'Gustavo Lima','teste7@gmail.com','login7','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 92222-2222','789.012.345-67','1998-04-10 03:00:00.000000','2024-03-18 23:49:40.185090',1,'Paciente',1),(21,'99992',14,13,13,NULL,'Helena Costa','teste8@gmail.com','login8','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 91111-1111','890.123.456-78','1977-08-25 03:00:00.000000','2024-05-18 23:49:47.285025',1,'Paciente',1),(22,'99991',15,14,14,NULL,'Isabela Dias','teste9@gmail.com','login9','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 90000-0000','901.234.567-89','1992-06-05 03:00:00.000000','2024-03-18 23:49:54.270016',1,'Paciente',1),(23,'99990',16,15,15,NULL,'João Pedro','teste10@gmail.com','login10','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 98888-7777','012.345.678-90','1996-10-18 03:00:00.000000','2024-02-18 23:50:01.632598',1,'Paciente',1),(24,'99989',17,16,16,NULL,'Larissa Carvalho','teste11@gmail.com','login11','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 97777-6666','123.654.789-01','1991-12-12 03:00:00.000000','2024-02-18 23:50:08.427226',1,'Paciente',1),(25,'99988',18,17,17,NULL,'Marcos Antonio','teste12@gmail.com','login12','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 96666-5555','234.765.890-12','1984-02-02 03:00:00.000000','2024-01-18 23:50:14.863324',1,'Paciente',1),(26,'00000',19,18,18,NULL,'David','teste@teste','David','40bd001563085fc35165329ea1ff5c5ecbdbbeef','(32) 99999-9999','005.867.640-62','2024-05-01 03:00:00.000000','2024-05-20 11:54:19.384926',1,'Paciente',1);
/*!40000 ALTER TABLE `pacientes` ENABLE KEYS */;
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
