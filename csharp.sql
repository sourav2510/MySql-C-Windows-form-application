-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 04, 2020 at 11:07 PM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.4.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `csharp`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `BookAddOrEdit` (`_BookID` INT, `_BookName` VARCHAR(45), `_Author` VARCHAR(45), `_Description` VARCHAR(250))  BEGIN
	IF _BookID=0 THEN
		INSERT INTO Book(BookName,Author,Description)
        VALUES (_BookName,_Author,_Description);
	else
		UPDATE Book
        SET
			BookName=_BookName,
            Author=_Author,
            Description=_Description
		where BookID=_BookID;
	END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `BookDeleteByID` (`_BookID` INT)  BEGIN
	delete from book where BookID=_BookID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `BookSearchByValue` (`_SearchValue` VARCHAR(45))  BEGIN
	select * from Book where BookName like concat('%',_SearchValue,'%')
    || Author like concat('%',_SearchValue,'%');

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `BookViewAll` ()  BEGIN
	select * from book;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ViewBookByID` (`_BookID` INT)  BEGIN
	select * from book where BookID=_BookID;

END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `book`
--

CREATE TABLE `book` (
  `BookID` int(11) NOT NULL,
  `BookName` varchar(45) DEFAULT NULL,
  `Author` varchar(45) DEFAULT NULL,
  `Description` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `book`
--

INSERT INTO `book` (`BookID`, `BookName`, `Author`, `Description`) VALUES
(2, 'book2', 'author2', 'description2'),
(6, 'book3', 'author3', 'description3'),
(9, 'alchemist', 'paul coelho', 'based on hope'),
(10, 'Attitude is Everything', 'Jeff Keller', 'Change your attitude and you change your life');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `book`
--
ALTER TABLE `book`
  ADD PRIMARY KEY (`BookID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `book`
--
ALTER TABLE `book`
  MODIFY `BookID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
