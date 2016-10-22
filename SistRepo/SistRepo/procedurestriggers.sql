CREATE trigger [dbo].[t_Asignacion]
on [dbo].[reposiciones]
for update
as
BEGIN
IF  ( (select idEstado from inserted) = 2 )
	BEGIN
	update reposiciones
	set horaAsignacion = GETDATE()
	where reposiciones.idReposicion = (select idReposicion from inserted )
	END
IF ( (select idEstado from inserted) = 3 )
	BEGIN
	update reposiciones
	set horaResolucion = GETDATE()
	where reposiciones.idReposicion = (select idReposicion from inserted )
	END
END
GO

CREATE trigger [dbo].[t_Cancelacion]
on [dbo].[reposiciones]
for delete
as
DECLARE @id int
SELECT @id = idReposicion from deleted
DECLARE @idS int
SELECT @idS = idSolicitante from deleted
DECLARE @hc datetime
SELECT @hc = horaCreacion from deleted
DECLARE @idH int
SELECT @idH = idHerramienta from deleted
DECLARE @idP int
SELECT @idP = idSolicitante from deleted
BEGIN
	insert into canceladas(idReposicion,idSolicitante,horaCreacion,horaCancelacion,idHerramienta,idPuesto) values(@id,@idS,@hc,GETDATE(),@idH,@idP)
END
GO

CREATE trigger [dbo].[t_Creacion]
on [dbo].[reposiciones]
for insert
as
BEGIN
	update reposiciones
	set horaCreacion = GETDATE()
	where reposiciones.idReposicion = (select idReposicion from inserted )
END
GO

CREATE trigger [dbo].[t_stock]
on [dbo].[reposiciones]
for update
as
BEGIN
IF ( (select idEstado from inserted) = 3 )
	BEGIN
	update herramientas
	set stock = stock - 1
	where herramientas.idHerramienta = (select idHerramienta from inserted )
	END
END
GO