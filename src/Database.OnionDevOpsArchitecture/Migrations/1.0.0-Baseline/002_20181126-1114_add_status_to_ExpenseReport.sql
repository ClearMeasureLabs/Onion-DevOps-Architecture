-- <Migration ID="3b43a8ef-79f2-47f8-8908-28b4490cdf13" />
GO
ALTER TABLE dbo.ExpenseReport
ADD [Status] [nchar] (3) NOT NULL
GO
