USE COUNTERS
GO

-- ---------------------------------------------------------------------------
-- 1. vwCounterValue
-- ������������ ��� ����������� � �������� ����� ����������
-- user friendly
-- 10.04.2015
------------------------------------------------------------------------------

--�������� �� ������������� view
IF Exists (select * FROM vwCounterValue) 
	DROP VIEW vwCounterValue 
GO


CREATE VIEW vwCounterValue AS (  	
                                                
SELECT CounterValue.DT		AS	'����',      
	   CounterType.Name		AS	'�������', 
	   CounterValue.Value	AS	'���������', 
	   CounterValue.Code	AS	'���',
	   CounterValue.ID		AS	'ID'

FROM   CounterType INNER JOIN
       CounterValue 

ON	   CounterType.Code = CounterValue.Code
)

GO

