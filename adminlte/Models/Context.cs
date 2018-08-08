using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace adminlte.Models
{
    public class Context : DbContext
    {
        public DbSet<Tarefas> Tarefas { get; set; }
        public DbSet<Financeiro> Financeiro { get; set; }
        public DbSet<Eventos> Eventos { get; set; }

        
    }
}