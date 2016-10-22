create table [dbo].[provincias]
(
idProvincia int identity (1,1),
provincia varchar(20)
constraint "pk_prov" primary key (idProvincia)
);
create table [dbo].[localidades]
(
idLocalidad int identity (1,1),
localidad varchar(20),
idProvincia int
constraint "pk_loc" primary key(idLocalidad),
constraint "fk_prov" foreign key (idProvincia) references provincias (idProvincia)
) ;
create table [dbo].[roles]
(
idRol varchar(3) unique,
rol varchar(14)
constraint "pk_rol" primary key(idRol)
) ;

create table [dbo].[sucursales]
(
idSucursal int identity(1,1),
sucursal varchar(20)
constraint "pk_suc" primary key(idSucursal)
) ;
create table [dbo].[pisos]
(
idPiso int identity(1,1),
idSucursal int,
piso int
constraint "pk_piso" primary key(idPiso),
constraint "fk_suc" foreign key(idSucursal) references sucursales(idSucursal)
) ;
create table [dbo].[puestos]
(
idPuesto int identity(1,1),
idPiso int,
puesto varchar(20)
constraint "pk_pue" primary key (idPuesto),
constraint "fk_piso" foreign key (idPiso) references pisos(idPiso)
) ;
create table [dbo].[herramientas]
(
idHerramienta int identity (1,1),
herramienta varchar(25)
constraint "pk_her" primary key(idHerramienta)
) ;
create table [dbo].[usuarios]
(
idUsuario int identity (1,1),
nombre varchar(30),
apellido varchar(30),
telefono varchar(18),
fechaNacimiento datetime,
fechaContratacion datetime,
idRol varchar(3),
idLocalidad int,
username varchar(20) not null unique,
claveacc varchar(20),
idSupervisor int
constraint "pk_user" primary key(idUsuario),
constraint "fk_rol" foreign key(idRol) references roles(idRol),
constraint "fk_loc" foreign key(idLocalidad) references localidades(idLocalidad),
constraint "fk_sup" foreign key(idSupervisor) references usuarios(idUsuario)
) 
ALTER TABLE usuarios
ALTER COLUMN idRol varchar(3) not null

create table [dbo].[reposiciones]
(
idReposicion int identity(1,1),
idOperario int,
idSolicitante int,
idPuesto int,
idHerramienta int,
comentario varchar(100),
horaCreacion datetime,
horaAsignacion datetime,
horaResolucion datetime
constraint "pk_rep" primary key(idReposicion),
constraint "fk_op" foreign key(idOperario) references usuarios(idUsuario),
constraint "fk_sol" foreign key(idSolicitante) references usuarios(idUsuario),
constraint "fk_pue" foreign key(idPuesto) references puestos(idPuesto),
constraint "fk_her" foreign key(idHerramienta) references herramientas(idHerramienta)
) 
create table [dbo].[estados]
(
idEstado int identity(1,1),
estado varchar(25) not null
constraint "pk" primary key(idEstado)
)
insert into [dbo].[estados] ([estado]) values ('En espera'),('En tratamiento'),('Resuelta')
alter table Reposiciones
add constraint "fk_estado" foreign key(idEstado) references estados(idEstado)


