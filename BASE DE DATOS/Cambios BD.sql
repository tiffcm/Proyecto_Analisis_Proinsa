


ALTER   PROCEDURE [dbo].[ConsultarDatosEmpleado]  
@CORREO NVARCHAR(255)
AS
BEGIN
    DECLARE @NOMBREROL NVARCHAR(300);
    DECLARE @IDROL     INT;

    -- Obtener el rol del empleado
    SELECT @NOMBREROL = r.[Name], @IDROL = r.Id
    FROM [dbo].[AspNetUsers] u
    JOIN [dbo].[AspNetUserRoles] ur ON u.[Id] = ur.[UserId]
    JOIN [dbo].[AspNetRoles] r ON ur.[RoleId] = r.[Id]
    WHERE u.[Email] = @CORREO;

    -- Consultar los detalles del empleado
    SELECT e.ID_EMPLEADO , ISNULL(E.[IDENTIFICACION], '') AS IDENTIFICACION, ISNULL( E.[NOMBRECOMPLETO], '')  AS NOMBRECOMPLETO , E.[FECHA_INGRESO],
           E.[FOTO], E.[TIPO_FOTO], E.[SALARIO], E.[SALDO_VACACIONES], ISNULL(CA.NOMBRE_CARGO , '')AS CARGO, C.CORREO,
           E.[HORARIOLABORAL_ID], ISNULL(D.NOMBRE_DEPARTAMENTO,'') AS DEPARTAMENTO, T.TELEFONO, ISNULL(DI.DIRRECION,'') AS DIRECCION,
           @NOMBREROL AS NOMBREROL, @IDROL AS IDROL
    FROM [dbo].[EMPLEADO] E
    left JOIN CORREO C ON C.ID_CORREO = E.CORREO_ID
    left JOIN DEPARTAMENTO D ON E.DEPARTAMENTO_ID = D.ID_DEPARTAMENTO
    left JOIN CARGO CA ON E.CARGO_ID = CA.ID_CARGO
    left JOIN EMPLEADOTELEFONO ET ON ET.EMPLEADO_ID = E.ID_EMPLEADO
    left JOIN TELEFONO T ON T.ID_TELEFONO = ET.TELEFONO_ID
    left JOIN EMPLEADODIRRECCION ED ON ED.EMPLEADO_ID = E.ID_EMPLEADO
    left JOIN DIRRECCION DI ON DI.ID_DIRECCION = ED.DIRRECION_ID    
    WHERE C.CORREO = @CORREO;
END

go

create TRIGGER [dbo].[trg_AfterInsert_AspNetUsers]
ON [dbo].[AspNetUsers]
AFTER INSERT
AS
BEGIN
    BEGIN TRY
        -- Validar si el correo existe
        IF NOT EXISTS (
            SELECT 1
            FROM CORREO C
            INNER JOIN INSERTED I ON C.CORREO = I.Email
        )
        BEGIN
            -- Insertar el correo en la tabla CORREO solo si no existe
            INSERT INTO CORREO (CORREO)
            SELECT DISTINCT Email 
            FROM INSERTED;
        END
        
        
        DECLARE @ImagenPorDefecto VARBINARY(MAX);
        SET @ImagenPorDefecto = 0x89504E470D0A1A0A0000000D4948445200000001000000010806000000FF3F61050000000A49444154789C63600000000200010001005A2DBA280000000049454E44AE426082; -- Imagen PNG 1x1 pixel

        -- Insertar en la tabla EMPLEADO con el CORREO_ID correspondiente al correo.
        INSERT INTO EMPLEADO (FECHA_INGRESO, FOTO, TIPO_FOTO, SALARIO,SALDO_VACACIONES, CORREO_ID, HORARIOLABORAL_ID, ESTADO, AspNetUsers_ID)
        SELECT GETDATE(), @ImagenPorDefecto, 'image/png', 0.00, 083, C.ID_CORREO, 1, 1, ASP.Id
        FROM CORREO C
        INNER JOIN INSERTED I ON C.CORREO = I.Email
        INNER JOIN [AspNetUsers] ASP ON ASP.Email = I.Email;

        -- Insertar en la tabla AspNetUserRoles con el ROL 1 (Empleado)
        INSERT INTO AspNetUserRoles (UserId, RoleId)
        SELECT I.Id, 1
        FROM INSERTED I;

        -- Insertar los teléfonos
        DECLARE @Telefono1ID BIGINT;
        DECLARE @Telefono2ID BIGINT;
        DECLARE @EmpleadoID BIGINT;

        -- Obtener el ID del nuevo empleado
        SELECT @EmpleadoID = E.ID_EMPLEADO
        FROM EMPLEADO E
        INNER JOIN INSERTED I ON E.AspNetUsers_ID = I.Id;

        -- Insertar teléfonos
        INSERT INTO TELEFONO (TELEFONO) VALUES ('000 000 000');
        SET @Telefono1ID = SCOPE_IDENTITY();

      

        -- Relacionar teléfonos con empleado
        INSERT INTO EMPLEADOTELEFONO (EMPLEADO_ID, TELEFONO_ID) VALUES (@EmpleadoID, @Telefono1ID);
       

    END TRY
    BEGIN CATCH
        -- Manejo de errores
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

