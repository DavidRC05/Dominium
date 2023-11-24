use Dominium;

CREATE TABLE TUsers (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    Email NVARCHAR(255),
    PhoneNumber NVARCHAR(20),
    Password NVARCHAR(255),
	Rol INT
);



CREATE PROCEDURE RegisterUsers
    @FirstName NVARCHAR(255),
    @LastName NVARCHAR(255),
    @Email NVARCHAR(255),
    @PhoneNumber NVARCHAR(20),
    @Password NVARCHAR(255),
	@Rol INT
AS
BEGIN
    INSERT INTO TUsers (FirstName, LastName, Email, PhoneNumber, Password, Rol)
    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Password, @Rol);
END;


select * from TUsers;


