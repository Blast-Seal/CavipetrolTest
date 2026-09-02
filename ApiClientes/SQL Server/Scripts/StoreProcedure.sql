-- =============================================
-- Author:		'Luis Calvache'
-- Create date: '01-09-2026 20:28:00'
-- Description:	Procedimiento almacenado encargado de devolver la informacion de un cliente por coincidencia con el parametro identificacion
-- =============================================
CREATE PROCEDURE sp_GetClientByIdentification
	-- Add the parameters for the stored procedure here
	@Identificacion VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, identificacion, nombre, apellido, email from Clientes WHERE identificacion = @Identificacion;
END
GO
