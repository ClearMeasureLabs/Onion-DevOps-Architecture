CREATE TABLE [dbo].[ExpenseReport]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Number] NCHAR(10) NOT NULL, 
    [Title] NVARCHAR(10) NULL, 
    [Description] NVARCHAR(50) NULL
)
