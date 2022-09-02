SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


CREATE TABLE `users` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `username` varchar(10) NOT NULL,
  `password` varchar(50) NOT NULL,
  `score` int(10) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  UNIQUE KEY (`username`),
  UNIQUE KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

  

INSERT INTO `users` (`username`, `password`, `score`) VALUES
('Maklo', 'ee54742a64ac6be74c68382ddca6d929', 100),
('Lumine', '827ccb0eea8a706c4c34a16891f84e7b', 300),
('Monny', '827ccb0eea8a706c4c34a16891f84e7b', 300);

-- Pass: kucing123, 12345, 12345
COMMIT;

