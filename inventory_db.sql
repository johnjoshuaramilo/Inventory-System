-- phpMyAdmin SQL Dump
-- version 5.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 22, 2020 at 12:29 AM
-- Server version: 10.4.11-MariaDB
-- PHP Version: 7.4.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `inventory_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `tblacclog`
--

CREATE TABLE `tblacclog` (
  `fID` int(11) NOT NULL,
  `fAccount` varchar(255) DEFAULT NULL,
  `fDate` datetime DEFAULT NULL ON UPDATE current_timestamp(),
  `fAction` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tblitemlog`
--

CREATE TABLE `tblitemlog` (
  `fID` int(11) NOT NULL,
  `fItem` varchar(255) DEFAULT NULL,
  `fDate` datetime DEFAULT NULL ON UPDATE current_timestamp(),
  `fAction` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tblitems`
--

CREATE TABLE `tblitems` (
  `fID` bigint(20) NOT NULL,
  `fTagNumber` int(11) DEFAULT NULL,
  `fEmployee` varchar(255) DEFAULT NULL,
  `fItemName` varchar(255) DEFAULT NULL,
  `fLifeSpan` varchar(255) DEFAULT NULL,
  `fAcquired` varchar(255) DEFAULT NULL,
  `fCost` int(11) DEFAULT NULL,
  `fValue` int(11) DEFAULT NULL,
  `fStatus` varchar(255) DEFAULT NULL,
  `fTimeStamp` datetime DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tblloghistory`
--

CREATE TABLE `tblloghistory` (
  `fID` int(11) NOT NULL,
  `fUserName` varchar(255) DEFAULT NULL,
  `fTimeIn` datetime DEFAULT NULL,
  `fTimeOut` datetime DEFAULT NULL,
  `fAccess` varchar(255) DEFAULT NULL,
  `fTimestamp` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tblpurchase`
--

CREATE TABLE `tblpurchase` (
  `fID` int(11) NOT NULL,
  `fDate` datetime DEFAULT NULL,
  `fSupplier` varchar(255) DEFAULT NULL,
  `fTotal` bigint(255) DEFAULT NULL,
  `fCost` bigint(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tblpurchaselog`
--

CREATE TABLE `tblpurchaselog` (
  `fID` int(11) NOT NULL,
  `fEmployee` varchar(255) DEFAULT NULL,
  `fPurchaseSupplier` varchar(255) DEFAULT NULL,
  `fDate` datetime DEFAULT NULL ON UPDATE current_timestamp(),
  `fAction` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tblrecords`
--

CREATE TABLE `tblrecords` (
  `fID` int(11) NOT NULL,
  `fStrand` varchar(255) DEFAULT NULL,
  `fIDNumber` int(11) DEFAULT NULL,
  `fLRN` int(11) DEFAULT NULL,
  `fFName` varchar(255) DEFAULT NULL,
  `fMName` varchar(255) DEFAULT NULL,
  `fLName` varchar(255) DEFAULT NULL,
  `fCivil` varchar(255) DEFAULT NULL,
  `fBDate` datetime DEFAULT NULL,
  `fNation` varchar(255) DEFAULT NULL,
  `fReligion` varchar(255) DEFAULT NULL,
  `fContact` varchar(255) DEFAULT NULL,
  `fEmail` varchar(255) DEFAULT NULL,
  `fAddress` varchar(255) DEFAULT NULL,
  `fMother` varchar(255) DEFAULT NULL,
  `fMAddress` varchar(255) DEFAULT NULL,
  `fFather` varchar(255) DEFAULT NULL,
  `fFAddress` varchar(255) DEFAULT NULL,
  `fGuardian` varchar(255) DEFAULT NULL,
  `fGAddress` varchar(255) DEFAULT NULL,
  `fElem` varchar(255) DEFAULT NULL,
  `fJHS` varchar(255) DEFAULT NULL,
  `fSHS` varchar(255) DEFAULT NULL,
  `fTimeStamp` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=COMPACT;

-- --------------------------------------------------------

--
-- Table structure for table `tblsupplier`
--

CREATE TABLE `tblsupplier` (
  `fID` int(11) NOT NULL,
  `fSupplierName` varchar(255) DEFAULT NULL,
  `fTelNumber` int(11) DEFAULT NULL,
  `fContactName` varchar(255) DEFAULT NULL,
  `fMobileNumber` int(11) DEFAULT NULL,
  `fEmail` varchar(255) DEFAULT NULL,
  `fTimestamp` datetime DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tblsupplierlog`
--

CREATE TABLE `tblsupplierlog` (
  `fID` int(11) NOT NULL,
  `fSupplier` varchar(255) DEFAULT NULL,
  `fDate` datetime DEFAULT NULL ON UPDATE current_timestamp(),
  `fAction` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `tblusers`
--

CREATE TABLE `tblusers` (
  `fID` int(11) NOT NULL,
  `fUserName` varchar(255) DEFAULT NULL,
  `fPassword` varchar(255) DEFAULT NULL,
  `fAccess` varchar(255) DEFAULT NULL,
  `fFirstName` varchar(255) DEFAULT NULL,
  `fLastName` varchar(255) DEFAULT NULL,
  `fDepartment` varchar(255) DEFAULT NULL,
  `fTimeStamp` datetime DEFAULT NULL ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tblusers`
--

INSERT INTO `tblusers` (`fID`, `fUserName`, `fPassword`, `fAccess`, `fFirstName`, `fLastName`, `fDepartment`, `fTimeStamp`) VALUES
(1, 'josh', 'josh', 'admin', 'joshua', 'josh', 'Marketing', '2020-02-27 02:37:59'),
(4, 'guest', 'guest', 'regular', 'testing', 'testing', 'Marketing', '2020-02-27 01:11:50');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tblacclog`
--
ALTER TABLE `tblacclog`
  ADD PRIMARY KEY (`fID`);

--
-- Indexes for table `tblitemlog`
--
ALTER TABLE `tblitemlog`
  ADD PRIMARY KEY (`fID`);

--
-- Indexes for table `tblitems`
--
ALTER TABLE `tblitems`
  ADD PRIMARY KEY (`fID`);

--
-- Indexes for table `tblloghistory`
--
ALTER TABLE `tblloghistory`
  ADD PRIMARY KEY (`fID`);

--
-- Indexes for table `tblpurchase`
--
ALTER TABLE `tblpurchase`
  ADD PRIMARY KEY (`fID`);

--
-- Indexes for table `tblpurchaselog`
--
ALTER TABLE `tblpurchaselog`
  ADD PRIMARY KEY (`fID`);

--
-- Indexes for table `tblrecords`
--
ALTER TABLE `tblrecords`
  ADD PRIMARY KEY (`fID`) USING BTREE,
  ADD UNIQUE KEY `fIDNumber` (`fIDNumber`) USING BTREE;

--
-- Indexes for table `tblsupplier`
--
ALTER TABLE `tblsupplier`
  ADD PRIMARY KEY (`fID`);

--
-- Indexes for table `tblsupplierlog`
--
ALTER TABLE `tblsupplierlog`
  ADD PRIMARY KEY (`fID`);

--
-- Indexes for table `tblusers`
--
ALTER TABLE `tblusers`
  ADD PRIMARY KEY (`fID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tblacclog`
--
ALTER TABLE `tblacclog`
  MODIFY `fID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `tblitemlog`
--
ALTER TABLE `tblitemlog`
  MODIFY `fID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `tblitems`
--
ALTER TABLE `tblitems`
  MODIFY `fID` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `tblloghistory`
--
ALTER TABLE `tblloghistory`
  MODIFY `fID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=119;

--
-- AUTO_INCREMENT for table `tblpurchase`
--
ALTER TABLE `tblpurchase`
  MODIFY `fID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `tblpurchaselog`
--
ALTER TABLE `tblpurchaselog`
  MODIFY `fID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT for table `tblrecords`
--
ALTER TABLE `tblrecords`
  MODIFY `fID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `tblsupplier`
--
ALTER TABLE `tblsupplier`
  MODIFY `fID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `tblsupplierlog`
--
ALTER TABLE `tblsupplierlog`
  MODIFY `fID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
