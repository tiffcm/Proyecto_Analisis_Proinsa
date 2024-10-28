ALTER TRIGGER [dbo].[trg_AfterInsert_AspNetUsers]
ON [dbo].[AspNetUsers]
AFTER INSERT
AS
BEGIN
    BEGIN TRY
      
        IF NOT EXISTS (
            SELECT 1
            FROM CORREO C
            INNER JOIN INSERTED I ON C.CORREO = I.Email
        )
        BEGIN
          
            INSERT INTO CORREO (CORREO)
            SELECT DISTINCT Email 
            FROM INSERTED;	
        END
        
        DECLARE @ImagenPorDefecto VARBINARY(MAX);
        SET @ImagenPorDefecto = 0x89504E470D0A1A0A0000000D4948445200000001000000010806000000FF3F61050000000A49444154789C63600000000200010001005A2DBA280000000049454E44AE426082; -- Imagen PNG 1x1 pixel

        
        INSERT INTO EMPLEADO (FECHA_INGRESO, FOTO, TIPO_FOTO, SALARIO, SALDO_VACACIONES, CORREO_ID, HORARIOLABORAL_ID, ESTADO, AspNetUsers_ID)
        SELECT GETDATE(), @ImagenPorDefecto, 'image/png', 0.00, 083, C.ID_CORREO, 1, 1, ASP.Id
        FROM CORREO C
        INNER JOIN INSERTED I ON C.CORREO = I.Email
        INNER JOIN [AspNetUsers] ASP ON ASP.Email = I.Email;

        
        INSERT INTO AspNetUserRoles (UserId, RoleId)
        SELECT I.Id, 1
        FROM INSERTED I;

      
        DECLARE @EmpleadoID BIGINT;
        SELECT @EmpleadoID = E.ID_EMPLEADO
        FROM EMPLEADO E
        INNER JOIN INSERTED I ON E.AspNetUsers_ID = I.Id;

       
        DECLARE @DireccionID BIGINT;
        INSERT INTO DIRRECCION (DIRRECION) VALUES ('DEFINIR DIRECCION');  
        SET @DireccionID = SCOPE_IDENTITY();

       
        INSERT INTO EMPLEADODIRRECCION (EMPLEADO_ID, DIRRECION_ID) VALUES (@EmpleadoID, @DireccionID);

        
        DECLARE @Telefono1ID BIGINT;
        DECLARE @Telefono2ID BIGINT;

        INSERT INTO TELEFONO (TELEFONO) VALUES ('000 000 000');  
        SET @Telefono1ID = SCOPE_IDENTITY();

        INSERT INTO TELEFONO (TELEFONO) VALUES ('000 000 000');  
        SET @Telefono2ID = SCOPE_IDENTITY();

       
        INSERT INTO EMPLEADOTELEFONO (EMPLEADO_ID, TELEFONO_ID) VALUES (@EmpleadoID, @Telefono1ID);
        INSERT INTO EMPLEADOTELEFONO (EMPLEADO_ID, TELEFONO_ID) VALUES (@EmpleadoID, @Telefono2ID);

    END TRY
    BEGIN CATCH
      
        INSERT INTO [dbo].[DB_ERRORES]
               ([UserName]
               ,[ErrorNumber]
               ,[ErrorState]
               ,[ErrorSeverity]
               ,[ErrorLine]
               ,[ErrorProcedure]
               ,[ErrorMessage]
               ,[ErrorDateTime])
        VALUES
        (SUSER_SNAME(),
        ERROR_NUMBER(),
        ERROR_STATE(),
        ERROR_SEVERITY(),
        ERROR_LINE(),
        ERROR_PROCEDURE(),
        ERROR_MESSAGE(),
        GETDATE());
    END CATCH
END;
