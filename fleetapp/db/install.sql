DROP TABLE Project;

CREATE TABLE Project (
   id INT IDENTITY(1,1) PRIMARY KEY,
   Name VARCHAR(50) NOT NULL,
   Description VARCHAR(400),
   CreatedDate	DATETIME,
   ModifiedDate DATETIME,
   UNIQUE (Name)
);
DROP TABLE Scenario; 

CREATE TABLE Scenario (
   id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectID INT NOT NULL,
   Name VARCHAR(50),
   StartYear INT,
   TimePeriod INT,
   unique (ProjectID, Name)
);

DROP TABLE Fleet; 

CREATE TABLE Fleet (
   id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectID INT NOT NULL,
   FleetID VARCHAR(50) NOT NULL,
   AssetType VARCHAR(50),
   AssetModel VARCHAR(50),
   unique (ProjectID, FleetID)
);

DROP TABLE Hub; 

CREATE TABLE Hub (
   id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectID INT NOT NULL,
   Name VARCHAR(50),
   unique (ProjectID, Name)
);


DROP TABLE HubAllocation; 

CREATE TABLE HubAllocation (
   id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectID INT NOT NULL,
   HubID INT NOT NULL,
   AssetModel VARCHAR(50),
   IsManned BIT,
   IsAHS BIT,
   unique (ProjectID, HubID, AssetModel)
);


DROP TABLE TruckPayload; 

CREATE TABLE TruckPayload (
   id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioID INT NOT NULL,
   AssetModel VARCHAR(50),
   MaterialType VARCHAR(10),
   Payload INT NOT NULL,
   unique (ScenarioID, AssetModel, MaterialType)
);

DROP TABLE TruckGroup; 

CREATE TABLE TruckGroup (
   id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioID INT NOT NULL,
   Name VARCHAR(50),
   AssetModel VARCHAR(50),
   FleetID VARCHAR(50) NOT NULL,
   unique (ScenarioID,Name, AssetModel, FleetID)
);

DROP TABLE TruckHours; 

CREATE TABLE TruckHours (
   id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioID INT NOT NULL,
   AssetModel VARCHAR(50),
   GroupName VARCHAR(50),
   Hub VARCHAR(50),
   Mode VARCHAR(50),
   unique (ScenarioID,AssetModel, GroupName, Hub, Mode)
);

DROP TABLE TruckHourYearMapping; 

CREATE TABLE TruckHourYearMapping (
   TruckHourID INT,
   Year INT,
   Value NUMERIC,
   unique (TruckHoursID,Year)
);

DROP TABLE MinePlan; 

CREATE TABLE MinePlan (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioID INT NOT NULL,
   Hub VARCHAR(50),
   Physical VARCHAR(50),
   unique (ScenarioID, Hub, Physical)
);

DROP TABLE MinePlanYearMapping; 

CREATE TABLE MinePlanYearMapping (
   MinePlanID INT,
   Year INT,
   Value NUMERIC,
   unique (MinePlanID,Year)
);

DROP TABLE TruckTypeMinePlan; 

CREATE TABLE TruckTypeMinePlan (
   Id INT IDENTITY(1,1) PRIMARY KEY,
   ScenarioID INT NOT NULL,
   Hub VARCHAR(50),
   Physical VARCHAR(50),
   TruckType VARCHAR(50),
   MinePlanPayload INT,
   unique (ScenarioID, Hub, Physical, TruckType)
);

DROP TABLE TruckTypeMinePlanYearMapping; 

CREATE TABLE TruckTypeMinePlanYearMapping (
   TruckTypeMinePlanID INT,
   Year INT,
   Value NUMERIC,
   unique (TruckTypeMinePlanID,Year)
);

