Create database UTestDB

--> Category Table
CREATE TABLE Category (
    ID int  PRIMARY KEY identity(1,1),
	subject
	Title varchar(30)
);
INSERT INTO Category(Title)
VALUES ('HSC 21'),('HSC 22'),('Admission');

SELECT * FROM Category 

--> Student Table
CREATE TABLE Student (
    ID int  PRIMARY KEY identity(1,1),
	Name varchar(30),
    Email varchar(30) UNIQUE,
    Password varchar(30),
	About text,
	Category int FOREIGN KEY REFERENCES Category(ID)
);

-- SET IDENTITY_INSERT Student ON
INSERT INTO Student (Name, Email, Password, Category)
VALUES ('Fahim', 'fahimpranto002@gmail.com', '1', 3);

SELECT * FROM Student

--DROP TABLE Student
--DELETE FROM Student



--> Subject Table
CREATE TABLE Subject (
    ID int  PRIMARY KEY identity(1,1),
	Category int FOREIGN KEY REFERENCES Category(ID),
	Title varchar(30)
);
INSERT INTO Subject(Title,Category)
VALUES ('Physics',3),('Math',3),('Chemistry',3);

SELECT * FROM Subject


--> Question Table
CREATE TABLE Question(
	ID int PRIMARY KEY identity(1,1),
	Category int FOREIGN KEY REFERENCES Category(ID),
	Subject int FOREIGN KEY REFERENCES Subject(ID),
	Title text,
	Option_a text,
	Option_b text,
	Option_c text,
	Option_d text,
	Correct_option text,
	Solution text
);

INSERT INTO Question(Category,Subject,Title,Option_a,Option_b,Option_c,Option_d,Correct_option,Solution) VALUES 
(3,1,'Ques-1 title c-b','op-a','op-b','op-c','op-d','op-b','solve like this'),
(3,1,'Ques-2 title c-c','op-a','op-b','op-c','op-d','op-c','solve like this'),
(3,1,'Ques-3 title c-b','op-a','op-b','op-c','op-d','op-b','solve like this'),
(3,1,'Ques-4 title c-a','op-a','op-b','op-c','op-d','op-a','solve like this'),
(3,1,'Ques-5 title c-d','op-a','op-b','op-c','op-d','op-d','solve like this')

select * from Question


--> Exam Table
CREATE TABLE Exam(
	ID int PRIMARY KEY identity(1,1),
	Category int FOREIGN KEY REFERENCES Category(ID),
	Student int FOREIGN KEY REFERENCES Student(ID),
	Subjects text,
	Obtained_marks int,
	Total_marks int,
	Date datetime default(getDate())
);

INSERT INTO Exam(Category,Student,Subjects,Obtained_marks,Total_marks) VALUES 
(3,7,'Math,Physics,',1,2)

select * from Exam

