CREATE PROCEDURE Employee_Update
    @Id INT,
    @Name NVARCHAR(100),
    @Age INT,
    @Department NVARCHAR(50),
    @Salary DECIMAL(18,2)
AS
BEGIN
    UPDATE Employee
    SET Name = @Name, Age = @Age, Department = @Department, Salary = @Salary
    WHERE Id = @Id;
END;