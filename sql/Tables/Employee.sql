CREATE TABLE Employee (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Age INT,
    Department NVARCHAR(50),
    Salary DECIMAL(18,2)
);
