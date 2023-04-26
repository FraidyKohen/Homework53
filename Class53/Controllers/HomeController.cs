
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Class53.Controllers;
using Class53.data;

namespace Class53.Controllers
{
    public class HomeController : Controller
    {
        string _connectionString = "Data Source=.\\sqlexpress;Initial Catalog=People;Integrated Security=true";

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPeople ()
        {
            var personRepo = new PersonRepository(_connectionString);
            List<Person> people = personRepo.GetPeople();
            return Json(people);
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            var personRepo = new PersonRepository(_connectionString);
            personRepo.AddPerson(person);
        }

        [HttpPost]
        public void EditPerson (Person person)
        {
            var personRepo = new PersonRepository(_connectionString);
            personRepo.EditPerson(person);
        }

        public IActionResult GetPersonById (int id)
        {
            var personRepo = new PersonRepository(_connectionString);
            Person p = personRepo.GetPersonById(id);
            return Json(p);
        }

        public void DeletePerson(int id)
        {
            var personRepo = new PersonRepository(_connectionString);
            personRepo.DeletePersonById(id);
        }
      
    }
}