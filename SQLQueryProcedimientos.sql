select * from USUARIO

CREATE PROC SP_REGISTARUSUARIOS(
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output,
@Mensaje varchar(500) output
)
as
begin
	set @IdUsuarioResultado = 0
	set @Mensaje = ''

	if not exists(select * from USUARIO where Documento = @Documento)
	begin
		insert into USUARIO(Documento,NombreCompleto,Correo,Clave,IdRol,Estado) values
		(@Documento,@NombreCompleto,@Correo,@Clave,@IdRol,@Estado)

		set @IdUsuarioResultado = SCOPE_IDENTITY()
		
	end
	else
		set @Mensaje = 'No se puede repetir el documento para mas de un usuario'
end

go

CREATE PROC SP_EDITARUSUARIOS(
@IdUsuario int,
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@IdRol int,
@Estado bit,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''

	if not exists(select * from USUARIO where Documento = @Documento and IdUsuario != @IdUsuario)
	begin
		UPDATE USUARIO set
		Documento = @Documento,
		NombreCompleto = @NombreCompleto,
		Correo = @Correo,
		Clave = @Clave,
		IdRol = @IdRol,
		Estado = @Estado
		where IdUsuario = @IdUsuario

		set @Respuesta = 1
		
	end
	else
		set @Mensaje = 'No se puede repetir el documento para mas de un usuario'
end

go

CREATE PROC SP_ELIMINARUSUARIOS(
@IdUsuario int,
@Respuesta bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit = 1


	IF EXISTS(SELECT * FROM COMPRA C
	INNER JOIN USUARIO U ON U.IdUsuario = C.IdUsuario
	WHERE U.IdUsuario = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje  + 'No se puede eliminar porque el usuario se encuentra relacionado a una COMPRA\n'
	END

	IF EXISTS(SELECT * FROM VENTA V
	INNER JOIN USUARIO U ON U.IdUsuario = V.IdUsuario
	WHERE U.IdUsuario = @IdUsuario
	)
	BEGIN
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje  + 'No se puede eliminar porque el usuario se encuentra relacionado a una VENTA\n'
	END


	IF(@pasoreglas = 1)
	BEGIN  
		DELETE FROM USUARIO WHERE IdUsuario = @IdUsuario
		SET @Respuesta = 1
	END


end






declare @respuesta bit
declare @mensaje varchar(500)
exec SP_EDITARUSUARIOS 3,'123','pruebas2','test@gmail.com','456',2,1,@respuesta output,@mensaje output


select @respuesta
select @mensaje


select * from USUARIO







/* ------------------PROCEDIMIENTOS PARA CATEGORIA-------------------------*/
CREATE PROC SP_REGISTRARCATEGORIA(
@Descripcion varchar(50),
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0 
	IF NOT EXISTS(SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion)
	BEGIN 
		INSERT INTO CATEGORIA(Descripcion) VALUES (@Descripcion)
		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'

END


go


CREATE PROC SP_EDITARCATEGORIA(
@IdCategoria int,
@Descripcion varchar(50),
@Resultado bit output,
@Mensaje varchar(500) output

)as
begin
	SET @Resultado = 1
	IF NOT EXISTS(SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion AND IdCategoria != @IdCategoria)
	
		UPDATE CATEGORIA SET
		Descripcion = @Descripcion
		WHERE IdCategoria = @IdCategoria
	ELSE
	BEGIN
		set @Resultado = 0
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'
	END

END



GO

CREATE PROC SP_ELIMINARCATEGORIA(
@IdCategoria int,
@Resultado bit output,
@Mensaje varchar(500) output

)as
begin
	SET @Resultado = 1
	IF NOT EXISTS(
		SELECT * FROM CATEGORIA c
		iNNER JOIN PRODUCTO p on p.IdCategoria = c.IdCategoria
		WHERE C.IdCategoria = @IdCategoria
	)BEGIN
		
		DELETE TOP(1) FROM CATEGORIA WHERE IdCategoria = @IdCategoria

	END
	ELSE
	BEGIN
		set @Resultado = 0
		set @Mensaje = 'La categoria se encuentra relacionada a un producto'
	END

END



select IdCategoria,Descripcion,Estado from CATEGORIA