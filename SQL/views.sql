USE COUNTERS
GO

-- ---------------------------------------------------------------------------
-- 1. vwCounterValue
-- Используется для отображения в экранной форме приложения
-- user friendly
-- 10.04.2015
------------------------------------------------------------------------------

--проверка на существование view
IF Exists (select * FROM vwCounterValue) 
	DROP VIEW vwCounterValue 
GO


CREATE VIEW vwCounterValue AS (  	
                                                
SELECT CounterValue.DT		AS	'Дата',      
	   CounterType.Name		AS	'Счетчик', 
	   CounterValue.Value	AS	'Показания', 
	   CounterValue.Code	AS	'Код',
	   CounterValue.ID		AS	'ID'

FROM   CounterType INNER JOIN
       CounterValue 

ON	   CounterType.Code = CounterValue.Code
)

GO

