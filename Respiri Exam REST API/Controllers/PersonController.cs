using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Respiri_Exam_REST_API.Models;
using Respiri_Exam_REST_API.Data;

namespace Respiri_Exam_REST_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApiContext _context;

        public PersonController(ApiContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<string> GetVersionNumber()
        {
            return new string[] { "Hello World", GetType().Assembly.GetName().Version.ToString() };
        }

        [HttpPost]
        public string CreatePerson(Person person)
        {
            var personRecord = _context.Persons.Find(person.Id);
            var result = string.Empty;
            if (personRecord == null)
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
                result = "Record succesfully created";
            }
            else
                result = "Record already exists";

            return result;
        }

        [HttpGet]
        public List<Person> GetPersonList()
        {
            var result = _context.Persons.ToList();
            return result;
        }

        [HttpGet]
        public Person GetPersonById(int id)
        {
            var result = _context.Persons.Find(id);
            if (result == null)
                return new Person();

            return result;
        }
    }
}
