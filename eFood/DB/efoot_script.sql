USE [master]
GO
/****** Object:  Database [efood]    Script Date: 12/6/2020 3:34:14 AM ******/
CREATE DATABASE [efood]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'efood', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\efood.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'efood_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\efood_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [efood] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [efood].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [efood] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [efood] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [efood] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [efood] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [efood] SET ARITHABORT OFF 
GO
ALTER DATABASE [efood] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [efood] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [efood] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [efood] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [efood] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [efood] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [efood] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [efood] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [efood] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [efood] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [efood] SET  DISABLE_BROKER 
GO
ALTER DATABASE [efood] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [efood] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [efood] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [efood] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [efood] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [efood] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [efood] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [efood] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [efood] SET  MULTI_USER 
GO
ALTER DATABASE [efood] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [efood] SET DB_CHAINING OFF 
GO
ALTER DATABASE [efood] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [efood] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [efood]
GO
/****** Object:  StoredProcedure [dbo].[actuaiza_temp_det_fact]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[actuaiza_temp_det_fact]

 @id_factura int,
 @id_plato int,
 @cantidad numeric(12,2), 
 @precio numeric(12,2),
 @itbis decimal(12,2)

as

IF NOT EXISTS(SELECT @id_factura FROM temp_det_factura WHERE id_factura=@id_factura and id_plato =@id_plato) 
	INSERT INTO temp_det_factura (id_factura, id_plato, cantidad, precio, itbis)
  VALUES						 (@id_factura, @id_plato, @cantidad, @precio, @itbis)

ELSE

	UPDATE temp_det_factura SET  cantidad= @cantidad, precio = @precio, itbis = @itbis
		WHERE id_plato = @id_plato and id_factura =@id_factura

GO
/****** Object:  StoredProcedure [dbo].[actuaiza_temp_ENC_fact]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[actuaiza_temp_ENC_fact]

 @id_factura int,
 @id_mesa int,
 @fecha_factura date,
 @id_cliente int,
 @id_usuario int,
 @rnc int,
 @itbis float,
 @total float, 
 @porciento_ley float,
 @sub_total  float,
 @estado varchar(1),
 @nota varchar(250)

as
 INSERT INTO temp_enc_factura (id_factura, id_mesa, fecha_factura, id_cliente, id_usuario, rnc, itbis, total, porciento_ley, sub_total, estado, nota)
   VALUES					  (@id_factura, @id_mesa, @fecha_factura, @id_cliente, @id_usuario,@rnc,@itbis,@total,@porciento_ley,@sub_total,@estado,@nota )


GO
/****** Object:  StoredProcedure [dbo].[actualiza_det_cobro]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[actualiza_det_cobro]

@id_apertura int,
@id_factura int,
@monto decimal(15, 2),
@monto_pendiente decimal(15, 2),
@monto_devuelto decimal(15, 2),
@cuotas int,
@fecha_creacion datetime,
@fecha_actualizacion datetime,
@id_usuario int 

as
begin 
insert into enc_cobros (id_apertura , id_factura, monto, monto_pendiente, monto_devuelto, cuotas , fecha_creacion , fecha_actualizacion ,id_usuario)
	values (@id_apertura ,@id_factura, @monto, @monto_pendiente,@monto_devuelto,@cuotas ,@fecha_creacion ,@fecha_actualizacion ,@id_usuario)

end
GO
/****** Object:  StoredProcedure [dbo].[actualiza_det_cobros]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[actualiza_det_cobros]

@id_cobro int ,
@id_tipo_pago int, 
@monto decimal (15, 2),
@numero_cheque varchar(20), 
@id_banco int ,
@numero_tarjeta varchar(20),
@aprobacion varchar(20),
@id_devolucion int,
@id_moneda int,
@tasa decimal(15, 2) ,
@id_usuario int,
@desactivado varchar(1),
@fecha_creacion datetime,
@fecha_actualizacion datetime

as

begin
	
	insert into det_cobros (id_cobro, id_tipo_pago ,  monto, numero_cheque,  id_banco , numero_tarjeta , aprobacion , id_devolucion, id_moneda , tasa , id_usuario , desactivado , fecha_creacion , fecha_actualizacion)
		values (@id_cobro, @id_tipo_pago ,  @monto, @numero_cheque,  @id_banco , @numero_tarjeta , @aprobacion , @id_devolucion, @id_moneda , @tasa , @id_usuario , @desactivado , @fecha_creacion , @fecha_actualizacion)

end



GO
/****** Object:  StoredProcedure [dbo].[actualiza_det_factura]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[actualiza_det_factura]


 @id_plato int,
 @id_unidad int,
 @cantidad decimal(12, 2),
 @precio decimal(12, 2),
 @importe decimal(12, 2),
 @id_factor int,
 @id_itbis int,
 @tasa_itbis decimal(12, 2),
 @total_itbis decimal(12, 2),
 @id_usuario int,
 @id_almacen int,
 @costo decimal(12, 2)

AS

INSERT INTO det_factura ( id_plato, id_unidad, cantidad, precio, importe, id_factor, id_itbis, tasa_itbis, 
						 total_itbis, id_usuario, id_almacen, costo)

   VALUES	 ( @id_plato, @id_unidad, @cantidad, @precio, @importe, @id_factor,
              @id_itbis,@tasa_itbis, @total_itbis, @id_usuario, @id_almacen, @costo)


GO
/****** Object:  StoredProcedure [dbo].[actualiza_detalle_receta]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[actualiza_detalle_receta]
 
@id_receta int,
@id_producto int, 
@porcion numeric, 
@unidad int

AS

IF NOT EXISTS(SELECT id_receta FROM detalle_receta WHERE id_receta= @id_receta)
INSERT INTO detalle_receta(id_receta, id_producto, porcion, id_unidad)
VALUES (@id_receta, @id_producto, @porcion, @unidad)

ELSE 
update detalle_receta SET  id_producto=@id_producto, porcion=@porcion, id_unidad=@unidad
WHERE id_receta = @id_receta
GO
/****** Object:  StoredProcedure [dbo].[actualiza_enc_cobro]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[actualiza_enc_cobro]

@id_apertura int,
@id_factura int,
@monto decimal(15, 2),
@monto_pendiente decimal(15, 2),
@monto_devuelto decimal(15, 2),
@cuotas int,
@fecha_creacion datetime,
@fecha_actualizacion datetime,
@id_usuario int 

as
begin 
insert into enc_cobros (id_apertura , id_factura, monto, monto_pendiente, monto_devuelto, cuotas , fecha_creacion , fecha_actualizacion ,id_usuario)
	values (@id_apertura ,@id_factura, @monto, @monto_pendiente,@monto_devuelto,@cuotas ,@fecha_creacion ,@fecha_actualizacion ,@id_usuario)

end
GO
/****** Object:  StoredProcedure [dbo].[actualiza_enc_factura]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[actualiza_enc_factura]

 @id_mesa int,
 @id_cliente int,
 @id_sucursal int,
 @fecha_factura date,
 @rnc varchar(11),
 @id_tipo_ncf int,
 @ncf varchar(11),
 @itbis float,
 @total float,
 @sub_total  float,
 @total_descuento decimal,
 @id_usuario int, 
 @nombre_cliente varchar(50),
 @direccion varchar(500),
 @telefono int,
 @dias_acuerdo int,
 @fecha_vencimiento_ncf date,
 @fecha_vencimiento_factura date,
 @nota varchar(500),
 @id_tipo_cobro int,
 @porciento_ley float

AS

INSERT INTO enc_factura ( id_mesa, id_cliente, id_sucursal, fecha_factura, rnc , id_tipo_ncf, ncf ,itbis ,total, sub_total , 
						   total_descuento, id_usuario, nombre_cliente, direccion, telefono, dias_acuerdo, fecha_vencimiento_ncf, fecha_vencimiento_factura,
						   nota, id_tipo_cobro,  porciento_ley )

   VALUES					  (  @id_mesa, @id_cliente, @id_sucursal, @fecha_factura,  @rnc, @id_tipo_ncf,
                                @ncf, @itbis, @total, @sub_total , @total_descuento, @id_usuario, @nombre_cliente, @direccion,
							    @telefono, @dias_acuerdo, @fecha_vencimiento_ncf, @fecha_vencimiento_factura, @nota, @id_tipo_cobro, @porciento_ley)


GO
/****** Object:  StoredProcedure [dbo].[actualiza_secuencia_generada]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[actualiza_secuencia_generada]

@id_secuencia int,
@ultimo_generado int

as

Begin 

if exists (select id_secuencia from secuencia_ncf where id_secuencia=@id_secuencia)

update secuencia_ncf set 

ultimo_generado = @ultimo_generado

where id_secuencia=@id_secuencia

End
GO
/****** Object:  StoredProcedure [dbo].[actualiza_secuencia_ncf]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[actualiza_secuencia_ncf]

@id_tipo_ncf int,
@letra varchar(50),
@secuencia_inicial int,
@secuencia_final int,
@fecha_vencimiento date = null,
@id_usuario int,
@id_secuencia int = 0

as 

if  not exists (select 1 from secuencia_ncf where id_secuencia=@id_secuencia)

		if exists (Select top(1) secuencia_final from secuencia_ncf where secuencia_final < @secuencia_inicial )
		begin

			insert into secuencia_ncf( id_tipo_ncf, letra, secuencia_inicial, secuencia_final, fecha_vencimiento, id_usuario)
			values              (@id_tipo_ncf, @letra, @secuencia_inicial, @secuencia_final, @fecha_vencimiento, @id_usuario)

		end ;

else

update secuencia_ncf set id_tipo_ncf=@id_tipo_ncf, letra=@letra, secuencia_inicial=@secuencia_inicial, secuencia_final=@secuencia_final, fecha_vencimiento=@fecha_vencimiento , id_usuario =@id_usuario

where id_secuencia = @id_secuencia
GO
/****** Object:  StoredProcedure [dbo].[actualizacliente]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[actualizacliente] 

@id_cliente int,
@id_persona int,
@rnc varchar(50),
@id_condicion int,
@excento_itbis varchar(1),
@id_tipo_ncf int,
@nota varchar(200),
@limite_credito decimal(12,2),
@porcentaje_mora decimal(12,2),
@porcentaje_descuento decimal(12,2),
@estado bit,
@credito_activo bit


AS

BEGIN

if not exists (select id_cliente from cliente where id_cliente=@id_cliente)
	insert into cliente(id_cliente, id_persona, rnc, id_condicion, excento_itbis, id_tipo_ncf, nota, limite_credito,porcentaje_mora, porcentaje_descuento, estado, credito_activo)
values              (@id_cliente, @id_persona, @rnc, @id_condicion, @excento_itbis, @id_tipo_ncf, @nota, @limite_credito, @porcentaje_mora, @porcentaje_descuento, @estado, @credito_activo )

else  

update cliente set 

rnc = @rnc, id_condicion = @id_condicion, excento_itbis = @excento_itbis, id_tipo_ncf =  @id_tipo_ncf, nota =@nota, limite_credito =@limite_credito,porcentaje_mora = @porcentaje_mora, porcentaje_descuento =@porcentaje_descuento, estado=@estado, @credito_activo=@credito_activo

where id_cliente=@id_cliente

END



GO
/****** Object:  StoredProcedure [dbo].[actualizaempleado]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[actualizaempleado]
 
 @ficha int,
 @id_empelado int,
 @fecha_entrada date, 
 @fecha_salida date, 
 @id_cargo varchar(50),
 @id_departamento varchar(50),
 @tipo_pago varchar(50),
 @salrario decimal,
 @edad int, 
 @sexo varchar(1),
 @id_nacionalidad int,
 @id_pais int,
 @foto varchar(2000),
 @nss varchar(20)


as

if NOT EXISTS(SELECT ficha FROM empleado WHERE ficha=@ficha)

insert into empleado (ficha, id_persona, fecha_entrda, fecha_salida, id_cargo, id_departamento, id_pago, salario, edad, sexo, id_nacionalidad, id_pais, foto, nss)

values                (@ficha, @id_empelado, @fecha_entrada, @fecha_salida, @id_cargo, @id_departamento, @tipo_pago, @salrario, @edad, @sexo, @id_nacionalidad,@id_pais, @foto, @nss)

else 

update empleado set fecha_entrda=@fecha_entrada, fecha_salida=@fecha_salida, id_cargo=@id_cargo,
id_departamento=@id_departamento, id_pago=@tipo_pago, salario=@salrario, edad = @edad, sexo=@sexo, id_nacionalidad =@id_nacionalidad, id_pais =@id_pais, foto =@foto, nss = @nss
where ficha=@ficha
GO
/****** Object:  StoredProcedure [dbo].[actualizamarcas]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[actualizamarcas]

@id_marca int,
@marca varchar(30)

as 

if not exists(select * from marcas where id_marca=@id_marca)

insert into marcas(id_marca, marca) values (@id_marca, @marca)

else

update marcas

set marca = @marca
where id_marca =@id_marca
GO
/****** Object:  StoredProcedure [dbo].[actualizapersona]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[actualizapersona]

@id_persona int,
@nombre varchar(50),
@apellido1 varchar(50),
@apellido2 varchar(50),
@direccion varchar(50),
@documento varchar(50),
@foto varchar(500),
@telefono varchar ,
@correo varchar(80),
@id_estado_civil int 

as 

if not exists (select id_persona from persona where id_persona=@id_persona)
insert into persona (id_persona, nombre1, apellido1, apellido2, direccion, documento, estado, foto, telefono, correo, id_estado_civil)
values              (@id_persona, @nombre, @apellido1, @apellido2, @direccion, @documento, 1, @foto, @telefono, @correo, @id_estado_civil)

else

update persona set nombre1=@nombre, apellido1=@apellido1, apellido2=@apellido2, direccion=@direccion, documento=@documento, foto=@foto , telefono = @telefono, correo = @correo, id_estado_civil = @id_estado_civil

where id_persona = @id_persona
GO
/****** Object:  StoredProcedure [dbo].[actualizaplatos]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[actualizaplatos]
 

@id_plato int, 
@plato varchar(100),
@descripcion varchar(1000), 
@precio numeric(12,2), 
@itbis numeric(12,2), 
@id_receta int ,
@id_categoria int

AS

IF NOT EXISTS(SELECT id_plato FROM platos WHERE id_plato= @id_plato)
INSERT INTO platos(id_plato, plato, descripcion, precio, itbis, id_receta, id_categoria)
VALUES (@id_plato, @plato, @descripcion, @precio, @itbis, @id_receta, @id_categoria)

ELSE 
update platos SET  plato=@plato, descripcion=@descripcion, precio=@precio, itbis=@itbis, id_receta=@id_receta
WHERE id_categoria = @id_categoria
GO
/****** Object:  StoredProcedure [dbo].[actualizaproductos]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[actualizaproductos] 

@id_productos int, @productos varchar(50), @tipo varchar(50), @descripcion varchar(100),
@cantidad int, @reorden int, @id_unidad int, @id_marca int 

as

if NOT EXISTS(SELECT id_productos FROM productos WHERE id_productos=@id_productos)
insert into productos (id_productos, productos, tipo_producto, descripcion, cantidad, reorden, id_unidad, id_marca)
values (@id_productos, @productos, @tipo, @descripcion, @cantidad, @reorden, @id_unidad, @id_marca)

else 
update productos set  productos=@productos, tipo_producto=@tipo, descripcion=@descripcion,
@cantidad=@cantidad, reorden=@reorden, id_unidad=@id_unidad
where id_productos=@id_productos
GO
/****** Object:  StoredProcedure [dbo].[actualizaproveedor]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[actualizaproveedor]

@id_proveedor int,
@id_persona int,
@rnc int


as 

if not exists(select * from proveedor where id_proveedor=@id_persona)

insert into proveedor(id_proveedor, id_persona, rnc) values (@id_proveedor, @id_persona, @rnc)

else

update proveedor

set id_persona =@id_persona, rnc=@rnc

where id_proveedor =@id_proveedor

GO
/****** Object:  StoredProcedure [dbo].[actualizareceta]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[actualizareceta]
 

@id_recetas int, 
@receta varchar(100),
@descripcion varchar(1000), 
@categoria varchar(60), 
@comensales int, 
@tiempo int 

AS

IF NOT EXISTS(SELECT id_receta FROM recetas WHERE id_receta= @id_recetas)
INSERT INTO recetas(id_receta, receta, descripcion, categoria, comensales, tiempo)
VALUES (@id_recetas, @receta, @descripcion, @categoria, @comensales, @tiempo)

ELSE 
update recetas SET  receta=@receta, descripcion=@descripcion, categoria=@categoria, comensales=@comensales, tiempo=@tiempo
WHERE id_receta = @id_recetas
GO
/****** Object:  StoredProcedure [dbo].[actualizausuarios]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[actualizausuarios]

@id_usuario int,
@usuario varchar(50),
@password varchar(50),
@fecha_creacion date,
@id_persona int,
@ficha int


as

if not exists (select id_usuario from usuarios where id_usuario=@id_usuario)
	insert into usuarios(id_usuario, usuario, pass, fecha_creacion, id_persona, ficha, estado)
values              (@id_usuario, @usuario, @password, @fecha_creacion, @id_persona, @ficha, 1)

else  
update usuarios set 

usuario=@usuario, pass=@password, fecha_creacion=@fecha_creacion

where id_usuario=@id_usuario

GO
/****** Object:  StoredProcedure [dbo].[datos_factura]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[datos_factura] @NumFac int

as

SELECT        dbo.temp_enc_factura.id_factura, dbo.temp_enc_factura.id_mesa, dbo.temp_enc_factura.fecha_factura, dbo.temp_det_factura.id_plato, dbo.temp_det_factura.cantidad, 
                         dbo.temp_det_factura.precio,dbo.temp_det_factura.cantidad * dbo.temp_det_factura.precio importe ,dbo.temp_det_factura.itbis, dbo.platos.plato, dbo.platos.descripcion 
FROM            dbo.temp_det_factura INNER JOIN
                         dbo.temp_enc_factura ON dbo.temp_det_factura.id_factura = dbo.temp_enc_factura.id_factura INNER JOIN
                         dbo.platos ON dbo.temp_det_factura.id_plato = dbo.platos.id_plato	

WHERE		 dbo.temp_enc_factura.id_factura = @NumFac ;

GO
/****** Object:  StoredProcedure [dbo].[eliminaempleado]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[eliminaempleado]

@id_empleado int 


as 

delete from persona where id_persona = @id_empleado delete from empleado where ficha = ficha

GO
/****** Object:  StoredProcedure [dbo].[eliminapersona]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[eliminapersona]

@id_persona int 


as 

delete from persona where id_persona = @id_persona
GO
/****** Object:  StoredProcedure [dbo].[eliminararticulos]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[eliminararticulos]

@id_productos int 


as 

UPDATE productos
	 SET  estado = 0
WHERE id_productos= @id_productos
GO
/****** Object:  StoredProcedure [dbo].[eliminausuarios]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[eliminausuarios]
@ficha int 


as 

update usuarios

set estado = 0 where ficha=@ficha
GO
/****** Object:  StoredProcedure [dbo].[SetSecuenciaNCF]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SetSecuenciaNCF]
@tipo int
as
begin
	declare
	@tmp table(
	  id int,
	  proximo_generado int);
	 begin tran
	 
	  begin try
		 insert into @tmp(proximo_generado,id)
			 select proximo_generado,id_secuencia
			   From GetSecuenciaNCF(@tipo);
		 
		 Update secuencia_ncf Set ultimo_generado = a.proximo_generado 
		   From @tmp a Inner Join
			secuencia_ncf On secuencia_ncf.id_secuencia = a.id;
         
		 commit tran;

		 Select top 1 proximo_generado as secuencia_generada 
			From @tmp;

	  end try

	  begin catch
		  rollback tran;
          Select error_message() AS ErrorMessage;
      end catch;

		   
	 end 

GO
/****** Object:  Table [dbo].[almacen]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[almacen](
	[id_almacen] [int] NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
	[estado] [varchar](1) NULL,
 CONSTRAINT [PK_almacen] PRIMARY KEY CLUSTERED 
(
	[id_almacen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[almacen_productos]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[almacen_productos](
	[id_almacen] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[reorden] [int] NULL,
	[maximo] [decimal](12, 2) NULL,
	[minimo] [decimal](12, 2) NULL,
	[existencia] [decimal](12, 2) NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_almacen_productos] PRIMARY KEY CLUSTERED 
(
	[id_almacen] ASC,
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bancos]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bancos](
	[id] [int] NOT NULL,
	[descripcion] [varchar](250) NULL,
	[desactivado] [varchar](1) NULL,
	[id_usuario] [int] NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_actualizacion] [datetime] NULL,
 CONSTRAINT [PK_bancos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cajas]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cajas](
	[id] [int] NOT NULL,
	[caja] [varchar](250) NOT NULL,
	[id_usuario] [int] NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
	[desactivado] [varchar](1) NOT NULL,
 CONSTRAINT [PK_cajas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cargo]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cargo](
	[id_cargo] [int] NOT NULL,
	[cargo] [varchar](50) NOT NULL,
	[id_departamento] [int] NULL,
 CONSTRAINT [PK_cargo] PRIMARY KEY CLUSTERED 
(
	[id_cargo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[categoria]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[categoria](
	[ID_CATEGORIA] [int] NOT NULL,
	[DESCRIPCION] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CAEGORIA] PRIMARY KEY CLUSTERED 
(
	[ID_CATEGORIA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ciudad]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ciudad](
	[id_ciudad] [int] NOT NULL,
	[ciudad] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ciudad] PRIMARY KEY CLUSTERED 
(
	[id_ciudad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cliente](
	[id_cliente] [int] NOT NULL,
	[id_persona] [int] NOT NULL,
	[rnc] [varchar](50) NULL,
	[id_condicion] [int] NULL,
	[excento_itbis] [bit] NULL,
	[id_tipo_ncf] [int] NULL,
	[nota] [varchar](200) NULL,
	[limite_credito] [decimal](12, 2) NULL,
	[porcentaje_mora] [decimal](12, 2) NULL,
	[porcentaje_descuento] [decimal](12, 2) NULL,
	[estado] [bit] NULL,
	[credito_activo] [bit] NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[codigos_barra]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[codigos_barra](
	[codigo_barra] [int] NOT NULL,
	[id_factor] [int] NULL,
	[id_articlulo] [int] NULL,
	[desactivado] [varchar](1) NULL,
	[fecha_creacion] [date] NULL,
	[id_usuario] [int] NOT NULL,
 CONSTRAINT [PK_codigos_barra] PRIMARY KEY CLUSTERED 
(
	[codigo_barra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[condiciones_pago]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[condiciones_pago](
	[id_condicion_pago] [int] NOT NULL,
	[descripcion] [varchar](250) NULL,
	[dias] [int] NULL,
	[desactivado] [varchar](1) NULL,
	[fecha_creacion] [date] NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_condiciones_pago] PRIMARY KEY CLUSTERED 
(
	[id_condicion_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[contactos]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[contactos](
	[id_contacto] [int] NOT NULL,
	[contacto] [varchar](50) NOT NULL,
	[id_persona] [int] NOT NULL,
 CONSTRAINT [PK_contactos] PRIMARY KEY CLUSTERED 
(
	[id_contacto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[denominaciones]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[denominaciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[valor] [decimal](15, 2) NULL,
	[desactivado] [varchar](1) NOT NULL,
	[moneda] [varchar](1) NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_denominaciones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[departamento]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[departamento](
	[id_departamento] [int] NOT NULL,
	[departamento] [varchar](50) NOT NULL,
 CONSTRAINT [PK_departamento_1] PRIMARY KEY CLUSTERED 
(
	[id_departamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[det_apertura_caja]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[det_apertura_caja](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_apertura_caja] [int] NOT NULL,
	[id_denominacion] [int] NOT NULL,
	[valor] [decimal](15, 2) NOT NULL,
	[cantidad] [decimal](15, 2) NOT NULL,
 CONSTRAINT [PK_det_apertura_caja] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[det_cierre_caja]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[det_cierre_caja](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_denominacion] [int] NOT NULL,
	[id_cierre_caja] [int] NOT NULL,
	[cantidad] [decimal](15, 2) NOT NULL,
	[monto] [decimal](15, 2) NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
 CONSTRAINT [PK_det_cierre_caja] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[det_cobros]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[det_cobros](
	[id] [int] NOT NULL,
	[id_cobro] [int] NOT NULL,
	[id_tipo_pago] [int] NOT NULL,
	[monto] [decimal](15, 2) NOT NULL,
	[numero_cheque] [varchar](20) NULL,
	[id_banco] [int] NULL,
	[numero_tarjeta] [varchar](20) NULL,
	[aprobacion] [varchar](20) NULL,
	[id_devolucion] [int] NULL,
	[id_moneda] [int] NOT NULL,
	[tasa] [decimal](15, 2) NOT NULL,
	[id_usuario] [int] NULL,
	[desactivado] [varchar](1) NOT NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_actualizacion] [datetime] NULL,
 CONSTRAINT [PK_det_cobros] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[det_factura]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[det_factura](
	[id_factura] [int] IDENTITY(1,1) NOT NULL,
	[id_plato] [int] NOT NULL,
	[id_unidad] [int] NOT NULL,
	[cantidad] [decimal](10, 2) NOT NULL,
	[precio] [decimal](10, 2) NOT NULL,
	[importe] [decimal](12, 2) NULL,
	[id_factor] [int] NULL,
	[id_itbis] [int] NULL,
	[tasa_itbis] [decimal](3, 2) NULL,
	[total_itbis] [decimal](3, 2) NULL,
	[id_usuario] [int] NULL,
	[id_almacen] [int] NULL,
	[costo] [decimal](12, 2) NULL,
 CONSTRAINT [PK_det_factura_1] PRIMARY KEY CLUSTERED 
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[detalle_receta]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalle_receta](
	[id_receta] [int] NOT NULL,
	[id_producto] [int] NOT NULL,
	[porcion] [numeric](18, 0) NOT NULL,
	[id_unidad] [int] NOT NULL,
 CONSTRAINT [PK_detalle_receta] PRIMARY KEY CLUSTERED 
(
	[id_receta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[empleado]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[empleado](
	[ficha] [int] NOT NULL,
	[id_persona] [int] NOT NULL,
	[fecha_entrda] [date] NOT NULL,
	[fecha_salida] [date] NULL,
	[id_cargo] [int] NOT NULL,
	[id_departamento] [int] NOT NULL,
	[id_pago] [int] NOT NULL,
	[salario] [decimal](12, 2) NOT NULL,
	[estado] [bit] NULL,
	[foto] [varchar](2000) NULL,
	[edad] [int] NULL,
	[sexo] [varchar](1) NULL,
	[id_nacionalidad] [int] NULL,
	[id_pais] [int] NULL,
	[nss] [varchar](20) NULL,
 CONSTRAINT [PK_empleado] PRIMARY KEY CLUSTERED 
(
	[ficha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[empresa]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[empresa](
	[id_empresa] [int] NOT NULL,
	[empresa] [varchar](50) NOT NULL,
	[id_ciudad] [int] NOT NULL,
	[fecha_creacion] [date] NULL,
	[cedula] [varchar](11) NULL,
	[rnc] [varchar](9) NULL,
	[contacto] [varchar](50) NULL,
	[correo] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[direccion] [varchar](200) NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_empresa] PRIMARY KEY CLUSTERED 
(
	[id_empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[enc_apertura_caja]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[enc_apertura_caja](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha_inicio] [datetime] NOT NULL,
	[fecha_final] [datetime] NULL,
	[id_moneda] [int] NOT NULL,
	[monto_inicial] [decimal](15, 2) NOT NULL,
	[monto_final] [decimal](15, 2) NOT NULL,
	[id_caja] [int] NOT NULL,
	[id_usuario] [int] NULL,
	[desactivado] [varchar](1) NOT NULL,
	[estado] [varchar](1) NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
 CONSTRAINT [PK_enc_apertura_caja] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[enc_cierre_caja]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[enc_cierre_caja](
	[id] [int] NOT NULL,
	[fecha_apertura] [datetime] NOT NULL,
	[fecha_cierre] [datetime] NULL,
	[id_apertura] [int] NOT NULL,
	[monto_apertura_inicial] [decimal](15, 2) NOT NULL,
	[total_balance_caja] [decimal](15, 2) NOT NULL,
	[total_cobrado_caja] [decimal](15, 2) NOT NULL,
	[total_dolares] [decimal](15, 2) NOT NULL,
	[total_cobrado_dolares] [decimal](15, 2) NOT NULL,
	[nota] [varchar](max) NULL,
	[id_usuario] [int] NULL,
	[desactivado] [varchar](1) NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
 CONSTRAINT [PK_cierre_caja] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[enc_cobros]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[enc_cobros](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_apertura] [int] NOT NULL,
	[id_factura] [int] NULL,
	[monto] [decimal](15, 2) NOT NULL,
	[monto_pendiente] [decimal](15, 2) NOT NULL,
	[monto_devuelto] [decimal](15, 2) NOT NULL,
	[cuotas] [int] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_cobros] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[enc_factura]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[enc_factura](
	[id_factura] [int] IDENTITY(1,1) NOT NULL,
	[id_mesa] [int] NOT NULL,
	[id_cliente] [int] NULL,
	[id_sucursal] [int] NULL,
	[id_tipo_factura] [int] NULL,
	[fecha_factura] [date] NOT NULL,
	[rnc] [varchar](11) NULL,
	[id_tipo_ncf] [int] NULL,
	[ncf] [varchar](11) NULL,
	[itbis] [decimal](12, 2) NULL,
	[total] [decimal](12, 2) NULL,
	[porciento_ley] [decimal](12, 2) NULL,
	[sub_total] [decimal](12, 2) NULL,
	[total_descuento] [decimal](12, 2) NULL,
	[id_usuario] [int] NOT NULL,
	[nombre_cliente] [varchar](50) NULL,
	[direccion] [varchar](500) NULL,
	[telefono] [int] NULL,
	[dias_acuerdo] [int] NULL,
	[fecha_vencimiento_ncf] [date] NULL,
	[fecha_vencimiento_factura] [date] NULL,
	[nota] [varchar](500) NULL,
	[id_tipo_cobro] [int] NULL,
 CONSTRAINT [PK_enc_factura] PRIMARY KEY CLUSTERED 
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[estado_civil]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[estado_civil](
	[id_estado_civil] [int] IDENTITY(1,1) NOT NULL,
	[estado] [varchar](100) NULL,
	[fecha_creacion] [date] NULL,
	[id_usuario] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[factor_conversion]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[factor_conversion](
	[id_producto] [int] NOT NULL,
	[id_unidad] [int] NULL,
	[factor_venta] [decimal](12, 2) NULL,
	[factor_compra] [decimal](12, 2) NULL,
	[id_unidad_compra] [int] NULL,
	[id_unidad_venta] [int] NULL,
 CONSTRAINT [PK_factor_conversion] PRIMARY KEY CLUSTERED 
(
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[itbis]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[itbis](
	[id_itbis] [int] NOT NULL,
	[itbis] [decimal](12, 2) NULL,
	[fecha_creacion] [date] NULL,
	[desactivado] [varchar](1) NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_itbis] PRIMARY KEY CLUSTERED 
(
	[id_itbis] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[marcas]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[marcas](
	[id_marca] [int] NOT NULL,
	[marca] [varchar](50) NOT NULL,
 CONSTRAINT [PK_marcas] PRIMARY KEY CLUSTERED 
(
	[id_marca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mesa]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mesa](
	[id_mesa] [int] NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
	[estado] [char](1) NULL,
	[id_ubicacion] [int] NOT NULL,
 CONSTRAINT [PK_mesa] PRIMARY KEY CLUSTERED 
(
	[id_mesa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[monedas]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[monedas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[moneda] [varchar](250) NOT NULL,
	[abreviatura] [varchar](5) NULL,
	[tasa_del_dia] [decimal](12, 2) NOT NULL,
	[sentido_compra] [varchar](1) NOT NULL,
	[sentido_venta] [varchar](1) NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
	[id_usuario] [int] NULL,
	[desactivado] [varchar](1) NOT NULL,
 CONSTRAINT [PK_moneda] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[nacionalidad]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[nacionalidad](
	[id_pais] [int] NOT NULL,
	[gentilicio] [varchar](100) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[id_pais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[newtable]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[newtable](
	[id_secuencia] [int] IDENTITY(1,1) NOT NULL,
	[id_tipo_ncf] [int] NOT NULL,
	[letra] [varchar](1) NULL,
	[secuencia_inicial] [int] NULL,
	[secuencia_final] [int] NULL,
	[ultimo_generado] [int] NULL,
	[fecha_vencimiento] [date] NULL,
	[fecha_creacion] [date] NULL,
	[id_usuario] [int] NOT NULL,
	[estado] [varchar](1) NULL,
	[generado] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[paises]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[paises](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [int] NOT NULL,
	[iso3166a1] [varchar](2) NOT NULL,
	[iso3166a2] [varchar](3) NOT NULL,
	[descripcion] [varchar](128) NOT NULL,
	[desactivado] [varchar](1) NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_paises] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[persona]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[persona](
	[id_persona] [int] NOT NULL,
	[nombre1] [varchar](20) NOT NULL,
	[apellido1] [varchar](20) NULL,
	[apellido2] [varchar](20) NULL,
	[direccion] [varchar](100) NULL,
	[documento] [varchar](50) NOT NULL,
	[estado] [bit] NULL,
	[foto] [varchar](500) NULL,
	[telefono] [varchar](50) NULL,
	[correo] [varchar](80) NULL,
	[id_estado_civil] [int] NULL,
 CONSTRAINT [PK_persona] PRIMARY KEY CLUSTERED 
(
	[id_persona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[platos]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[platos](
	[id_plato] [int] NOT NULL,
	[plato] [varchar](250) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[precio] [numeric](12, 2) NOT NULL,
	[itbis] [numeric](12, 2) NULL,
	[id_receta] [int] NULL,
	[id_categoria] [int] NOT NULL,
	[id_itbis] [int] NULL,
 CONSTRAINT [PK_platos] PRIMARY KEY CLUSTERED 
(
	[id_plato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[productos]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[productos](
	[id_productos] [int] NOT NULL,
	[productos] [varchar](50) NOT NULL,
	[tipo_producto] [int] NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[id_unidad] [int] NOT NULL,
	[id_marca] [int] NULL,
	[estado] [bit] NULL,
	[codigo_interno] [int] NULL,
	[fecha_creacion] [date] NULL,
	[costo_promedio] [decimal](12, 2) NULL,
	[costo_comercial] [decimal](12, 2) NULL,
	[id_itbis] [int] NULL,
	[excento_itbis] [varchar](1) NULL,
 CONSTRAINT [PK_productos] PRIMARY KEY CLUSTERED 
(
	[id_productos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[proveedor]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedor](
	[id_proveedor] [int] NOT NULL,
	[id_persona] [int] NOT NULL,
	[rnc] [int] NULL,
 CONSTRAINT [PK_proveedor] PRIMARY KEY CLUSTERED 
(
	[id_proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[recetas]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[recetas](
	[id_receta] [int] NOT NULL,
	[receta] [varchar](100) NOT NULL,
	[descripcion] [varchar](1000) NULL,
	[categoria] [varchar](100) NULL,
	[comensales] [int] NOT NULL,
	[tiempo] [int] NOT NULL,
	[imagen] [varchar](500) NULL,
 CONSTRAINT [PK_recetas] PRIMARY KEY CLUSTERED 
(
	[id_receta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[secuencia_ncf]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[secuencia_ncf](
	[id_secuencia] [int] IDENTITY(1,1) NOT NULL,
	[id_tipo_ncf] [int] NOT NULL,
	[letra] [varchar](1) NULL,
	[secuencia_inicial] [int] NULL,
	[secuencia_final] [int] NULL,
	[ultimo_generado] [int] NULL,
	[fecha_vencimiento] [date] NULL,
	[fecha_creacion] [date] NULL,
	[id_usuario] [int] NOT NULL,
	[desactivado] [varchar](1) NOT NULL,
 CONSTRAINT [PK_secuencia_ncf] PRIMARY KEY CLUSTERED 
(
	[id_secuencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sub_categoria]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sub_categoria](
	[ID_SUB_CATEGORIA] [int] NOT NULL,
	[DESCRIPCION] [varchar](100) NULL,
	[ID_CATEGORIA] [int] NOT NULL,
 CONSTRAINT [PK_SUB_CATEGORIA] PRIMARY KEY CLUSTERED 
(
	[ID_SUB_CATEGORIA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sucursal]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sucursal](
	[id_sucursal] [int] NOT NULL,
	[sucursal] [varchar](50) NOT NULL,
	[id_empresa] [int] NOT NULL,
 CONSTRAINT [PK_sucursal] PRIMARY KEY CLUSTERED 
(
	[id_sucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[temp_det_factura]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[temp_det_factura](
	[id_factura] [int] NOT NULL,
	[id_plato] [int] NOT NULL,
	[cantidad] [numeric](18, 2) NOT NULL,
	[precio] [numeric](10, 2) NOT NULL,
	[itbis] [decimal](20, 2) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[temp_enc_factura]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[temp_enc_factura](
	[id_factura] [int] NOT NULL,
	[id_mesa] [nchar](10) NULL,
	[fecha_factura] [date] NOT NULL,
	[id_cliente] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[rnc] [int] NULL,
	[itbis] [numeric](12, 2) NULL,
	[total] [numeric](12, 2) NULL,
	[porciento_ley] [numeric](12, 0) NULL,
	[sub_total] [numeric](12, 2) NULL,
	[estado] [varchar](1) NULL,
	[nota] [varchar](250) NULL,
 CONSTRAINT [PK_temp_enc_factura] PRIMARY KEY CLUSTERED 
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipo_]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipo_](
	[id_producto] [int] NULL,
	[descripcion] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipo_cliente]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipo_cliente](
	[id_tipo] [int] NOT NULL,
	[descripcion] [varchar](100) NULL,
	[estado] [varchar](1) NULL,
	[fecha_creacion] [date] NULL,
	[id_usuario] [int] NULL,
 CONSTRAINT [PK_tipo_cliente] PRIMARY KEY CLUSTERED 
(
	[id_tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipo_cobro]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipo_cobro](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[desactivado] [varchar](1) NOT NULL,
	[id_usuario] [int] NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
 CONSTRAINT [PK_tipo_cobro] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipo_factura]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipo_factura](
	[id_tipo_factura] [int] NOT NULL,
	[tipo_factura] [varchar](50) NULL,
 CONSTRAINT [PK_tipo_factura] PRIMARY KEY CLUSTERED 
(
	[id_tipo_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipo_ncf]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipo_ncf](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [int] NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[desactivado] [varchar](1) NOT NULL,
	[facturable] [varchar](1) NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[fecha_actualizacion] [datetime] NULL,
	[id_usuario] [int] NULL,
	[nota] [varchar](500) NULL,
 CONSTRAINT [PK_tipo_ncf] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipo_pago]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipo_pago](
	[id_pago] [int] NOT NULL,
	[tipo_pago] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tipo_pago] PRIMARY KEY CLUSTERED 
(
	[id_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipo_platos]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipo_platos](
	[id_tipo_plato] [int] NOT NULL,
	[tipo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tipo_platos] PRIMARY KEY CLUSTERED 
(
	[id_tipo_plato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipo_producto]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipo_producto](
	[id_tipo] [int] NOT NULL,
	[nombre_tipo] [varchar](50) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tipo_producto] PRIMARY KEY CLUSTERED 
(
	[id_tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ubicacion]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ubicacion](
	[id_ubicacion] [int] NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[estado] [char](1) NULL,
 CONSTRAINT [PK_ubicacion] PRIMARY KEY CLUSTERED 
(
	[id_ubicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[unidad]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[unidad](
	[id_unidad] [int] NOT NULL,
	[unidad] [varchar](50) NOT NULL,
 CONSTRAINT [PK_unidad] PRIMARY KEY CLUSTERED 
(
	[id_unidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[unidad_medida]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[unidad_medida](
	[id_unidad] [int] NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_unidad_medida] PRIMARY KEY CLUSTERED 
(
	[id_unidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuarios](
	[id_usuario] [int] NOT NULL,
	[usuario] [varchar](50) NOT NULL,
	[pass] [varchar](50) NOT NULL,
	[fecha_creacion] [date] NOT NULL,
	[id_persona] [int] NOT NULL,
	[ficha] [int] NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[GetSecuenciaNCF]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetSecuenciaNCF](@tipo int)
RETURNS table

return (  select top 1 *, (COALESCE(ultimo_generado, secuencia_inicial) + case when ultimo_generado is null then 0 else 1 end) proximo_generado
			from secuencia_ncf 
		   where id_tipo_ncf = @tipo
			And secuencia_final >= COALESCE(ultimo_generado + case when ultimo_generado is null then 0 else 1 end, secuencia_inicial) 
            And secuencia_inicial <= COALESCE(ultimo_generado + case when ultimo_generado is null then 0 else 1 end, secuencia_inicial) 
			And (fecha_vencimiento>= getdate() or fecha_vencimiento is null)
			And desactivado = 'N'
			order by 1 asc);
GO
/****** Object:  View [dbo].[vClientes]    Script Date: 12/6/2020 3:34:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vClientes]
AS
SELECT        dbo.condiciones_pago.id_condicion_pago, dbo.condiciones_pago.descripcion, dbo.condiciones_pago.dias, dbo.cliente.id_cliente, dbo.cliente.id_persona, dbo.cliente.rnc, dbo.cliente.id_condicion, dbo.cliente.excento_itbis, 
                         dbo.cliente.id_tipo_ncf, dbo.cliente.nota, dbo.cliente.limite_credito, dbo.cliente.porcentaje_mora, dbo.cliente.porcentaje_descuento, dbo.persona.nombre1, dbo.persona.apellido2, dbo.persona.apellido1, dbo.persona.direccion, 
                         dbo.persona.documento, dbo.persona.estado, dbo.persona.foto, dbo.persona.telefono
FROM            dbo.condiciones_pago LEFT OUTER JOIN
                         dbo.cliente ON dbo.condiciones_pago.id_condicion_pago = dbo.cliente.id_condicion LEFT OUTER JOIN
                         dbo.persona ON dbo.cliente.id_persona = dbo.persona.id_persona

GO
INSERT [dbo].[cajas] ([id], [caja], [id_usuario], [fecha_creacion], [fecha_actualizacion], [desactivado]) VALUES (1, N'Caja 1', 1, CAST(0x0000AC8200000000 AS DateTime), NULL, N'N')
INSERT [dbo].[cajas] ([id], [caja], [id_usuario], [fecha_creacion], [fecha_actualizacion], [desactivado]) VALUES (2, N'Caja 2', 1, CAST(0x0000AC8200000000 AS DateTime), NULL, N'N')
INSERT [dbo].[cajas] ([id], [caja], [id_usuario], [fecha_creacion], [fecha_actualizacion], [desactivado]) VALUES (3, N'Caja 3 ', 1, CAST(0x0000AC8200000000 AS DateTime), NULL, N'N')
INSERT [dbo].[cargo] ([id_cargo], [cargo], [id_departamento]) VALUES (1, N'Cajero', 2)
INSERT [dbo].[categoria] ([ID_CATEGORIA], [DESCRIPCION]) VALUES (1, N'Carnes')
INSERT [dbo].[categoria] ([ID_CATEGORIA], [DESCRIPCION]) VALUES (2, N'Pescado')
INSERT [dbo].[categoria] ([ID_CATEGORIA], [DESCRIPCION]) VALUES (3, N'Pasta')
INSERT [dbo].[categoria] ([ID_CATEGORIA], [DESCRIPCION]) VALUES (4, N'Bebidas')
INSERT [dbo].[categoria] ([ID_CATEGORIA], [DESCRIPCION]) VALUES (5, N'Ensaladas')
INSERT [dbo].[categoria] ([ID_CATEGORIA], [DESCRIPCION]) VALUES (6, N'Marisacos')
INSERT [dbo].[categoria] ([ID_CATEGORIA], [DESCRIPCION]) VALUES (7, N'Entradas')
INSERT [dbo].[categoria] ([ID_CATEGORIA], [DESCRIPCION]) VALUES (8, N'Caldos')
INSERT [dbo].[categoria] ([ID_CATEGORIA], [DESCRIPCION]) VALUES (9, N'Postres')
INSERT [dbo].[ciudad] ([id_ciudad], [ciudad]) VALUES (1, N'Santiago')
INSERT [dbo].[cliente] ([id_cliente], [id_persona], [rnc], [id_condicion], [excento_itbis], [id_tipo_ncf], [nota], [limite_credito], [porcentaje_mora], [porcentaje_descuento], [estado], [credito_activo]) VALUES (1, 2, NULL, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[cliente] ([id_cliente], [id_persona], [rnc], [id_condicion], [excento_itbis], [id_tipo_ncf], [nota], [limite_credito], [porcentaje_mora], [porcentaje_descuento], [estado], [credito_activo]) VALUES (6, 6, NULL, 1, NULL, 2, NULL, NULL, NULL, NULL, 1, NULL)
INSERT [dbo].[cliente] ([id_cliente], [id_persona], [rnc], [id_condicion], [excento_itbis], [id_tipo_ncf], [nota], [limite_credito], [porcentaje_mora], [porcentaje_descuento], [estado], [credito_activo]) VALUES (7, 7, N'164646669', 1, 0, 1, N'', CAST(20000.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), 1, 0)
INSERT [dbo].[cliente] ([id_cliente], [id_persona], [rnc], [id_condicion], [excento_itbis], [id_tipo_ncf], [nota], [limite_credito], [porcentaje_mora], [porcentaje_descuento], [estado], [credito_activo]) VALUES (8, 8, N'', 1, 0, 1, N'', CAST(20000.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), 1, 1)
INSERT [dbo].[cliente] ([id_cliente], [id_persona], [rnc], [id_condicion], [excento_itbis], [id_tipo_ncf], [nota], [limite_credito], [porcentaje_mora], [porcentaje_descuento], [estado], [credito_activo]) VALUES (9, 9, N'', 1, 0, 1, N'', CAST(20000.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), 1, 1)
INSERT [dbo].[cliente] ([id_cliente], [id_persona], [rnc], [id_condicion], [excento_itbis], [id_tipo_ncf], [nota], [limite_credito], [porcentaje_mora], [porcentaje_descuento], [estado], [credito_activo]) VALUES (10, 10, N'', 1, 0, 1, N'', CAST(20000.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), 1, 1)
INSERT [dbo].[cliente] ([id_cliente], [id_persona], [rnc], [id_condicion], [excento_itbis], [id_tipo_ncf], [nota], [limite_credito], [porcentaje_mora], [porcentaje_descuento], [estado], [credito_activo]) VALUES (11, 11, N'', 1, 1, 1, N'', CAST(20000.00 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), CAST(0.10 AS Decimal(12, 2)), 1, 1)
INSERT [dbo].[condiciones_pago] ([id_condicion_pago], [descripcion], [dias], [desactivado], [fecha_creacion], [id_usuario]) VALUES (1, N'Instante', 0, NULL, CAST(0xCC410B00 AS Date), 1)
INSERT [dbo].[condiciones_pago] ([id_condicion_pago], [descripcion], [dias], [desactivado], [fecha_creacion], [id_usuario]) VALUES (2, N'Condicion 30 dias', 30, NULL, CAST(0xCC410B00 AS Date), 1)
SET IDENTITY_INSERT [dbo].[denominaciones] ON 

INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (2, N'2,000 X', CAST(2000.00 AS Decimal(15, 2)), N'N', N'S', CAST(0x0000AC80017BCFFE AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (3, N'1,000 X', CAST(1000.00 AS Decimal(15, 2)), N'N', N'S', CAST(0x0000AC80017BCFFE AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (4, N'500 X', CAST(500.00 AS Decimal(15, 2)), N'N', N'S', CAST(0x0000AC80017BCFFE AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (5, N'200 X', CAST(200.00 AS Decimal(15, 2)), N'N', N'S', CAST(0x0000AC80017BCFFE AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (6, N'100 X', CAST(100.00 AS Decimal(15, 2)), N'N', N'S', CAST(0x0000AC80017BCFFE AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (7, N'50 X', CAST(50.00 AS Decimal(15, 2)), N'N', N'S', CAST(0x0000AC80017BCFFE AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (8, N'25 X', CAST(25.00 AS Decimal(15, 2)), N'N', N'S', CAST(0x0000AC80017BCFFE AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (9, N'20 X', CAST(20.00 AS Decimal(15, 2)), N'N', N'S', CAST(0x0000AC80017BCFFE AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (10, N'10 X', CAST(10.00 AS Decimal(15, 2)), N'N', N'S', CAST(0x0000AC80017BCFFF AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (11, N'5 X', CAST(5.00 AS Decimal(15, 2)), N'N', N'S', CAST(0x0000AC80017BCFFF AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (12, N'1 X', CAST(1.00 AS Decimal(15, 2)), N'N', N'S', CAST(0x0000AC80017BCFFF AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (13, N'TARJETA', CAST(1.00 AS Decimal(15, 2)), N'N', N'N', CAST(0x0000AC80017BCFFF AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (14, N'CHEQUE', CAST(1.00 AS Decimal(15, 2)), N'N', N'N', CAST(0x0000AC80017BCFFF AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (15, N'NOTA DE CREDITO', CAST(1.00 AS Decimal(15, 2)), N'N', N'N', CAST(0x0000AC80017BCFFF AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (16, N'DEPOSITO', CAST(1.00 AS Decimal(15, 2)), N'N', N'N', CAST(0x0000AC80017BCFFF AS DateTime), NULL, 2)
INSERT [dbo].[denominaciones] ([id], [descripcion], [valor], [desactivado], [moneda], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (17, N'DOLLAR', CAST(1.00 AS Decimal(15, 2)), N'N', N'N', CAST(0x0000AC80017BCFFF AS DateTime), NULL, 2)
SET IDENTITY_INSERT [dbo].[denominaciones] OFF
INSERT [dbo].[departamento] ([id_departamento], [departamento]) VALUES (1, N'Compras')
INSERT [dbo].[departamento] ([id_departamento], [departamento]) VALUES (2, N'Caja')
INSERT [dbo].[departamento] ([id_departamento], [departamento]) VALUES (3, N'Gestion Humana')
INSERT [dbo].[departamento] ([id_departamento], [departamento]) VALUES (4, N'Contabilidad')
INSERT [dbo].[departamento] ([id_departamento], [departamento]) VALUES (5, N'Control Calidad')
INSERT [dbo].[departamento] ([id_departamento], [departamento]) VALUES (6, N'Ventas Externas')
INSERT [dbo].[departamento] ([id_departamento], [departamento]) VALUES (7, N'Contraloria')
INSERT [dbo].[departamento] ([id_departamento], [departamento]) VALUES (8, N'Despacho')
INSERT [dbo].[departamento] ([id_departamento], [departamento]) VALUES (9, N'Recepcion')
INSERT [dbo].[departamento] ([id_departamento], [departamento]) VALUES (10, N'Tecnologia')
SET IDENTITY_INSERT [dbo].[det_apertura_caja] ON 

INSERT [dbo].[det_apertura_caja] ([id], [id_apertura_caja], [id_denominacion], [valor], [cantidad]) VALUES (4, 8, 2, CAST(2000.00 AS Decimal(15, 2)), CAST(20.00 AS Decimal(15, 2)))
SET IDENTITY_INSERT [dbo].[det_apertura_caja] OFF
INSERT [dbo].[detalle_receta] ([id_receta], [id_producto], [porcion], [id_unidad]) VALUES (2, 1, CAST(5 AS Numeric(18, 0)), 3)
INSERT [dbo].[detalle_receta] ([id_receta], [id_producto], [porcion], [id_unidad]) VALUES (6, 2, CAST(10 AS Numeric(18, 0)), 3)
INSERT [dbo].[empleado] ([ficha], [id_persona], [fecha_entrda], [fecha_salida], [id_cargo], [id_departamento], [id_pago], [salario], [estado], [foto], [edad], [sexo], [id_nacionalidad], [id_pais], [nss]) VALUES (11246, 1, CAST(0xE33F0B00 AS Date), NULL, 1, 1, 1, CAST(20000.00 AS Decimal(12, 2)), 1, NULL, 25, N'M', NULL, NULL, NULL)
INSERT [dbo].[empleado] ([ficha], [id_persona], [fecha_entrda], [fecha_salida], [id_cargo], [id_departamento], [id_pago], [salario], [estado], [foto], [edad], [sexo], [id_nacionalidad], [id_pais], [nss]) VALUES (11247, 2, CAST(0x05410B00 AS Date), CAST(0x05410B00 AS Date), 1, 1, 1, CAST(15000.00 AS Decimal(12, 2)), 1, NULL, 25, N'M', NULL, NULL, NULL)
INSERT [dbo].[empleado] ([ficha], [id_persona], [fecha_entrda], [fecha_salida], [id_cargo], [id_departamento], [id_pago], [salario], [estado], [foto], [edad], [sexo], [id_nacionalidad], [id_pais], [nss]) VALUES (11249, 4, CAST(0xDC410B00 AS Date), CAST(0xDC410B00 AS Date), 1, 2, 2, CAST(15000.00 AS Decimal(12, 2)), 1, NULL, 25, N'M', NULL, NULL, NULL)
INSERT [dbo].[empleado] ([ficha], [id_persona], [fecha_entrda], [fecha_salida], [id_cargo], [id_departamento], [id_pago], [salario], [estado], [foto], [edad], [sexo], [id_nacionalidad], [id_pais], [nss]) VALUES (11250, 5, CAST(0xDC410B00 AS Date), CAST(0x49430B00 AS Date), 1, 2, 2, CAST(30000.00 AS Decimal(12, 2)), 1, N'', 55, N'F', 62, 62, NULL)
INSERT [dbo].[empleado] ([ficha], [id_persona], [fecha_entrda], [fecha_salida], [id_cargo], [id_departamento], [id_pago], [salario], [estado], [foto], [edad], [sexo], [id_nacionalidad], [id_pais], [nss]) VALUES (11251, 6, CAST(0xDC410B00 AS Date), CAST(0xDC410B00 AS Date), 1, 2, 2, CAST(30000.00 AS Decimal(12, 2)), 1, N'C:\Users\marlon\Downloads\Scanbot Sep 15, 2019 9.43 PM.JPG', 61, N'F', 62, 62, N'55203056')
INSERT [dbo].[empresa] ([id_empresa], [empresa], [id_ciudad], [fecha_creacion], [cedula], [rnc], [contacto], [correo], [telefono], [direccion], [id_usuario]) VALUES (1, N'eFood', 1, CAST(0xE1410B00 AS Date), NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[enc_apertura_caja] ON 

INSERT [dbo].[enc_apertura_caja] ([id], [fecha_inicio], [fecha_final], [id_moneda], [monto_inicial], [monto_final], [id_caja], [id_usuario], [desactivado], [estado], [fecha_creacion], [fecha_actualizacion]) VALUES (8, CAST(0x0000AC8800000000 AS DateTime), CAST(0x0000AC8800000000 AS DateTime), 1, CAST(40000.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), 1, 1, N'N', N'A', CAST(0x0000AC8800000000 AS DateTime), NULL)
INSERT [dbo].[enc_apertura_caja] ([id], [fecha_inicio], [fecha_final], [id_moneda], [monto_inicial], [monto_final], [id_caja], [id_usuario], [desactivado], [estado], [fecha_creacion], [fecha_actualizacion]) VALUES (10, CAST(0x0000AC8900000000 AS DateTime), NULL, 1, CAST(40000.00 AS Decimal(15, 2)), CAST(0.00 AS Decimal(15, 2)), 1, 1, N'N', N'A', CAST(0x0000AC8900000000 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[enc_apertura_caja] OFF
SET IDENTITY_INSERT [dbo].[estado_civil] ON 

INSERT [dbo].[estado_civil] ([id_estado_civil], [estado], [fecha_creacion], [id_usuario]) VALUES (1, N'Soltero', CAST(0xDC410B00 AS Date), 1)
INSERT [dbo].[estado_civil] ([id_estado_civil], [estado], [fecha_creacion], [id_usuario]) VALUES (2, N'Casado', CAST(0xDC410B00 AS Date), 1)
INSERT [dbo].[estado_civil] ([id_estado_civil], [estado], [fecha_creacion], [id_usuario]) VALUES (3, N'Union Libre', CAST(0xDC410B00 AS Date), 1)
SET IDENTITY_INSERT [dbo].[estado_civil] OFF
INSERT [dbo].[itbis] ([id_itbis], [itbis], [fecha_creacion], [desactivado], [id_usuario]) VALUES (1, CAST(0.18 AS Decimal(12, 2)), CAST(0xCD410B00 AS Date), NULL, 2)
INSERT [dbo].[marcas] ([id_marca], [marca]) VALUES (1, N'Baldom')
INSERT [dbo].[marcas] ([id_marca], [marca]) VALUES (2, N'Linda')
INSERT [dbo].[marcas] ([id_marca], [marca]) VALUES (3, N'Princesa')
INSERT [dbo].[marcas] ([id_marca], [marca]) VALUES (4, N'Pinco')
INSERT [dbo].[marcas] ([id_marca], [marca]) VALUES (5, N'La Garza')
INSERT [dbo].[marcas] ([id_marca], [marca]) VALUES (6, N'Generica')
INSERT [dbo].[marcas] ([id_marca], [marca]) VALUES (7, N'Parma')
INSERT [dbo].[mesa] ([id_mesa], [descripcion], [estado], [id_ubicacion]) VALUES (1, N'MESA 1', N'O', 1)
INSERT [dbo].[mesa] ([id_mesa], [descripcion], [estado], [id_ubicacion]) VALUES (2, N'MESA 2', N'O', 1)
INSERT [dbo].[mesa] ([id_mesa], [descripcion], [estado], [id_ubicacion]) VALUES (3, N'MESA 3', N'O', 1)
INSERT [dbo].[mesa] ([id_mesa], [descripcion], [estado], [id_ubicacion]) VALUES (4, N'MESA 4', N'O', 2)
INSERT [dbo].[mesa] ([id_mesa], [descripcion], [estado], [id_ubicacion]) VALUES (5, N'MESA 5', N'O', 2)
INSERT [dbo].[mesa] ([id_mesa], [descripcion], [estado], [id_ubicacion]) VALUES (6, N'MESA 6', N'D', 2)
INSERT [dbo].[mesa] ([id_mesa], [descripcion], [estado], [id_ubicacion]) VALUES (7, N'MESA 7', N'D', 2)
INSERT [dbo].[mesa] ([id_mesa], [descripcion], [estado], [id_ubicacion]) VALUES (8, N'MESA 8', N'D', 3)
INSERT [dbo].[mesa] ([id_mesa], [descripcion], [estado], [id_ubicacion]) VALUES (9, N'MESA 9', N'D', 3)
INSERT [dbo].[mesa] ([id_mesa], [descripcion], [estado], [id_ubicacion]) VALUES (10, N'MESA 10', N'D', 3)
SET IDENTITY_INSERT [dbo].[monedas] ON 

INSERT [dbo].[monedas] ([id], [moneda], [abreviatura], [tasa_del_dia], [sentido_compra], [sentido_venta], [fecha_creacion], [fecha_actualizacion], [id_usuario], [desactivado]) VALUES (1, N'Peso Dominicano', N'DOP', CAST(1.00 AS Decimal(12, 2)), N'M', N'D', CAST(0x0000AC8400000000 AS DateTime), NULL, 1, N'N')
INSERT [dbo].[monedas] ([id], [moneda], [abreviatura], [tasa_del_dia], [sentido_compra], [sentido_venta], [fecha_creacion], [fecha_actualizacion], [id_usuario], [desactivado]) VALUES (2, N'Dolar Americano', N'USD', CAST(58.40 AS Decimal(12, 2)), N'M', N'D', CAST(0x0000AC8800000000 AS DateTime), NULL, 1, N'N')
SET IDENTITY_INSERT [dbo].[monedas] OFF
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (1, N'AFGANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (3, N'ALBANESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (4, N'ALEMANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (5, N'ANDORRANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (6, N'ANGOLEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (9, N'ANTIGUANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (11, N'SAUDI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (12, N'ARGELINA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (13, N'ARGENTINA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (14, N'ARMENIA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (15, N'ARUBEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (16, N'AUSTRALIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (17, N'AUSTRIACA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (18, N'AZERBAIYANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (19, N'BAHAMEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (20, N'BAREINI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (21, N'BANGLADESI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (22, N'BARBADENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (23, N'BIELORRUSA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (24, N'BELGA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (25, N'BELICEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (26, N'BENINESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (29, N'BOLIVIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (30, N'BOSNIA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (31, N'BOTSUANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (33, N'BRASILEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (34, N'BRUNEANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (35, N'BULGARA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (36, N'BURKINES')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (37, N'BURUNDESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (38, N'CABOVERDIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (40, N'CAMBOYANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (41, N'CAMERUNESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (42, N'CANADIENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (43, N'CENTROAFRICANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (44, N'CHADIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (45, N'CHECA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (46, N'CHILENA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (47, N'CHINA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (48, N'CHIPRIOTA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (49, N'CHRISTMAS ISLANDER')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (50, N'VATICANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (52, N'COLOMBIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (53, N'COMORENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (54, N'CONGOLEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (55, N'CONGOLEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (56, N'COOKIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (57, N'NORCOREANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (58, N'SURCOREANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (59, N'MARFILEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (60, N'COSTARRICENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (61, N'CROATA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (62, N'CUBANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (63, N'DANESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (64, N'DOMINIQUES')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (65, N'DOMINICANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (66, N'ECUATORIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (67, N'EGIPCIA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (68, N'SALVADOREÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (69, N'EMIRATI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (70, N'ERITREA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (71, N'ESLOVACA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (73, N'ESPAÑOLA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (75, N'ESTADOUNIDENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (76, N'ESTONIA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (77, N'ETIOPE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (79, N'FILIPINA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (80, N'FINLANDESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (81, N'FIYIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (82, N'FRANCESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (83, N'GABONESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (84, N'GAMBIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (85, N'GEORGIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (87, N'GHANESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (88, N'GIBRALTAREÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (89, N'GRANADINA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (91, N'GROENLANDESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (94, N'GUATEMALTECA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (96, N'GUINEANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (97, N'ECUATOGUINEANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (98, N'GUINEANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (99, N'GUYANESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (100, N'HAITIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (102, N'HONDUREÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (104, N'HUNGARA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (105, N'HINDU')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (106, N'INDONESIA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (107, N'IRANI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (108, N'IRAQUI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (109, N'IRLANDESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (110, N'ISLANDESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (111, N'ISRAELI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (112, N'ITALIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (113, N'JAMAIQUINA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (114, N'JAPONESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (115, N'JORDANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (116, N'KAZAJA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (117, N'KENIATA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (118, N'KIRGUISA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (119, N'KIRIBATIANA')
GO
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (120, N'KUWAITI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (121, N'LAOSIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (122, N'LESOTENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (123, N'LETONA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (124, N'LIBANESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (125, N'LIBERIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (126, N'LIBIA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (127, N'LIECHTENSTEINIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (128, N'LITUANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (129, N'LUXEMBURGUESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (131, N'MACEDONIA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (132, N'MALGACHE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (133, N'MALASIA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (134, N'MALAUI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (135, N'MALDIVA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (136, N'MALIENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (137, N'MALTESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (140, N'MARROQUI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (141, N'MARSHALESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (142, N'MARTINIQUES')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (143, N'MAURICIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (144, N'MAURITANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (146, N'MEXICANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (147, N'MICRONESIA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (148, N'MOLDAVA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (149, N'MONEGASCA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (150, N'MONGOLA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (152, N'MOZAMBIQUEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (153, N'BIRMANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (154, N'NAMIBIA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (155, N'NAURUANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (156, N'NEPALI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (157, N'NICARAGÜENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (158, N'NIGERINA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (159, N'NIGERIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (162, N'NORUEGA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (164, N'NEOZELANDESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (165, N'OMANI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (166, N'NEERLANDESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (167, N'PAKISTANI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (168, N'PALAUANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (169, N'PALESTINA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (170, N'PANAMEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (171, N'PAPU')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (172, N'PARAGUAYA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (173, N'PERUANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (176, N'POLACA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (177, N'PORTUGUESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (178, N'PUERTORRIQUEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (179, N'CATARI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (180, N'BRITANICA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (182, N'RUANDESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (183, N'RUMANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (184, N'RUSA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (186, N'SALOMONENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (187, N'SAMOANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (189, N'CRISTOBALEÑA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (190, N'SANMARINENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (192, N'SANVICENTINA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (193, N'GRIEGA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (194, N'SANTALUCENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (195, N'SANTOTOMENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (196, N'SENEGALESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (197, N'MONTENEGRINA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (198, N'SEYCHELLENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (199, N'SIERRALEONESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (200, N'SINGAPURENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (201, N'SIRIA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (202, N'SOMALI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (203, N'CEILANESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (204, N'SUAZI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (205, N'SUDAFRICANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (206, N'SUDANDESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (207, N'SUECA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (208, N'SUIZA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (209, N'SURINAMESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (211, N'TAILANDESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (213, N'TANZANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (214, N'TAYIKA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (217, N'TIMORENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (218, N'TOGOLESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (220, N'TONGANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (221, N'TRINITENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (222, N'TUNECINA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (223, N'TURCA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (224, N'TURCOMANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (226, N'TUVALUANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (227, N'UCRANIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (228, N'UGANDESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (229, N'URUGUAYA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (230, N'UZBEKA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (231, N'VANUATUENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (232, N'VENEZOLANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (233, N'VIETNAMITA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (237, N'YEMENI')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (238, N'YIBUTIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (239, N'ZAMBIANA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (240, N'ZIMBABUENSE')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (241, N'SURSUDANESA')
INSERT [dbo].[nacionalidad] ([id_pais], [gentilicio]) VALUES (242, N'BUTANESA')
GO
SET IDENTITY_INSERT [dbo].[newtable] ON 

INSERT [dbo].[newtable] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [estado], [generado]) VALUES (15, 1, N'A', 40, 50, NULL, NULL, CAST(0xD5410B00 AS Date), 2, N'A', 40)
INSERT [dbo].[newtable] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [estado], [generado]) VALUES (19, 1, N'A', 80, 85, NULL, NULL, CAST(0xD6410B00 AS Date), 2, N'A', 80)
INSERT [dbo].[newtable] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [estado], [generado]) VALUES (21, 1, N'A', 82, 84, NULL, NULL, CAST(0xD6410B00 AS Date), 2, N'A', 82)
SET IDENTITY_INSERT [dbo].[newtable] OFF
SET IDENTITY_INSERT [dbo].[paises] ON 

INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (1, 4, N'AF', N'AFG', N'Afganistan', N'N', CAST(0x0000AC400184F517 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (2, 248, N'AX', N'ALA', N'Islas Gland', N'N', CAST(0x0000AC400184F517 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (3, 8, N'AL', N'ALB', N'Albania', N'N', CAST(0x0000AC400184F517 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (4, 276, N'DE', N'DEU', N'Alemania', N'N', CAST(0x0000AC400184F517 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (5, 20, N'AD', N'AND', N'Andorra', N'N', CAST(0x0000AC400184F518 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (6, 24, N'AO', N'AGO', N'Angola', N'N', CAST(0x0000AC400184F518 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (7, 660, N'AI', N'AIA', N'Anguilla', N'N', CAST(0x0000AC400184F518 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (8, 10, N'AQ', N'ATA', N'Antartida', N'N', CAST(0x0000AC400184F518 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (9, 28, N'AG', N'ATG', N'Antigua y Barbuda', N'N', CAST(0x0000AC400184F518 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (10, 530, N'AN', N'ANT', N'Antillas Holandesas', N'N', CAST(0x0000AC400184F519 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (11, 682, N'SA', N'SAU', N'Arabia Saudi', N'N', CAST(0x0000AC400184F519 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (12, 12, N'DZ', N'DZA', N'Argelia', N'N', CAST(0x0000AC400184F519 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (13, 32, N'AR', N'ARG', N'Argentina', N'N', CAST(0x0000AC400184F519 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (14, 51, N'AM', N'ARM', N'Armenia', N'N', CAST(0x0000AC400184F519 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (15, 533, N'AW', N'ABW', N'Aruba', N'N', CAST(0x0000AC400184F519 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (16, 36, N'AU', N'AUS', N'Australia', N'N', CAST(0x0000AC400184F519 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (17, 40, N'AT', N'AUT', N'Austria', N'N', CAST(0x0000AC400184F519 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (18, 31, N'AZ', N'AZE', N'Azerbaiyan', N'N', CAST(0x0000AC400184F519 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (19, 44, N'BS', N'BHS', N'Bahamas', N'N', CAST(0x0000AC400184F519 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (20, 48, N'BH', N'BHR', N'Bahrein', N'N', CAST(0x0000AC400184F519 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (21, 50, N'BD', N'BGD', N'Bangladesh', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (22, 52, N'BB', N'BRB', N'Barbados', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (23, 112, N'BY', N'BLR', N'Bielorrusia', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (24, 56, N'BE', N'BEL', N'Belgica', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (25, 84, N'BZ', N'BLZ', N'Belice', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (26, 204, N'BJ', N'BEN', N'Benin', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (27, 60, N'BM', N'BMU', N'Bermudas', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (28, 64, N'BT', N'BTN', N'Bhutan', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (29, 68, N'BO', N'BOL', N'Bolivia', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (30, 70, N'BA', N'BIH', N'Bosnia y Herzegovina', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (31, 72, N'BW', N'BWA', N'Botsuana', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (32, 74, N'BV', N'BVT', N'Isla Bouvet', N'N', CAST(0x0000AC400184F51A AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (33, 76, N'BR', N'BRA', N'Brasil', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (34, 96, N'BN', N'BRN', N'Brunei', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (35, 100, N'BG', N'BGR', N'Bulgaria', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (36, 854, N'BF', N'BFA', N'Burkina Faso', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (37, 108, N'BI', N'BDI', N'Burundi', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (38, 132, N'CV', N'CPV', N'Cabo Verde', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (39, 136, N'KY', N'CYM', N'Islas Caiman', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (40, 116, N'KH', N'KHM', N'Camboya', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (41, 120, N'CM', N'CMR', N'Camerun', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (42, 124, N'CA', N'CAN', N'Canada', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (43, 140, N'CF', N'CAF', N'Republica Centroafricana', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (44, 148, N'TD', N'TCD', N'Chad', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (45, 203, N'CZ', N'CZE', N'Republica Checa', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (46, 152, N'CL', N'CHL', N'Chile', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (47, 156, N'CN', N'CHN', N'China', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (48, 196, N'CY', N'CYP', N'Chipre', N'N', CAST(0x0000AC400184F51B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (49, 162, N'CX', N'CXR', N'Isla de Navidad', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (50, 336, N'VA', N'VAT', N'Ciudad del Vaticano', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (51, 166, N'CC', N'CCK', N'Islas Cocos', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (52, 170, N'CO', N'COL', N'Colombia', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (53, 174, N'KM', N'COM', N'Comoras', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (54, 180, N'CD', N'COD', N'Republica Democratica del Congo', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (55, 178, N'CG', N'COG', N'Congo', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (56, 184, N'CK', N'COK', N'Islas Cook', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (57, 408, N'KP', N'PRK', N'Corea del Norte', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (58, 410, N'KR', N'KOR', N'Corea del Sur', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (59, 384, N'CI', N'CIV', N'Costa de Marfil', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (60, 188, N'CR', N'CRI', N'Costa Rica', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (61, 191, N'HR', N'HRV', N'Croacia', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (62, 192, N'CU', N'CUB', N'Cuba', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (63, 208, N'DK', N'DNK', N'Dinamarca', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (64, 212, N'DM', N'DMA', N'Dominica', N'N', CAST(0x0000AC400184F51C AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (65, 214, N'DO', N'DOM', N'Republica Dominicana', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (66, 218, N'EC', N'ECU', N'Ecuador', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (67, 818, N'EG', N'EGY', N'Egipto', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (68, 222, N'SV', N'SLV', N'El Salvador', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (69, 784, N'AE', N'ARE', N'Emiratos arabes Unidos', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (70, 232, N'ER', N'ERI', N'Eritrea', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (71, 703, N'SK', N'SVK', N'Eslovaquia', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (72, 705, N'SI', N'SVN', N'Eslovenia', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (73, 724, N'ES', N'ESP', N'España', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (74, 581, N'UM', N'UMI', N'Islas ultramarinas de Estados Unidos', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (75, 840, N'US', N'USA', N'Estados Unidos', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (76, 233, N'EE', N'EST', N'Estonia', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (77, 231, N'ET', N'ETH', N'Etiopia', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (78, 234, N'FO', N'FRO', N'Islas Feroe', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (79, 608, N'PH', N'PHL', N'Filipinas', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (80, 246, N'FI', N'FIN', N'Finlandia', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (81, 242, N'FJ', N'FJI', N'Fiyi', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (82, 250, N'FR', N'FRA', N'Francia', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (83, 266, N'GA', N'GAB', N'Gabon', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (84, 270, N'GM', N'GMB', N'Gambia', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (85, 268, N'GE', N'GEO', N'Georgia', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (86, 239, N'GS', N'SGS', N'Islas Georgias del Sur y Sandwich del Sur', N'N', CAST(0x0000AC400184F51D AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (87, 288, N'GH', N'GHA', N'Ghana', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (88, 292, N'GI', N'GIB', N'Gibraltar', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (89, 308, N'GD', N'GRD', N'Granada', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (90, 300, N'GR', N'GRC', N'Grecia', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (91, 304, N'GL', N'GRL', N'Groenlandia', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (92, 312, N'GP', N'GLP', N'Guadalupe', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (93, 316, N'GU', N'GUM', N'Guam', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (94, 320, N'GT', N'GTM', N'Guatemala', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (95, 254, N'GF', N'GUF', N'Guayana Francesa', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (96, 324, N'GN', N'GIN', N'Guinea', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (97, 226, N'GQ', N'GNQ', N'Guinea Ecuatorial', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (98, 624, N'GW', N'GNB', N'Guinea-Bissau', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (99, 328, N'GY', N'GUY', N'Guyana', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
GO
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (100, 332, N'HT', N'HTI', N'Haiti', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (101, 334, N'HM', N'HMD', N'Islas Heard y McDonald', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (102, 340, N'HN', N'HND', N'Honduras', N'N', CAST(0x0000AC400184F51E AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (103, 344, N'HK', N'HKG', N'Hong Kong', N'N', CAST(0x0000AC400184F51F AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (104, 348, N'HU', N'HUN', N'Hungria', N'N', CAST(0x0000AC400184F51F AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (105, 356, N'IN', N'IND', N'India', N'N', CAST(0x0000AC400184F51F AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (106, 360, N'ID', N'IDN', N'Indonesia', N'N', CAST(0x0000AC400184F51F AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (107, 364, N'IR', N'IRN', N'Iran', N'N', CAST(0x0000AC400184F51F AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (108, 368, N'IQ', N'IRQ', N'Iraq', N'N', CAST(0x0000AC400184F51F AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (109, 372, N'IE', N'IRL', N'Irlanda', N'N', CAST(0x0000AC400184F51F AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (110, 352, N'IS', N'ISL', N'Islandia', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (111, 376, N'IL', N'ISR', N'Israel', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (112, 380, N'IT', N'ITA', N'Italia', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (113, 388, N'JM', N'JAM', N'Jamaica', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (114, 392, N'JP', N'JPN', N'Japon', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (115, 400, N'JO', N'JOR', N'Jordania', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (116, 398, N'KZ', N'KAZ', N'Kazajstan', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (117, 404, N'KE', N'KEN', N'Kenia', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (118, 417, N'KG', N'KGZ', N'Kirguistan', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (119, 296, N'KI', N'KIR', N'Kiribati', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (120, 414, N'KW', N'KWT', N'Kuwait', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (121, 418, N'LA', N'LAO', N'Laos', N'N', CAST(0x0000AC400184F520 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (122, 426, N'LS', N'LSO', N'Lesotho', N'N', CAST(0x0000AC400184F521 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (123, 428, N'LV', N'LVA', N'Letonia', N'N', CAST(0x0000AC400184F521 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (124, 422, N'LB', N'LBN', N'Libano', N'N', CAST(0x0000AC400184F521 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (125, 430, N'LR', N'LBR', N'Liberia', N'N', CAST(0x0000AC400184F521 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (126, 434, N'LY', N'LBY', N'Libia', N'N', CAST(0x0000AC400184F521 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (127, 438, N'LI', N'LIE', N'Liechtenstein', N'N', CAST(0x0000AC400184F521 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (128, 440, N'LT', N'LTU', N'Lituania', N'N', CAST(0x0000AC400184F521 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (129, 442, N'LU', N'LUX', N'Luxemburgo', N'N', CAST(0x0000AC400184F521 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (130, 446, N'MO', N'MAC', N'Macao', N'N', CAST(0x0000AC400184F521 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (131, 807, N'MK', N'MKD', N'ARY Macedonia', N'N', CAST(0x0000AC400184F521 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (132, 450, N'MG', N'MDG', N'Madagascar', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (133, 458, N'MY', N'MYS', N'Malasia', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (134, 454, N'MW', N'MWI', N'Malawi', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (135, 462, N'MV', N'MDV', N'Maldivas', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (136, 466, N'ML', N'MLI', N'Mali', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (137, 470, N'MT', N'MLT', N'Malta', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (138, 238, N'FK', N'FLK', N'Islas Malvinas', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (139, 580, N'MP', N'MNP', N'Islas Marianas del Norte', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (140, 504, N'MA', N'MAR', N'Marruecos', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (141, 584, N'MH', N'MHL', N'Islas Marshall', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (142, 474, N'MQ', N'MTQ', N'Martinica', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (143, 480, N'MU', N'MUS', N'Mauricio', N'N', CAST(0x0000AC400184F522 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (144, 478, N'MR', N'MRT', N'Mauritania', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (145, 175, N'YT', N'MYT', N'Mayotte', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (146, 484, N'MX', N'MEX', N'Mexico', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (147, 583, N'FM', N'FSM', N'Micronesia', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (148, 498, N'MD', N'MDA', N'Moldavia', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (149, 492, N'MC', N'MCO', N'Monaco', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (150, 496, N'MN', N'MNG', N'Mongolia', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (151, 500, N'MS', N'MSR', N'Montserrat', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (152, 508, N'MZ', N'MOZ', N'Mozambique', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (153, 104, N'MM', N'MMR', N'Myanmar', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (154, 516, N'NA', N'NAM', N'Namibia', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (155, 520, N'NR', N'NRU', N'Nauru', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (156, 524, N'NP', N'NPL', N'Nepal', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (157, 558, N'NI', N'NIC', N'Nicaragua', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (158, 562, N'NE', N'NER', N'Niger', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (159, 566, N'NG', N'NGA', N'Nigeria', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (160, 570, N'NU', N'NIU', N'Niue', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (161, 574, N'NF', N'NFK', N'Isla Norfolk', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (162, 578, N'NO', N'NOR', N'Noruega', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (163, 540, N'NC', N'NCL', N'Nueva Caledonia', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (164, 554, N'NZ', N'NZL', N'Nueva Zelanda', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (165, 512, N'OM', N'OMN', N'Oman', N'N', CAST(0x0000AC400184F523 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (166, 528, N'NL', N'NLD', N'Paises Bajos', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (167, 586, N'PK', N'PAK', N'Pakistan', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (168, 585, N'PW', N'PLW', N'Palau', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (169, 275, N'PS', N'PSE', N'Palestina', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (170, 591, N'PA', N'PAN', N'Panama', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (171, 598, N'PG', N'PNG', N'Papua Nueva Guinea', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (172, 600, N'PY', N'PRY', N'Paraguay', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (173, 604, N'PE', N'PER', N'Peru', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (174, 612, N'PN', N'PCN', N'Islas Pitcairn', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (175, 258, N'PF', N'PYF', N'Polinesia Francesa', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (176, 616, N'PL', N'POL', N'Polonia', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (177, 620, N'PT', N'PRT', N'Portugal', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (178, 630, N'PR', N'PRI', N'Puerto Rico', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (179, 634, N'QA', N'QAT', N'Qatar', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (180, 826, N'GB', N'GBR', N'Reino Unido', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (181, 638, N'RE', N'REU', N'Reunion', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (182, 646, N'RW', N'RWA', N'Ruanda', N'N', CAST(0x0000AC400184F524 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (183, 642, N'RO', N'ROU', N'Rumania', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (184, 643, N'RU', N'RUS', N'Rusia', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (185, 732, N'EH', N'ESH', N'Sahara Occidental', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (186, 90, N'SB', N'SLB', N'Islas Salomon', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (187, 882, N'WS', N'WSM', N'Samoa', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (188, 16, N'AS', N'ASM', N'Samoa Americana', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (189, 659, N'KN', N'KNA', N'San Cristobal y Nevis', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (190, 674, N'SM', N'SMR', N'San Marino', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (191, 666, N'PM', N'SPM', N'San Pedro y Miquelon', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (192, 670, N'VC', N'VCT', N'San Vicente y las Granadinas', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (193, 654, N'SH', N'SHN', N'Santa Helena', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (194, 662, N'LC', N'LCA', N'Santa Lucia', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (195, 678, N'ST', N'STP', N'Santo Tome y Principe', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (196, 686, N'SN', N'SEN', N'Senegal', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (197, 891, N'CS', N'SCG', N'Serbia y Montenegro', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (198, 690, N'SC', N'SYC', N'Seychelles', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (199, 694, N'SL', N'SLE', N'Sierra Leona', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
GO
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (200, 702, N'SG', N'SGP', N'Singapur', N'N', CAST(0x0000AC400184F525 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (201, 760, N'SY', N'SYR', N'Siria', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (202, 706, N'SO', N'SOM', N'Somalia', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (203, 144, N'LK', N'LKA', N'Sri Lanka', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (204, 748, N'SZ', N'SWZ', N'Suazilandia', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (205, 710, N'ZA', N'ZAF', N'Sudafrica', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (206, 736, N'SD', N'SDN', N'Sudan', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (207, 752, N'SE', N'SWE', N'Suecia', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (208, 756, N'CH', N'CHE', N'Suiza', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (209, 740, N'SR', N'SUR', N'Surinam', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (210, 744, N'SJ', N'SJM', N'Svalbard y Jan Mayen', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (211, 764, N'TH', N'THA', N'Tailandia', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (212, 158, N'TW', N'TWN', N'Taiwan', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (213, 834, N'TZ', N'TZA', N'Tanzania', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (214, 762, N'TJ', N'TJK', N'Tayikistan', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (215, 86, N'IO', N'IOT', N'Territorio Britanico del Oceano indico', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (216, 260, N'TF', N'ATF', N'Territorios Australes Franceses', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (217, 626, N'TL', N'TLS', N'Timor Oriental', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (218, 768, N'TG', N'TGO', N'Togo', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (219, 772, N'TK', N'TKL', N'Tokelau', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (220, 776, N'TO', N'TON', N'Tonga', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (221, 780, N'TT', N'TTO', N'Trinidad y Tobago', N'N', CAST(0x0000AC400184F526 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (222, 788, N'TN', N'TUN', N'Tunez', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (223, 796, N'TC', N'TCA', N'Islas Turcas y Caicos', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (224, 795, N'TM', N'TKM', N'Turkmenistan', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (225, 792, N'TR', N'TUR', N'Turquia', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (226, 798, N'TV', N'TUV', N'Tuvalu', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (227, 804, N'UA', N'UKR', N'Ucrania', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (228, 800, N'UG', N'UGA', N'Uganda', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (229, 858, N'UY', N'URY', N'Uruguay', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (230, 860, N'UZ', N'UZB', N'Uzbekistan', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (231, 548, N'VU', N'VUT', N'Vanuatu', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (232, 862, N'VE', N'VEN', N'Venezuela', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (233, 704, N'VN', N'VNM', N'Vietnam', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (234, 92, N'VG', N'VGB', N'Islas Virgenes Britanicas', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (235, 850, N'VI', N'VIR', N'Islas Virgenes de los Estados Unidos', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (236, 876, N'WF', N'WLF', N'Wallis y Futuna', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (237, 887, N'YE', N'YEM', N'Yemen', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (238, 262, N'DJ', N'DJI', N'Yibuti', N'N', CAST(0x0000AC400184F527 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (239, 894, N'ZM', N'ZMB', N'Zambia', N'N', CAST(0x0000AC400184F528 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (240, 716, N'ZW', N'ZWE', N'Zimbabue', N'N', CAST(0x0000AC400184F528 AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (241, 728, N'SS', N'SSD', N'Sudan del Sur', N'N', CAST(0x0000AC4700D2618B AS DateTime), NULL, NULL)
INSERT [dbo].[paises] ([id], [code], [iso3166a1], [iso3166a2], [descripcion], [desactivado], [fecha_creacion], [fecha_actualizacion], [id_usuario]) VALUES (242, 0, N'BT', N'BTN', N'Reino de Butan', N'N', CAST(0x0000AC4700D414EA AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[paises] OFF
INSERT [dbo].[persona] ([id_persona], [nombre1], [apellido1], [apellido2], [direccion], [documento], [estado], [foto], [telefono], [correo], [id_estado_civil]) VALUES (1, N'Marlon', N'Peña', N'Rivas', N'Calle 11', N'40225193248', 1, N'C:\Users\marlon\Downloads\3018587-64.png', NULL, NULL, NULL)
INSERT [dbo].[persona] ([id_persona], [nombre1], [apellido1], [apellido2], [direccion], [documento], [estado], [foto], [telefono], [correo], [id_estado_civil]) VALUES (2, N'Pedro', N'Dias', N'Ruiz', N'Calle 4', N'40225193248', 1, N'', NULL, NULL, NULL)
INSERT [dbo].[persona] ([id_persona], [nombre1], [apellido1], [apellido2], [direccion], [documento], [estado], [foto], [telefono], [correo], [id_estado_civil]) VALUES (4, N'Eliezer', N'Olivo', N'Montero', N'Calle 5', N'40257896357', 1, N'', NULL, NULL, NULL)
INSERT [dbo].[persona] ([id_persona], [nombre1], [apellido1], [apellido2], [direccion], [documento], [estado], [foto], [telefono], [correo], [id_estado_civil]) VALUES (5, N'Edelmis', N'Rivas', N'Machin', N'Calle 7, Dorado 1ro', N'4024201911', 1, N'C:\Users\marlon\Downloads\Scanbot Sep 15, 2019 9.43 PM.JPG', NULL, NULL, NULL)
INSERT [dbo].[persona] ([id_persona], [nombre1], [apellido1], [apellido2], [direccion], [documento], [estado], [foto], [telefono], [correo], [id_estado_civil]) VALUES (6, N'Contado', N'', N'', N'', N'', 1, N'', N'8', N'', 2)
INSERT [dbo].[persona] ([id_persona], [nombre1], [apellido1], [apellido2], [direccion], [documento], [estado], [foto], [telefono], [correo], [id_estado_civil]) VALUES (7, N'Juan', N'Rodriguez', N'Guzman', N'Calle 4, Los jaedinez, Santiago', N'40245689963', 1, N'', N'8', N'super@gmail', 0)
INSERT [dbo].[persona] ([id_persona], [nombre1], [apellido1], [apellido2], [direccion], [documento], [estado], [foto], [telefono], [correo], [id_estado_civil]) VALUES (8, N'Glenny', N'Dominguez', N'Uceta', N'calle 1', N'40225193248', 1, N'', N'8', N'ddd@gmail', 0)
INSERT [dbo].[persona] ([id_persona], [nombre1], [apellido1], [apellido2], [direccion], [documento], [estado], [foto], [telefono], [correo], [id_estado_civil]) VALUES (9, N'vdsdvsd', N'dvsv', N'sdvsdv', N'asda', N'40225193248', 1, N'', N'7', N'asda', 0)
INSERT [dbo].[persona] ([id_persona], [nombre1], [apellido1], [apellido2], [direccion], [documento], [estado], [foto], [telefono], [correo], [id_estado_civil]) VALUES (10, N'csdsds', N'xcvxv', N'xcvx', N'gdfdfg', N'40225193248', 1, N'', N'8', N'gdfggdfgdfg', 0)
INSERT [dbo].[persona] ([id_persona], [nombre1], [apellido1], [apellido2], [direccion], [documento], [estado], [foto], [telefono], [correo], [id_estado_civil]) VALUES (11, N'wefwefw', N'wefwf', N'efwf', N'asasasa', N'40225193248', 1, N'', N'8', N'asas', 0)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (1, N'Espaguetis', N'Espaguetis', CAST(300.00 AS Numeric(12, 2)), CAST(0.18 AS Numeric(12, 2)), 6, 3, 1)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (2, N'Croquetas de pescado', N'Croquetas de pescado', CAST(250.00 AS Numeric(12, 2)), CAST(0.18 AS Numeric(12, 2)), 2, 1, 1)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (3, N'Dorado', N'Filete a la plancha', CAST(600.00 AS Numeric(12, 2)), CAST(0.18 AS Numeric(12, 2)), 2, 2, 1)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (4, N'Agua', N'Agua', CAST(50.00 AS Numeric(12, 2)), CAST(0.18 AS Numeric(12, 2)), 2, 4, 1)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (5, N'Ensalada Cesar', N'Ensalada Cesar con pollo', CAST(350.00 AS Numeric(12, 2)), CAST(0.18 AS Numeric(12, 2)), 3, 5, 1)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (6, N'Ceviche', N'Ceviche', CAST(400.00 AS Numeric(12, 2)), CAST(0.18 AS Numeric(12, 2)), NULL, 6, 1)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (7, N'Croquetas De Pollo', N'Croquetas de pollo', CAST(250.00 AS Numeric(12, 2)), CAST(0.18 AS Numeric(12, 2)), NULL, 7, 1)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (8, N'Crema De Auyama', N'Crema de auyama', CAST(400.00 AS Numeric(12, 2)), CAST(0.10 AS Numeric(12, 2)), NULL, 8, 1)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (9, N'Flan de leche', N'Flan de leche', CAST(150.00 AS Numeric(12, 2)), CAST(0.18 AS Numeric(12, 2)), 2, 9, 1)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (10, N'Croquetas de pescado', N'Croquetas de pescado', CAST(250.00 AS Numeric(12, 2)), CAST(0.18 AS Numeric(12, 2)), 2, 1, 1)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (11, N'Flan de leche', N'Flan de leche', CAST(150.00 AS Numeric(12, 2)), CAST(0.18 AS Numeric(12, 2)), 2, 9, 1)
INSERT [dbo].[platos] ([id_plato], [plato], [descripcion], [precio], [itbis], [id_receta], [id_categoria], [id_itbis]) VALUES (12, N'Croquetas de pescado', N'Croquetas de pescado', CAST(250.00 AS Numeric(12, 2)), CAST(0.18 AS Numeric(12, 2)), 2, 1, 1)
INSERT [dbo].[productos] ([id_productos], [productos], [tipo_producto], [descripcion], [id_unidad], [id_marca], [estado], [codigo_interno], [fecha_creacion], [costo_promedio], [costo_comercial], [id_itbis], [excento_itbis]) VALUES (1, N'Tomate', 1, N'Tomates Ensalada', 2, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[productos] ([id_productos], [productos], [tipo_producto], [descripcion], [id_unidad], [id_marca], [estado], [codigo_interno], [fecha_creacion], [costo_promedio], [costo_comercial], [id_itbis], [excento_itbis]) VALUES (2, N'Queso Parmesano', 6, N'Parmesano en polvo', 2, 7, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[productos] ([id_productos], [productos], [tipo_producto], [descripcion], [id_unidad], [id_marca], [estado], [codigo_interno], [fecha_creacion], [costo_promedio], [costo_comercial], [id_itbis], [excento_itbis]) VALUES (3, N'Azucar', 1, N'', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[recetas] ([id_receta], [receta], [descripcion], [categoria], [comensales], [tiempo], [imagen]) VALUES (2, N'Penne', N'Pasta boloñesa tipo penne', N'Primer Plato', 1, 15, NULL)
INSERT [dbo].[recetas] ([id_receta], [receta], [descripcion], [categoria], [comensales], [tiempo], [imagen]) VALUES (3, N'Ensalada Cesar ', N'Ensalada Cesar con pollo y pan al hirno', N'Primer Plato', 1, 15, NULL)
INSERT [dbo].[recetas] ([id_receta], [receta], [descripcion], [categoria], [comensales], [tiempo], [imagen]) VALUES (6, N'carbonara', N'pruebaa', N'Primer Plato', 1, 15, NULL)
SET IDENTITY_INSERT [dbo].[secuencia_ncf] ON 

INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (8, 1, N'A', 1, 10, NULL, CAST(0xDC410B00 AS Date), CAST(0xCC410B00 AS Date), 2, N'N')
INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (9, 1, N'A', 12, 14, NULL, CAST(0xD5410B00 AS Date), CAST(0xD5410B00 AS Date), 2, N'N')
INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (10, 1, N'A', 16, 20, NULL, CAST(0xAF410B00 AS Date), CAST(0xD5410B00 AS Date), 2, N'N')
INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (11, 1, N'A', 25, 30, NULL, CAST(0x5B950A00 AS Date), CAST(0xD5410B00 AS Date), 2, N'N')
INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (12, 1, N'A', 21, 24, NULL, CAST(0x5B950A00 AS Date), CAST(0xD5410B00 AS Date), 2, N'N')
INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (13, 1, N'A', 31, 36, NULL, CAST(0x5B950A00 AS Date), CAST(0xD5410B00 AS Date), 2, N'N')
INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (15, 1, N'A', 40, 50, 42, NULL, CAST(0xD5410B00 AS Date), 2, N'N')
INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (16, 1, N'A', 60, 70, NULL, CAST(0x5B950A00 AS Date), CAST(0xD6410B00 AS Date), 2, N'N')
INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (17, 1, N'A', 71, 75, NULL, CAST(0x5B950A00 AS Date), CAST(0xD6410B00 AS Date), 2, N'N')
INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (19, 1, N'A', 80, 85, NULL, NULL, CAST(0xD6410B00 AS Date), 2, N'N')
INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (20, 1, N'A', 72, 73, NULL, CAST(0xDE410B00 AS Date), CAST(0xD6410B00 AS Date), 2, N'N')
INSERT [dbo].[secuencia_ncf] ([id_secuencia], [id_tipo_ncf], [letra], [secuencia_inicial], [secuencia_final], [ultimo_generado], [fecha_vencimiento], [fecha_creacion], [id_usuario], [desactivado]) VALUES (21, 1, N'A', 82, 84, NULL, NULL, CAST(0xD6410B00 AS Date), 2, N'N')
SET IDENTITY_INSERT [dbo].[secuencia_ncf] OFF
INSERT [dbo].[sucursal] ([id_sucursal], [sucursal], [id_empresa]) VALUES (1, N'eFood Restaurante', 1)
INSERT [dbo].[temp_det_factura] ([id_factura], [id_plato], [cantidad], [precio], [itbis]) VALUES (6, 1, CAST(1.00 AS Numeric(18, 2)), CAST(300.00 AS Numeric(10, 2)), CAST(0.18 AS Decimal(20, 2)))
INSERT [dbo].[temp_det_factura] ([id_factura], [id_plato], [cantidad], [precio], [itbis]) VALUES (3, 5, CAST(1.00 AS Numeric(18, 2)), CAST(350.00 AS Numeric(10, 2)), CAST(0.00 AS Decimal(20, 2)))
INSERT [dbo].[temp_det_factura] ([id_factura], [id_plato], [cantidad], [precio], [itbis]) VALUES (6, 5, CAST(1.00 AS Numeric(18, 2)), CAST(350.00 AS Numeric(10, 2)), CAST(0.18 AS Decimal(20, 2)))
INSERT [dbo].[temp_det_factura] ([id_factura], [id_plato], [cantidad], [precio], [itbis]) VALUES (7, 4, CAST(1.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(10, 2)), CAST(0.18 AS Decimal(20, 2)))
INSERT [dbo].[temp_det_factura] ([id_factura], [id_plato], [cantidad], [precio], [itbis]) VALUES (9, 3, CAST(1.00 AS Numeric(18, 2)), CAST(600.00 AS Numeric(10, 2)), CAST(0.18 AS Decimal(20, 2)))
INSERT [dbo].[temp_det_factura] ([id_factura], [id_plato], [cantidad], [precio], [itbis]) VALUES (6, 2, CAST(1.00 AS Numeric(18, 2)), CAST(250.00 AS Numeric(10, 2)), CAST(0.18 AS Decimal(20, 2)))
INSERT [dbo].[temp_det_factura] ([id_factura], [id_plato], [cantidad], [precio], [itbis]) VALUES (11, 4, CAST(1.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(10, 2)), CAST(0.18 AS Decimal(20, 2)))
INSERT [dbo].[temp_det_factura] ([id_factura], [id_plato], [cantidad], [precio], [itbis]) VALUES (11, 2, CAST(1.00 AS Numeric(18, 2)), CAST(250.00 AS Numeric(10, 2)), CAST(0.18 AS Decimal(20, 2)))
INSERT [dbo].[temp_det_factura] ([id_factura], [id_plato], [cantidad], [precio], [itbis]) VALUES (11, 1, CAST(1.00 AS Numeric(18, 2)), CAST(300.00 AS Numeric(10, 2)), CAST(0.18 AS Decimal(20, 2)))
INSERT [dbo].[temp_det_factura] ([id_factura], [id_plato], [cantidad], [precio], [itbis]) VALUES (11, 5, CAST(1.00 AS Numeric(18, 2)), CAST(350.00 AS Numeric(10, 2)), CAST(0.18 AS Decimal(20, 2)))
INSERT [dbo].[temp_enc_factura] ([id_factura], [id_mesa], [fecha_factura], [id_cliente], [id_usuario], [rnc], [itbis], [total], [porciento_ley], [sub_total], [estado], [nota]) VALUES (1, N'2         ', CAST(0x05410B00 AS Date), 6, 1, 0, CAST(90.00 AS Numeric(12, 2)), CAST(640.00 AS Numeric(12, 2)), CAST(50 AS Numeric(12, 0)), CAST(500.00 AS Numeric(12, 2)), N'A', N'')
INSERT [dbo].[temp_enc_factura] ([id_factura], [id_mesa], [fecha_factura], [id_cliente], [id_usuario], [rnc], [itbis], [total], [porciento_ley], [sub_total], [estado], [nota]) VALUES (2, N'2         ', CAST(0xCD410B00 AS Date), 6, 1, 0, CAST(63.00 AS Numeric(12, 2)), CAST(448.00 AS Numeric(12, 2)), CAST(35 AS Numeric(12, 0)), CAST(350.00 AS Numeric(12, 2)), N'A', N'')
INSERT [dbo].[temp_enc_factura] ([id_factura], [id_mesa], [fecha_factura], [id_cliente], [id_usuario], [rnc], [itbis], [total], [porciento_ley], [sub_total], [estado], [nota]) VALUES (3, N'2         ', CAST(0xCD410B00 AS Date), 6, 1, 0, CAST(306.00 AS Numeric(12, 2)), CAST(2176.00 AS Numeric(12, 2)), CAST(170 AS Numeric(12, 0)), CAST(1700.00 AS Numeric(12, 2)), N'A', N'')
INSERT [dbo].[temp_enc_factura] ([id_factura], [id_mesa], [fecha_factura], [id_cliente], [id_usuario], [rnc], [itbis], [total], [porciento_ley], [sub_total], [estado], [nota]) VALUES (4, N'2         ', CAST(0xCF410B00 AS Date), 6, 1, 0, CAST(63.00 AS Numeric(12, 2)), CAST(448.00 AS Numeric(12, 2)), CAST(35 AS Numeric(12, 0)), CAST(350.00 AS Numeric(12, 2)), N'A', N'')
INSERT [dbo].[temp_enc_factura] ([id_factura], [id_mesa], [fecha_factura], [id_cliente], [id_usuario], [rnc], [itbis], [total], [porciento_ley], [sub_total], [estado], [nota]) VALUES (5, N'2         ', CAST(0xCF410B00 AS Date), 6, 1, 0, CAST(126.00 AS Numeric(12, 2)), CAST(896.00 AS Numeric(12, 2)), CAST(70 AS Numeric(12, 0)), CAST(700.00 AS Numeric(12, 2)), N'A', N'')
INSERT [dbo].[temp_enc_factura] ([id_factura], [id_mesa], [fecha_factura], [id_cliente], [id_usuario], [rnc], [itbis], [total], [porciento_ley], [sub_total], [estado], [nota]) VALUES (6, N'1         ', CAST(0xCF410B00 AS Date), 6, 1, 0, CAST(45.00 AS Numeric(12, 2)), CAST(1152.00 AS Numeric(12, 2)), CAST(25 AS Numeric(12, 0)), CAST(250.00 AS Numeric(12, 2)), N'A', N'')
INSERT [dbo].[temp_enc_factura] ([id_factura], [id_mesa], [fecha_factura], [id_cliente], [id_usuario], [rnc], [itbis], [total], [porciento_ley], [sub_total], [estado], [nota]) VALUES (7, N'3         ', CAST(0xD9410B00 AS Date), 6, 1, 0, CAST(9.00 AS Numeric(12, 2)), CAST(64.00 AS Numeric(12, 2)), CAST(5 AS Numeric(12, 0)), CAST(50.00 AS Numeric(12, 2)), N'A', N'')
INSERT [dbo].[temp_enc_factura] ([id_factura], [id_mesa], [fecha_factura], [id_cliente], [id_usuario], [rnc], [itbis], [total], [porciento_ley], [sub_total], [estado], [nota]) VALUES (8, N'4         ', CAST(0xD9410B00 AS Date), 6, 1, 0, CAST(216.00 AS Numeric(12, 2)), CAST(1536.00 AS Numeric(12, 2)), CAST(120 AS Numeric(12, 0)), CAST(1200.00 AS Numeric(12, 2)), N'A', N'')
INSERT [dbo].[temp_enc_factura] ([id_factura], [id_mesa], [fecha_factura], [id_cliente], [id_usuario], [rnc], [itbis], [total], [porciento_ley], [sub_total], [estado], [nota]) VALUES (9, N'4         ', CAST(0xD9410B00 AS Date), 6, 1, 0, CAST(171.00 AS Numeric(12, 2)), CAST(1216.00 AS Numeric(12, 2)), CAST(95 AS Numeric(12, 0)), CAST(950.00 AS Numeric(12, 2)), N'A', N'')
INSERT [dbo].[temp_enc_factura] ([id_factura], [id_mesa], [fecha_factura], [id_cliente], [id_usuario], [rnc], [itbis], [total], [porciento_ley], [sub_total], [estado], [nota]) VALUES (10, N'4         ', CAST(0xD9410B00 AS Date), 6, 1, 0, CAST(9.00 AS Numeric(12, 2)), CAST(64.00 AS Numeric(12, 2)), CAST(5 AS Numeric(12, 0)), CAST(50.00 AS Numeric(12, 2)), N'A', N'')
INSERT [dbo].[temp_enc_factura] ([id_factura], [id_mesa], [fecha_factura], [id_cliente], [id_usuario], [rnc], [itbis], [total], [porciento_ley], [sub_total], [estado], [nota]) VALUES (11, N'5         ', CAST(0xE3410B00 AS Date), 6, 1, 0, CAST(171.00 AS Numeric(12, 2)), CAST(1216.00 AS Numeric(12, 2)), CAST(95 AS Numeric(12, 0)), CAST(950.00 AS Numeric(12, 2)), N'A', N'')
SET IDENTITY_INSERT [dbo].[tipo_cobro] ON 

INSERT [dbo].[tipo_cobro] ([id], [descripcion], [desactivado], [id_usuario], [fecha_creacion], [fecha_actualizacion]) VALUES (1, N'EFECTIVO', N'N', 2, CAST(0x0000AC7F01894474 AS DateTime), NULL)
INSERT [dbo].[tipo_cobro] ([id], [descripcion], [desactivado], [id_usuario], [fecha_creacion], [fecha_actualizacion]) VALUES (2, N'TARJETA', N'N', 2, CAST(0x0000AC7F01894475 AS DateTime), NULL)
INSERT [dbo].[tipo_cobro] ([id], [descripcion], [desactivado], [id_usuario], [fecha_creacion], [fecha_actualizacion]) VALUES (3, N'CHEQUES', N'N', 2, CAST(0x0000AC7F01894475 AS DateTime), NULL)
INSERT [dbo].[tipo_cobro] ([id], [descripcion], [desactivado], [id_usuario], [fecha_creacion], [fecha_actualizacion]) VALUES (4, N'NOTA DE CREDITO', N'N', 2, CAST(0x0000AC7F01894475 AS DateTime), NULL)
INSERT [dbo].[tipo_cobro] ([id], [descripcion], [desactivado], [id_usuario], [fecha_creacion], [fecha_actualizacion]) VALUES (5, N'DOLARES', N'N', 2, CAST(0x0000AC7F01894475 AS DateTime), NULL)
INSERT [dbo].[tipo_cobro] ([id], [descripcion], [desactivado], [id_usuario], [fecha_creacion], [fecha_actualizacion]) VALUES (6, N'TRANSFERENCIAS', N'N', 2, CAST(0x0000AC7F01894475 AS DateTime), NULL)
INSERT [dbo].[tipo_cobro] ([id], [descripcion], [desactivado], [id_usuario], [fecha_creacion], [fecha_actualizacion]) VALUES (7, N'DEPOSITO', N'N', 2, CAST(0x0000AC7F01894475 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[tipo_cobro] OFF
INSERT [dbo].[tipo_factura] ([id_tipo_factura], [tipo_factura]) VALUES (1, N'Contado')
INSERT [dbo].[tipo_factura] ([id_tipo_factura], [tipo_factura]) VALUES (2, N'Credito')
SET IDENTITY_INSERT [dbo].[tipo_ncf] ON 

INSERT [dbo].[tipo_ncf] ([id], [tipo], [descripcion], [desactivado], [facturable], [fecha_creacion], [fecha_actualizacion], [id_usuario], [nota]) VALUES (1, 1, N'CREDITO FISCAL', N'N', N'S', CAST(0x0000ABDF017A6334 AS DateTime), NULL, 2, N'Registran las transacciones comerciales de compra y venta de bienes y/o los que prestan algún servicio. Permiten al comprador o usuario que lo solicite sustentar gastos y costos del ISR o créditos del ITBIS.')
INSERT [dbo].[tipo_ncf] ([id], [tipo], [descripcion], [desactivado], [facturable], [fecha_creacion], [fecha_actualizacion], [id_usuario], [nota]) VALUES (2, 2, N'CONSUMO', N'N', N'S', CAST(0x0000ABDF017A6334 AS DateTime), NULL, 2, N'Acreditan la transferencia de bienes, la entrega en uso o la prestación de servicios a consumidores finales. No poseen efectos tributarios, es decir, que no podrán ser utilizados para créditos en el ITBIS y/o reducir gastos y costos del ISR')
INSERT [dbo].[tipo_ncf] ([id], [tipo], [descripcion], [desactivado], [facturable], [fecha_creacion], [fecha_actualizacion], [id_usuario], [nota]) VALUES (3, 3, N'NOTA DE CREDITO', N'N', N'N', CAST(0x0000ABDF017A6334 AS DateTime), NULL, 2, N'Documentos que emiten los vendedores de bienes y/o los que prestan servicios para recuperar costos y gastos, como: intereses por mora, fletes u otros, después de emitido el comprobante fiscal. Sólo podrán ser emitidas al mismo adquiriente o usuario para modificar comprobantes emitidos con anterioridad.')
INSERT [dbo].[tipo_ncf] ([id], [tipo], [descripcion], [desactivado], [facturable], [fecha_creacion], [fecha_actualizacion], [id_usuario], [nota]) VALUES (4, 4, N'NOTA DE DEBITO', N'N', N'N', CAST(0x0000ABDF017A6334 AS DateTime), NULL, 2, N'Documentos que emiten los vendedores de bienes y/ o prestadores de servicios por modificaciones posteriores en las condiciones de venta originalmente pactadas, es decir, para anular operaciones, efectuar devoluciones, conceder descuentos y bonificaciones, corregir errores o casos similares')
INSERT [dbo].[tipo_ncf] ([id], [tipo], [descripcion], [desactivado], [facturable], [fecha_creacion], [fecha_actualizacion], [id_usuario], [nota]) VALUES (5, 11, N'PROVEEDORES INFORMALES', N'N', N'N', CAST(0x0000ABDF017A6334 AS DateTime), NULL, 2, N'Documentos emitido por las personas físicas o jurídicas cuando adquieran bienes o servicios de personas no registradas como contribuyentes o que sean autorizados mediante norma general.')
INSERT [dbo].[tipo_ncf] ([id], [tipo], [descripcion], [desactivado], [facturable], [fecha_creacion], [fecha_actualizacion], [id_usuario], [nota]) VALUES (6, 13, N'GASTOS MENORES', N'N', N'N', CAST(0x0000ABDF017A6335 AS DateTime), NULL, 2, N'Son aquellos comprobantes emitidos por las personas físicas o jurídicas para sustentar pagos realizados por su personal, sean estos efectuados en territorio dominicano o en el extranjero y en ocasión a las actividades relacionadas al trabajo, tales como: consumibles, pasajes, transporte público, tarifas de estacionamiento y Peajes.')
INSERT [dbo].[tipo_ncf] ([id], [tipo], [descripcion], [desactivado], [facturable], [fecha_creacion], [fecha_actualizacion], [id_usuario], [nota]) VALUES (7, 14, N'REGIMEN ESPECIAL', N'N', N'S', CAST(0x0000ABDF017A6335 AS DateTime), NULL, 2, N'Son utilizados para facturar las ventas de bienes o prestación de servicios exentos del ITBIS o ISC a personas físicas o jurídicas acogidas a regímenes especiales de tributación, mediante leyes especiales, contratos o convenios debidamente ratificados por el Congreso Nacional.')
INSERT [dbo].[tipo_ncf] ([id], [tipo], [descripcion], [desactivado], [facturable], [fecha_creacion], [fecha_actualizacion], [id_usuario], [nota]) VALUES (8, 15, N'GUBERNAMENTAL', N'N', N'S', CAST(0x0000ABDF017A6335 AS DateTime), NULL, 2, N'Son utilizados para facturar la venta de bienes y la prestación de servicios al Gobierno Central, Instituciones Descentralizadas y Autónomas, Instituciones de Seguridad Social y cualquier entidad gubernamental que no realice una actividad comercial.')
SET IDENTITY_INSERT [dbo].[tipo_ncf] OFF
INSERT [dbo].[tipo_pago] ([id_pago], [tipo_pago]) VALUES (1, N'Semanal')
INSERT [dbo].[tipo_pago] ([id_pago], [tipo_pago]) VALUES (2, N'Quincenal')
INSERT [dbo].[tipo_pago] ([id_pago], [tipo_pago]) VALUES (3, N'Mensual')
INSERT [dbo].[tipo_producto] ([id_tipo], [nombre_tipo], [descripcion]) VALUES (1, N'Enlatado', N'Productos enlatados')
INSERT [dbo].[tipo_producto] ([id_tipo], [nombre_tipo], [descripcion]) VALUES (2, N'Vegetales', N'Vegetales y hortalizas')
INSERT [dbo].[tipo_producto] ([id_tipo], [nombre_tipo], [descripcion]) VALUES (3, N'Carnicos', N'Carnicos Varios')
INSERT [dbo].[tipo_producto] ([id_tipo], [nombre_tipo], [descripcion]) VALUES (4, N'Especias', N'Especias ')
INSERT [dbo].[tipo_producto] ([id_tipo], [nombre_tipo], [descripcion]) VALUES (5, N'Condimentos', N'Condimentos')
INSERT [dbo].[tipo_producto] ([id_tipo], [nombre_tipo], [descripcion]) VALUES (6, N'Quesos', N'Queos')
INSERT [dbo].[tipo_producto] ([id_tipo], [nombre_tipo], [descripcion]) VALUES (7, N'Pastas', N'Pastas')
INSERT [dbo].[ubicacion] ([id_ubicacion], [descripcion], [estado]) VALUES (1, N'INTERIOR', NULL)
INSERT [dbo].[ubicacion] ([id_ubicacion], [descripcion], [estado]) VALUES (2, N'TERRAZA', NULL)
INSERT [dbo].[ubicacion] ([id_ubicacion], [descripcion], [estado]) VALUES (3, N'RESERVADO', NULL)
INSERT [dbo].[unidad] ([id_unidad], [unidad]) VALUES (1, N'Libras')
INSERT [dbo].[unidad] ([id_unidad], [unidad]) VALUES (2, N'Unidades')
INSERT [dbo].[unidad] ([id_unidad], [unidad]) VALUES (3, N'Enlatados')
INSERT [dbo].[unidad] ([id_unidad], [unidad]) VALUES (4, N'Gramos')
INSERT [dbo].[unidad] ([id_unidad], [unidad]) VALUES (5, N'Detallado')
INSERT [dbo].[unidad] ([id_unidad], [unidad]) VALUES (6, N'Paquete')
INSERT [dbo].[unidad_medida] ([id_unidad], [descripcion]) VALUES (1, N'Gramos')
INSERT [dbo].[unidad_medida] ([id_unidad], [descripcion]) VALUES (2, N'Tazas')
INSERT [dbo].[unidad_medida] ([id_unidad], [descripcion]) VALUES (3, N'Cuharadas')
INSERT [dbo].[unidad_medida] ([id_unidad], [descripcion]) VALUES (4, N'Unidades')
INSERT [dbo].[usuarios] ([id_usuario], [usuario], [pass], [fecha_creacion], [id_persona], [ficha], [estado]) VALUES (1, N'Marlonp95', N'MQAyADMAMQAyADMA', CAST(0xE33F0B00 AS Date), 4, 11249, NULL)
INSERT [dbo].[usuarios] ([id_usuario], [usuario], [pass], [fecha_creacion], [id_persona], [ficha], [estado]) VALUES (2, N'Marlonp95', N'MQAyADMANAA1ADYA', CAST(0xDD410B00 AS Date), 1, 11246, 1)
/****** Object:  Index [IX_cliente]    Script Date: 12/6/2020 3:34:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_cliente] ON [dbo].[cliente]
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ_det_cobros_tipo_pago_cobro]    Script Date: 12/6/2020 3:34:14 AM ******/
ALTER TABLE [dbo].[det_cobros] ADD  CONSTRAINT [UQ_det_cobros_tipo_pago_cobro] UNIQUE NONCLUSTERED 
(
	[id_tipo_pago] ASC,
	[id_cobro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_empresa]    Script Date: 12/6/2020 3:34:14 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_empresa] ON [dbo].[empresa]
