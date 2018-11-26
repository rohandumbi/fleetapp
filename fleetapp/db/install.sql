IF OBJECT_ID('Project', 'U') IS NOT NULL 
DROP TABLE Project;

CREATE TABLE Project (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   Name VARCHAR(50) NOT NULL,
   Description VARCHAR(400),
   CreatedDate	DATETIME,
   ModifiedDate DATETIME,
   UNIQUE (Name)
);

IF OBJECT_ID('Scenario', 'U') IS NOT NULL 
DROP TABLE Scenario; 

CREATE TABLE Scenario (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectId INT NOT NULL,
   Name VARCHAR(50),
   StartYear INT,
   TimePeriod INT,
   unique (ProjectId, Name)
);

IF OBJECT_ID('Fleet', 'U') IS NOT NULL 
DROP TABLE Fleet; 

CREATE TABLE Fleet (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectId INT NOT NULL,
   AssetNumber INT NOT NULL,
   FleetId VARCHAR(50) NOT NULL,
   AssetType VARCHAR(50),
   AssetModel VARCHAR(50),
   InitialAge INT,
   FinalAge INT,
   Priority INT,
   unique (ProjectId, FleetId)
);

IF OBJECT_ID('Hub', 'U') IS NOT NULL
DROP TABLE Hub; 

CREATE TABLE Hub (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectId INT NOT NULL,
   Name VARCHAR(50),
   unique (ProjectId, Name)
);

IF OBJECT_ID('TruckHubPriority', 'U') IS NOT NULL
DROP TABLE TruckHubPriority; 

CREATE TABLE TruckHubPriority (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectId INT NOT NULL,
   AssetModel VARCHAR(50),
   Hub VARCHAR(50),
   Priority INT,
   unique (ProjectId, AssetModel, Hub)
);

IF OBJECT_ID('HubAllocation', 'U') IS NOT NULL
DROP TABLE HubAllocation; 

CREATE TABLE HubAllocation (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectId INT NOT NULL,
   HubId INT NOT NULL,
   AssetModel VARCHAR(50),
   IsManned BIT,
   IsAHS BIT,
   unique (ProjectId, HubId, AssetModel)
);

IF OBJECT_ID('TruckPayload', 'U') IS NOT NULL
DROP TABLE TruckPayload; 

CREATE TABLE TruckPayload (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioId INT NOT NULL,
   AssetModel VARCHAR(50),
   MaterialType VARCHAR(10),
   Payload INT NOT NULL,
   unique (ScenarioId, AssetModel, MaterialType)
);

IF OBJECT_ID('TruckGroup', 'U') IS NOT NULL
DROP TABLE TruckGroup; 

CREATE TABLE TruckGroup (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioId INT NOT NULL,
   Name VARCHAR(50),
   AssetModel VARCHAR(50),
   FleetId VARCHAR(50) NOT NULL,
   unique (ScenarioId, Name, AssetModel, FleetId)
);

IF OBJECT_ID('MachineParameter', 'U') IS NOT NULL
DROP TABLE MachineParameter; 

CREATE TABLE MachineParameter (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioId INT NOT NULL,
   AssetModel VARCHAR(50),
   Hub VARCHAR(50),
   Mode VARCHAR(50),
   unique (ScenarioId,AssetModel, Hub, Mode)
);

IF OBJECT_ID('MachineParameterYearMapping', 'U') IS NOT NULL
DROP TABLE MachineParameterYearMapping; 

CREATE TABLE MachineParameterYearMapping (
   MachineParameterId INT,
   Year INT,
   StartDate DATETIME,
   Days INT,
   SchEU DECIMAL(18, 10),
   Npot  DECIMAL(18, 10),
   UtEu  DECIMAL(18, 10),
   Payload INT,
   EngineHours  DECIMAL(18, 10),
   UsableHours  DECIMAL(18, 10),
   unique (MachineParameterId,Year)
);

IF OBJECT_ID('TruckHour', 'U') IS NOT NULL
DROP TABLE TruckHour; 

CREATE TABLE TruckHour (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioId INT NOT NULL,
   AssetModel VARCHAR(50),
   GroupName VARCHAR(50),
   Hub VARCHAR(50),
   Mode VARCHAR(50),
   unique (ScenarioId,AssetModel, GroupName, Hub, Mode)
);

IF OBJECT_ID('TruckHourYearMapping', 'U') IS NOT NULL
DROP TABLE TruckHourYearMapping; 

CREATE TABLE TruckHourYearMapping (
   TruckHourId INT,
   Year INT,
   Value NUMERIC,
   unique (TruckHourId,Year)
);

IF OBJECT_ID('MinePlan', 'U') IS NOT NULL
DROP TABLE MinePlan; 

CREATE TABLE MinePlan (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioId INT NOT NULL,
   Hub VARCHAR(50),
   Physical VARCHAR(50),
   unique (ScenarioId, Hub, Physical)
);

IF OBJECT_ID('MinePlanYearMapping', 'U') IS NOT NULL
DROP TABLE MinePlanYearMapping; 

CREATE TABLE MinePlanYearMapping (
   MinePlanId INT,
   Year INT,
   Value NUMERIC,
   unique (MinePlanId, Year)
);

IF OBJECT_ID('TruckTypeMinePlan', 'U') IS NOT NULL
DROP TABLE TruckTypeMinePlan; 

CREATE TABLE TruckTypeMinePlan (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioId INT NOT NULL,
   Hub VARCHAR(50),
   TruckType VARCHAR(50),
   MinePlanPayload INT,
   unique (ScenarioId, Hub, TruckType)
);

IF OBJECT_ID('TruckTypeMinePlanYearMapping', 'U') IS NOT NULL
DROP TABLE TruckTypeMinePlanYearMapping; 

CREATE TABLE TruckTypeMinePlanYearMapping (
   TruckTypeMinePlanId INT,
   Year INT,
   Value DECIMAL(18,10),
   unique (TruckTypeMinePlanId,Year)
);