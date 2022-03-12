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
    Password varchar(2000),
	About text,
	Category int FOREIGN KEY REFERENCES Category(ID)
);

--alter table Student
--alter column Password varchar(2000)
-- SET IDENTITY_INSERT Student ON
INSERT INTO Student (Name, Email, Password, Category)
VALUES ('User2', 'user2@gmail.com', '1', 3);

SELECT * FROM Student

--DROP TABLE Student
DELETE FROM Student



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
	Correct_ans text,
	Solution text
);

--EXEC sp_RENAME 'Question.Correct_option' , 'Correct_ans', 'COLUMN'

INSERT INTO Question(Category,Subject,Title,Option_a,Option_b,Option_c,Option_d,Correct_ans,Solution) VALUES 
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
	Obtained_marks float default 0,
	Total_marks float default 0,
	Total_correct_ans int default 0,
	Total_wrong_ans int default 0,
	Total_skipped_ans int default 0,
	Exam_date datetime default(getDate())
);


--INSERT INTO Exam(Category,Student,Subjects,Obtained_marks,Total_marks,Total_correct_ans,Total_wrong_ans,Total_skipped_ans) VALUES 
--(3,7,'Math,Physics,',1,2,1,1,0)

--INSERT INTO Exam(Category,Student,Subjects,Obtained_marks,Total_marks,Total_correct_ans,Total_wrong_ans,Total_skipped_ans) VALUES 
--(3,9,'Math,Physics,',3,2,1,1,0)

--INSERT INTO Exam(Category,Student,Subjects,Obtained_marks,Total_marks,Total_correct_ans,Total_wrong_ans,Total_skipped_ans) VALUES 
--(3,10,'Math,Physics,',2,2,1,1,0)

--INSERT INTO Exam(Category,Student,Subjects,Obtained_marks,Total_marks,Total_correct_ans,Total_wrong_ans,Total_skipped_ans) VALUES 
--(3,7,'Math,Physics,',4,2,1,1,0)

select * from Exam
delete from Exam
