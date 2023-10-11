use DominiumDB;


CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    BirthdayDate DATE,
    Gender NVARCHAR(10),
    Email NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20),
	Password NVARCHAR(15)
);

CREATE PROCEDURE sp_RegisterUser
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @BirthdayDate DATE,
    @Gender NVARCHAR(10),
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(20),
    @Password NVARCHAR(15)
AS
BEGIN
    INSERT INTO Users (FirstName, LastName, BirthdayDate, Gender, Email, PhoneNumber, Password)
    VALUES (@FirstName, @LastName, @BirthdayDate, @Gender, @Email, @PhoneNumber, @Password);
END


-- Ejemplo de uso del procedimiento almacenado sp_RegisterUser
DECLARE @FirstName NVARCHAR(50) = 'John';
DECLARE @LastName NVARCHAR(50) = 'Doe';
DECLARE @BirthdayDate DATE = '1990-01-15';
DECLARE @Gender NVARCHAR(10) = 'Male';
DECLARE @Email NVARCHAR(100) = 'johndoe@example.com';
DECLARE @PhoneNumber NVARCHAR(20) = '555-555-5555';
DECLARE @Password NVARCHAR(15) = 'SecurePass123';

-- Ejecuta el procedimiento almacenado
EXEC sp_RegisterUser 
    @FirstName,
    @LastName,
    @BirthdayDate,
    @Gender,
    @Email,
    @PhoneNumber,
    @Password;



select * from Users;

delete from Users where UserId = 1