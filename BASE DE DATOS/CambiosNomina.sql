create PROCEDURE [dbo].[EliminarDeduccionEmpleado] 
    @ID_DEDUCCION_NOMINADETALLE BIGINT
AS
BEGIN
    
    DECLARE @tipo_deduccion BIGINT;
	DECLARE @FECHA DATETIME
	SET @FECHA=GETDATE()
  
    

   
    BEGIN TRANSACTION;

    BEGIN TRY
       
        SELECT 
            @tipo_deduccion = DEDUCCION_ID
           
           
        FROM DEDUCCIONNOMINADETALLE 
        WHERE ID_DEDUCCIONNOMINADETALLE = @ID_DEDUCCION_NOMINADETALLE;

        
        IF @tipo_deduccion <> 1 or  @tipo_deduccion <> 2
       
        BEGIN
           
            DELETE FROM DEDUCCIONNOMINADETALLE 
            WHERE ID_DEDUCCIONNOMINADETALLE = @ID_DEDUCCION_NOMINADETALLE;

           
          

           
        END

		exec [dbo].[CalculoNominaFinal] @FECHA
        

        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
       
        ROLLBACK TRANSACTION;

       
        THROW;
    END CATCH
END;
go

create PROCEDURE [dbo].[EliminarIngresoEmpleado] 
    @ID_INGRESONOMINADETALLE BIGINT
AS
BEGIN
    
    DECLARE @tipo_ingreso BIGINT;
    DECLARE @cantidad INT;
    DECLARE @NOMINADETALLE_ID BIGINT;
	DECLARE @FECHA DATETIME
	SET @FECHA=GETDATE()

   
    BEGIN TRANSACTION;

    BEGIN TRY
       
        SELECT 
            @tipo_ingreso = INGRESO_ID,
            @cantidad = ISNULL(CANTIDAD,0),
            @NOMINADETALLE_ID = NOMINADETALLE_ID
        FROM INGRESONOMINADETALLE 
        WHERE ID_INGRESONOMINADETALLE = @ID_INGRESONOMINADETALLE;

        
        IF @tipo_ingreso <> 1
        BEGIN
           
            DELETE FROM INGRESONOMINADETALLE 
            WHERE ID_INGRESONOMINADETALLE = @ID_INGRESONOMINADETALLE;

           
            IF @tipo_ingreso = 2
            BEGIN
                UPDATE NOMINADETALLE 
                SET TOTAL_HORAS_EXTRA = TOTAL_HORAS_EXTRA - @cantidad
                WHERE ID_NOMINADETALLE = @NOMINADETALLE_ID;
            END

            IF @tipo_ingreso = 3
            BEGIN
                UPDATE NOMINADETALLE 
                SET TOTAL_DIAS_EXTRA = TOTAL_DIAS_EXTRA - @cantidad
                WHERE ID_NOMINADETALLE = @NOMINADETALLE_ID;
            END
        END
        
		EXEC [dbo].[CalculoNominaFinal] @Fecha = @FECHA

        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
       
        ROLLBACK TRANSACTION;

       
        THROW;
    END CATCH
END;
go
create PROCEDURE [dbo].[ActualizarDeduccionEmpleado]
    @ID_DEDUCCIONNOMINADETALLE BIGINT,
    @MONTO DECIMAL(10,2),
    @DETALLE VARCHAR(100),
    @DEDUCCION_ID BIGINT
AS
BEGIN
    DECLARE @FECHA DATETIME;
    SET @FECHA = GETDATE();


    UPDATE DEDUCCIONNOMINADETALLE
    SET 
        MONTO = @MONTO,
        DETALLE = @DETALLE,
        DEDUCCION_ID = @DEDUCCION_ID
    WHERE 
        ID_DEDUCCIONNOMINADETALLE = @ID_DEDUCCIONNOMINADETALLE
        AND (DEDUCCION_ID <> 1 AND DEDUCCION_ID <> 2);

    
    EXEC [dbo].[CalculoNominaFinal] @FECHA;
END;
go

create PROCEDURE [dbo].[ActualizarIngresoEmpleado] 
    @ID_INGRESONOMINADETALLE BIGINT,
    @MONTO DECIMAL(10,2),
    @DETALLE VARCHAR(100),
    @CANTIDAD INT,
    @INGRESO_ID BIGINT
AS
BEGIN

DECLARE @FECHA DATETIME
	SET @FECHA=GETDATE()
   
    UPDATE INGRESONOMINADETALLE
    SET 
        MONTO = @MONTO,
        DETALLE = @DETALLE,
        CANTIDAD = @CANTIDAD,
        INGRESO_ID = @INGRESO_ID
    WHERE 
        ID_INGRESONOMINADETALLE = @ID_INGRESONOMINADETALLE
        AND INGRESO_ID <> 1;  

		exec [dbo].[CalculoNominaFinal] @FECHA


END;
