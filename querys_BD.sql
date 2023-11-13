use Dominium;

CREATE TABLE TUsers (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    Email NVARCHAR(255),
    PhoneNumber NVARCHAR(20),
    Password NVARCHAR(255)
);

ALTER TABLE TUsers
ADD RoleID INT,
FOREIGN KEY (RoleID) REFERENCES Roles(RoleID);

CREATE TABLE Roles (
    RoleID INT PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL
);


CREATE PROCEDURE RegisterUsers
    @FirstName NVARCHAR(255),
    @LastName NVARCHAR(255),
    @Email NVARCHAR(255),
    @PhoneNumber NVARCHAR(20),
    @Password NVARCHAR(255),
	@RoleID INT
AS
BEGIN
    INSERT INTO TUsers (FirstName, LastName, Email, PhoneNumber, Password, RoleID)
    VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Password, @RoleID);
END;

drop procedure RegisterUser

select * from TUsers;
select * from Roles;