USE Test
GO

IF EXISTS (SELECT * FROM Orders)
DROP TABLE Orders 
GO


Create Table Orders (											-- ������� �������
	ID				INT IDENTITY(1,1)	NOT NULL,				-- ���������� ID
	Number			VARCHAR(50)			NOT NULL,				-- �����
	CreationDate	DATETIME			NOT NULL,				-- ���� ���������
	UnloadDate		DATETIME			NULL,					-- ���� ��������
	Manager			INT					NOT NULL,				-- ��������			
	Notes			VARCHAR(MAX)		NULL					-- ����������
)
GO


IF EXISTS (SELECT * FROM Managers)
DROP TABLE Managers  
GO

Create Table Managers											--������� ����������
	(
	ID		INT IDENTITY(1,1)			NOT NULL,				 --���������� ID
	Name	VARCHAR(MAX)				NOT NULL				 --��� ��������� 
	)
GO