﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web_Dev3.Models;

namespace Web_Dev3.Controllers
{
    public class TodoController : Controller
    {
        public const string ADMIN = "Admin";
        public const string ADMIN_EMAIL = "admin@todo.de";
        public const string ADMIN_INDEX = "AdminIndex";
        public const string INDEX = "Index";



        // GET: Todo
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.User.IsInRole(ADMIN))
                {
                    return RedirectToAction(ADMIN_INDEX);
                }
                else
                {
                    using (var db = new ApplicationDbContext())
                    {
                        var loggedInUser = HttpContext.User.Identity.Name;
                        var user = db.Users.First(u => u.Email == loggedInUser);
                        var todoList = user.Todos.ToList();
                        return View(todoList);
                    }
                }
            }
            return View();
        }

        public ActionResult AdminIndex()
        {
            using (var db = new ApplicationDbContext())
            {
                List<ApplicationUser> userList;
                try
                {
                    userList = db.Users.ToList();
                }
                catch
                {
                    return View();
                }
                var userAndTodos = new List<string[]>();
                var ownerTodos = new List<ICollection<TodoModel>>();
                foreach (var user in userList)
                {
                    if (!user.Roles.Equals(ADMIN))
                    {
                        var todolist = user.Todos.ToList();
                        ownerTodos.Add(todolist);
                        foreach (var todo in todolist)
                        {
                            string[] todos = new string[3];
                            todos[0] = user.UserName;
                            todos[1] = todo.Titel;
                            todos[2] = todo.Description;
                            userAndTodos.Add(todos);
                        }
                    }
                }
                ViewData["ownerTodos"] = ownerTodos;
                return View();
            }
        }

        // GET: Todo/Create
        public ActionResult Add()
        {

            return View();
        }

        // POST: Todo/Create
        [HttpPost]
        public ActionResult Add(TodoModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using (var db = new Models.ApplicationDbContext())
                {
                    if (HttpContext.User.Identity.IsAuthenticated)
                    {
                        var loggedInUser = HttpContext.User.Identity.Name;
                        var user = db.Users.First(u => u.Email == loggedInUser);
                        model.Owner = user;
                        db.Todo.Add(model);
                        db.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("LogIn", "Account", new { area = "" });
                    }
                }
                return RedirectToAction(INDEX);
            }
            catch
            {
                return RedirectToAction(INDEX);
            }
        }

        // GET: Todo/Edit/5
        public ActionResult Edit(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var loggedInUser = HttpContext.User.Identity.Name;
                    var user = db.Users.First(u => u.Email == loggedInUser);
                    var list = db.Todo.Where(b => b.Id == id);
                    return View(list.First());

                } else
                {
                    return RedirectToAction("LogIn", "Account", new { area = "" });
                }
            }
        }

        // POST: Todo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TodoModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using (var db = new ApplicationDbContext())
                {
                    if (HttpContext.User.Identity.IsAuthenticated)
                    {
                        var list = db.Todo.Where(b => b.Id == model.Id);
                        list.First().Titel = model.Titel;
                        list.First().Description = model.Description;
                        db.SaveChanges();
                        return RedirectToAction(INDEX);
                    }
                    else
                    {
                        return RedirectToAction("LogIn", "Account", new { area = "" });
                    }
                }
            }
            catch
            {
                return RedirectToAction(INDEX);
            }
        }

        // GET: Todo/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    if (HttpContext.User.Identity.IsAuthenticated)
                    {
                        var list = db.Todo.Where(b => b.Id == id);
                        return View(list.First());
                    }
                    else
                    {
                        return RedirectToAction("LogIn", "Account", new { area = "" });
                    }
                }
            }
            catch
            {
                return RedirectToAction(INDEX);
            }
        }

        // POST: Todo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    if (HttpContext.User.Identity.IsAuthenticated)
                    {
                        var list = db.Todo.Where(b => b.Id == id);
                        db.Todo.Remove(list.First());
                        db.SaveChanges();
                        return RedirectToAction(INDEX);
                    }
                    else
                    {
                        return RedirectToAction("LogIn", "Account", new { area = "" });
                    }
                }
            }
            catch
            {
                return RedirectToAction(INDEX);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}