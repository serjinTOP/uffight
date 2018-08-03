using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace adminlte.Models
{
    [Table("TAREFAS")]
    public class Tarefas
    {
        [Key]
        [Column("ID")]
        public int TarefaId { get; set; }

        [Column("TAREFA")]
        public string Tarefa { get; set; }

        [Column("ROBO")]
        public string Robo { get; set; }

        [Column("URGENCIA")]
        public string Urgencia { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }
    }
}