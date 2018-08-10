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
    public class TarefasController : Controller
    {
        private Context db = new Context();

        // GET: /Tarefas/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NumOfTasks()
        {
            var tasks = db.Tarefas.Where(x => x.Status != "Resolvida").ToList().Count();

            return Json(tasks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TaskList()
        {
            var tarefasList = db.Tarefas.Where(x => x.Status != "Resolvida").OrderBy(x => x.Robo).ThenBy(x => x.Urgencia).ThenBy(x => x.Status);

            return Json(tarefasList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(string tarefa, string robo, string urgencia, string status)
        {
            try
            {
                var tarefas = new Tarefas();
                tarefas.Tarefa = tarefa;
                tarefas.Robo = robo;
                tarefas.Urgencia = urgencia;
                tarefas.Status = status;

                db.Tarefas.Add(tarefas);
                db.SaveChanges();

                Response.Redirect(Url.Action("Index", "Tarefas"));
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
        public JsonResult Delete(Tarefas tarefa)
        {
            //var delete = new Tarefas { TarefaId = id };

            db.Tarefas.Attach(tarefa);
            db.Tarefas.Remove(tarefa);
            db.SaveChanges();

            Response.Redirect(Url.Action("Index", "Tarefas"));

            return null;
        }   

        public JsonResult EditForm(int id)
        {
            var tarefa = db.Tarefas.ToList().Where(x => x.TarefaId == id).FirstOrDefault();

            return Json(tarefa, JsonRequestBehavior.AllowGet);
        }

        // POST: Tarefas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.

        public JsonResult Edit( Tarefas tarefas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarefas).State = EntityState.Modified;
                db.SaveChanges();
            }
            Response.Redirect(Url.Action("Index", "Tarefas"));
            return null;
        }
    }
}
