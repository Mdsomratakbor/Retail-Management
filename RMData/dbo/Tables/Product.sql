CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductName] NVARCHAR(100) NOT NULL, 
        [QuantityStock] INT NOT NULL DEFAULT 1,
    [Description] NVARCHAR(MAX) NOT NULL, 
	[RetailPrice] MONEY NOT NULL DEFAULT 0, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [LastModified] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [IsTaxable] BIT NOT NULL DEFAULT 1 

)
