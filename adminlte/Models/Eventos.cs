﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace adminlte.Models
{
    [Table("EVENTOS")]
    public class Eventos
    {
        [Key]
        [Column("ID")]
        public int EventosId { get; set; }

        [Column("EVENTO")]
        public string Evento { get; set; }

        [Column("DATA")]
        public DateTime Data { get; set; }

        public String TxData { get { return Data.ToString("dd/MM/yyyy"); } }
    }
}