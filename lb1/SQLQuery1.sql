-- Видалення бази даних, якщо вона існує
USE master;
ALTER DATABASE lb1 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE lb1;

-- Створення нової бази даних
CREATE DATABASE lb1;
USE lb1;

-- Створення таблиць
CREATE TABLE Authors (
    AuthorID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50)
);

CREATE TABLE Genres (
    GenreID INT PRIMARY KEY IDENTITY(1,1),
    GenreName NVARCHAR(50)
);

CREATE TABLE Books (
    BookID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100),
    AuthorID INT FOREIGN KEY REFERENCES Authors(AuthorID),
    Price DECIMAL(10, 2),
    GenreID INT FOREIGN KEY REFERENCES Genres(GenreID)
);

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
    OrderDate DATE,
    TotalAmount DECIMAL(10, 2)
);

-- Вставка даних
INSERT INTO Authors (FirstName, LastName) VALUES ('George', 'Orwell');
INSERT INTO Authors (FirstName, LastName) VALUES ('J.K.', 'Rowling');
INSERT INTO Genres (GenreName) VALUES ('Dystopian');
INSERT INTO Genres (GenreName) VALUES ('Fantasy');
INSERT INTO Books (Title, AuthorID, Price, GenreID) VALUES ('1984', 1, 15.99, 1);
INSERT INTO Books (Title, AuthorID, Price, GenreID) VALUES ('Harry Potter', 2, 20.99, 2);
INSERT INTO Customers (FirstName, LastName, Email) VALUES ('John', 'Doe', 'john.doe@example.com');
INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) VALUES (1, '2024-09-24', 36.98);
