﻿using ExoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExoApi.Contexts
{
    public class ChapterContext : DbContext
    {

        // dbcontext é a ponte entre o modelo de classe e o banco de  dados


        public ChapterContext()
        {
        }
        public ChapterContext(DbContextOptions<ChapterContext> options) : base(options)
        {
        }
        // vamos utilizar esse método para configurar o banco de  dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // cada provedor tem sua sintaxe para especificação 
                //  - "Server=localhost,7205;Database=EXOPAI;TrustServerCertificate=True" User ID=sa;Password=1q2w3e4r@#$
                optionsBuilder.UseSqlServer("Data Source = .; initial catalog = EXOAPI; Integrated Security = true");
            }
        }
        // dbset representa as entidades que serão utilizadas nas operações de leitura, criação, atualização e deleção
        public DbSet<Projetos> PROJETOS { get; set; }
        public DbSet<Usuarios> USUARIOS { get; set; }


    }
}
