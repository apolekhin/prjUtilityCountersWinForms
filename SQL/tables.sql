USE Test
GO

IF EXISTS (SELECT * FROM Orders)
DROP TABLE Orders 
GO


Create Table Orders (											-- Таблица заказов
	ID				INT IDENTITY(1,1)	NOT NULL,				-- Уникальный ID
	Number			VARCHAR(50)			NOT NULL,				-- Номер
	CreationDate	DATETIME			NOT NULL,				-- Дата создания 
	UnloadDate		DATETIME			NULL,					-- Дата отгрузки
	Manager			INT					NOT NULL,				-- Менеджер			
	Notes			VARCHAR(MAX)		NULL					-- Примечание
)
GO


IF EXISTS (SELECT * FROM Managers)
DROP TABLE Managers  
GO

Create Table Managers											--Таблица менеджеров
	(
	ID		INT IDENTITY(1,1)			NOT NULL,				 --Уникальный ID
	Name	VARCHAR(MAX)				NOT NULL				 --Имя менеджера 
	)
GO