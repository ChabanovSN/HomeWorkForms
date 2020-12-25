  use master;
  IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Scheduler')
    BEGIN
    CREATE DATABASE [Scheduler];
    END
   
  
       IF  EXISTS(SELECT * FROM sys.databases WHERE name = 'Scheduler')
    BEGIN
       USE [Scheduler]; 

          IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TaskList' and xtype='U')
            BEGIN
     CREATE TABLE TaskList (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        [Name] VARCHAR(100) NOT NULL,
        [Date] DATE NOT NULL
     )
             END
    END