USE AutoWorksDb 
GO 



CREATE TABLE VehicleModels (
	ModelId INT IDENTITY(1,1) PRIMARY KEY, 
	ManufacturerId INT NOT NULL, 
	ModelName NVARCHAR(100) NOT NULL, 
	Segment NVARCHAR(50) NULL,	-- e.g.: SUV/Sedan/Hatch etc. 
	CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(), 
	CONSTRAINT FK_VehicleModels_Manufacturer
		FOREIGN KEY (ManufacturerId) REFERENCES Manufacturers(ManufacturerId)
);