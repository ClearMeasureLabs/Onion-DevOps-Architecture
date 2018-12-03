-- <Migration ID="abca6049-6824-40a8-911c-98b78c1ac886" />
GO

PRINT N'Creating [dbo].[ExpenseReport]'
GO
CREATE TABLE [dbo].[ExpenseReport]
(
[Id] [uniqueidentifier] NOT NULL,
[Number] [nvarchar] (10) NOT NULL,
[Title] [nvarchar] (200) NULL,
[Description] [nvarchar] (4000) NULL
)
GO
PRINT N'Creating primary key [PK__ExpenseR__3214EC07D5285F98] on [dbo].[ExpenseReport]'
GO
ALTER TABLE [dbo].[ExpenseReport] ADD CONSTRAINT [PK__ExpenseR__3214EC07D5285F98] PRIMARY KEY CLUSTERED  ([Id])
GO
