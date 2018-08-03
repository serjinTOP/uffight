using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace adminlte.Models
{
    [Table("FINANCEIRO")]
    public class Financeiro
    {
        [Key]
        [Column("ID")]
        public int FinanceiroId { get; set; }

        [Column("VALOR")]
        public decimal Valor { get; set; }

        [Column("MOTIVO")]
        public string Motivo { get; set; }

        [Column("TIPO")]
        public string Tipo { get; set; }
    }
}