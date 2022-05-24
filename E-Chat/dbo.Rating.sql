CREATE TABLE [dbo].[Rating] (
    [Name]     NVARCHAR (450) NOT NULL,
    [Feedback] NVARCHAR (MAX) NULL,
    [Date]     NVARCHAR (MAX) NULL,
    [Time]     NVARCHAR (MAX) NULL,
    [Score]    INT NULL,
    CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED ([Name] ASC)
);

