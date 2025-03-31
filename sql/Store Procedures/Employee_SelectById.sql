CREATE PROCEDURE Employee_SelectById
    @Id INT
AS
BEGIN
    SELECT * FROM Employee WHERE Id = @Id;
END;