

-- =================================================================================
--     Developer: Orah Kittrell
--   Create Date: 08-05-2014
-- Revision Date: 04-13-2015
--          Name: uspCleanDatabase
--      Abstract: uspCleanDatabase for AssetDisposition Database 
-- =================================================================================

-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------
--USE [AssetDisposition];     -- Get out of the master database
USE [DevNet] 
SET NOCOUNT ON -- Report only errors

-- --------------------------------------------------------------------------------
-- Creates stored procedure named uspDropForeignKeys
-- --------------------------------------------------------------------------------
GO


-- =================================================================================
--     Developer: Orah Kittrell
--   Create Date: 08-05-2014
-- Revision Date: 09-19-2014
--          Name: uspDropUserForeignKeys
--      Abstract: Drops all foreign key constraits 
-- =================================================================================
CREATE PROCEDURE uspDropUserForeignKeys
AS
SET NOCOUNT ON
DECLARE @strMessage		VARCHAR(250)
DECLARE @strForeignKey	VARCHAR(250)
DECLARE @strChildTable	VARCHAR(250)
DECLARE @strCommand		VARCHAR(250)
DECLARE @strTab			CHAR = CHAR(9)

PRINT @strTab + 'DROP ALL USER FOREIGN KEYS ...'


DECLARE crsForeignKeys CURSOR FOR
SELECT
	 name						AS strForeignKey
	,OBJECT_NAME( parent_obj )	AS strChildTable
	
FROM
	SysObjects
WHERE
		type		=	'F'				/* Foreign Keys Only */
	--AND(
--			name	LIKE	'FK__P_%'
--		)
--	AND OBJECT_NAME( parent_obj ) LIKE 'P_%'
	
ORDER BY
	name

OPEN crsForeignKeys
FETCH NEXT FROM crsForeignKeys  INTO @strForeignKey, @strChildTable

-- Loop until no more records
WHILE @@FETCH_STATUS = 0
BEGIN
	SELECT @strMessage = @strTab + @strTab + '-DROP ' + @strForeignKey
	PRINT  @strMessage 
	
	-- Build command
	SELECT @strCommand = 'ALTER TABLE ' + @strChildTable + ' DROP CONSTRAINT ' + @strForeignKey
	
	-- Execute command
	EXEC( @strCommand )
	
	FETCH NEXT FROM crsForeignKeys INTO @strForeignKey, @strChildTable
END

-- Clean up
CLOSE crsForeignKeys
DEALLOCATE crsForeignKeys

PRINT @strTab + 'DONE'
GO


-- --------------------------------------------------------------------------------
-- Creates stored procedure named uspDropUserViews
-- --------------------------------------------------------------------------------
	
GO
-- =================================================================================
--     Developer: Orah Kittrell
--   Create Date: 08-05-2014
-- Revision Date: 09-19-2014
--          Name: uspDropUserViews
--      Abstract: Drops all views
-- =================================================================================
CREATE PROCEDURE uspDropUserViews
AS
SET NOCOUNT ON
DECLARE @strMessage		VARCHAR(250)
DECLARE @strUserView	VARCHAR(250)
DECLARE @strCommand		VARCHAR(250)
DECLARE @strTab			CHAR = CHAR(9)

PRINT @strTab + 'DROP ALL USER VIEWS ...'

DECLARE crsUserViews CURSOR FOR
SELECT
					
	name	AS strUserView
	
FROM
	SysObjects
WHERE
		xtype		=	'V'				/* VIEWS Only */
	AND name LIKE 'V%'
ORDER BY
	name

OPEN crsUserViews
FETCH NEXT FROM crsUserViews  INTO @strUserView

