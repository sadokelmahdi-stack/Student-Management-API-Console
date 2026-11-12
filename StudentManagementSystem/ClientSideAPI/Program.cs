using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace ClientSideCodeAPI
{
    class Program
    {

        static readonly HttpClient HttpClient = new HttpClient();

        static async Task Main(string[] args)
        {

            HttpClient.BaseAddress = new Uri("https://localhost:7002/api/StudentsAPI/");

            await GetAllStudents();
            await GetPassedStudents();
            await GetAverageGrade();
            await GetStudentById(3);
            await GetStudentById(30); // Testing 

            Students student = new Students {Name ="kamal" , Age = 33 , Grade =44};
            await AddNewStudent(student);
            await GetAllStudents();

            await DeleteStudent(2);
            await GetAllStudents();

            Students student1 = new Students { Name = "Mohammed", Age = 66, Grade = 60 };

            await UpdateStudent(1,student1);
            await GetAllStudents();

        }

        static async Task GetAllStudents()
        {

            try
            {
                Console.WriteLine("----------------------------------\n");
                Console.WriteLine("Fatching All Students ... ");
                var Students = await HttpClient.GetFromJsonAsync<List<Students>>("All");
                if (Students != null)
                {
                    foreach (var Student in Students)
                    {

                        Console.WriteLine($"ID : {Student.StudentID} , Name : {Student.Name} , Age : {Student.Age} , Grade : {Student.Grade}");

                    }

                }
                else
                {
                    Console.WriteLine("the Students are not Exists ...");
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Error all Students : " + ex.Message);
            }

        }
        static async Task GetPassedStudents()
        {
            try
            {
                Console.WriteLine("----------------------------------------\n");
                Console.WriteLine("Fatshing Passed Students ... ");

                var Students = await HttpClient.GetFromJsonAsync<List<Students>>("Pass");

                if (Students != null)
                {
                    foreach (var Student in Students)
                    {

                        Console.WriteLine($"ID : {Student.StudentID} , Name : {Student.Name} , Age : {Student.Age} , Grade : {Student.Grade}");

                    }
                }
                else
                {
                    Console.WriteLine("there Is No Passed Students");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error passed students : " + ex.Message);
            }


        }
        static async Task GetAverageGrade()
        {
            try
            {
                Console.WriteLine("----------------------------------------\n");
                Console.WriteLine("Fetching Average Grade ... "); // تصحيح اسم الرسالة

                // المعدل عبارة عن رقم مزدوج (double) وليس قائمة طلاب
                var averageGrade = await HttpClient.GetFromJsonAsync<double>("AVG");

                Console.WriteLine($"The Average Grade of Students is: {averageGrade}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error average grade : " + ex.Message);
            }
        }

        static async Task GetStudentById(int id)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"\nFetching student with ID {id}...\n");

                var response = await HttpClient.GetAsync($"{id}"); //explain to me this un details what the method of GetAsync

                if (response.IsSuccessStatusCode) // and this 
                {
                    var student = await response.Content.ReadFromJsonAsync<Students>();
                    if (student != null)
                    {
                        Console.WriteLine($"ID: {student.StudentID}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) // and this 
                {
                    Console.WriteLine($"Bad Request: Not accepted ID {id}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) // and this
                {
                    Console.WriteLine($"Not Found: Student with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static async Task AddNewStudent(Students newStudent)
        {


            Console.WriteLine("_________________________________________\n");
            Console.WriteLine("Fetching New Student ...");

            try
            {
                var Response = await HttpClient.PostAsJsonAsync("", newStudent);

                if (Response.IsSuccessStatusCode)
                {

                    var Student = await Response.Content.ReadFromJsonAsync<Students>();

                  
                        Console.WriteLine($"Added StudentID : {Student.StudentID} , Name : {Student.Name} , Age : {Student.Age} , Grade : {Student.Grade}");
                    Console.WriteLine("the Student Added Successfully ");
                    

                }else if(Response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Bad Request . ");
                }

            }catch(Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }
        }
        static async Task DeleteStudent(int id)
        {

            Console.WriteLine("----------------------------------------------\n");
            Console.WriteLine("Fetching Delete Student ... ");
            try
            {
                var Response = await HttpClient.DeleteAsync($"{id}");

                if (Response.IsSuccessStatusCode) {

                    Console.WriteLine($"the Student with ID {id} Deleted Successfully");
                }
                else if(Response.StatusCode == System.Net.HttpStatusCode.BadRequest){

                    Console.WriteLine("Error : Bad Request");

                }else if(Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Error : Not Found");
                }


            }catch(Exception ex)
            {
                Console.WriteLine("Error : "+ex.Message);

            }

        
        }

        static async Task UpdateStudent(int id , Students Updatedstudent)
        {

            Console.WriteLine("__________________________________________\n");
            Console.WriteLine("Fetching Update ...");

            try
            {
                var Response = await HttpClient.PutAsJsonAsync($"{id}" , Updatedstudent);

                if (Response.IsSuccessStatusCode) {

                    var student = await Response.Content.ReadFromJsonAsync<Students>();
                    
                    Console.WriteLine($"the Student with ID {id} updated successfully");
                    Console.WriteLine($"ID : {student.StudentID} , Name : {student.Name} , Age : {student.Age} , Grade : {student.Grade}");

                
                }else if(Response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Console.WriteLine("Error : Bad Request ");
                }else if(Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Error : Not Found");
                }

            }catch(Exception ex)
            {
                Console.WriteLine("Error : "+ex.Message);

            }
        
        
        
        
        
        
        
        
        }
    }


        public class Students
        {

            public int StudentID { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public int Grade { get; set; }


        }
    }


