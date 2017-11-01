SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Nodes] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Feature]      VARCHAR (500) NOT NULL,
    [Animal]       VARCHAR (100) NOT NULL,
    [Type]         BIT           NOT NULL,
    [ParentNodeId] INT           NOT NULL
);

