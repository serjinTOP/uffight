using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using adminlte.Models;
using System.Data.Entity.Validation;

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

        public ActionResult MoneyList()
        {
            var moneyList = db.Financeiro.ToList().OrderBy(x => x.Tipo);

            return Json(moneyList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(decimal valor, string motivo, string tipo)
        {
            try
            {
                var movFinanceira = new Financeiro();
                movFinanceira.Valor = valor;
                movFinanceira.Motivo = motivo;
                movFinanceira.Tipo = tipo;

                db.Financeiro.Add(movFinanceira);
                db.SaveChanges(); 

                Response.Redirect(Url.Action("Index", "Financeiro"));
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return null;
        }
        public JsonResult Delete(int id)
        {
            var delete = new Financeiro { FinanceiroId = id };
            db.Financeiro.Attach(delete);
            db.Financeiro.Remove(delete);
            db.SaveChanges();

            Response.Redirect(Url.Action("Index", "Financeiro"));

            return null;
        }
    }
}
