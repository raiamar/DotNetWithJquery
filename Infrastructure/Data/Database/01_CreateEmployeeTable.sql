CREATE TABLE SW_TBL_EMPLOYEE
(
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeName NVARCHAR(250) NOT NULL,
    Salary DECIMAL(18,2) NOT NULL
)