(
	[id_empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ_enc_cobros_apertura_factura]    Script Date: 12/6/2020 3:34:14 AM ******/
ALTER TABLE [dbo].[enc_cobros] ADD  CONSTRAINT [UQ_enc_cobros_apertura_factura] UNIQUE NONCLUSTERED 
(
	[id_apertura] ASC,
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [U_secuencia_ncf_final_inicio]    Script Date: 12/6/2020 3:34:14 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [U_secuencia_ncf_final_inicio] ON [dbo].[secuencia_ncf]
(
	[secuencia_final] ASC,
	[secuencia_inicial] ASC,
	[letra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ_tipo_cobro_descripcion]    Script Date: 12/6/2020 3:34:14 AM ******/
ALTER TABLE [dbo].[tipo_cobro] ADD  CONSTRAINT [UQ_tipo_cobro_descripcion] UNIQUE NONCLUSTERED 
(
	[descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [uc_tipo]    Script Date: 12/6/2020 3:34:14 AM ******/
ALTER TABLE [dbo].[tipo_ncf] ADD  CONSTRAINT [uc_tipo] UNIQUE NONCLUSTERED 
(
	[tipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_usuarios]    Script Date: 12/6/2020 3:34:14 AM ******/
CREATE NONCLUSTERED INDEX [IX_usuarios] ON [dbo].[usuarios]
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cajas] ADD  CONSTRAINT [DF_cajas_fecha_creacion]  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[cajas] ADD  CONSTRAINT [DF_cajas_desactivado]  DEFAULT ('N') FOR [desactivado]
GO
ALTER TABLE [dbo].[denominaciones] ADD  CONSTRAINT [DF_denominaciones_desactivado]  DEFAULT ('N') FOR [desactivado]
GO
ALTER TABLE [dbo].[denominaciones] ADD  CONSTRAINT [DF_denominaciones_fecha_creacion]  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[det_cierre_caja] ADD  CONSTRAINT [DF_det_cierre_caja_fecha_creacion]  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[det_cobros] ADD  CONSTRAINT [DF_det_cobros_monto]  DEFAULT ((0)) FOR [monto]
GO
ALTER TABLE [dbo].[det_cobros] ADD  CONSTRAINT [DF_det_cobros_tasa]  DEFAULT ((1)) FOR [tasa]
GO
ALTER TABLE [dbo].[det_cobros] ADD  CONSTRAINT [DF_det_cobros_desactivado]  DEFAULT ('N') FOR [desactivado]
GO
ALTER TABLE [dbo].[det_cobros] ADD  CONSTRAINT [DF_det_cobros_fecha_creacion]  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[enc_apertura_caja] ADD  CONSTRAINT [DF_apertura_caja_monto_inicial]  DEFAULT ((0)) FOR [monto_inicial]
GO
ALTER TABLE [dbo].[enc_apertura_caja] ADD  CONSTRAINT [DF_apertura_caja_monto_final]  DEFAULT ((0)) FOR [monto_final]
GO
ALTER TABLE [dbo].[enc_apertura_caja] ADD  CONSTRAINT [DF_apertura_caja_desactivado]  DEFAULT ('N') FOR [desactivado]
GO
ALTER TABLE [dbo].[enc_apertura_caja] ADD  CONSTRAINT [DF_enc_apertura_caja_estado]  DEFAULT ('A') FOR [estado]
GO
ALTER TABLE [dbo].[enc_apertura_caja] ADD  CONSTRAINT [DF_apertura_caja_fecha_creacion]  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[enc_cierre_caja] ADD  CONSTRAINT [DF_enc_cierre_caja_monto_apertura_inicial]  DEFAULT ((0)) FOR [monto_apertura_inicial]
GO
ALTER TABLE [dbo].[enc_cierre_caja] ADD  CONSTRAINT [DF_enc_cierre_caja_total_balance_caja]  DEFAULT ((0)) FOR [total_balance_caja]
GO
ALTER TABLE [dbo].[enc_cierre_caja] ADD  CONSTRAINT [DF_enc_cierre_caja_total_cobrado_caja]  DEFAULT ((0)) FOR [total_cobrado_caja]
GO
ALTER TABLE [dbo].[enc_cierre_caja] ADD  CONSTRAINT [DF_enc_cierre_caja_total_dolares]  DEFAULT ((0)) FOR [total_dolares]
GO
ALTER TABLE [dbo].[enc_cierre_caja] ADD  CONSTRAINT [DF_enc_cierre_caja_total_cobrado_dolares]  DEFAULT ((0)) FOR [total_cobrado_dolares]
GO
ALTER TABLE [dbo].[enc_cierre_caja] ADD  CONSTRAINT [DF_enc_cierre_caja_desactivado]  DEFAULT ('N') FOR [desactivado]
GO
ALTER TABLE [dbo].[enc_cierre_caja] ADD  CONSTRAINT [DF_enc_cierre_caja_fecha_creacion]  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[enc_cobros] ADD  CONSTRAINT [DF_enc_cobros_monto]  DEFAULT ((0)) FOR [monto]
GO
ALTER TABLE [dbo].[enc_cobros] ADD  CONSTRAINT [DF_enc_cobros_cuotas]  DEFAULT ((1)) FOR [cuotas]
GO
ALTER TABLE [dbo].[enc_cobros] ADD  CONSTRAINT [DF_cobros_fecha_creacion]  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[monedas] ADD  CONSTRAINT [DF_moneda_sentido]  DEFAULT ('M') FOR [sentido_compra]
GO
ALTER TABLE [dbo].[monedas] ADD  CONSTRAINT [DF_monedas_sentido_venta]  DEFAULT ('D') FOR [sentido_venta]
GO
ALTER TABLE [dbo].[monedas] ADD  CONSTRAINT [DF_moneda_fecha_creacion]  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[monedas] ADD  CONSTRAINT [DF_moneda_desactivado]  DEFAULT ('N') FOR [desactivado]
GO
ALTER TABLE [dbo].[paises] ADD  CONSTRAINT [DF_paises_desactivado]  DEFAULT ('N') FOR [desactivado]
GO
ALTER TABLE [dbo].[paises] ADD  CONSTRAINT [DF_paises_fecha_creacion]  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[secuencia_ncf] ADD  CONSTRAINT [DF_secuencia_ncf_ultimo_generado]  DEFAULT ((0)) FOR [ultimo_generado]
GO
ALTER TABLE [dbo].[secuencia_ncf] ADD  CONSTRAINT [DF_secuencia_ncf_fecha_creacion]  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[tipo_cobro] ADD  CONSTRAINT [DF_tipo_cobro_desactivado]  DEFAULT ('N') FOR [desactivado]
GO
ALTER TABLE [dbo].[tipo_cobro] ADD  CONSTRAINT [DF_tipo_cobro_fecha_creacion]  DEFAULT (sysdatetime()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[almacen_productos]  WITH CHECK ADD  CONSTRAINT [FK_almacen_productos_almacen] FOREIGN KEY([id_almacen])
REFERENCES [dbo].[almacen] ([id_almacen])
GO
ALTER TABLE [dbo].[almacen_productos] CHECK CONSTRAINT [FK_almacen_productos_almacen]
GO
ALTER TABLE [dbo].[almacen_productos]  WITH CHECK ADD  CONSTRAINT [FK_almacen_productos_productos] FOREIGN KEY([id_producto])
REFERENCES [dbo].[productos] ([id_productos])
GO
ALTER TABLE [dbo].[almacen_productos] CHECK CONSTRAINT [FK_almacen_productos_productos]
GO
ALTER TABLE [dbo].[cajas]  WITH CHECK ADD  CONSTRAINT [FK_cajas_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[cajas] CHECK CONSTRAINT [FK_cajas_usuarios]
GO
ALTER TABLE [dbo].[cargo]  WITH CHECK ADD  CONSTRAINT [FK_cargo_departamento] FOREIGN KEY([id_departamento])
REFERENCES [dbo].[departamento] ([id_departamento])
GO
ALTER TABLE [dbo].[cargo] CHECK CONSTRAINT [FK_cargo_departamento]
GO
ALTER TABLE [dbo].[cargo]  WITH CHECK ADD  CONSTRAINT [FK_cargo_departamento1] FOREIGN KEY([id_departamento])
REFERENCES [dbo].[departamento] ([id_departamento])
GO
ALTER TABLE [dbo].[cargo] CHECK CONSTRAINT [FK_cargo_departamento1]
GO
ALTER TABLE [dbo].[cliente]  WITH CHECK ADD  CONSTRAINT [FK_cliente_condiciones_pago] FOREIGN KEY([id_condicion])
REFERENCES [dbo].[condiciones_pago] ([id_condicion_pago])
GO
ALTER TABLE [dbo].[cliente] CHECK CONSTRAINT [FK_cliente_condiciones_pago]
GO
ALTER TABLE [dbo].[cliente]  WITH CHECK ADD  CONSTRAINT [FK_cliente_persona] FOREIGN KEY([id_persona])
REFERENCES [dbo].[persona] ([id_persona])
GO
ALTER TABLE [dbo].[cliente] CHECK CONSTRAINT [FK_cliente_persona]
GO
ALTER TABLE [dbo].[cliente]  WITH CHECK ADD  CONSTRAINT [FK_cliente_tipo_ncf] FOREIGN KEY([id_tipo_ncf])
REFERENCES [dbo].[tipo_ncf] ([tipo])
GO
ALTER TABLE [dbo].[cliente] CHECK CONSTRAINT [FK_cliente_tipo_ncf]
GO
ALTER TABLE [dbo].[condiciones_pago]  WITH CHECK ADD  CONSTRAINT [FK_condiciones_pago_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[condiciones_pago] CHECK CONSTRAINT [FK_condiciones_pago_usuarios]
GO
ALTER TABLE [dbo].[contactos]  WITH CHECK ADD  CONSTRAINT [FK_contactos_persona1] FOREIGN KEY([id_persona])
REFERENCES [dbo].[persona] ([id_persona])
GO
ALTER TABLE [dbo].[contactos] CHECK CONSTRAINT [FK_contactos_persona1]
GO
ALTER TABLE [dbo].[denominaciones]  WITH CHECK ADD  CONSTRAINT [FK_denominaciones_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[denominaciones] CHECK CONSTRAINT [FK_denominaciones_usuarios]
GO
ALTER TABLE [dbo].[det_apertura_caja]  WITH CHECK ADD  CONSTRAINT [FK_det_apertura_caja_apertura] FOREIGN KEY([id_apertura_caja])
REFERENCES [dbo].[enc_apertura_caja] ([id])
GO
ALTER TABLE [dbo].[det_apertura_caja] CHECK CONSTRAINT [FK_det_apertura_caja_apertura]
GO
ALTER TABLE [dbo].[det_apertura_caja]  WITH CHECK ADD  CONSTRAINT [FK_det_apertura_caja_denominacion] FOREIGN KEY([id_denominacion])
REFERENCES [dbo].[denominaciones] ([id])
GO
ALTER TABLE [dbo].[det_apertura_caja] CHECK CONSTRAINT [FK_det_apertura_caja_denominacion]
GO
ALTER TABLE [dbo].[det_cierre_caja]  WITH CHECK ADD  CONSTRAINT [FK_det_cierre_caja_cierre_caja] FOREIGN KEY([id_cierre_caja])
REFERENCES [dbo].[enc_cierre_caja] ([id])
GO
ALTER TABLE [dbo].[det_cierre_caja] CHECK CONSTRAINT [FK_det_cierre_caja_cierre_caja]
GO
ALTER TABLE [dbo].[det_cierre_caja]  WITH CHECK ADD  CONSTRAINT [FK_det_cierre_caja_denominaciones] FOREIGN KEY([id_denominacion])
REFERENCES [dbo].[denominaciones] ([id])
GO
ALTER TABLE [dbo].[det_cierre_caja] CHECK CONSTRAINT [FK_det_cierre_caja_denominaciones]
GO
ALTER TABLE [dbo].[det_cobros]  WITH CHECK ADD  CONSTRAINT [FK_det_cobros_det_cobros] FOREIGN KEY([id])
REFERENCES [dbo].[det_cobros] ([id])
GO
ALTER TABLE [dbo].[det_cobros] CHECK CONSTRAINT [FK_det_cobros_det_cobros]
GO
ALTER TABLE [dbo].[det_factura]  WITH CHECK ADD  CONSTRAINT [FK_det_factura_productos] FOREIGN KEY([id_plato])
REFERENCES [dbo].[productos] ([id_productos])
GO
ALTER TABLE [dbo].[det_factura] CHECK CONSTRAINT [FK_det_factura_productos]
GO
ALTER TABLE [dbo].[detalle_receta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_receta_productos] FOREIGN KEY([id_producto])
REFERENCES [dbo].[productos] ([id_productos])
GO
ALTER TABLE [dbo].[detalle_receta] CHECK CONSTRAINT [FK_detalle_receta_productos]
GO
ALTER TABLE [dbo].[detalle_receta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_receta_recetas] FOREIGN KEY([id_receta])
REFERENCES [dbo].[recetas] ([id_receta])
GO
ALTER TABLE [dbo].[detalle_receta] CHECK CONSTRAINT [FK_detalle_receta_recetas]
GO
ALTER TABLE [dbo].[detalle_receta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_receta_recetas1] FOREIGN KEY([id_receta])
REFERENCES [dbo].[recetas] ([id_receta])
GO
ALTER TABLE [dbo].[detalle_receta] CHECK CONSTRAINT [FK_detalle_receta_recetas1]
GO
ALTER TABLE [dbo].[detalle_receta]  WITH CHECK ADD  CONSTRAINT [FK_detalle_receta_unidad_medida] FOREIGN KEY([id_unidad])
REFERENCES [dbo].[unidad_medida] ([id_unidad])
GO
ALTER TABLE [dbo].[detalle_receta] CHECK CONSTRAINT [FK_detalle_receta_unidad_medida]
GO
ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [FK_empleado_cargo1] FOREIGN KEY([id_cargo])
REFERENCES [dbo].[cargo] ([id_cargo])
GO
ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [FK_empleado_cargo1]
GO
ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [FK_empleado_departamento1] FOREIGN KEY([id_departamento])
REFERENCES [dbo].[departamento] ([id_departamento])
GO
ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [FK_empleado_departamento1]
GO
ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [FK_empleado_persona] FOREIGN KEY([id_persona])
REFERENCES [dbo].[persona] ([id_persona])
GO
ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [FK_empleado_persona]
GO
ALTER TABLE [dbo].[empleado]  WITH CHECK ADD  CONSTRAINT [FK_empleado_tipo_pago] FOREIGN KEY([id_pago])
REFERENCES [dbo].[tipo_pago] ([id_pago])
GO
ALTER TABLE [dbo].[empleado] CHECK CONSTRAINT [FK_empleado_tipo_pago]
GO
ALTER TABLE [dbo].[empresa]  WITH CHECK ADD  CONSTRAINT [FK_empresa_ciudad] FOREIGN KEY([id_ciudad])
REFERENCES [dbo].[ciudad] ([id_ciudad])
GO
ALTER TABLE [dbo].[empresa] CHECK CONSTRAINT [FK_empresa_ciudad]
GO
ALTER TABLE [dbo].[enc_apertura_caja]  WITH CHECK ADD  CONSTRAINT [FK_apertura_caja_moneda] FOREIGN KEY([id_moneda])
REFERENCES [dbo].[monedas] ([id])
GO
ALTER TABLE [dbo].[enc_apertura_caja] CHECK CONSTRAINT [FK_apertura_caja_moneda]
GO
ALTER TABLE [dbo].[enc_apertura_caja]  WITH CHECK ADD  CONSTRAINT [FK_apertura_caja_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[enc_apertura_caja] CHECK CONSTRAINT [FK_apertura_caja_usuarios]
GO
ALTER TABLE [dbo].[enc_cierre_caja]  WITH CHECK ADD  CONSTRAINT [FK_enc_cierre_caja_apertura] FOREIGN KEY([id_apertura])
REFERENCES [dbo].[enc_apertura_caja] ([id])
GO
ALTER TABLE [dbo].[enc_cierre_caja] CHECK CONSTRAINT [FK_enc_cierre_caja_apertura]
GO
ALTER TABLE [dbo].[enc_cierre_caja]  WITH CHECK ADD  CONSTRAINT [FK_enc_cierre_caja_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[enc_cierre_caja] CHECK CONSTRAINT [FK_enc_cierre_caja_usuarios]
GO
ALTER TABLE [dbo].[enc_cobros]  WITH CHECK ADD  CONSTRAINT [FK_cobros_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[enc_cobros] CHECK CONSTRAINT [FK_cobros_usuarios]
GO
ALTER TABLE [dbo].[enc_cobros]  WITH CHECK ADD  CONSTRAINT [FK_enc_cobros_apertura] FOREIGN KEY([id_apertura])
REFERENCES [dbo].[enc_apertura_caja] ([id])
GO
ALTER TABLE [dbo].[enc_cobros] CHECK CONSTRAINT [FK_enc_cobros_apertura]
GO
ALTER TABLE [dbo].[enc_cobros]  WITH CHECK ADD  CONSTRAINT [FK_enc_cobros_facturas_tmp] FOREIGN KEY([id_factura])
REFERENCES [dbo].[temp_enc_factura] ([id_factura])
GO
ALTER TABLE [dbo].[enc_cobros] CHECK CONSTRAINT [FK_enc_cobros_facturas_tmp]
GO
ALTER TABLE [dbo].[enc_factura]  WITH CHECK ADD  CONSTRAINT [FK_enc_factura_tipo_factura] FOREIGN KEY([id_tipo_factura])
REFERENCES [dbo].[tipo_factura] ([id_tipo_factura])
GO
ALTER TABLE [dbo].[enc_factura] CHECK CONSTRAINT [FK_enc_factura_tipo_factura]
GO
ALTER TABLE [dbo].[enc_factura]  WITH CHECK ADD  CONSTRAINT [FK_enc_factura_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[enc_factura] CHECK CONSTRAINT [FK_enc_factura_usuarios]
GO
ALTER TABLE [dbo].[enc_factura]  WITH CHECK ADD  CONSTRAINT [FK_enc_factura_usuarios1] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[enc_factura] CHECK CONSTRAINT [FK_enc_factura_usuarios1]
GO
ALTER TABLE [dbo].[mesa]  WITH CHECK ADD  CONSTRAINT [FK_mesa_ubicacion] FOREIGN KEY([id_ubicacion])
REFERENCES [dbo].[ubicacion] ([id_ubicacion])
GO
ALTER TABLE [dbo].[mesa] CHECK CONSTRAINT [FK_mesa_ubicacion]
GO
ALTER TABLE [dbo].[monedas]  WITH CHECK ADD  CONSTRAINT [FK_moneda_usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[monedas] CHECK CONSTRAINT [FK_moneda_usuario]
GO
ALTER TABLE [dbo].[nacionalidad]  WITH CHECK ADD  CONSTRAINT [FK_nacionalidad_pais] FOREIGN KEY([id_pais])
REFERENCES [dbo].[paises] ([id])
GO
ALTER TABLE [dbo].[nacionalidad] CHECK CONSTRAINT [FK_nacionalidad_pais]
GO
ALTER TABLE [dbo].[platos]  WITH CHECK ADD  CONSTRAINT [FK_platos_itbis] FOREIGN KEY([id_itbis])
REFERENCES [dbo].[itbis] ([id_itbis])
GO
ALTER TABLE [dbo].[platos] CHECK CONSTRAINT [FK_platos_itbis]
GO
ALTER TABLE [dbo].[platos]  WITH CHECK ADD  CONSTRAINT [FK_platos_recetas] FOREIGN KEY([id_receta])
REFERENCES [dbo].[recetas] ([id_receta])
GO
ALTER TABLE [dbo].[platos] CHECK CONSTRAINT [FK_platos_recetas]
GO
ALTER TABLE [dbo].[productos]  WITH CHECK ADD  CONSTRAINT [FK_productos_categoria] FOREIGN KEY([tipo_producto])
REFERENCES [dbo].[categoria] ([ID_CATEGORIA])
GO
ALTER TABLE [dbo].[productos] CHECK CONSTRAINT [FK_productos_categoria]
GO
ALTER TABLE [dbo].[productos]  WITH CHECK ADD  CONSTRAINT [FK_productos_itbis] FOREIGN KEY([id_itbis])
REFERENCES [dbo].[itbis] ([id_itbis])
GO
ALTER TABLE [dbo].[productos] CHECK CONSTRAINT [FK_productos_itbis]
GO
ALTER TABLE [dbo].[productos]  WITH CHECK ADD  CONSTRAINT [FK_productos_marcas] FOREIGN KEY([id_marca])
REFERENCES [dbo].[marcas] ([id_marca])
GO
ALTER TABLE [dbo].[productos] CHECK CONSTRAINT [FK_productos_marcas]
GO
ALTER TABLE [dbo].[productos]  WITH CHECK ADD  CONSTRAINT [FK_productos_tipo_producto] FOREIGN KEY([tipo_producto])
REFERENCES [dbo].[tipo_producto] ([id_tipo])
GO
ALTER TABLE [dbo].[productos] CHECK CONSTRAINT [FK_productos_tipo_producto]
GO
ALTER TABLE [dbo].[productos]  WITH CHECK ADD  CONSTRAINT [FK_productos_unidad] FOREIGN KEY([id_unidad])
REFERENCES [dbo].[unidad] ([id_unidad])
GO
ALTER TABLE [dbo].[productos] CHECK CONSTRAINT [FK_productos_unidad]
GO
ALTER TABLE [dbo].[proveedor]  WITH CHECK ADD  CONSTRAINT [FK_proveedor_persona1] FOREIGN KEY([id_persona])
REFERENCES [dbo].[persona] ([id_persona])
GO
ALTER TABLE [dbo].[proveedor] CHECK CONSTRAINT [FK_proveedor_persona1]
GO
ALTER TABLE [dbo].[secuencia_ncf]  WITH CHECK ADD  CONSTRAINT [FK_secuencia_ncf_tipo_ncf] FOREIGN KEY([id_tipo_ncf])
REFERENCES [dbo].[tipo_ncf] ([id])
GO
ALTER TABLE [dbo].[secuencia_ncf] CHECK CONSTRAINT [FK_secuencia_ncf_tipo_ncf]
GO
ALTER TABLE [dbo].[secuencia_ncf]  WITH CHECK ADD  CONSTRAINT [FK_secuencia_ncf_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[secuencia_ncf] CHECK CONSTRAINT [FK_secuencia_ncf_usuarios]
GO
ALTER TABLE [dbo].[sub_categoria]  WITH CHECK ADD  CONSTRAINT [FK_SUB_CATEGORIA_CAEGORIA] FOREIGN KEY([ID_CATEGORIA])
REFERENCES [dbo].[categoria] ([ID_CATEGORIA])
GO
ALTER TABLE [dbo].[sub_categoria] CHECK CONSTRAINT [FK_SUB_CATEGORIA_CAEGORIA]
GO
ALTER TABLE [dbo].[sucursal]  WITH CHECK ADD  CONSTRAINT [FK_sucursal_empresa] FOREIGN KEY([id_empresa])
REFERENCES [dbo].[empresa] ([id_empresa])
GO
ALTER TABLE [dbo].[sucursal] CHECK CONSTRAINT [FK_sucursal_empresa]
GO
ALTER TABLE [dbo].[temp_det_factura]  WITH CHECK ADD  CONSTRAINT [FK_temp_det_factura_temp_enc_factura] FOREIGN KEY([id_factura])
REFERENCES [dbo].[temp_enc_factura] ([id_factura])
GO
ALTER TABLE [dbo].[temp_det_factura] CHECK CONSTRAINT [FK_temp_det_factura_temp_enc_factura]
GO
ALTER TABLE [dbo].[temp_enc_factura]  WITH CHECK ADD  CONSTRAINT [FK_temp_enc_factura_cliente] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[cliente] ([id_cliente])
GO
ALTER TABLE [dbo].[temp_enc_factura] CHECK CONSTRAINT [FK_temp_enc_factura_cliente]
GO
ALTER TABLE [dbo].[temp_enc_factura]  WITH CHECK ADD  CONSTRAINT [FK_temp_enc_factura_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[temp_enc_factura] CHECK CONSTRAINT [FK_temp_enc_factura_usuarios]
GO
ALTER TABLE [dbo].[tipo_cobro]  WITH CHECK ADD  CONSTRAINT [FK_tipo_cobro_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[tipo_cobro] CHECK CONSTRAINT [FK_tipo_cobro_usuarios]
GO
ALTER TABLE [dbo].[tipo_ncf]  WITH CHECK ADD  CONSTRAINT [FK_tipo_ncf_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[tipo_ncf] CHECK CONSTRAINT [FK_tipo_ncf_usuarios]
GO
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_empleado] FOREIGN KEY([ficha])
REFERENCES [dbo].[empleado] ([ficha])
GO
ALTER TABLE [dbo].[usuarios] CHECK CONSTRAINT [FK_usuarios_empleado]
GO
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_persona] FOREIGN KEY([id_persona])
REFERENCES [dbo].[persona] ([id_persona])
GO
ALTER TABLE [dbo].[usuarios] CHECK CONSTRAINT [FK_usuarios_persona]
GO
ALTER TABLE [dbo].[cajas]  WITH CHECK ADD  CONSTRAINT [CK_cajas_desactivado] CHECK  (([desactivado]='S' OR [desactivado]='N'))
GO
ALTER TABLE [dbo].[cajas] CHECK CONSTRAINT [CK_cajas_desactivado]
GO
ALTER TABLE [dbo].[denominaciones]  WITH CHECK ADD  CONSTRAINT [CK_denominaciones_desactivado] CHECK  (([desactivado]='S' OR [desactivado]='N'))
GO
ALTER TABLE [dbo].[denominaciones] CHECK CONSTRAINT [CK_denominaciones_desactivado]
GO
ALTER TABLE [dbo].[denominaciones]  WITH CHECK ADD  CONSTRAINT [CK_denominaciones_moneda] CHECK  (([moneda]='S' OR [moneda]='N'))
GO
ALTER TABLE [dbo].[denominaciones] CHECK CONSTRAINT [CK_denominaciones_moneda]
GO
ALTER TABLE [dbo].[denominaciones]  WITH CHECK ADD  CONSTRAINT [CK_denominaciones_valor] CHECK  (([valor] IS NULL OR [valor]>(0)))
GO
ALTER TABLE [dbo].[denominaciones] CHECK CONSTRAINT [CK_denominaciones_valor]
GO
ALTER TABLE [dbo].[det_apertura_caja]  WITH CHECK ADD  CONSTRAINT [CK_det_apertura_caja_cantidad] CHECK  (([cantidad]>(0)))
GO
ALTER TABLE [dbo].[det_apertura_caja] CHECK CONSTRAINT [CK_det_apertura_caja_cantidad]
GO
ALTER TABLE [dbo].[det_cierre_caja]  WITH CHECK ADD  CONSTRAINT [CK_det_cierre_caja_cantidad] CHECK  (([cantidad]>(0)))
GO
ALTER TABLE [dbo].[det_cierre_caja] CHECK CONSTRAINT [CK_det_cierre_caja_cantidad]
GO
ALTER TABLE [dbo].[det_cierre_caja]  WITH CHECK ADD  CONSTRAINT [CK_det_cierre_caja_monto] CHECK  (([monto]>(0)))
GO
ALTER TABLE [dbo].[det_cierre_caja] CHECK CONSTRAINT [CK_det_cierre_caja_monto]
GO
ALTER TABLE [dbo].[det_cobros]  WITH CHECK ADD  CONSTRAINT [CK_det_cobros_desactivado] CHECK  (([desactivado]='N' OR [desactivado]='S'))
GO
ALTER TABLE [dbo].[det_cobros] CHECK CONSTRAINT [CK_det_cobros_desactivado]
GO
ALTER TABLE [dbo].[enc_apertura_caja]  WITH CHECK ADD  CONSTRAINT [CK_apertura_caja_desactivado] CHECK  (([desactivado]='S' OR [desactivado]='N'))
GO
ALTER TABLE [dbo].[enc_apertura_caja] CHECK CONSTRAINT [CK_apertura_caja_desactivado]
GO
ALTER TABLE [dbo].[enc_apertura_caja]  WITH CHECK ADD  CONSTRAINT [CK_apertura_caja_fecha_final] CHECK  (([fecha_final]>=[fecha_inicio]))
GO
ALTER TABLE [dbo].[enc_apertura_caja] CHECK CONSTRAINT [CK_apertura_caja_fecha_final]
GO
ALTER TABLE [dbo].[enc_apertura_caja]  WITH CHECK ADD  CONSTRAINT [CK_apertura_caja_monto_final] CHECK  (([monto_final]>=(0)))
GO
ALTER TABLE [dbo].[enc_apertura_caja] CHECK CONSTRAINT [CK_apertura_caja_monto_final]
GO
ALTER TABLE [dbo].[enc_apertura_caja]  WITH CHECK ADD  CONSTRAINT [CK_apertura_caja_monto_inicial] CHECK  (([monto_inicial]>=(0)))
GO
ALTER TABLE [dbo].[enc_apertura_caja] CHECK CONSTRAINT [CK_apertura_caja_monto_inicial]
GO
ALTER TABLE [dbo].[enc_cierre_caja]  WITH CHECK ADD  CONSTRAINT [CK_enc_cierre_caja_desactivado] CHECK  (([desactivado]='S' OR [desactivado]='N'))
GO
ALTER TABLE [dbo].[enc_cierre_caja] CHECK CONSTRAINT [CK_enc_cierre_caja_desactivado]
GO
ALTER TABLE [dbo].[enc_cierre_caja]  WITH CHECK ADD  CONSTRAINT [CK_enc_cierre_caja_fecha_cierre] CHECK  (([fecha_cierre] IS NULL OR [fecha_cierre]>=[fecha_apertura]))
GO
ALTER TABLE [dbo].[enc_cierre_caja] CHECK CONSTRAINT [CK_enc_cierre_caja_fecha_cierre]
GO
ALTER TABLE [dbo].[enc_cobros]  WITH CHECK ADD  CONSTRAINT [CK_cobros_monto] CHECK  (([monto]>(0)))
GO
ALTER TABLE [dbo].[enc_cobros] CHECK CONSTRAINT [CK_cobros_monto]
GO
ALTER TABLE [dbo].[monedas]  WITH CHECK ADD  CONSTRAINT [CK_moneda_desactivado] CHECK  (([desactivado]='S' OR [desactivado]='N'))
GO
ALTER TABLE [dbo].[monedas] CHECK CONSTRAINT [CK_moneda_desactivado]
GO
ALTER TABLE [dbo].[monedas]  WITH CHECK ADD  CONSTRAINT [CK_moneda_sentido_compra] CHECK  (([sentido_compra]='M' OR [sentido_compra]='D'))
GO
ALTER TABLE [dbo].[monedas] CHECK CONSTRAINT [CK_moneda_sentido_compra]
GO
ALTER TABLE [dbo].[monedas]  WITH CHECK ADD  CONSTRAINT [CK_moneda_tasa_del_dia] CHECK  (([tasa_del_dia]>=(1)))
GO
ALTER TABLE [dbo].[monedas] CHECK CONSTRAINT [CK_moneda_tasa_del_dia]
GO
ALTER TABLE [dbo].[monedas]  WITH CHECK ADD  CONSTRAINT [CK_monedas_sentido_venta] CHECK  (([sentido_venta]='M' OR [sentido_venta]='D'))
GO
ALTER TABLE [dbo].[monedas] CHECK CONSTRAINT [CK_monedas_sentido_venta]
GO
ALTER TABLE [dbo].[paises]  WITH CHECK ADD  CONSTRAINT [CK_paises_desactivado] CHECK  (([desactivado]='N' OR [desactivado]='S'))
GO
ALTER TABLE [dbo].[paises] CHECK CONSTRAINT [CK_paises_desactivado]
GO
ALTER TABLE [dbo].[secuencia_ncf]  WITH CHECK ADD  CONSTRAINT [CK_secuencia_ncf_desactivado] CHECK  (([desactivado]='S' OR [desactivado]='N'))
GO
ALTER TABLE [dbo].[secuencia_ncf] CHECK CONSTRAINT [CK_secuencia_ncf_desactivado]
GO
ALTER TABLE [dbo].[secuencia_ncf]  WITH CHECK ADD  CONSTRAINT [CK_secuencia_ncf_final] CHECK  (([secuencia_final]>=[secuencia_inicial]))
GO
ALTER TABLE [dbo].[secuencia_ncf] CHECK CONSTRAINT [CK_secuencia_ncf_final]
GO
ALTER TABLE [dbo].[tipo_cobro]  WITH CHECK ADD  CONSTRAINT [CK_tipo_cobro_desactivado] CHECK  (([desactivado]='S' OR [desactivado]='N'))
GO
ALTER TABLE [dbo].[tipo_cobro] CHECK CONSTRAINT [CK_tipo_cobro_desactivado]
GO
ALTER TABLE [dbo].[tipo_ncf]  WITH CHECK ADD  CONSTRAINT [ck_desactivado_ncf] CHECK  (([desactivado]='S' OR [desactivado]='N'))
GO
ALTER TABLE [dbo].[tipo_ncf] CHECK CONSTRAINT [ck_desactivado_ncf]
GO
ALTER TABLE [dbo].[tipo_ncf]  WITH CHECK ADD  CONSTRAINT [ck_facturable_ncf] CHECK  (([facturable]='S' OR [desactivado]='N'))
GO
ALTER TABLE [dbo].[tipo_ncf] CHECK CONSTRAINT [ck_facturable_ncf]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identifica si el valor de la tasa debe ser multiplicado o dividido' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'monedas', @level2type=N'COLUMN',@level2name=N'sentido_compra'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'S => desactivado,, N=> activo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'secuencia_ncf', @level2type=N'CONSTRAINT',@level2name=N'CK_secuencia_ncf_desactivado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "condiciones_pago"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 227
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "cliente"
            Begin Extent = 
               Top = 6
               Left = 265
               Bottom = 136
               Right = 470
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "persona"
            Begin Extent = 
               Top = 6
               Left = 508
               Bottom = 136
               Right = 678
            End
            DisplayFlags = 280
            TopColumn = 5
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vClientes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vClientes'
GO
USE [master]
GO
ALTER DATABASE [efood] SET  READ_WRITE 
GO
