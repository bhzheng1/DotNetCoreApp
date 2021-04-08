CREATE TABLE [dbo].[Course] (
    [Id] [int] NOT NULL IDENTITY,
    [Title] [nvarchar](50) NOT NULL,
    [Credits] [int] NOT NULL,
    [DepartmentId] [int] NOT NULL,
    [CreatedDate] [datetime],
    [CreatedBy] [nvarchar](256),
    [UpdatedDate] [datetime],
    [UpdatedBy] [nvarchar](256),
    CONSTRAINT [PK_dbo.Course] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_DepartmentId] ON [dbo].[Course]([DepartmentId])
CREATE TABLE [dbo].[Department] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](150) NOT NULL,
    [Budget] [decimal](18, 2) NOT NULL,
    [StartDate] [datetime] NOT NULL,
    [InstructorId] [int],
    [RowVersion] [varbinary](max),
    [CreatedDate] [datetime],
    [CreatedBy] [nvarchar](256),
    [UpdatedDate] [datetime],
    [UpdatedBy] [nvarchar](256),
    CONSTRAINT [PK_dbo.Department] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_InstructorId] ON [dbo].[Department]([InstructorId])
CREATE TABLE [dbo].[Person] (
    [Id] [int] NOT NULL IDENTITY,
    [LastName] [nvarchar](150) NOT NULL,
    [FirstName] [nvarchar](150) NOT NULL,
    [MiddleName] [nvarchar](150),
    [DateofBirth] [datetime] NOT NULL,
    [Email] [nvarchar](150),
    [Phone] [nvarchar](150),
    [AddressLine1] [nvarchar](150),
    [AddressLine2] [nvarchar](150),
    [UnitOrApartmentNumber] [nvarchar](50),
    [City] [nvarchar](100),
    [State] [nvarchar](50),
    [ZipCode] [nvarchar](20),
    [CreatedDate] [datetime],
    [CreatedBy] [nvarchar](256),
    [UpdatedDate] [datetime],
    [UpdatedBy] [nvarchar](256),
    CONSTRAINT [PK_dbo.Person] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[OfficeAssignment] (
    [InstructorId] [int] NOT NULL,
    [Location] [nvarchar](150),
    [CreatedDate] [datetime],
    [CreatedBy] [nvarchar](256),
    [UpdatedDate] [datetime],
    [UpdatedBy] [nvarchar](256),
    CONSTRAINT [PK_dbo.OfficeAssignment] PRIMARY KEY ([InstructorId])
)
CREATE INDEX [IX_InstructorId] ON [dbo].[OfficeAssignment]([InstructorId])
CREATE TABLE [dbo].[Enrollment] (
    [Id] [int] NOT NULL IDENTITY,
    [CourseId] [int] NOT NULL,
    [StudentId] [int] NOT NULL,
    [Grade] [int] NULL,
    [CreatedDate] [datetime],
    [CreatedBy] [nvarchar](256),
    [UpdatedDate] [datetime],
    [UpdatedBy] [nvarchar](256),
    CONSTRAINT [PK_dbo.Enrollment] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CourseId] ON [dbo].[Enrollment]([CourseId])
CREATE INDEX [IX_StudentId] ON [dbo].[Enrollment]([StudentId])
CREATE TABLE [dbo].[CourseAssignment] (
    [InstructorId] [int] NOT NULL,
    [CourseId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CourseAssignment] PRIMARY KEY ([InstructorId], [CourseId])
)
CREATE INDEX [IX_InstructorId] ON [dbo].[CourseAssignment]([InstructorId])
CREATE INDEX [IX_CourseId] ON [dbo].[CourseAssignment]([CourseId])
CREATE TABLE [dbo].[Instructor] (
    [Id] [int] NOT NULL IDENTITY,
    [LastName] [nvarchar](50) NOT NULL,
    [FirstName] [nvarchar](50) NOT NULL,
    [HireDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.Instructor] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Id] ON [dbo].[Instructor]([Id])
CREATE TABLE [dbo].[Student] (
    [Id] [int] NOT NULL IDENTITY,
    [LastName] [nvarchar](50) NOT NULL,
    [FirstName] [nvarchar](50) NOT NULL,
    [EnrollmentDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.Student] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Id] ON [dbo].[Student]([Id])
ALTER TABLE [dbo].[Course] ADD CONSTRAINT [FK_dbo.Course_dbo.Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Department] ADD CONSTRAINT [FK_dbo.Department_dbo.Instructor_InstructorId] FOREIGN KEY ([InstructorId]) REFERENCES [dbo].[Instructor] ([Id])
ALTER TABLE [dbo].[OfficeAssignment] ADD CONSTRAINT [FK_dbo.OfficeAssignment_dbo.Instructor_InstructorId] FOREIGN KEY ([InstructorId]) REFERENCES [dbo].[Instructor] ([Id])
ALTER TABLE [dbo].[Enrollment] ADD CONSTRAINT [FK_dbo.Enrollment_dbo.Course_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Enrollment] ADD CONSTRAINT [FK_dbo.Enrollment_dbo.Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([Id])
ALTER TABLE [dbo].[CourseAssignment] ADD CONSTRAINT [FK_dbo.CourseAssignment_dbo.Instructor_Instructor_Id] FOREIGN KEY ([InstructorId]) REFERENCES [dbo].[Instructor] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[CourseAssignment] ADD CONSTRAINT [FK_dbo.CourseAssignment_dbo.Course_Course_Id] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course] ([Id]) ON DELETE CASCADE