
CREATE DATABASE mydb1;
GO

USE mydb1;
GO

CREATE TABLE Department (
    DNUM INT PRIMARY KEY,
    DName VARCHAR(100) NOT NULL UNIQUE,
    MGR_SSN CHAR(9) NOT NULL UNIQUE,
    DEP_Location VARCHAR(100) NOT NULL
    FOREIGN KEY (MGR_SSN) REFERENCES Employee(SSN) ON DELETE SET NULL
);

CREATE TABLE Employee (
    SSN CHAR(9) PRIMARY KEY,
    Fname VARCHAR(50) NOT NULL,
    Lname VARCHAR(50) NOT NULL,
    Bdate DATE NOT NULL,
    Gender CHAR(1) CHECK (Gender IN ('M', 'F')),
    DNum INT NOT NULL,
    SuperSSN CHAR(9),
    FOREIGN KEY (DNum) REFERENCES Department(DNUM),
    FOREIGN KEY (SuperSSN) REFERENCES Employee(SSN)
);


CREATE TABLE Project (
    PNumber INT PRIMARY KEY,
    Pname VARCHAR(100) NOT NULL ,
    Location VARCHAR(100) NOT NULL,
    DNUM INT NOT NULL,
    FOREIGN KEY (DNUM) REFERENCES Department(DNUM)
);


CREATE TABLE Employee_projects (
    SSN CHAR(9),
    PNumber INT,
    Working_Hours DECIMAL(5,2) NOT NULL DEFAULT 0,
    PRIMARY KEY (SSN, PNumber),
    FOREIGN KEY (SSN) REFERENCES Employee(SSN),
    FOREIGN KEY (PNumber) REFERENCES Project(PNumber)
);

CREATE TABLE Dependent (
    SSN CHAR(9),
    Dependent_Name VARCHAR(50),
    Gender CHAR(1) CHECK (Gender IN ('M' , 'F')),
    Bdate DATE NOT NULL,
    PRIMARY KEY (SSN, Dependent_Name),
    FOREIGN KEY (SSN) REFERENCES Employee(SSN) ON DELETE CASCADE
);


CREATE TABLE Manage_HireDate(
    SSN CHAR(9),
    DNum INT,
    Hire_Date DATE NOT NULL,
    PRIMARY KEY (SSN , DNum),
    FOREIGN KEY (SSN) REFERENCES Employee(SSN),
    FOREIGN KEY (DNum) REFERENCES Department(DNUM),

);


INSERT INTO Department VALUES (1, 'IT', '111223333', 'Cairo')
, (2, 'HR', '222334444', 'Giza')
, (3, 'Finance', '333445555', 'Menofia');

INSERT INTO Employee VALUES ('111223333' , 'Ahmed' , 'Ali' , '2004-6-9' , 'M' , 1 , NULL),
 ('222334444', 'Omar', 'Ahmed', '1985-05-15', 'M', 2, '111223333'),
 ('333445555', 'Mai', 'Samir', '1980-07-20', 'F', 3, '111223333'),
('444556666', 'David', 'Jimmy', '1992-03-12', 'M', 1, '222334444'),
 ('555667777', 'Sara', 'Mohamed', '1995-09-25', 'F', 2, '333445555');

INSERT INTO Project VALUES (1 , 'Web APP' , 'Cairo' , 1),
(2 , 'Android APP' , 'Giza' , 2);


INSERT INTO Employee_projects VALUES ('222334444' , 2 , 8.44),
 ('444556666' , 1 , 6.44);

ALTER TABLE Employee ALTER COLUMN DNum INT NOT NULL 

-----------------------------
-- New additions to Task Week 2
ALTER TABLE Manage_HireDate ADD DeptNum INT
ALTER TABLE Manage_HireDate ADD CONSTRAINT FK_Manage_HireDate_Department
FOREIGN KEY (DeptNum) REFERENCES Department(DNUM)
ALTER TABLE Employee ALTER COLUMN Fname VARCHAR(70)
ALTER TABLE Manage_HireDate DROP CONSTRAINT FK_Manage_HireDate_Department
----------------------------------

ALTER TABLE Manage_HireDate DROP COLUMN DeptNum 

UPDATE Employee SET DNum = 3 WHERE SSN = '444556666';

DELETE FROM Dependent WHERE SSN = '555667777' AND Dependent_Name = 'Amr';

SELECT * FROM Employee WHERE DNum = 1;

SELECT E.SSN , E.Fname , E.Lname , EP.PNumber , EP.Working_Hours
FROM Employee E JOIN Employee_projects EP ON E.SSN = EP.SSN

