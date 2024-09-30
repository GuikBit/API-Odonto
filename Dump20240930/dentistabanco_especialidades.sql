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
-- Table structure for table `especialidades`
--

DROP TABLE IF EXISTS `especialidades`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `especialidades` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Tipo` longtext,
  `Descricao` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `especialidades`
--

LOCK TABLES `especialidades` WRITE;
/*!40000 ALTER TABLE `especialidades` DISABLE KEYS */;
INSERT INTO `especialidades` VALUES (3,'Cirurgia e Traumatologia Buco-Maxilo-Faciais','Especialidade focada em procedimentos cirúrgicos da região bucal, maxilar e facial, incluindo remoção de dentes inclusos, tratamento de fraturas faciais, cirurgias ortognáticas, entre outros.\n'),(4,'Dentística','Concentra-se na restauração e estética dos dentes, utilizando técnicas como restaurações com resinas compostas, facetas de porcelana e clareamento dental.\n'),(5,'Disfunção Temporomandibular e Dor Orofacial','Dedica-se ao diagnóstico e tratamento de problemas relacionados à articulação temporomandibular (ATM), como disfunções articulares e dor facial.\n'),(6,'Endodontia',' Especialidade responsável pelo tratamento dos tecidos internos dos dentes, como a polpa dental, realizando procedimentos como tratamento de canal e apicectomia.'),(7,'Estomatologia','Envolvida no diagnóstico e tratamento de doenças e condições que afetam a cavidade oral e estruturas relacionadas, incluindo mucosas, glândulas salivares e lábios.'),(8,'Radiologia Odontológica e Imaginologia','Responsável pela interpretação e realização de exames de imagem como radiografias, tomografias e ressonâncias magnéticas da região bucal e maxilofacial.');
/*!40000 ALTER TABLE `especialidades` ENABLE KEYS */;
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
