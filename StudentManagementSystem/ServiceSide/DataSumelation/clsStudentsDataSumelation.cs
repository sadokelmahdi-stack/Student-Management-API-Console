
using StudentsAPI.Model;

namespace StudentsAPI.DataSumelation
{
    public static class clsStudentsDataSumelation
    {


        public static readonly List<clsStudents> StudentsList =new List<clsStudents>
        {
            new clsStudents{StudentID=1,Name="Mahdi" , Age=28,Grade=9},
            new clsStudents{StudentID=2,Name="Abidin" , Age=30,Grade=2},
            new clsStudents{StudentID=3,Name="Khalid" , Age=38,Grade=4},

            new clsStudents{StudentID=4,Name="Mossa" , Age=14,Grade=55},
            new clsStudents{StudentID=5,Name="Abu-Hadhoud" , Age=33,Grade=66},
            new clsStudents{StudentID=6,Name="Morad" , Age=50,Grade=77}



        };

    }
}
