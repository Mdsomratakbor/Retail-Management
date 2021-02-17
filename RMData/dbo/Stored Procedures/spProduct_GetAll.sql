﻿CREATE PROCEDURE [dbo].[spProduct_GetAll]
AS
BEGIN
SET NOCOUNT ON;
	SELECT Id, ProductName, [Description], RetailPrice, QuantityStock, IsTaxable
	FROM Product
	ORDER BY ProductName
END
