create database veterinaria;
go
use veterinaria;
go
create table usuarios(
id int primary key identity,
usuario varchar(50),
password varchar(100),
role varchar(20)
);
go
create table clientes (
  id int primary key identity,
  nombre varchar(200),
  telefono varchar(9),
  sexo varchar(20),
  direccion varchar(200)
);
go
create table mascotas (
  id int primary key identity,
  nombre varchar(50),
  tipo varchar(50),
  sexo varchar(20),
  fecha_nacimiento date,
  cliente_id int
);
go
create table veterinarios (
   id integer primary key identity,
  nombre varchar(200),
  telefono varchar(9),
  sexo varchar(20),
  direccion varchar(200)
);
go
create table citas (
  id int primary key identity,
  fecha datetime,
  mascota_id int,
  veterinario_id int,
  estado varchar(10)
);
go
create table expediente (
   id int primary key identity,
   mascota_id int,
   cita_id int,
   diagnostico varchar(500),
   recetas varchar(500)
);
go
--llaves
alter table mascotas add constraint pk_cliente_mascota foreign key (cliente_id) references clientes(id);
go
alter table citas add constraint fk_citas_mascota foreign key (mascota_id) references mascotas(id);
go
alter table citas add constraint fk_citas_veterinario foreign key (veterinario_id) references veterinarios(id);
go
alter table expediente add constraint fk_exp_mascota foreign key (mascota_id) references mascotas(id);
go
alter table expediente add constraint fk_exp_cita foreign key (cita_id) references citas(id);


