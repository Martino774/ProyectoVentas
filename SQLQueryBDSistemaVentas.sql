CREATE DATABASE DBSISTEMA_VENTAS

GO

USE DBSISTEMA_VENTAS

GO

CREATE TABLE ROL(
	IdRol int primary key identity,
	Descripcion varchar(50),
	FechaCreacion datetime default getdate()
)
go

CREATE TABLE PERIMSO(
	IdPermiso int primary key identity,
	IdRol int  references ROL(IdRoL),
	NobreMenu varchar(100),
	FechaCreacion datetime default getdate()
)

go

CREATE TABLE PROVEEDOR(
	IdProveedor int primary key identity,
	Documento varchar(50),
	RazonSocial varchar(50),
	Correo varchar(50),
	Telefono varchar(50),
	Estado bit,
	FechaRegistro datetime default getdate()
)

go

CREATE TABLE CLIENTE(
	IdCliente int primary key identity,
	Documento varchar(50),
	NombreCompleto varchar(50),
	Correo varchar(50),
	Telefono varchar(50),
	Estado bit,
	FechaRegistro datetime default getdate()
)
go

CREATE TABLE USUARIO(
	IdUsuario int primary key identity,
	Documento varchar(50),
	NombreCompleto varchar(50),
	Correo varchar(50),
	Clave varchar(50),
	IdRol int  references ROL(IdRoL),
	Estado bit,
	FechaRegistro datetime default getdate()
)
go

CREATE TABLE CATEGORIA(
	IdCategoria int primary key identity,
	Descripcion varchar(100),
	Estado bit,
	FechaRegistro datetime default getdate()
)
go

CREATE TABLE PRODUCTO(
	IdProducto int primary key identity,
	Codigo varchar(50),
	Descripcion varchar(100) default 0,
	IdCategoria int references CATEGORIA(iDCategoria),
	Stock int not null,
	PrecioCompra decimal(10,2) default 0,
	PrecioVenta decimal(10,2) default 0,
	Estado bit,
	FechaRegistro datetime default getdate()
)
go

CREATE TABLE COMPRA(
	IdCompra int primary key identity,
	IdUsuario int references USUARIO(Idusuario),
	IdProveedor int references PROVEEDOR(IdProveedor),
	TipoDocuento varchar(50),
	NumeroDocumento varchar(50),
	MontoTotal decimal(10,2),
	FechaRegistro datetime default getdate()
)
go

CREATE TABLE DETALLE_COMPRA(
	IdDetalleCompra int primary key identity,
	IdUcompra int references COMPRA(IdCompra),
	IdProducto int references Producto(IdProducto),
	PrecioCompra decimal(10,2) default 0,
	PrecioVenta decimal(10,2) default 0,
	Cantidad int,
	MontoTotal decimal(10,2),
	FechaRegistro datetime default getdate()
)
go

CREATE TABLE VENTA(
	IdVenta int primary key identity,
	IdUsuario int references USUARIO(Idusuario),
	TipoDocuento varchar(50),
	NumeroDocumento varchar(50),
	DocumentoCliente varchar(50),
	NombreCliente varchar(100),
	MontoPago decimal(10,2),
	MontoCambio decimal(10,2),
	MontoTotal decimal(10,2),
	FechaRegistro datetime default getdate()
)
go

CREATE TABLE DETALLE_VENTA(
	IdDetalleVenta int primary key identity,
	IdVenta int references VENTA(IdVenta),
	IdProducto int references Producto(IdProducto),
	PrecioCompra decimal(10,2) default 0,
	PrecioVenta decimal(10,2),
	Cantidad int,
	SubTotal decimal(10,2),
	FechaRegistro datetime default getdate()
)
go