INSERT INTO [Provincias] ([provincia])VALUES('Buenos Aires'),('Capital Federal'),('Catamarca'),('Chaco'),('Chubut'),('Córdoba'),('Corrientes'),('Entre Ríos'),('Formosa'),('Jujuy'),('La Pampa'),('La Rioja'),('Mendoza'),('Misiones'),('Neuquén'),('Río Negro'),('Salta'),('San Juan'),('San Luis'),('Santa Cruz'),('Santa Fe'),('Santa Fe'),('Tierra del Fuego'),('Tucumán')
Insert into [localidades] ([idProvincia],[localidad]) values 
(1,'25 de Mayo'),(1,'3 de febrero'),(1,'A. Alsina'),(1,'A. Gonzáles Cháves'),
(2,'11 de Septiembre'),(2,'20 de Junio'),(2,'25 de Mayo'),(2,'Acassuso'),
(3,'Aconquija'),(3,'Andalgalá'),(3,'Catamarca'),(3,'Icano'),
(4,'Barranqueras'),(4,'Campo Largo'),(4,'Las Garcitas'),(4,'Puerto Vilelas'),
(5,'Cholila'),(5,'El Maitén'),(5,'Sarmiento'),(5,'Trelew'),
(6,'Assunta'),(6,'Canals'),(6,'Cerro Colorado'),(6,'Comechingones'),
(7,'Empedrado'),(7,'Goya'),(7,'Nueve de Julio'),(7,'Santa Ana'),
(8,'Arroyo Corralito'),(8,'Chajarí'),(8,'Crucecitas'),(8,'Federación'),
(9,'Ibarreta'),(9,'Pirané'),(9,'Tres Lagunas'),(9,'Villa Escolar'),
(10,'El Talar'),(10,'La Esperanza'),(10,'Rinconada'),(10,'Yaví'),
(11,'Catriló'),(11,'Falucho'),(11,'Puelches'),(11,'Sarah'),
(12,'Famatina'),(12,'Independencia'),(12,'Vinchina'),(12,'Sanagasta'),
(13,'Abramo'),(13,'Adolfo Van Praet'),(13,'Agustoni'),(13,'Algarrobo del Aguila'),(13,'Alpachiri'),
(14,'Capioví'),(14,'Cerro Azul'),(14,'Posadas'),(14,'Tres Capones'),
(15,'Cipolletti'),(15,'Guanacos'),(15,'Paso Aguerre'),(15,'Senillosa'),
(16,'Chelforo'),(16,'Comallo'),(16,'Darwin'),(16,'Norquinco'),
(17,'Pilcaniyeu'),(17,'Coronel Moldes'),(17,'Guachipas'),(17,'La Vina'),
(18,'Angaco'),(18,'Chimbas'),(18,'Pocito'),(18,'Rawson'),
(19,'La Florida'),(19,'Lavaisse'),(19,'Navia'),(19,'Renca'),
(20,'Jaramillo'),(20,'Río Gallegos'),(20,'Pico Truncado'),(20,'Los Antiguos'),
(21,'Angeloni'),(21,'Arequito'),(21,'Coronda'),(21,'Egusquiza'),
(22,'Lugones'),(22,'Real Sayana'),(22,'Tapso'),(22,'Termas de Rio Hondo'),
(23,'Río Grande'),(23,'Tolhuin'),(23,'Ushuaia'),
(24,'Bella Vista'),(24,'Los Sosa'),(24,'Monte Bello'),(24,'Río Chico')

-- src = "https://code.google.com/p/apuranqn/source/browse/trunk/+apuranqn/Script+Provincias+y+Localidades.sql?r=8"

INSERT INTO [dbo].[roles] ([idRol],[rol]) values ('sol','solicitante'),('ope','operario')
,('jso','SupSolicitante'),('jop','SOperario'),('ger','Gerencia'),('usr','CargarUsuarios'),('adm','Administrador')

INSERT INTO [dbo].[sucursales] ([sucursal])  values ('Central'),('Sucursal uno'),('Sucursal dos')

insert into [dbo].[pisos] ([idSucursal],[piso]) values (1,1),(1,2),(1,3),(1,4),(2,1),(2,2),(3,1),(3,2)

