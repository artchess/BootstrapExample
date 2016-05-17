CREATE TABLE [dbo].[DBAudit](
	[AuditId] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RevisionStamp] [datetime] NULL,
	[TableName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Actions] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OldData] [xml] NULL,
	[NewData] [xml] NULL,
	[ChangedColumns] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_DBAudit] PRIMARY KEY CLUSTERED 
(
	[AuditId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
