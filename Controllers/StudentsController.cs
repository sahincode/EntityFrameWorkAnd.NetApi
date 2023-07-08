using EntityFramework.Configurations;
using EntityFramework.DbData;
using EntityFramework.DbData.Entities;
using EntityFramework.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EntityFramework.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly PositionOptions _positionOptions;
        private readonly StudentEntityContext _studentEntityContext;


        public StudentsController(IConfiguration configuration,
            IOptions<PositionOptions> options,
            StudentEntityContext studentEntityContext
           
            ) {
            _configuration = configuration;
            _positionOptions = options.Value;
            _studentEntityContext = studentEntityContext;

        }
        [HttpGet("GetCOnfiguration")]

        public object getConfiguration()
        {
            var configurations = new
            {
                GeneralAPIKEY = _configuration["Api-Key"],
                SMSApiKey = _configuration["SMSApi:Api-Key"],
                FromNumberApiKey = _configuration["SMSApi:FromEmail"],
                PositonOptions = _positionOptions

            };
            return configurations;

        }
        [HttpGet("GetAll")]

        public async Task<List<Student>> getallStudents()
        {
            return await _studentEntityContext.Students.ToListAsync();
        }
        [HttpGet("GetAllByID")]
        public async Task<IActionResult> getStudentByID(int ID)
        {
            var Student = await _studentEntityContext.Students.FirstOrDefaultAsync(s => s.StudentID == ID);
            if (Student == null)
            {
                return NotFound();
            }
            return Ok(Student);
        }
        [HttpPut("Update")]
        public async Task<Student> UpdateById(int id, Student newstudent )
        {
            var  student = await _studentEntityContext.Students.FirstOrDefaultAsync(s => s.StudentID == id);

            student.StudentName = newstudent.StudentName;
            return newstudent;

        }
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Student student)
        {
            await _studentEntityContext.Students.AddAsync(student);
            await _studentEntityContext.SaveChangesAsync();
            return Created($"api/student/student/{student.StudentID}", student);
        }


    }

}