insert into [dbo].[puestos] ([idPiso],[puesto]) values (1,'Piso:1-Terminal:1'),(1,'Piso:1-Terminal:2'),(1,'Piso:1-Terminal:3'),(1,'Piso:1-Terminal:4'),(1,'Piso:1-Terminal:5'),(1,'Piso:1-Terminal:6'),(1,'Piso:1-Terminal:7'),(1,'Piso:1-Terminal:8'),(1,'Piso:1-Terminal:9'),(1,'Piso:1-Terminal:10'),(1,'Piso:1-Terminal:11'),(1,'Piso:1-Terminal:12'),(1,'Piso:1-Terminal:13'),(1,'Piso:1-Terminal:14'),(1,'Piso:1-Terminal:15'),(2,'Piso:2-Terminal:1'),(2,'Piso:2-Terminal:2'),(2,'Piso:2-Terminal:3'),(2,'Piso:2-Terminal:4'),(2,'Piso:2-Terminal:5'),(2,'Piso:2-Terminal:6'),(2,'Piso:2-Terminal:7'),(2,'Piso:2-Terminal:8'),(2,'Piso:2-Terminal:9'),(2,'Piso:2-Terminal:10'),(2,'Piso:2-Terminal:11'),(2,'Piso:2-Terminal:12'),(2,'Piso:2-Terminal:13'),(2,'Piso:2-Terminal:14'),(2,'Piso:2-Terminal:15'),(3,'Piso:3-Terminal:1'),(3,'Piso:3-Terminal:2'),(3,'Piso:3-Terminal:3'),(3,'Piso:3-Terminal:4'),(3,'Piso:3-Terminal:5'),(3,'Piso:3-Terminal:6'),(3,'Piso:3-Terminal:7'),(3,'Piso:3-Terminal:8'),(3,'Piso:3-Terminal:9'),(3,'Piso:3-Terminal:10'),(3,'Piso:3-Terminal:11'),(3,'Piso:3-Terminal:12'),(3,'Piso:3-Terminal:13'),(3,'Piso:3-Terminal:14'),(3,'Piso:3-Terminal:15'),(4,'Piso:4-Terminal:1'),(4,'Piso:4-Terminal:2'),(4,'Piso:4-Terminal:3'),(4,'Piso:4-Terminal:4'),(4,'Piso:4-Terminal:5'),(4,'Piso:4-Terminal:6'),(4,'Piso:4-Terminal:7'),(4,'Piso:4-Terminal:8'),(4,'Piso:4-Terminal:9'),(4,'Piso:4-Terminal:10'),(4,'Piso:4-Terminal:11'),(4,'Piso:4-Terminal:12'),(4,'Piso:4-Terminal:13'),(4,'Piso:4-Terminal:14'),(4,'Piso:4-Terminal:15'),(5,'Piso:5-Terminal:1'),(5,'Piso:5-Terminal:2'),(5,'Piso:5-Terminal:3'),(5,'Piso:5-Terminal:4'),(5,'Piso:5-Terminal:5'),(5,'Piso:5-Terminal:6'),(5,'Piso:5-Terminal:7'),(5,'Piso:5-Terminal:8'),(5,'Piso:5-Terminal:9'),(5,'Piso:5-Terminal:10'),(5,'Piso:5-Terminal:11'),(5,'Piso:5-Terminal:12'),(5,'Piso:5-Terminal:13'),(5,'Piso:5-Terminal:14'),(5,'Piso:5-Terminal:15'),(6,'Piso:6-Terminal:1'),(6,'Piso:6-Terminal:2'),(6,'Piso:6-Terminal:3'),(6,'Piso:6-Terminal:4'),(6,'Piso:6-Terminal:5'),(6,'Piso:6-Terminal:6'),(6,'Piso:6-Terminal:7'),(6,'Piso:6-Terminal:8'),(6,'Piso:6-Terminal:9'),(6,'Piso:6-Terminal:10'),(6,'Piso:6-Terminal:11'),(6,'Piso:6-Terminal:12'),(6,'Piso:6-Terminal:13'),(6,'Piso:6-Terminal:14'),(6,'Piso:6-Terminal:15'),(7,'Piso:7-Terminal:1'),(7,'Piso:7-Terminal:2'),(7,'Piso:7-Terminal:3'),(7,'Piso:7-Terminal:4'),(7,'Piso:7-Terminal:5'),(7,'Piso:7-Terminal:6'),(7,'Piso:7-Terminal:7'),(7,'Piso:7-Terminal:8'),(7,'Piso:7-Terminal:9'),(7,'Piso:7-Terminal:10'),(7,'Piso:7-Terminal:11'),(7,'Piso:7-Terminal:12'),(7,'Piso:7-Terminal:13'),(7,'Piso:7-Terminal:14'),(7,'Piso:7-Terminal:15')

insert into [dbo].[herramientas]([herramienta]) values ('Auricular'),('Microfono'),('CPU'),('Monitor'),('Silla'),('Teclado'),('Mouse')





