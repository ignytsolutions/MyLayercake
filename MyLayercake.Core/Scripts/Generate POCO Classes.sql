-- #########################################################
-- Author:		Ignyt Solutions
-- Copyright:	(c) Ignyt Solutions. You are free to use and redistribute.
-- Purpose:		Generate POCO Classes from all tables
-- Notes:		
-- Assumptions:	
-- #########################################################

DECLARE @tableName varchar(200)
DECLARE @referencedTableName varchar(200)
DECLARE @constraintType varchar(200)
DECLARE @columnName varchar(200)
DECLARE @nullable varchar(50)
DECLARE @datatype varchar(50)
DECLARE @maxlen int
DECLARE @sType varchar(50)
DECLARE @sProperty varchar(200)

DECLARE table_cursor CURSOR FOR 
	SELECT A.TABLE_NAME
	FROM [INFORMATION_SCHEMA].[TABLES] A
	ORDER BY A.TABLE_NAME

OPEN table_cursor

FETCH NEXT FROM table_cursor INTO @tableName

WHILE @@FETCH_STATUS = 0
BEGIN
	PRINT '[SelectProcedureName("' + @tableName + '_GetAll")]'
	PRINT '[SelectByIDProcedureName("' + @tableName + '_GetById")]'
	PRINT '[InsertProcedureName("' + @tableName + '_Save")]'
	PRINT '[UpdateProcedureName("' + @tableName + '_Save")]'
	PRINT '[DeleteProcedureName("' + @tableName + '_Delete")]'
	PRINT 'public class ' + @tableName + ' : IEntity {'

    DECLARE column_cursor CURSOR FOR 
		SELECT A.COLUMN_NAME, A.IS_NULLABLE, A.DATA_TYPE, ISNULL(A.CHARACTER_MAXIMUM_LENGTH,'-1'),C.CONSTRAINT_TYPE,OBJECT_NAME(H.referenced_object_id) 
		FROM [INFORMATION_SCHEMA].[COLUMNS] A LEFT JOIN [INFORMATION_SCHEMA].[TABLE_CONSTRAINTS] AS C INNER JOIN [INFORMATION_SCHEMA].[KEY_COLUMN_USAGE] AS K
		ON C.TABLE_NAME = K.TABLE_NAME AND C.CONSTRAINT_CATALOG = K.CONSTRAINT_CATALOG AND C.CONSTRAINT_SCHEMA = K.CONSTRAINT_SCHEMA AND C.CONSTRAINT_NAME = K.CONSTRAINT_NAME LEFT JOIN [sys].[foreign_keys] H
		ON C.CONSTRAINT_NAME = H.[name]  
		ON A.TABLE_NAME = K.TABLE_NAME AND A.COLUMN_NAME = K.COLUMN_NAME
		WHERE A.[TABLE_NAME] = @tableName
		ORDER BY A.[ORDINAL_POSITION]

    OPEN column_cursor

    FETCH NEXT FROM column_cursor INTO @columnName, @nullable, @datatype, @maxlen, @constraintType,@referencedTableName

    WHILE @@FETCH_STATUS = 0
    BEGIN
		-- datatype
		SELECT @sType = CASE @datatype
			WHEN 'int' THEN 'int'
			WHEN 'decimal' THEN 'decimal'
			WHEN 'money' THEN 'decimal'
			WHEN 'char' THEN 'string'
			WHEN 'nchar' THEN 'string'
			WHEN 'varchar' THEN 'string'
			WHEN 'nvarchar' THEN 'string'
			WHEN 'uniqueidentifier' THEN 'Guid'
			WHEN 'datetime' THEN 'DateTime'
			WHEN 'bit' THEN 'bool'
			WHEN 'int64' THEN 'bigint'
			WHEN 'byte' THEN 'smallint'
			ELSE 'string'
		END

		IF @constraintType = 'PRIMARY KEY' 
			PRINT '[IsPrimaryKey]'
		IF (@nullable = 'NO')
			PRINT '[IsRequired]'
		IF (@sType = 'String' and @maxLen <> '-1')
			PRINT '[MaxLength(' +  convert(VARCHAR(4),@maxLen) + ')]'
		
		IF @constraintType = 'FOREIGN KEY' 
			SELECT @sProperty = 'public ' + @referencedTableName + ' ' + @columnName + ' { get; set;}'
		ELSE 
			SELECT @sProperty = 'public ' + @sType + ' ' + @columnName + ' { get; set;}'

		PRINT @sProperty

		PRINT ''

		FETCH NEXT FROM column_cursor INTO @columnName, @nullable, @datatype, @maxlen, @constraintType,@referencedTableName
	END

    CLOSE column_cursor
    DEALLOCATE column_cursor

	PRINT '}'
	PRINT ''

    FETCH NEXT FROM table_cursor INTO @tableName
END

CLOSE table_cursor
DEALLOCATE table_cursor