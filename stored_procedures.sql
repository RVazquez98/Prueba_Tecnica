-- CREATE
IF OBJECT_ID('InsertSchool', 'P') IS NOT NULL DROP PROCEDURE InsertSchool; GO
CREATE PROCEDURE InsertSchool
    @Name NVARCHAR(100),
    @Description NVARCHAR(255),
    @Code NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Schools (Name, Description, Code)
    VALUES (@Name, @Description, @Code);
END
GO

-- UPDATE
IF OBJECT_ID('UpdateSchool', 'P') IS NOT NULL DROP PROCEDURE UpdateSchool; GO
CREATE PROCEDURE UpdateSchool
    @SchoolsID INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(255),
    @Code NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Schools
    SET Name = @Name,
        Description = @Description,
        Code = @Code
    WHERE SchoolsID = @SchoolsID;
END
GO

-- DELETE
IF OBJECT_ID('DeleteSchool', 'P') IS NOT NULL DROP PROCEDURE DeleteSchool; GO
CREATE PROCEDURE DeleteSchool
    @SchoolsID INT
AS
BEGIN
    SET NOCOUNT ON;
    -- Borra relaciones en StudentTeachers
    DELETE ST
    FROM StudentTeachers ST
    INNER JOIN Teachers T ON ST.TeacherId = T.TeachersID
    WHERE T.SchoolId = @SchoolsID;

    -- Borra alumnos y profesores de la escuela
    DELETE FROM Students WHERE SchoolId = @SchoolsID;
    DELETE FROM Teachers WHERE SchoolId = @SchoolsID;

    DELETE FROM Schools WHERE SchoolsID = @SchoolsID;
END
GO
-- CREATE
IF OBJECT_ID('InsertStudent', 'P') IS NOT NULL DROP PROCEDURE InsertStudent; GO
CREATE PROCEDURE InsertStudent
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @IdentificationNumber NVARCHAR(50),
    @SchoolId INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Students (FirstName, LastName, IdentificationNumber, SchoolId)
    VALUES (@FirstName, @LastName, @IdentificationNumber, @SchoolId);
END
GO

-- UPDATE
IF OBJECT_ID('UpdateStudent', 'P') IS NOT NULL DROP PROCEDURE UpdateStudent; GO
CREATE PROCEDURE UpdateStudent
    @StudentsID INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @IdentificationNumber NVARCHAR(50),
    @SchoolId INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Students
    SET FirstName = @FirstName,
        LastName = @LastName,
        IdentificationNumber = @IdentificationNumber,
        SchoolId = @SchoolId
    WHERE StudentsID = @StudentsID;
END
GO

-- DELETE
IF OBJECT_ID('DeleteStudent', 'P') IS NOT NULL DROP PROCEDURE DeleteStudent; GO
CREATE PROCEDURE DeleteStudent
    @StudentsID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM StudentTeachers WHERE StudentId = @StudentsID;
    DELETE FROM Students WHERE StudentsID = @StudentsID;
END
GO
-- CREATE
IF OBJECT_ID('InsertTeacher', 'P') IS NOT NULL DROP PROCEDURE InsertTeacher; GO
CREATE PROCEDURE InsertTeacher
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @IdentificationNumber NVARCHAR(50),
    @SchoolId INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Teachers (FirstName, LastName, IdentificationNumber, SchoolId)
    VALUES (@FirstName, @LastName, @IdentificationNumber, @SchoolId);
END
GO

-- UPDATE
IF OBJECT_ID('UpdateTeacher', 'P') IS NOT NULL DROP PROCEDURE UpdateTeacher; GO
CREATE PROCEDURE UpdateTeacher
    @TeachersID INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @IdentificationNumber NVARCHAR(50),
    @SchoolId INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Teachers
    SET FirstName = @FirstName,
        LastName = @LastName,
        IdentificationNumber = @IdentificationNumber,
        SchoolId = @SchoolId
    WHERE TeachersID = @TeachersID;
END
GO

-- DELETE
IF OBJECT_ID('DeleteTeacher', 'P') IS NOT NULL DROP PROCEDURE DeleteTeacher; GO
CREATE PROCEDURE DeleteTeacher
    @TeachersID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM StudentTeachers WHERE TeacherId = @TeachersID;
    DELETE FROM Teachers WHERE TeachersID = @TeachersID;
END
GO

CREATE OR ALTER PROCEDURE GetStudentsByTeacher
    @TeachersID INT
AS
BEGIN
    SELECT 
        S.Id AS StudentId,
        S.FirstName,
        S.LastName,
        S.IdentificationNumber,
        Sc.Id AS SchoolId,
        Sc.Name AS SchoolName
    FROM 
        StudentTeachers ST
    INNER JOIN Students S ON ST.StudentId = S.Id
    INNER JOIN Schools Sc ON S.SchoolId = Sc.Id
    WHERE 
        ST.TeacherId = @TeachersID;
END

CREATE OR ALTER PROCEDURE GetSchoolsAndStudentsByTeacher
    @TeachersID INT
AS
BEGIN
    SELECT 
        Sc.Id AS SchoolId,
        Sc.Name AS SchoolName,
        S.Id AS StudentId,
        S.FirstName,
        S.LastName,
        S.IdentificationNumber
    FROM 
        Teachers T
    INNER JOIN Schools Sc ON T.SchoolId = Sc.Id
    INNER JOIN StudentTeachers ST ON T.Id = ST.TeacherId
    INNER JOIN Students S ON ST.StudentId = S.Id
    WHERE 
        T.Id = @TeachersID;
END
