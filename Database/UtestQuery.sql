Create database Login
DROP TABLE Users
CREATE TABLE Users (
   ID int  PRIMARY KEY identity(1,1),
    Email varchar(50),
    Password varchar(50),
	Category varchar(50)
	
);
SELECT * FROM Users
SET IDENTITY_INSERT USERS ON
INSERT INTO Users (Email,Password,Category)
VALUES ('Fariya@gmail.com','123490','HSC 22');