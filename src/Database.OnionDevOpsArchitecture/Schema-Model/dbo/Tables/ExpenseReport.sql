CREATE TABLE [dbo].[ExpenseReport]
(
[Id] [uniqueidentifier] NOT NULL,
[Number] [nvarchar] (10) NOT NULL,
[Title] [nvarchar] (200) NULL,
[Description] [nvarchar] (4000) NULL,
[Status] [nchar] (3) NOT NULL
)
GO
ALTER TABLE [dbo].[ExpenseReport] ADD CONSTRAINT [PK__ExpenseR__3214EC07D5285F98] PRIMARY KEY CLUSTERED  ([Id])
GO
