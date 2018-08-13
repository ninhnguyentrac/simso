/*CREATE PROCEDURE [dbo].UndefineEnum
 @TableName sysname
AS
SET NOCOUNT ON;

DECLARE 
 @TableId int = OBJECT_ID(@TableName),
 @TableSchemaName_normalized sysname = OBJECT_SCHEMA_NAME(OBJECT_ID(@TableName)),
 @TableName_normalized sysname = OBJECT_NAME(OBJECT_ID(@TableName)),
 @True bit = 1,
 @False bit = 0,
 @Value_ColumnName sysname,
 @Key_ColumnName sysname;

SELECT 
 @Value_ColumnName = [ValueColumnName],
 @Key_ColumnName = [KeyColumnName]
FROM [dbo].[ListEnums]() 
WHERE
 [TableObjectId] = @TableId


BEGIN TRAN;
BEGIN TRY;
 IF (@Value_ColumnName IS NOT NULL)
 BEGIN;
  EXEC sys.sp_dropextendedproperty 
   @name = N'dbo.Attributes.Enums.EnumValue',
   @level0type = N'Schema', @level0name = @TableSchemaName_normalized,
   @level1type = N'Table',  @level1name = @TableName_normalized,
   @level2type = N'Column',  @level2name = @Value_ColumnName;
 END;

  IF (@Key_ColumnName IS NOT NULL)
 BEGIN;
  EXEC sys.sp_dropextendedproperty 
   @name = N'dbo.Attributes.Enums.EnumKey',
   @level0type = N'Schema', @level0name = @TableSchemaName_normalized,
   @level1type = N'Table',  @level1name = @TableName_normalized,
   @level2type = N'Column',  @level2name = @Key_ColumnName;
 END;
 
 IF (EXISTS (SELECT * FROM sys.[fn_listextendedproperty] (N'dbo.Attributes.Enums.Enum', N'Schema', @TableSchemaName_normalized, N'Table', @TableName_normalized, DEFAULT, DEFAULT)))
 BEGIN;
  EXEC sys.sp_dropextendedproperty 
   @name = N'dbo.Attributes.Enums.Enum',
   @level0type = N'Schema', @level0name = @TableSchemaName_normalized,
   @level1type = N'Table',  @level1name = @TableName_normalized;  
 END;
 IF (EXISTS (SELECT * FROM sys.[fn_listextendedproperty] (N'dbo.Attributes.Enums.EnumName', N'Schema', @TableSchemaName_normalized, N'Table', @TableName_normalized, DEFAULT, DEFAULT)))
 BEGIN;
  EXEC sys.sp_dropextendedproperty 
   @name = N'dbo.Attributes.Enums.EnumName',
   @level0type = N'Schema', @level0name = @TableSchemaName_normalized,
   @level1type = N'Table',  @level1name = @TableName_normalized;
 END;


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
 
 SET @ExceptionMessage = 'Failed to Undefine Enum';

  ROLLBACK TRAN; 
 RAISERROR (@ExceptionMessage, @Error_Severity, @Error_State);
 
 RETURN;
END CATCH;*/