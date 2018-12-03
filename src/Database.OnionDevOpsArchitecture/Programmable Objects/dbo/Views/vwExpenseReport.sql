IF OBJECT_ID('[dbo].[vwExpenseReport]') IS NOT NULL
	DROP VIEW [dbo].[vwExpenseReport];

GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
create view [dbo].[vwExpenseReport] as

select 
	ID,
	Number,
	Title,
	[Description],
	[Status]
from ExpenseReport

GO
