USE Counters
GO
----------------------------------------------------------
--Создание констрейнтов
--Уникальная связка дата+код в таблице показаний
--Привязка таблицы показаний к коду счетчика в таблице типов счетчиков 
----------------------------------------------------------
ALTER TABLE 	dbo.CounterValue 
DROP CONSTRAINT	UQ_Code_DT
GO

ALTER TABLE dbo.CounterValue
DROP CONSTRAINT FK_Code
GO

ALTER TABLE dbo.CounterType
DROP CONSTRAINT PK_Code
GO

ALTER TABLE 	dbo.CounterValue 
ADD CONSTRAINT 	UQ_Code_DT
UNIQUE (Code,DT)
GO

ALTER TABLE CounterType 
ADD CONSTRAINT PK_Code PRIMARY KEY (Code) 
GO 

ALTER TABLE CounterValue 
ADD CONSTRAINT FK_Code FOREIGN KEY (Code) 
REFERENCES CounterType (Code) 
GO