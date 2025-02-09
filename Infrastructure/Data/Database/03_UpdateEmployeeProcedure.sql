IF OBJECT_ID('SP_UpdateEmployeeSalary', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE SP_UpdateEmployeeSalary
        @EmployeeId INT,
        @Salary DECIMAL(18,2)
    AS
    BEGIN
        
        UPDATE SW_TBL_EMPLOYEE 
        SET Salary = @Salary
        WHERE EmployeeId = @EmployeeId;
    END
    ')
END
