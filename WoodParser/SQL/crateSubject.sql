CREATE TABLE [dbo].[Subject]
(
	[Id] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [Name] NCHAR(250) NOT NULL, 
    [Inn] NCHAR(30) NULL
)
