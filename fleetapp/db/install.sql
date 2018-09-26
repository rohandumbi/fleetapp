DROP TABLE IF EXISTS project;

CREATE TABLE project (
   id INT NOT NULL AUTO_INCREMENT,
   name VARCHAR(100) NOT NULL,
   description VARCHAR(400),
   created_date	DATETIME,
   modified_date DATETIME,
   PRIMARY KEY ( id ),
   UNIQUE (name)
);

DROP TABLE IF EXISTS project_data_files;

CREATE TABLE project_data_files (
   project_id INT NOT NULL,
   file_name VARCHAR(200) NOT NULL,
   UNIQUE ( project_id, file_name )
);

DROP TABLE IF EXISTS scenario; 

CREATE TABLE scenario (
   id INT NOT NULL AUTO_INCREMENT,
   project_id INT NOT NULL,
   name VARCHAR(100),
   start_year INT,
   time_period INT,
   PRIMARY KEY ( id ),
   unique (project_id, name)
);

DROP TABLE IF EXISTS fleet; 

CREATE TABLE fleet (
   id INT NOT NULL AUTO_INCREMENT,
   project_id INT NOT NULL,
   fleet_id VARCHAR(100) NOT NULL,
   asset_type VARCHAR(100),
   asset_model VARCHAR(100),
   PRIMARY KEY ( id ),
   unique (project_id, fleet_id)
);

DROP TABLE IF EXISTS hub; 

CREATE TABLE hub (
   id INT NOT NULL AUTO_INCREMENT,
   project_id INT NOT NULL,
   name VARCHAR(100),
   PRIMARY KEY ( id ),
   unique (project_id, name)
);