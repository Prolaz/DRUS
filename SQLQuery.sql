CREATE TABLE MEASUREMENTS(
       ID   INT IDENTITY(1,1) NOT NULL,
	   VALUE DECIMAL		 NOT NULL,
       STATION_ID NVARCHAR(20)		 NOT NULL,
       TYPE CHAR (20)        NOT NULL, 
	   TIME DATETIME		 NOT NULL,      
       PRIMARY KEY (ID)
);

CREATE TABLE STATIONS(
       ID   NVARCHAR(20)     NOT NULL,
	   NAME NVARCHAR(20)	 NOT NULL,
       LOCATION_ID INT		 NOT NULL,      
       PRIMARY KEY (ID)
);

CREATE TABLE LOCATIONS(
       ID   INT  IDENTITY(1,1) NOT NULL,
	   NAME NVARCHAR(20)	   NOT NULL,       
       PRIMARY KEY (ID)
);

ALTER TABLE STATIONS 
	ADD FOREIGN KEY (LOCATION_ID) REFERENCES LOCATIONS(ID);

ALTER TABLE MEASUREMENTS 
	ADD FOREIGN KEY (STATION_ID) REFERENCES STATIONS(ID);

CREATE UNIQUE INDEX StationNameIndex
on STATIONS (NAME);

CREATE UNIQUE INDEX LocationNameIndex
on LOCATIONS (NAME);

INSERT INTO LOCATIONS (NAME)
	VALUES ('Novi Sad'), ('Beograd'), ('Nis'), ('Zrenjanin'), ('Kragujevac');

