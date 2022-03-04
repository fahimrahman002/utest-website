Create database UTestDB

CREATE TABLE Student (
    ID int  PRIMARY KEY identity(1,1),
	Name varchar(30),
    Email varchar(30),
    Password varchar(30),
	Category varchar(30)
);
ALTER TABLE Student ADD UNIQUE(Email);
SELECT * FROM Student
--DROP TABLE Student
--DELETE FROM Student

CREATE TABLE Category (
    ID int  PRIMARY KEY identity(1,1),
	Title varchar(30)
);

SELECT * FROM Category 

-- SET IDENTITY_INSERT Student ON


INSERT INTO Category(Title)
VALUES ('HSC 21'),('HSC 22'),('Admission');

INSERT INTO Student (Name, Email, Password, Category)
VALUES ('Fahim', 'fahimpranto002@gmail.com', '123456', 'HSC 22');