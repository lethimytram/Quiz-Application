CREATE DATABASE QuizDB;
GO

USE QuizDB;
GO

CREATE TABLE [Users] (
    UserId INT IDENTITY(1,1) PRIMARY KEY, 
    Username NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Quizzes (
    QuizId INT IDENTITY(1,1) PRIMARY KEY, 
    Title NVARCHAR(255) NOT NULL,
    PassingCriteria INT NOT NULL 
);
GO

CREATE TABLE Questions (
    QuestionId INT IDENTITY(1,1) PRIMARY KEY, 
    QuizId INT NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_Question_Quiz FOREIGN KEY (QuizId) REFERENCES Quizzes(QuizId)
);
GO

CREATE TABLE [Options] (
    OptionId INT IDENTITY(1,1) PRIMARY KEY, 
    QuestionId INT NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    IsCorrect BIT NOT NULL,
    CONSTRAINT FK_Option_Question FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId)
);
GO

CREATE TABLE QuizSessions (
    SessionId INT IDENTITY(1,1) PRIMARY KEY, 
    UserId INT NOT NULL,
    QuizId INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    TotalCorrect INT NOT NULL,
    Passed BIT NOT NULL,
    CONSTRAINT FK_QuizSession_User FOREIGN KEY (UserId) REFERENCES [Users](UserId),
    CONSTRAINT FK_QuizSession_Quiz FOREIGN KEY (QuizId) REFERENCES Quizzes(QuizId)
);
GO

CREATE TABLE UserAnswers (
    SessionId INT NOT NULL,
    QuestionId INT NOT NULL,
    OptionId INT NOT NULL,
    CONSTRAINT PK_UserAnswer PRIMARY KEY (SessionId, QuestionId),
    CONSTRAINT FK_UserAnswer_Session FOREIGN KEY (SessionId) REFERENCES QuizSessions(SessionId),
    CONSTRAINT FK_UserAnswer_Question FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId),
    CONSTRAINT FK_UserAnswer_Option FOREIGN KEY (OptionId) REFERENCES [Options](OptionId)
);
GO

-- Thêm dữ liệu
INSERT INTO [Users] (Username) VALUES ('testuser');
DECLARE @UserId INT = SCOPE_IDENTITY(); 


INSERT INTO Quizzes (Title, PassingCriteria) VALUES ('Quiz', 70);
DECLARE @QuizId INT = SCOPE_IDENTITY(); 

INSERT INTO Questions (QuizId, Content) VALUES (@QuizId, 'What is the primary language used to build the .NET framework?');
DECLARE @QuestionId INT = SCOPE_IDENTITY(); 

INSERT INTO [Options] (QuestionId, Content, IsCorrect) VALUES
    (@QuestionId, 'Java', 0),
    (@QuestionId, 'CSharp', 1),
    (@QuestionId, 'Python', 0);
    (@QuestionId, 'C', 0);
DECLARE @OptionIdCorrect INT = SCOPE_IDENTITY() - 2; 

INSERT INTO QuizSessions (UserId, QuizId, StartTime, EndTime, TotalCorrect, Passed)
    VALUES (@UserId, @QuizId, '2025-06-05 01:00:00', '2025-06-05 01:05:00', 0, 0);
DECLARE @SessionId INT = SCOPE_IDENTITY(); 

INSERT INTO UserAnswers (SessionId, QuestionId, OptionId) VALUES (@SessionId, @QuestionId, @OptionIdCorrect);

-- Kiểm tra dữ liệu
SELECT * FROM [Users];
SELECT * FROM Quizzes;
SELECT * FROM Questions;
SELECT * FROM [Options];
SELECT * FROM QuizSessions;
SELECT * FROM UserAnswers;
GO