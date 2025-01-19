﻿// <auto-generated />
using System;
using ExperimentoAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExperimentoAPI.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    [Migration("20250118075912_AddUsuarioRolRelationship")]
    partial class AddUsuarioRolRelationship
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ExperimentoAPI.Models.Carrito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("carritos");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.CarritoDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CarritoId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductoId")
                        .HasColumnType("integer");

                    b.Property<int>("cantidad")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CarritoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("CarritoDetalles");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("categorias");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Consumidor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("EstaActivo")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Consumidor");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.DetalleVenta", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("cantidad")
                        .HasColumnType("integer");

                    b.Property<int>("idProducto")
                        .HasColumnType("integer");

                    b.Property<string>("nombreProducto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("precioUnitario")
                        .HasColumnType("numeric");

                    b.Property<decimal>("subTotal")
                        .HasColumnType("numeric");

                    b.Property<int>("ventaId")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("ventaId");

                    b.ToTable("detallesVenta");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Producto", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("precio")
                        .HasColumnType("integer");

                    b.Property<decimal>("stock")
                        .HasColumnType("numeric");

                    b.HasKey("id");

                    b.ToTable("productos", (string)null);
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Producto2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Precio")
                        .HasColumnType("numeric");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.Property<int?>("idCategoria")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("idCategoria");

                    b.ToTable("productos2");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("contraseña")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("usuario")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Venta", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("total")
                        .HasColumnType("numeric");

                    b.Property<int>("usuarioId")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("ventas");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Carrito", b =>
                {
                    b.HasOne("ExperimentoAPI.Models.Consumidor", "Usuario")
                        .WithMany("Carritos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.CarritoDetalle", b =>
                {
                    b.HasOne("ExperimentoAPI.Models.Carrito", "Carrito")
                        .WithMany("Detalles")
                        .HasForeignKey("CarritoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExperimentoAPI.Models.Producto2", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrito");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.DetalleVenta", b =>
                {
                    b.HasOne("ExperimentoAPI.Models.Venta", null)
                        .WithMany("detallesVenta")
                        .HasForeignKey("ventaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Producto2", b =>
                {
                    b.HasOne("ExperimentoAPI.Models.Categoria", "categoria")
                        .WithMany("productos")
                        .HasForeignKey("idCategoria");

                    b.Navigation("categoria");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Carrito", b =>
                {
                    b.Navigation("Detalles");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Categoria", b =>
                {
                    b.Navigation("productos");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Consumidor", b =>
                {
                    b.Navigation("Carritos");
                });

            modelBuilder.Entity("ExperimentoAPI.Models.Venta", b =>
                {
                    b.Navigation("detallesVenta");
                });
#pragma warning restore 612, 618
        }
    }
}
