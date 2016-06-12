CREATE TABLE [ScheduleSolution] (
[Id] INTEGER  NOT NULL PRIMARY KEY,
[ExtId] BLOB  NOT NULL,
[Schedule_Id] INTEGER  NOT NULL REFERENCES [Schedule]([Id]),
[Type] INTEGER NOT NULL,
[LastModified] DATETIME,
[ContentEncoded] TEXT,
[Discriminator] VARCHAR(128) NULL
)