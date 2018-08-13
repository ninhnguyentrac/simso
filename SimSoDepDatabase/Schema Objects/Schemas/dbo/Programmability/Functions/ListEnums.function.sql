CREATE FUNCTION [dbo].[ListEnums] ( )
RETURNS TABLE
	AS
RETURN
	(
	  SELECT	[object_id] AS TableObjectId,
				[dbo.Attributes.Enums.EnumValue] AS ValueColumnName,
				[dbo.Attributes.Enums.EnumKey] AS KeyColumnName,
				[dbo.Attributes.Enums.EnumName] AS EnumName,
				CONVERT(bit, CASE WHEN [dbo.Attributes.Enums.EnumValue_error] != '' THEN 1 ELSE 0 END) AS ValueColumnNameError,
				CONVERT(bit, CASE WHEN [dbo.Attributes.Enums.EnumKey_error] != '' THEN 1 ELSE 0 END) AS KeyColumnNameError
	  FROM		(
				  SELECT	[_p].[major_id] AS [object_id],
							[_p].[name],
							[_c].[name] AS value
				  FROM		[sys].[extended_properties] _p
							INNER JOIN [sys].[columns] _c
								ON [_c].[object_id] = [_p].[major_id]
								   AND [_c].[column_id] = [_p].[minor_id]
				  WHERE		[_p].[class] = 1 /* Object or Column */
							AND [_p].[name] IN (
							N'dbo.Attributes.Enums.EnumValue',
							N'dbo.Attributes.Enums.EnumKey' )
				  UNION
				  SELECT	[_p].[major_id] AS [object_id],
							[_p].[name],
							CONVERT(sysname, [_p].[value]) AS value
				  FROM		[sys].[extended_properties] _p
				  WHERE		[_p].[class] = 1 /* Object or Column */
							AND [_p].[name] IN (
							N'dbo.Attributes.Enums.Enum',
							N'dbo.Attributes.Enums.EnumName' )
				  UNION
  				  SELECT	[_p].[major_id] AS [object_id],
							[_p].[name] + N'_error' AS [name],
							CASE 
								WHEN _p.Name = N'dbo.Attributes.Enums.EnumValue' AND _c.[system_type_id] NOT IN (48,52,56,104,127) THEN 'true' 
								WHEN _p.Name = N'dbo.Attributes.Enums.EnumKey' AND _c.[system_type_id] NOT IN (167, 175, 231, 239) THEN 'true'
								ELSE N'' END 
							AS value
				  FROM		[sys].[extended_properties] _p
							INNER JOIN [sys].[columns] _c
								ON [_c].[object_id] = [_p].[major_id]
								   AND [_c].[column_id] = [_p].[minor_id]
				  WHERE		[_p].[class] = 1 /* Object or Column */
							AND [_p].[name] IN (
							N'dbo.Attributes.Enums.EnumValue',
							N'dbo.Attributes.Enums.EnumKey' )              
				) s PIVOT ( MIN([value]) FOR [s].[name] IN ( [dbo.Attributes.Enums.Enum],
															 [dbo.Attributes.Enums.EnumValue],
															 [dbo.Attributes.Enums.EnumKey],
															 [dbo.Attributes.Enums.EnumName],
															 [dbo.Attributes.Enums.EnumValue_error],
															 [dbo.Attributes.Enums.EnumKey_error] ) ) p
	);