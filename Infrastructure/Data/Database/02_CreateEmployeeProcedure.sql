IF OBJECT_ID('SP_CreateEmployee', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE SP_CreateEmployee
        @EmployeeName NVARCHAR(250),
        @Salary DECIMAL(18,2),
        @EmployeeId INT OUTPUT
    AS
    BEGIN
        SET NOCOUNT ON;
        
        INSERT INTO SW_TBL_EMPLOYEE (EmployeeName, Salary)
        VALUES (@EmployeeName, @Salary);
        
        SET @EmployeeId = SCOPE_IDENTITY();
    END
    ')
END
