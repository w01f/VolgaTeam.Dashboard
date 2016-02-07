CREATE TABLE [Schedule] (
[Id] INTEGER  NOT NULL PRIMARY KEY,
[Name] VARCHAR(128) NOT NULL,
[LastModified] DATETIME,
[SettingsEncoded] TEXT
)