-- Loop until no more records
WHILE @@FETCH_STATUS = 0
BEGIN
	SELECT @strMessage = @strTab + @strTab + '-DROP ' + @strUserView
	PRINT  @strMessage 
	
	-- Build command
	SELECT @strCommand = 'IF OBJECT_ID (''' + @strUserView + ''') IS NOT NULL DROP VIEW ' + @strUserView
	
	-- Execute command
	EXEC( @strCommand )
	
	FETCH NEXT FROM crsUserViews INTO @strUserView
END

-- Clean up
CLOSE crsUserViews
DEALLOCATE crsUserViews

PRINT @strTab + 'DONE'
GO



-- --------------------------------------------------------------------------------
-- Creates stored procedure named uspDropUserTables
-- --------------------------------------------------------------------------------
GO
-- =================================================================================
--     Developer: Orah Kittrell
--   Create Date: 08-05-2014
-- Revision Date: 09-19-2014
--          Name: uspDropUserTables
--      Abstract: Drops all user tables
-- =================================================================================
CREATE PROCEDURE uspDropUserTables
AS
SET NOCOUNT ON
DECLARE @strMessage		VARCHAR(250)
DECLARE @strUserTable	VARCHAR(250)
DECLARE @strCommand		VARCHAR(250)
DECLARE @strTab			CHAR = CHAR(9)

PRINT @strTab + 'DROP ALL USER TABLES ...'

DECLARE crsUserTables CURSOR FOR
SELECT
	 name						AS strUserTable
	
FROM
	SysObjects
WHERE
		xtype		=	'U'				/* User Tables */
	-- AND name	LIKE	'P_%'
	
ORDER BY
	name

OPEN crsUserTables
FETCH NEXT FROM crsUserTables  INTO @strUserTable

-- Loop until no more records
WHILE @@FETCH_STATUS = 0
BEGIN
	SELECT @strMessage = @strTab + @strTab + '-DROP ' + @strUserTable
	PRINT  @strMessage 
	
	-- Build command
	SELECT @strCommand = 'IF OBJECT_ID (''' + @strUserTable + ''') IS NOT NULL DROP TABLE ' + @strUserTable
	
	-- Execute command
	EXEC( @strCommand )
	
	FETCH NEXT FROM crsUserTables INTO @strUserTable
END

-- Clean up
CLOSE crsUserTables
DEALLOCATE crsUserTables

PRINT @strTab + 'DONE'
GO


-- --------------------------------------------------------------------------------
-- Creates stored procedure named uspDropUserStoredProcedures
-- --------------------------------------------------------------------------------
GO
-- =================================================================================
--     Developer: Orah Kittrell
--   Create Date: 08-05-2014
-- Revision Date: 09-19-2014
--          Name: uspDropUserStoredProcedures
--      Abstract: Drops all user stored procedures
-- =================================================================================
CREATE PROCEDURE uspDropUserStoredProcedures
AS
SET NOCOUNT ON
DECLARE @strMessage						VARCHAR(250)
DECLARE @strUserStoredProcedures		VARCHAR(250)
DECLARE @strCommand						VARCHAR(250)
DECLARE @strTab							CHAR = CHAR(9)

PRINT @strTab + 'DROP ALL USER STORED PROCEDURES ...'

DECLARE crsUserStoredProcedures CURSOR FOR
SELECT
	 name						AS strUserStoredProcedures
	
FROM
	SysObjects
WHERE
		xtype		=	'P'				/* User Stored procedures */
	
	AND  name    NOT LIKE 'uspDropUserForeignKeys'
	AND  name    NOT LIKE 'uspDropUserViews'
	AND  name    NOT LIKE 'uspDropUserTables'
	AND  name    NOT LIKE 'uspDropUserStoredProcedures'
	AND  name    NOT LIKE 'uspCleanDatabase' 
--	OR  name		LIKE 'Create%'
--	OR  name		LIKE 'Delete%'
--	OR  name		LIKE 'Get%'
--	OR  name		LIKE 'Update%'
	
ORDER BY
	name

OPEN crsUserStoredProcedures
FETCH NEXT FROM crsUserStoredProcedures  INTO @strUserStoredProcedures

-- Loop until no more records
WHILE @@FETCH_STATUS = 0
BEGIN
	SELECT @strMessage = @strTab + @strTab + '-DROP ' + @strUserStoredProcedures
	PRINT  @strMessage 
	
	-- Build command
	SELECT @strCommand = 'IF OBJECT_ID (''' + @strUserStoredProcedures + ''') IS NOT NULL DROP PROCEDURE ' + @strUserStoredProcedures
	
	-- Execute command
	EXEC( @strCommand )
	
	FETCH NEXT FROM crsUserStoredProcedures INTO @strUserStoredProcedures
END

-- Clean up
CLOSE crsUserStoredProcedures
DEALLOCATE crsUserStoredProcedures

PRINT @strTab + 'DONE'
GO


-- --------------------------------------------------------------------------------
-- Creates stored procedure named uspDropUser
-- --------------------------------------------------------------------------------
--GO
--CREATE PROCEDURE uspDropUser
--AS
--SET NOCOUNT ON
--DECLARE @strMessage		VARCHAR(250)
--DECLARE @strCommand	    VARCHAR(250)
--DECLARE @strTab		    CHAR = CHAR(9)

--BEGIN
	--SELECT @strMessage = @strTab + @strTab + '-DROP ' + name FROM  sys.schemas WHERE principal_id = USER_ID('wipedrive')
	--PRINT  @strMessage 
	
	-- Build command
	--SELECT @strCommand = 'ALTER AUTHORIZATION ON SCHEMA::db_accessadmin TO dbo ' + 
	--				     'ALTER AUTHORIZATION ON SCHEMA::db_datareader TO dbo' +
	--					 'ALTER AUTHORIZATION ON SCHEMA::db_datawriter TO dbo' +
	--					 'DROP USER wipedrive'
	-- Execute command
	--EXEC( @strCommand )
	
	--FETCH NEXT FROM crsUserTables INTO @strUserTable
	
	
	--SELECT name FROM  sys.schemas WHERE principal_id = USER_ID('wipedrive')
	
	--ALTER AUTHORIZATION ON SCHEMA::db_accessadmin TO dbo
	--ALTER AUTHORIZATION ON SCHEMA::db_datareader TO dbo
	--ALTER AUTHORIZATION ON SCHEMA::db_datawriter TO dbo
	--GO	
	--DROP USER wipedrive
	
--END



-- --------------------------------------------------------------------------------
-- Creates stored procedure named uspCleanDatabase
-- --------------------------------------------------------------------------------
GO
-- =================================================================================
--     Developer: Orah Kittrell
--   Create Date: 08-05-2014
-- Revision Date: 09-19-2014
--          Name: uspCleanDatabase
--      Abstract: Drops all foreign key constraints, views, user tables, and user stored procedures
-- =================================================================================
CREATE PROCEDURE uspCleanDatabase
AS
SET NOCOUNT ON
DECLARE @strMessage				VARCHAR(250)
DECLARE @strCommand				VARCHAR(250)
DECLARE @strTab					CHAR = CHAR(9)

BEGIN TRANSACTION

PRINT 'CLEANING THE DATABASE ...'

EXECUTE uspDropUserForeignKeys
PRINT ''

EXECUTE uspDropUserViews
PRINT ''

EXECUTE uspDropUserStoredProcedures
PRINT ''

EXECUTE uspDropUserTables
--PRINT ''

--EXECUTE uspDropUser


COMMIT TRANSACTION

GO

EXECUTE uspCleanDatabase

GO

-- --------------------------------------------------------------------------------
-- Promise to never trash a database with deadly stored procedures 
-- --------------------------------------------------------------------------------

-- I promise never, ever to create these stored procedures or anything like them
-- on a production database unless my boss tells me to - Orah Kittrell