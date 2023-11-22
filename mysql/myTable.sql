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
('Jeff Atwood', 'In software, we rarely have meaningful requirements. Even if we do, the only measure of success that matters is whether our solution solves the customer’s shifting idea of what their problem is.', 'http://quotes.stormconsultancy.co.uk/quotes/42', 'wwwroot/images/robot.png'),
('Robert Sewell', 'If Java had true garbage collection, most programs would delete themselves upon execution.', 'http://quotes.stormconsultancy.co.uk/quotes/41', 'wwwroot/images/robot.png'),
('Bjarne Stroustrup', 'In C++ it’s harder to shoot yourself in the foot, but when you do, you blow off your whole leg.', 'http://quotes.stormconsultancy.co.uk/quotes/39', 'wwwroot/images/robot.png'),
('Alan Kay', 'Most software today is very much like an Egyptian pyramid with millions of bricks piled on top of each other, with no structural integrity, but just done by brute force and thousands of slaves.', 'http://quotes.stormconsultancy.co.uk/quotes/38', 'wwwroot/images/robot.png'),
('Larry DeLuca', 'I’ve noticed lately that the paranoid fear of computers becoming intelligent and taking over the world has almost entirely disappeared from the common culture. Near as I can tell, this coincides with the release of MS-DOS.', 'http://quotes.stormconsultancy.co.uk/quotes/37', 'wwwroot/images/robot.png'),
('Mark Gibbs', 'No matter how slick the demo is in rehearsal, when you do it in front of a live audience, the probability of a flawless presentation is inversely proportional to the number of people watching, raised to the power of the amount of money involved.', 'http://quotes.stormconsultancy.co.uk/quotes/36', 'wwwroot/images/robot.png'),
('Henry Petroski', 'The most amazing achievement of the computer software industry is its continuing cancellation of the steady and staggering gains made by the computer hardware industry.', 'http://quotes.stormconsultancy.co.uk/quotes/35', 'wwwroot/images/robot.png'),
('Jeremy S. Anderson', 'There are two major products that come out of Berkeley: LSD and UNIX. We don’t believe this to be a coincidence.', 'http://quotes.stormconsultancy.co.uk/quotes/34', 'wwwroot/images/robot.png'),
('Jamie Zawinski', 'Linux is only free if your time has no value.', 'http://quotes.stormconsultancy.co.uk/quotes/32', 'wwwroot/images/robot.png'),
('Richard Moore', 'The difference between theory and practice is that in theory, there is no difference between theory and practice.', 'http://quotes.stormconsultancy.co.uk/quotes/30', 'wwwroot/images/robot.png'),
('Bjarne Stroustrup', 'There are only two kinds of programming languages: those people always bitch about and those nobody uses.', 'http://quotes.stormconsultancy.co.uk/quotes/28', 'wwwroot/images/robot.png'),
('Donald Knuth', 'Beware of bugs in the above code; I have only proved it correct, not tried it.', 'http://quotes.stormconsultancy.co.uk/quotes/27', 'wwwroot/images/robot.png');
