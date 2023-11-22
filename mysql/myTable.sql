-- Host: localhost:3306
-- Generation Time: [Your Generation Time]
-- Server version: [Your Server Version]
-- PHP Version: [Your PHP Version]

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

-- Create the Quotes table
CREATE TABLE IF NOT EXISTS `Quotes` (
  `Id` int(10) NOT NULL AUTO_INCREMENT,
  `Author` varchar(100) NOT NULL,
  `QuoteText` text NOT NULL,
  `Permalink` varchar(255), -- Optional permalink
  `Image` varchar(255), -- Path to the image file
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Sample Data
INSERT INTO `Quotes` (`Author`, `QuoteText`, `Permalink`, `Image`) VALUES
('Author 1', 'This is a sample quote 1', 'http://example.com/1', 'wwwroot/images/robot.png'),
('Author 2', 'This is a sample quote 2', 'http://example.com/2', 'wwwroot/images/robot.png'),
('Author 3', 'This is a sample quote 3', 'http://example.com/3', 'wwwroot/images/robot.png');
