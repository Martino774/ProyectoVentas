use indices;

create table listado(
	Nombre varchar(50)not null,
	Apellido varchar(50)not null,
	Direccion varchar(50),
	Tel int,
	Cel int,
	email varchar(50)
);

insert into listado values ('ximena','asdperez','gdireccion1',221111111,771111111,'maruiasdaso@gmail.com');
insert into listado values ('mario','pdderez','adireccion1',221111111,771111111,'maruido@gmail.com');
insert into listado values ('mario1','persaez','addireccisdaon1',221111111,771111111,'maruddio@gmail.com');
insert into listado values ('mario13','perdddez','direccion1',221111111,771111111,'maruaaaio@gmail.com');

select * from listado;


--verifica la existencia de indices en la tabla
execute sp_helpindex 'listado'

--creacion de indices 

--indice agrupado
create clustered index IDX_Nombre
on listado (Nombre)
--al hacer de nuevo el select tenemos como resulatado ordenados alfabeticamente 
--por el atributo nombre 


--indice no agrupado
create nonclustered index IDX_Apellido
on listado (Apellido)

select Nombre, Apellido from listado;
--al hacer el select con los dos atributos se ordenara por apellido
--porque al programa se le hace mas facil este segundo indice

--ASIGNACION DE PERMISOS

--creacion de login y usuarios
create login Mperez
	with password = '340$Uuxwp7Mcxo7Khy';
go

create user Mperez for login Mperez;
go

