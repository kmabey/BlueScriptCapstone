
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/03/2013 22:44:36
-- Generated from EDMX file: C:\Users\Kylie\Documents\0_School\Homework\8th_Quarter\Capstone\BlueScriptCapstone\CODE\Blue_Script\Blue_Script\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BlueScript];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SceneSetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Scenes] DROP CONSTRAINT [FK_SceneSetting];
GO
IF OBJECT_ID(N'[dbo].[FK_SceneCharacter_Scene]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SceneCharacter] DROP CONSTRAINT [FK_SceneCharacter_Scene];
GO
IF OBJECT_ID(N'[dbo].[FK_SceneCharacter_Character]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SceneCharacter] DROP CONSTRAINT [FK_SceneCharacter_Character];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Characters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Characters];
GO
IF OBJECT_ID(N'[dbo].[Scenes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Scenes];
GO
IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO
IF OBJECT_ID(N'[dbo].[SceneCharacter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SceneCharacter];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Characters'
CREATE TABLE [dbo].[Characters] (
    [CharacterID] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(max)  NULL,
    [Notes] nvarchar(max)  NULL
);
GO

-- Creating table 'Scenes'
CREATE TABLE [dbo].[Scenes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Notes] nvarchar(max)  NOT NULL,
    [SettingID] int  NOT NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Notes] nvarchar(max)  NULL
);
GO

-- Creating table 'SceneCharacter'
CREATE TABLE [dbo].[SceneCharacter] (
    [Scenes_ID] int  NOT NULL,
    [Characters_CharacterID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CharacterID] in table 'Characters'
ALTER TABLE [dbo].[Characters]
ADD CONSTRAINT [PK_Characters]
    PRIMARY KEY CLUSTERED ([CharacterID] ASC);
GO

-- Creating primary key on [ID] in table 'Scenes'
ALTER TABLE [dbo].[Scenes]
ADD CONSTRAINT [PK_Scenes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Scenes_ID], [Characters_CharacterID] in table 'SceneCharacter'
ALTER TABLE [dbo].[SceneCharacter]
ADD CONSTRAINT [PK_SceneCharacter]
    PRIMARY KEY NONCLUSTERED ([Scenes_ID], [Characters_CharacterID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SettingID] in table 'Scenes'
ALTER TABLE [dbo].[Scenes]
ADD CONSTRAINT [FK_SceneSetting]
    FOREIGN KEY ([SettingID])
    REFERENCES [dbo].[Settings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SceneSetting'
CREATE INDEX [IX_FK_SceneSetting]
ON [dbo].[Scenes]
    ([SettingID]);
GO

-- Creating foreign key on [Scenes_ID] in table 'SceneCharacter'
ALTER TABLE [dbo].[SceneCharacter]
ADD CONSTRAINT [FK_SceneCharacter_Scene]
    FOREIGN KEY ([Scenes_ID])
    REFERENCES [dbo].[Scenes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Characters_CharacterID] in table 'SceneCharacter'
ALTER TABLE [dbo].[SceneCharacter]
ADD CONSTRAINT [FK_SceneCharacter_Character]
    FOREIGN KEY ([Characters_CharacterID])
    REFERENCES [dbo].[Characters]
        ([CharacterID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SceneCharacter_Character'
CREATE INDEX [IX_FK_SceneCharacter_Character]
ON [dbo].[SceneCharacter]
    ([Characters_CharacterID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------