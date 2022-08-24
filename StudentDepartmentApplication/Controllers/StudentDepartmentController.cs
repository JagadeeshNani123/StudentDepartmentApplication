using Microsoft.AspNetCore.Mvc;
using StudentDepartmentApplication.Models;
using System.Runtime.Intrinsics.Arm;

namespace StudentDepartmentApplication.Controllers
{
    public class StudentDepartmentController : Controller
    {
        public static List<Student> students = new List<Student>();

        public static List<Department> departments = new List<Department>();

        [HttpPost]
        [Route("AddDepartment")]
        public IActionResult Post([FromBody] Department dpt)
        {
            //add student

            var result = "Not a valid Department or Department aleardy exists";
            if (departments.Any(dpn => dpn.DepartmentNo == dpt.DepartmentNo))
            {
                try
                {
                    departments.Add(dpt);
                    return Ok("Department Record Added");
                }
                catch (Exception ex)
                {
                    throw new Exception(result);
                }
            }
            else
                return BadRequest(result);
        }

        [HttpPost]
        [Route("AddStudent")]
        public IActionResult Post([FromBody] Student std)
        {
            //add employee
            var result = "Not a valid Department number";
            if (std != null && departments.Any(dp => std.DepartmentNo == dp.DepartmentNo))
            {
                try
                {
                    students.Add(std);
                    return Ok("Student Record Added");
                }
                catch (Exception ex)
                {
                    throw new Exception(result);
                }
            }
            else
                return BadRequest(result);
        }

        [HttpPut]
        [Route("EditName")]
        public IActionResult UpdateStudentNameDetails(int stdNo, string newStdName)
        {
            var result = "Student Not found";
            try
            {
                var student = students.FirstOrDefault(emp => emp.StudentNo == stdNo);
                if (student != null)
                    student.StudentName = newStdName;
                else
                    result = "Student name modified";
            }
            catch(Exception ex)
            {
                throw new Exception(result);
            }
            return Ok(result);
        }


        [HttpPut]
        [Route("EditDepartNo")]
        public IActionResult UpdateStudentDepartmentNoDetails(int stdNo, int newDepNo)
        {
            //add employee
            var result = "Student Not found";
            var student = students.FirstOrDefault(emp => emp.StudentNo == stdNo);
            if (student != null && departments.Any(dp => student.DepartmentNo == dp.DepartmentNo))
            {
                try
                {
                    student.DepartmentNo = newDepNo;

                    return Ok("Department Number modified");
                }
                catch (Exception ex)
                {
                    throw new Exception(result);
                }
            }
            else

                return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int stdNo)
        {
            var result = "Student Not found";
            var student = students.FirstOrDefault(emp => emp.StudentNo == stdNo);
            if (student != null)
            {
                try
                {
                    students.Remove(student);
                    return Ok("Student record removed");
                }

                catch (Exception ex)
                {
                    throw new Exception(result);
                }
            }
            else
                return Ok(result);
        }


        public IEnumerable<Student> GetStudentList()
        {
            return students;
        }

        public IEnumerable<Department> GetDepartmentList()
        {
            return departments;
        }
    }
}
