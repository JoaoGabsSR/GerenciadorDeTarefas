﻿// <auto-generated />
using System;
using GerenciadorDeTarefas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GerenciadorDeTarefas.Migrations
{
    [DbContext(typeof(GerenciadorDeTarefasContext))]
    partial class GerenciadorDeTarefasContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GerenciadorDeTarefas.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateFinished")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateToFinishing")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("TaskName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("GerenciadorDeTarefas.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GerenciadorDeTarefas.Models.Task", b =>
                {
                    b.HasOne("GerenciadorDeTarefas.Models.User", "user")
                        .WithMany("Tasks")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("GerenciadorDeTarefas.Models.User", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
