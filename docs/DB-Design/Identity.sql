CREATE TABLE [AppUser] (
  [Id] uuid PRIMARY KEY,
  [FirstName] nvarchar(255),
  [LastName] nvarchar(255),
  [FullName] nvarchar(255),
  [DayOfBirth] datetime,
  [IsDirector] boolean,
  [IsHeadOfDepartment] boolean,
  [ManagerId] uuid,
  [PositionId] uuid,
  [IsReceipient] int
)
GO

CREATE TABLE [AppRole] (
  [Id] uuid PRIMARY KEY,
  [Name] nvarchar(255),
  [NormalizedName] nvarchar(255),
  [ConcurrencyStamp] nvarchar(255),
  [Description] nvarchar(255),
  [RoleCode] nvarchar(255)
)
GO

CREATE TABLE [IdentityUserRole] (
  [UserId] uuid,
  [RoleId] uuid
)
GO

CREATE TABLE [IdentityRoleClaim] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [RoleId] uuid,
  [ClaimType] nvarchar(255),
  [ClaimValue] nvarchar(255)
)
GO

CREATE TABLE [IdentityUserClaim] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserId] uuid,
  [ClaimType] nvarchar(255),
  [ClaimValue] nvarchar(255)
)
GO

CREATE TABLE [IdentityUserLogin] (
  [LoginProvider] nvarchar(255),
  [ProviderKey] nvarchar(255),
  [ProviderDisplayName] nvarchar(255),
  [UserId] uuid,
  PRIMARY KEY ([LoginProvider], [ProviderKey])
)
GO

CREATE TABLE [IdentityUserToken] (
  [UserId] uuid,
  [LoginProvider] nvarchar(255),
  [Name] nvarchar(255),
  [Value] nvarchar(255)
)
GO

CREATE TABLE [Permission] (
  [RoleId] uuid,
  [FunctionId] nvarchar(255),
  [ActionId] nvarchar(255)
)
GO

CREATE TABLE [Function] (
  [Id] nvarchar(255) PRIMARY KEY,
  [Name] nvarchar(255),
  [Url] nvarchar(255),
  [ParrentId] nvarchar(255),
  [SortOrder] int,
  [CssClass] nvarchar(255),
  [IsActive] boolean
)
GO

CREATE TABLE [Action] (
  [Id] nvarchar(255) PRIMARY KEY,
  [Name] nvarchar(255),
  [SortOrder] int,
  [IsActive] boolean
)
GO

CREATE TABLE [ActionInFunction] (
  [ActionId] nvarchar(255),
  [FunctionId] nvarchar(255)
)
GO

EXEC sp_addextendedproperty
@name = N'Table_Description',
@value = 'Represents application users.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'AppUser';
GO

EXEC sp_addextendedproperty
@name = N'Table_Description',
@value = 'Represents application roles.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'AppRole';
GO

EXEC sp_addextendedproperty
@name = N'Table_Description',
@value = 'Join table between AppUser and AppRole',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'IdentityUserRole';
GO

EXEC sp_addextendedproperty
@name = N'Table_Description',
@value = 'Permission = Role + Function + Action',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Permission';
GO

EXEC sp_addextendedproperty
@name = N'Table_Description',
@value = 'Represents a screen/module in the system.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Function';
GO

EXEC sp_addextendedproperty
@name = N'Table_Description',
@value = 'Represents an operation: View, Edit, Delete, etc.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Action';
GO

EXEC sp_addextendedproperty
@name = N'Table_Description',
@value = 'Defines which actions are available in a function.',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'ActionInFunction';
GO

ALTER TABLE [AppUser] ADD FOREIGN KEY ([ManagerId]) REFERENCES [AppUser] ([Id])
GO

ALTER TABLE [IdentityUserRole] ADD FOREIGN KEY ([UserId]) REFERENCES [AppUser] ([Id])
GO

ALTER TABLE [IdentityUserRole] ADD FOREIGN KEY ([RoleId]) REFERENCES [AppRole] ([Id])
GO

ALTER TABLE [IdentityRoleClaim] ADD FOREIGN KEY ([RoleId]) REFERENCES [AppRole] ([Id])
GO

ALTER TABLE [IdentityUserClaim] ADD FOREIGN KEY ([UserId]) REFERENCES [AppUser] ([Id])
GO

ALTER TABLE [IdentityUserLogin] ADD FOREIGN KEY ([UserId]) REFERENCES [AppUser] ([Id])
GO

ALTER TABLE [IdentityUserToken] ADD FOREIGN KEY ([UserId]) REFERENCES [AppUser] ([Id])
GO

ALTER TABLE [Permission] ADD FOREIGN KEY ([RoleId]) REFERENCES [AppRole] ([Id])
GO

ALTER TABLE [Permission] ADD FOREIGN KEY ([FunctionId]) REFERENCES [Function] ([Id])
GO

ALTER TABLE [Permission] ADD FOREIGN KEY ([ActionId]) REFERENCES [Action] ([Id])
GO

ALTER TABLE [Function] ADD FOREIGN KEY ([ParrentId]) REFERENCES [Function] ([Id])
GO

ALTER TABLE [ActionInFunction] ADD FOREIGN KEY ([ActionId]) REFERENCES [Action] ([Id])
GO

ALTER TABLE [ActionInFunction] ADD FOREIGN KEY ([FunctionId]) REFERENCES [Function] ([Id])
GO
