﻿using System;
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
    public class EventosController : Controller
    {
        private Context db = new Context();

        // GET: Eventos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EventsList()
        {
            var events = db.Eventos.ToList().OrderBy(x => x.Data);


            return Json(events, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(string evento, DateTime data)
        {
            try
            {
                var eventos = new Eventos();
                eventos.Evento = evento;
                eventos.Data = data;

                db.Eventos.Add(eventos);
                db.SaveChanges();

                Response.Redirect(Url.Action("Index", "Eventos"));
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

        public ActionResult NextEvent()
        {
            var events = db.Eventos.ToList().OrderBy(x => x.Data).First();
            var dataEvento = events.Data.ToString("dd/MM/yyyy");
            var evento = events.Evento;

            List<string> eventoData = new List<string>();
            eventoData.Add(dataEvento);
            eventoData.Add(evento);

            return Json(eventoData, JsonRequestBehavior.AllowGet);
        }

    }
}
