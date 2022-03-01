delete Dependent
delete Employee
delete Quote
DBCC CHECKIDENT ('Dependent', RESEED, 0);
DBCC CHECKIDENT ('Employee', RESEED, 0);
DBCC CHECKIDENT ('Quote', RESEED, 0);

DECLARE @LASTID bigint;
insert Quote (Name) values ('First')
SET @LASTID = @@IDENTITY
insert Employee (FirstName, LastName, QuoteId) values ('Chris', 'Longo', @LASTID)
SET @LASTID = @@IDENTITY
insert Dependent (FirstName, LastName, EmployeeId) values ('Denise', 'Longo', @LASTID)
insert Dependent (FirstName, LastName, EmployeeId) values ('Matt', 'Longo', @LASTID)

insert Quote (Name) values ('Second')
SET @LASTID = @@IDENTITY
insert Employee (FirstName, LastName, QuoteId) values ('Chris', 'Long', @LASTID)
SET @LASTID = @@IDENTITY
insert Dependent (FirstName, LastName, EmployeeId) values ('Denise', 'Long', @LASTID)
insert Dependent (FirstName, LastName, EmployeeId) values ('Matt', 'Long', @LASTID)