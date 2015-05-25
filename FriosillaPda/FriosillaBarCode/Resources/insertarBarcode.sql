USE [FRIOSILLA]
GO
/****** Object:  StoredProcedure [dbo].[InsertarBarCode]    Script Date: 12/29/2014 14:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sami Racho
-- Create date: 30/04/2014
-- Ultima modificación: 21/05/2014
-- Description:	Procedimiento Para insertar códigos de barras en la base de datos de friosilla
-- =============================================
ALTER PROCEDURE [dbo].[InsertarBarCode] 
	-- Add the parameters for the stored procedure here
	@COD_PAL nchar(10), 
	@PAL3_LOT nvarchar(15),
	@PAL3_PES float,
	@PAL3_FCA smalldatetime,
	@PAL3_SRE nvarchar(30),
	@PAL3_UNI float,
	@PAL3_OT2 nvarchar(15),
	@PAL2_CB nvarchar(1024),
	@ULTIMO_LOTE bit,
    @PRIMER_LOTE bit,
	@RESULTADO tinyint OUTPUT
AS

-- DEVUELVE
-- 1 OK, inserción correcta
-- 2 ERROR, la caja ya existe
-- 3 ERROR, el palet no existe
-- 4 ERROR, el lote ya existe
-- 5 ERROR, la caja no existe

-- COD_PALCOD
DECLARE @COD_PALCOD nchar(13)
SELECT TOP 1 @COD_PALCOD = COD_PALCOD FROM  PALETS2 WHERE COD_PAL  = @COD_PAL AND PAL2_CB  = '' ORDER BY COD_PALCOD

IF @COD_PALCOD IS NULL
BEGIN
	SET @RESULTADO = 5
	RETURN @RESULTADO
END

/*
IF @PRIMER_LOTE = 'true'
BEGIN

	-- Comprobar si la caja existe (si existe salimos del procedimiento y no insertamos nada)
	DECLARE @COD_PEN AS nchar(6)
	DECLARE @COD_PEN2 AS nchar(6)
	DECLARE @COD_PSA AS nchar(6)
	DECLARE @COD_PSA2 AS nchar(6)
	DECLARE @COD_PALSEL AS nchar(10)

	SELECT @COD_PEN = COD_PEN, @COD_PSA = COD_PSA FROM PALETS WHERE COD_PAL = @COD_PAL

	DECLARE @SQLQuery NVARCHAR(500)
	DECLARE @ParmDefinition NVARCHAR(500)

	-- Construimos la consulta dinámica
	SET @SQLQuery = 'SELECT @COD_PALSEL_OUT = COD_PAL FROM PALETS2 WHERE PAL2_CB = @PAL2_CB_IN'
		IF @COD_PEN<>'' SET @SQLQuery =  @SQLQuery+' AND COD_PEN = @COD_PEN_IN';
		IF @COD_PSA<>'' SET @SQLQuery =  @SQLQuery+' AND COD_PSA = @COD_PSA_IN';

	-- Le pasamos los parámetros y la ejecutamos
	SET @ParmDefinition = '@COD_PALSEL_OUT nchar(10) OUTPUT, @PAL2_CB_IN AS nvarchar(1024), @COD_PEN_IN AS nchar(6),@COD_PSA_IN AS nchar(6)'
	EXECUTE sp_executesql @SQLQuery, @ParmDefinition, @PAL2_CB_IN = @PAL2_CB,@COD_PEN_IN = @COD_PEN,@COD_PSA_IN = @COD_PSA, @COD_PALSEL_OUT = @COD_PALSEL OUTPUT

	IF @COD_PALSEL IS NOT NULL
	BEGIN
		SET @RESULTADO = 2
		SELECT  @COD_PEN2 = COD_PEN, @COD_PSA2 = COD_PSA FROM  PALETS WHERE COD_PAL = @COD_PALSEL
		IF  (@COD_PEN<>'' AND @COD_PEN2<>'' AND @COD_PEN = @COD_PEN2)RETURN @RESULTADO
		IF  (@COD_PSA<>'' AND @COD_PSA2<>'' AND @COD_PSA = @COD_PSA2)RETURN @RESULTADO
		
		IF  (@COD_PEN2='' AND @COD_PSA2='')
		BEGIN
			SET @RESULTADO = 3
			RETURN @RESULTADO
		END
	END
	-- Fin comprobación caja existe
END
*/

IF @PRIMER_LOTE = 'true'
BEGIN
	IF (SELECT TOP 1 PALETS.COD_PAL FROM PALETS JOIN PALETS2 ON PALETS.COD_PAL = PALETS2.COD_PAL WHERE PALETS.COD_PSA = '' AND PALETS2.PAL2_CB = @PAL2_CB) IS NOT NULL
	BEGIN
		SET @RESULTADO = 2
		RETURN @RESULTADO
	END
END
	
-- Obtenemos el código de línea
DECLARE @cLinTemp AS int
SET @cLinTemp = CAST( (SELECT TOP 1 COD_LIN FROM PALETS3 WHERE COD_PALCOD = @COD_PALCOD ORDER BY COD_LIN DESC) AS int)

IF @cLinTemp IS NULL
BEGIN
    SET @cLinTemp = 0
END

-- Sumamos 1 y le agregamos los 0 necesarios para que tenga 3 dígitos
DECLARE @COD_LIN as nchar(3)
SET @COD_LIN = REPLACE(STR(@cLinTemp+1,3),' ','0')

-- COD_PALLOT
DECLARE @COD_PALLOT AS nchar(16)
SET @COD_PALLOT = @COD_PALCOD+@COD_LIN

-- PAL3_FAL?
DECLARE @PAL3_FAL AS smalldatetime
set @PAL3_FAL = CONVERT(smalldatetime ,GETDATE())

-- Si no existe lo insertamos
IF NOT EXISTS (SELECT COD_PALLOT FROM PALETS3 WHERE COD_PALLOT = @COD_PALLOT)
BEGIN
	-- Insertamos el lote
	INSERT INTO PALETS3 (COD_PALLOT, COD_PALCOD, COD_LIN, COD_PAL,PAL3_LOT, PAL3_PES, PAL3_FCA, PAL3_SRE,PAL3_UNI, PAL3_OT2, PAL3_REG, PAL3_FAL)
	VALUES (@COD_PALLOT, @COD_PALCOD, @COD_LIN, @COD_PAL, @PAL3_LOT, @PAL3_PES, @PAL3_FCA, @PAL3_SRE, @PAL3_UNI, @PAL3_OT2,' ', @PAL3_FAL);

	IF @ULTIMO_LOTE = 'true'
	BEGIN
		-- Actualizamos el código de la caja
		UPDATE PALETS2 SET PAL2_CB=@PAL2_CB WHERE COD_PALCOD = @COD_PALCOD;
	END
	
	SET @RESULTADO = 1
	RETURN @RESULTADO
END
ELSE
BEGIN
	SET @RESULTADO = 4
	RETURN @RESULTADO
END