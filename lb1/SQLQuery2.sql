CREATE DATABASE ClassifiedsDB;
GO

USE ClassifiedsDB;
GO

-- Create tables
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(15)
);

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255)
);

CREATE TABLE Ads (
    AdID INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    CategoryID INT FOREIGN KEY REFERENCES Categories(CategoryID),
    CreatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Transactions (
    TransactionID INT PRIMARY KEY IDENTITY,
    AdID INT FOREIGN KEY REFERENCES Ads(AdID),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    TransactionDate DATETIME DEFAULT GETDATE(),
    Amount DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Messages (
    MessageID INT PRIMARY KEY IDENTITY,
    AdID INT FOREIGN KEY REFERENCES Ads(AdID),
    SenderUserID INT FOREIGN KEY REFERENCES Users(UserID),
    ReceiverUserID INT FOREIGN KEY REFERENCES Users(UserID),
    MessageContent NVARCHAR(255) NOT NULL,
    SentDate DATETIME DEFAULT GETDATE()
);

-- Insert sample data
INSERT INTO Users (Username, Email, Password, PhoneNumber) VALUES
('JohnDoe', 'john@example.com', 'password123', '1234567890'),
('JaneSmith', 'jane@example.com', 'password456', '0987654321');

INSERT INTO Categories (CategoryName, Description) VALUES
('Electronics', 'Devices and gadgets'),
('Furniture', 'Home and office furniture');

INSERT INTO Ads (Title, Description, Price, UserID, CategoryID) VALUES
('Used Phone', 'A gently used smartphone.', 200.00, 1, 1),
('Wooden Table', 'Solid wood dining table.', 150.00, 2, 2);

INSERT INTO Transactions (AdID, UserID, Amount) VALUES
(1, 2, 200.00);

INSERT INTO Messages (AdID, SenderUserID, ReceiverUserID, MessageContent) VALUES
(1, 1, 2, 'Is the phone still available?');
