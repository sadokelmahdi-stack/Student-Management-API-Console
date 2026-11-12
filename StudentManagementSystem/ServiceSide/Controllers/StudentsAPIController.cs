using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.DataSumelation;
using StudentsAPI.Model;

namespace StudentsAPI.Controllers
{

    // [Route("api/[controller]")]

    [ApiController]
    [Route("api/StudentsAPI")]

    public class StudentsAPIController : ControllerBase
    {

        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<clsStudents>> GetAllStudents()
        {
            return Ok(clsStudentsDataSumelation.StudentsList);
        }

        [HttpGet("Pass", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<clsStudents>> GetPassedStudents()
        {

            var PassedStudents = clsStudentsDataSumelation.StudentsList.Where(Student => Student.Grade >= 50).ToList();
            return Ok(PassedStudents);

        }


        [HttpGet("AVG", Name = "AverageGrade")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<double> GetAverageGrade()
        {

            //clsStudentsDataSumelation.StudentsList.Clear();

            if (clsStudentsDataSumelation.StudentsList.Count == 0)
            {
                return NotFound("Not Student Found . ");
            }

            var AvrageGrade = clsStudentsDataSumelation.StudentsList.Average(Stu => Stu.Grade);

            return Ok(AvrageGrade);

        }

        [HttpGet("{ID}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<clsStudents> GetStudentByID(int ID)
        {

            if (ID < 1)
            {
                return BadRequest($"Not Accept ID {ID}");
            }

            var student = clsStudentsDataSumelation.StudentsList.FirstOrDefault(Student => Student.StudentID == ID);

            if (student == null)
            {
                return NotFound($"No Student with {ID}");
            }

            return Ok(student);


        }


        [HttpPost(Name = "AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<clsStudents> AddNewStudent(clsStudents NewStudent)
        {

            if (NewStudent == null || string.IsNullOrEmpty(NewStudent.Name) || NewStudent.Age == 0 || NewStudent.Grade < 1)
            {
                return BadRequest("Error : Bad Request . ");
            }


            NewStudent.StudentID = clsStudentsDataSumelation.StudentsList.Count > 0 ? clsStudentsDataSumelation.StudentsList.Max(s => s.StudentID) + 1 : 1;

            clsStudentsDataSumelation.StudentsList.Add(NewStudent);


            return CreatedAtRoute("GetStudentByID", new { id = NewStudent.StudentID }, NewStudent);
        }


        [HttpDelete("{id}" , Name ="DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<clsStudents> DeleteStudent(int id)
        {

            if(id < 0)
            {
                return BadRequest("Error : Bad Request");
            }

            var Students = clsStudentsDataSumelation.StudentsList.FirstOrDefault(st => st.StudentID==id);

            if(Students == null)
            {
                return NotFound("the student not found ");
            }

            var DeletedStudent = clsStudentsDataSumelation.StudentsList.Remove(Students);

            return Ok($"the Student wit ID {id} has ben deleted ");
        }


        [HttpPut("{id}" , Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<clsStudents> UpdateStudent(int id , clsStudents UpdatedStudent)
        {

            if (UpdatedStudent == null || string.IsNullOrEmpty(UpdatedStudent.Name) || UpdatedStudent.Age <1 || UpdatedStudent.Grade<1)
            {
                return BadRequest("Error : Bad Request");

            }

            var student = clsStudentsDataSumelation.StudentsList.FirstOrDefault(st=>st.StudentID==id);

            if (student == null)
            {

                return NotFound($"the Student with ID {id} not found");
            }



            student.Name = UpdatedStudent.Name;
            student.Age = UpdatedStudent.Age;
            student.Grade = UpdatedStudent.Grade;



            return Ok(student);


            }
    }
}