-- Tạo cơ sở dữ liệu
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ProjectPRN222')
BEGIN
    CREATE DATABASE ProjectPRN222;
END
GO

USE ProjectPRN222;
GO

-- Bảng Users
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Student', 'Teacher', 'TrafficPolice')),
    Class NVARCHAR(50),
    School NVARCHAR(100),
    Phone NVARCHAR(15)
);
GO

-- Bảng Courses
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseName NVARCHAR(100) NOT NULL,
    TeacherID INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    FOREIGN KEY (TeacherID) REFERENCES Users(UserID)
);
GO

-- Bảng Registrations
CREATE TABLE Registrations (
    RegistrationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    CourseID INT NOT NULL,
    Status NVARCHAR(20) DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Approved', 'Rejected')),
    Comments NVARCHAR(MAX),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);
GO

-- Bảng Exams
CREATE TABLE Exams (
    ExamID INT PRIMARY KEY IDENTITY(1,1),
    CourseID INT NOT NULL,
    Date DATE NOT NULL,
    Room NVARCHAR(50) NOT NULL,
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);
GO

-- Bảng Results
CREATE TABLE Results (
    ResultID INT PRIMARY KEY IDENTITY(1,1),
    ExamID INT NOT NULL,
    UserID INT NOT NULL,
    Score DECIMAL(5, 2) NOT NULL,
    PassStatus BIT NOT NULL,
    FOREIGN KEY (ExamID) REFERENCES Exams(ExamID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Bảng Certificates
CREATE TABLE Certificates (
    CertificateID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    IssuedDate DATE NOT NULL,
    ExpirationDate DATE NOT NULL,
    CertificateCode NVARCHAR(50) UNIQUE,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Bảng Notifications
CREATE TABLE Notifications (
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    SentDate DATETIME DEFAULT GETDATE(),
    IsRead BIT DEFAULT 0,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO
