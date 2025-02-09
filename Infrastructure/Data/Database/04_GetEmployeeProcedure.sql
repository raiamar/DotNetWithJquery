IF OBJECT_ID('SP_GetAllEmployees', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE SP_GetAllEmployees
        @PageNumber INT,
        @PageSize INT,
        @SortOrder VARCHAR(4),
        @TotalEmployees INT OUTPUT
    AS
    BEGIN
        SET NOCOUNT ON;
        
        SELECT @TotalEmployees = COUNT(*) 
        FROM SW_TBL_EMPLOYEE;
        
        SELECT 
            EmployeeId, 
            EmployeeName, 
            Salary
        FROM SW_TBL_EMPLOYEE
        ORDER BY 
            CASE @SortOrder
                WHEN ''DESC'' THEN Salary END DESC,
            CASE @SortOrder
                WHEN ''ASC'' THEN Salary END ASC,
            EmployeeName ASC
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;
    END
    ')
END