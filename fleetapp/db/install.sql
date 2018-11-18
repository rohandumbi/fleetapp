DROP TABLE Project;

CREATE TABLE Project (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   Name VARCHAR(50) NOT NULL,
   Description VARCHAR(400),
   CreatedDate	DATETIME,
   ModifiedDate DATETIME,
   UNIQUE (Name)
);
DROP TABLE Scenario; 

CREATE TABLE Scenario (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectId INT NOT NULL,
   Name VARCHAR(50),
   StartYear INT,
   TimePeriod INT,
   unique (ProjectId, Name)
);

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
   unique (ProjectId, FleetId)
);

DROP TABLE Hub; 

CREATE TABLE Hub (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectId INT NOT NULL,
   Name VARCHAR(50),
   unique (ProjectId, Name)
);


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


DROP TABLE TruckPayload; 

CREATE TABLE TruckPayload (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioId INT NOT NULL,
   AssetModel VARCHAR(50),
   MaterialType VARCHAR(10),
   Payload INT NOT NULL,
   unique (ScenarioId, AssetModel, MaterialType)
);

DROP TABLE TruckGroup; 

CREATE TABLE TruckGroup (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioId INT NOT NULL,
   Name VARCHAR(50),
   AssetModel VARCHAR(50),
   FleetId VARCHAR(50) NOT NULL,
   unique (ScenarioId, Name, AssetModel, FleetId)
);

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

DROP TABLE TruckHourYearMapping; 

CREATE TABLE TruckHourYearMapping (
   TruckHourId INT,
   Year INT,
   Value NUMERIC,
   unique (TruckHourId,Year)
);

DROP TABLE MinePlan; 

CREATE TABLE MinePlan (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioId INT NOT NULL,
   Hub VARCHAR(50),
   Physical VARCHAR(50),
   unique (ScenarioId, Hub, Physical)
);

DROP TABLE MinePlanYearMapping; 

CREATE TABLE MinePlanYearMapping (
   MinePlanId INT,
   Year INT,
   Value NUMERIC,
   unique (MinePlanId, Year)
);

DROP TABLE TruckTypeMinePlan; 

CREATE TABLE TruckTypeMinePlan (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioId INT NOT NULL,
   Hub VARCHAR(50),
   Physical VARCHAR(50),
   TruckType VARCHAR(50),
   MinePlanPayload INT,
   unique (ScenarioId, Hub, Physical, TruckType)
);

DROP TABLE TruckTypeMinePlanYearMapping; 

CREATE TABLE TruckTypeMinePlanYearMapping (
   TruckTypeMinePlanId INT,
   Year INT,
   Value NUMERIC,
   unique (TruckTypeMinePlanId,Year)
);

