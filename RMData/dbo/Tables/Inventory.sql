CREATE TABLE [dbo].[Inventory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductId] INT NOT NULL, 
    [Quantity] INT NOT NULL DEFAULT 1, 
    [PurchasePrice] MONEY NULL DEFAULT 0, 
    [PurchaseDate] DATETIME2 NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [FK_Inventory_ToProduct] FOREIGN KEY (ProductId) REFERENCES Product(Id)
)
