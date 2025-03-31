CREATE PROCEDURE Employee_Delete
    @Id INT
AS
BEGIN
    DELETE FROM Employee WHERE Id = @Id;
END;