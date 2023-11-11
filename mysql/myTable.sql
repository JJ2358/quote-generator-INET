-- Host: localhost:3306
-- Generation Time: Sep 25, 2016 at 10:48 PM
-- Server version: 5.6.33
-- PHP Version: 5.6.20

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

CREATE TABLE IF NOT EXISTS `Quotes` (
  `Id` int(10) NOT NULL AUTO_INCREMENT,
  `Author` varchar(100),
  `QuoteText` text NOT NULL,
  `Permalink` varchar(100),
  `Image` varchar(100),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1;

--
-- Dumping data for table `tblGrades`
--

-- Sample Data
INSERT INTO `Quotes` (`Author`, `QuoteText`, `Permalink`, `Image`) VALUES
('Author 1', 'This is a sample quote 1', 'http://example.com/1', 'image1.jpg'),
('Author 2', 'This is a sample quote 2', 'http://example.com/2', 'image2.jpg'),
('Author 3', 'This is a sample quote 3', 'http://example.com/3', 'image3.jpg');