/*CREATE PROCEDURE [dbo].DefineEnum
 @TableName sysname,
 @EnumName sysname,
 @Value_ColumnName sysname,
 @Key_ColumnName sysname
AS
SET NOCOUNT ON;

DECLARE 
 @TableId int = OBJECT_ID(@TableName),
 @TableSchemaName_normalized sysname = OBJECT_SCHEMA_NAME(OBJECT_ID(@TableName)),
 @TableName_normalized sysname = OBJECT_NAME(OBJECT_ID(@TableName)),
 @True bit = 1,
 @False bit = 0,
 @ValidationErrorMessage nvarchar(1000),
 @CurrentStep tinyint;

IF (ISNULL(@EnumName, N'') IS NULL)
BEGIN
 SET @ValidationErrorMessage = N'Parameter @EnumName must be not NULL or Empty string.'; 
 RAISERROR (@ValidationErrorMessage, 16, 1);
 RETURN;
END

BEGIN TRAN;
BEGIN TRY;
 IF (EXISTS (SELECT * FROM [dbo].[ListEnums]() WHERE [TableObjectId] = @TableId))
  EXEC [dbo].[UndefineEnum] @TableName = @TableName;

  SET @CurrentStep = 1;
 EXEC sys.sp_addextendedproperty 
  @name = N'dbo.Attributes.Enums.EnumName',
  @value = @EnumName,
  @level0type = N'Schema', @level0name = @TableSchemaName_normalized,
  @level1type = N'Table',  @level1name = @TableName_normalized;

  EXEC sys.sp_addextendedproperty 
  @name = N'dbo.Attributes.Enums.Enum',
  @value = @True,
  @level0type = N'Schema', @level0name = @TableSchemaName_normalized,
  @level1type = N'Table',  @level1name = @TableName_normalized;

  SET @CurrentStep = 2;
 EXEC sys.sp_addextendedproperty 
  @name = N'dbo.Attributes.Enums.EnumValue',
  @value = @True,
  @level0type = N'Schema', @level0name = @TableSchemaName_normalized,
  @level1type = N'Table',  @level1name = @TableName_normalized,
  @level2type = N'Column',  @level2name = @Value_ColumnName;

  SET @CurrentStep = 3;
 EXEC sys.sp_addextendedproperty 
  @name = N'dbo.Attributes.Enums.EnumKey',
  @value = @True,
  @level0type = N'Schema', @level0name = @TableSchemaName_normalized,
  @level1type = N'Table',  @level1name = @TableName_normalized,
  @level2type = N'Column',  @level2name = @Key_ColumnName;

  /* 
  Now when I know that all column and table names are correct, 
  make sure that the EnumValue and EnumKey columns each has a unique constraint or index 
 */

  IF (2 != (SELECT COUNT(*) 
 FROM sys.[indexes] i
  INNER JOIN sys.[index_columns] ic
   ON ic.[object_id] = i.[object_id]
    AND ic.[index_id] = i.[index_id]
  INNER JOIN (
   /* Limit to one-column indexes where this single column is not a covering column (however there should be none of these) */
   SELECT [object_id], [index_id]
   FROM sys.[index_columns] 
   WHERE 
    [is_included_column] = 0
   GROUP BY [object_id], [index_id]
   HAVING COUNT(*) = 1
  ) icx
   ON icx.[object_id] = ic.[object_id]
    AND icx.[index_id] = ic.[index_id]
  INNER JOIN sys.[columns] c
   ON c.[object_id] = ic.[object_id]
    AND c.[column_id] = ic.[column_id]
 WHERE 
  [is_unique] | [is_unique_constraint] = 1
  AND i.[object_id] = @TableId
  AND c.[name] IN (@Value_ColumnName, @Key_ColumnName)
 ))
 RAISERROR ('Both columns - key ([%s]) and value ([%s]) must be enforced by unique indexes', 16, 1, @Key_ColumnName, @Value_ColumnName);

  COMMIT TRAN;
END TRY
BEGIN CATCH;
 DECLARE 
  @Error_Severity int,
  @Error_State int,
  @ExceptionMessage nvarchar(max);

  SELECT 
  @Error_State = ERROR_STATE (),
  @Error_Severity = ERROR_SEVERITY ();

  IF (@CurrentStep = 1)
  SET @ValidationErrorMessage = N'Invalid object "' + ISNULL(@TableName, N'') + '".';

  IF (@CurrentStep = 2)
  SET @ValidationErrorMessage = N'Invalid Enum Value column "' + ISNULL(@Value_ColumnName, N'') + '".';

  IF (@CurrentStep = 3)
  SET @ValidationErrorMessage = N'Invalid Enum Title column "' + ISNULL(@Key_ColumnName, N'') + '".';
 
 ROLLBACK TRAN; 
 RAISERROR (@ValidationErrorMessage, @Error_Severity, @Error_State);
 
 RETURN;
END CATCH;*/