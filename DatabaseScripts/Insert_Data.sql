INSERT INTO [dbo].[Tracks]
           ([Complexity])
     VALUES('11110011100011001110011101'),('11111111001111110111100001','11001')
GO


INSERT INTO [dbo].[CarType]
           ([Description])
     VALUES ('CORV'), ('GTR'),('SUV')
GO


INSERT INTO [dbo].[VehicleAttributes]
           ([VehicleTypeId]
           ,[Accelaration]
           ,[Breaking]
           ,[Cornering]
           ,[TopSpeed])
     VALUES(1,8,3,4 ,9),(2,6,7,9,8),(3,9,9,4,8)
GO



