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
('Bill Sempf', 'QA Engineer walks into a bar. Orders a beer. Orders 0 beers. Orders 999999999 beers. Orders a lizard. Orders -1 beers. Orders a sfdeljknesv.', 'http://quotes.stormconsultancy.co.uk/quotes/44', 'wwwroot/images/robot.png'),
('Phil Karlton', 'There are only two hard things in Computer Science: cache invalidation, naming things and off-by-one errors.', 'http://quotes.stormconsultancy.co.uk/quotes/43', 'wwwroot/images/robot.png'),
('Donald Knuth', 'Beware of bugs in the above code; I have only proved it correct, not tried it.', 'http://quotes.stormconsultancy.co.uk/quotes/27', 'wwwroot/images/robot.png');
