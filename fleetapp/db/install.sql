DROP TABLE IF EXISTS Project;

CREATE TABLE Project (
   id INT IDENTITY(1,1) PRIMARY KEY,
   Name VARCHAR(50) NOT NULL,
   Description VARCHAR(400),
   CreatedDate	DATETIME,
   ModifiedDate DATETIME,
   UNIQUE (Name)
);

DROP TABLE IF EXISTS ProjectDataFile;

CREATE TABLE ProjectDataFile (
   ProjectID INT NOT NULL,
   FileName VARCHAR(200) NOT NULL,
   UNIQUE ( ProjectID, FileName )
);

DROP TABLE IF EXISTS Scenario; 

CREATE TABLE Scenario (
   id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectID INT NOT NULL,
   Name VARCHAR(50),
   StartYear INT,
   TimePeriod INT,
   unique (ProjectID, Name)
);

DROP TABLE IF EXISTS Fleet; 

CREATE TABLE Fleet (
   id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectID INT NOT NULL,
   FleetID VARCHAR(50) NOT NULL,
   AssestType VARCHAR(50),
   AssetModel VARCHAR(50),
   unique (ProjectID, FleetID)
);

DROP TABLE IF EXISTS Hub; 

CREATE TABLE Hub (
   id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectID INT NOT NULL,
   Name VARCHAR(50),
   unique (ProjectID, Name)
);


DROP TABLE IF EXISTS HubAllocation; 

CREATE TABLE HubAllocation (
   id INT IDENTITY(1,1) PRIMARY KEY,
   ProjectID INT NOT NULL,
   HubID INT NOT NULL,
   AssetModel VARCHAR(50),
   unique (ProjectID, HubID, AssetModel)
);