using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using adminlte.Models;

namespace adminlte.Controllers
{
    public class FinanceiroController : Controller
    {
        private Context db = new Context();

        // GET: /Financeiro/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TotalMoney()
        {
            var moneyList = db.Financeiro.ToList();
            var entries = moneyList.Where(x => x.Tipo == "Entrada").Sum(x => x.Valor);
            var outs = moneyList.Where(x => x.Tipo == "Saída").Sum(x => x.Valor);
            var total = entries - outs;

            return Json(total, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult NextEvent()
        //{
        //    var events = db.Eventos.ToList().OrderBy(x => x.Data).First();
        //    var dataEvento = events.Data.ToString("dd/MM/yyyy");
        //    var evento = events.Evento;

        //    List<string> eventoData = new List<string>();
        //    eventoData.Add(dataEvento);
        //    eventoData.Add(evento);

        //    return Json(eventoData, JsonRequestBehavior.AllowGet);
        //}
    }
}
