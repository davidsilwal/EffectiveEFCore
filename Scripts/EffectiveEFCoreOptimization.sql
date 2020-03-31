USE StackOverflow2010
GO

CREATE OR ALTER PROC [dbo].[usp_GetPostComments]
AS
BEGIN
    SELECT TOP (20) [p].[Body],
                         [p].[Id],
                         [p].[OwnerUserId],
                         [c].[Text],
                         [c].[Score]
      FROM      [dbo].[Posts] AS [p]
     INNER JOIN [dbo].[Comments] AS [c]
        ON [p].[Id] = [c].[PostId];

END;
GO

dbo.usp_GetPostComments

GO

CREATE OR ALTER VIEW [dbo].[GetPostComments]
WITH SCHEMABINDING, VIEW_METADATA
AS
SELECT      [p].[Body],
            [p].[Id],
            [p].[OwnerUserId],
            [c].[Text],
            [c].[Score]
  FROM      [dbo].[Posts] AS [p]
 INNER JOIN [dbo].[Comments] AS [c]
    ON [p].[Id] = [c].[PostId];


GO
IF  SERVERPROPERTY('IsXTPSupported') = 1
AND SERVERPROPERTY('EngineEdition') <> 5
BEGIN
    IF NOT EXISTS (   SELECT 1
                        FROM [sys].[filegroups] [FG]
                        JOIN [sys].[database_files] [F]
                          ON [FG].[data_space_id] = [F].[data_space_id]
                       WHERE [FG].[type] = N'FX'
                         AND [F].[type]  = 2)
    BEGIN
        ALTER DATABASE CURRENT SET AUTO_CLOSE OFF;
        DECLARE @db_name NVARCHAR(MAX) = DB_NAME();
        DECLARE @fg_name NVARCHAR(MAX);
        SELECT TOP (1) @fg_name = [name]
          FROM [sys].[filegroups]
         WHERE [type] = N'FX';

        IF @fg_name IS NULL
        BEGIN
            SET @fg_name = @db_name + N'_MODFG';
            EXEC (N'ALTER DATABASE CURRENT ADD FILEGROUP [' + @fg_name + '] CONTAINS MEMORY_OPTIMIZED_DATA;');
        END;

        DECLARE @path NVARCHAR(MAX);
        SELECT TOP (1) @path = [physical_name]
          FROM [sys].[database_files]
         WHERE CHARINDEX('\', [physical_name]) > 0
         ORDER BY [file_id];
        IF (@path IS NULL)
            SET @path = N'\' + @db_name;

        DECLARE @filename NVARCHAR(MAX) = RIGHT(@path, CHARINDEX('\', REVERSE(@path)) - 1);
        SET @filename
            = REPLACE(LEFT(@filename, LEN(@filename) - CHARINDEX('.', REVERSE(@filename))), '''', '''''') + N'_MOD';
        DECLARE @new_path NVARCHAR(MAX)
            = REPLACE(CAST(SERVERPROPERTY('InstanceDefaultDataPath') AS NVARCHAR(MAX)), '''', '''''') + @filename;

        EXEC (N'
            ALTER DATABASE CURRENT
            ADD FILE (NAME=''' + @filename + ''', filename=''' + @new_path + ''')
            TO FILEGROUP [' + @fg_name + '];');
    END;
END;

IF SERVERPROPERTY('IsXTPSupported') = 1
    EXEC (N'
    ALTER DATABASE CURRENT
    SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT ON;');

GO

CREATE TABLE [MemoryOptimizedPosts] ([Id] INT NOT NULL IDENTITY,
                                     [AcceptedAnswerId] INT NULL,
                                     [AnswerCount] INT NULL,
                                     [Body] NVARCHAR(MAX) NOT NULL,
                                     [ClosedDate] DATETIME NULL,
                                     [CommentCount] INT NULL,
                                     [CommunityOwnedDate] DATETIME NULL,
                                     [CreationDate] DATETIME NOT NULL,
                                     [FavoriteCount] INT NULL,
                                     [LastActivityDate] DATETIME NOT NULL,
                                     [LastEditDate] DATETIME NULL,
                                     [LastEditorDisplayName] NVARCHAR(40) NULL,
                                     [LastEditorUserId] INT NULL,
                                     [OwnerUserId] INT NULL,
                                     [ParentId] INT NULL,
                                     [PostTypeId] INT NOT NULL,
                                     [Score] INT NOT NULL,
                                     [Tags] NVARCHAR(150) NULL,
                                     [Title] NVARCHAR(250) NULL,
                                     [ViewCount] INT NOT NULL,
                                     CONSTRAINT [PK_MemoryOptimizedPosts]
                                         PRIMARY KEY NONCLUSTERED ([Id]))
WITH (MEMORY_OPTIMIZED = ON);

GO

SET IDENTITY_INSERT [MemoryOptimizedPosts] ON

INSERT INTO MemoryOptimizedPosts(
Id,
       AcceptedAnswerId,
       AnswerCount,
       Body,
       ClosedDate,
       CommentCount,
       CommunityOwnedDate,
       CreationDate,
       FavoriteCount,
       LastActivityDate,
       LastEditDate,
       LastEditorDisplayName,
       LastEditorUserId,
       OwnerUserId,
       ParentId,
       PostTypeId,
       Score,
       Tags,
       Title,
       ViewCount
)

SELECT Id,
       AcceptedAnswerId,
       AnswerCount,
       Body,
       ClosedDate,
       CommentCount,
       CommunityOwnedDate,
       CreationDate,
       FavoriteCount,
       LastActivityDate,
       LastEditDate,
       LastEditorDisplayName,
       LastEditorUserId,
       OwnerUserId,
       ParentId,
       PostTypeId,
       Score,
       Tags,
       Title,
       ViewCount FROM dbo.Posts
SET IDENTITY_INSERT MemoryOptimizedPosts OFF