CREATE PROCEDURE Employee_Insert
    @Name NVARCHAR(100),
    @Age INT,
    @Department NVARCHAR(50),
    @Salary DECIMAL(18,2)
AS
BEGIN
    INSERT INTO Employee (Name, Age, Department, Salary)
    VALUES (@Name, @Age, @Department, @Salary);
    
    SELECT SCOPE_IDENTITY() AS Id;
END;