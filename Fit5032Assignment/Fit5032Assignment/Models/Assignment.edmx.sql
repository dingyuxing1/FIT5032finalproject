
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/29/2022 09:56:52
-- Generated from EDMX file: C:\Users\yuxin\Desktop\Fit5032Assignment\Fit5032Assignment\Models\Assignment.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-Fit5032Assignment-20221029095202];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] nvarchar(128)  NOT NULL,
    [RoleId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'PatientSet'
CREATE TABLE [dbo].[PatientSet] (
    [Patient_id] nvarchar(max)  NOT NULL,
    [F_name] nvarchar(max)  NOT NULL,
    [L_name] nvarchar(max)  NOT NULL,
    [PhoneNumber] nchar(10)  NOT NULL,
    [Birthday] datetime  NOT NULL
);
GO

-- Creating table 'DoctorSet'
CREATE TABLE [dbo].[DoctorSet] (
    [Doctor_Id] nvarchar(max)  NOT NULL,
    [F_name] nvarchar(max)  NOT NULL,
    [L_name] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AppointmentSet'
CREATE TABLE [dbo].[AppointmentSet] (
    [Appointment_Id] int IDENTITY(1,1) NOT NULL,
    [Appointment_date] datetime  NOT NULL,
    [Appointment_time] time  NOT NULL,
    [PatientPatient_id] nvarchar(max)  NOT NULL,
    [DoctorDoctor_Id] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId], [RoleId] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([UserId], [RoleId] ASC);
GO

-- Creating primary key on [Patient_id] in table 'PatientSet'
ALTER TABLE [dbo].[PatientSet]
ADD CONSTRAINT [PK_PatientSet]
    PRIMARY KEY CLUSTERED ([Patient_id] ASC);
GO

-- Creating primary key on [Doctor_Id] in table 'DoctorSet'
ALTER TABLE [dbo].[DoctorSet]
ADD CONSTRAINT [PK_DoctorSet]
    PRIMARY KEY CLUSTERED ([Doctor_Id] ASC);
GO

-- Creating primary key on [Appointment_Id] in table 'AppointmentSet'
ALTER TABLE [dbo].[AppointmentSet]
ADD CONSTRAINT [PK_AppointmentSet]
    PRIMARY KEY CLUSTERED ([Appointment_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PatientPatient_id] in table 'AppointmentSet'
ALTER TABLE [dbo].[AppointmentSet]
ADD CONSTRAINT [FK_PatientAppointment]
    FOREIGN KEY ([PatientPatient_id])
    REFERENCES [dbo].[PatientSet]
        ([Patient_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientAppointment'
CREATE INDEX [IX_FK_PatientAppointment]
ON [dbo].[AppointmentSet]
    ([PatientPatient_id]);
GO

-- Creating foreign key on [DoctorDoctor_Id] in table 'AppointmentSet'
ALTER TABLE [dbo].[AppointmentSet]
ADD CONSTRAINT [FK_DoctorAppointment]
    FOREIGN KEY ([DoctorDoctor_Id])
    REFERENCES [dbo].[DoctorSet]
        ([Doctor_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorAppointment'
CREATE INDEX [IX_FK_DoctorAppointment]
ON [dbo].[AppointmentSet]
    ([DoctorDoctor_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------