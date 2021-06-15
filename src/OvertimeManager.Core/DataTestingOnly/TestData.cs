// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(TestData));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (TestData)serializer.Deserialize(reader);
// }

using System;
using System.Collections.Generic;

//public class Employees
//{
//    public Employee Employee { get; set; }
//}

//public class Companies
//{
//    public Company Company { get; set; }
//}

namespace OvertimeManager.Core.DataTestingOnly
{
    public class TestData
    {

        public const string CompanyOneKeyIdString = "54fd48e0-48ae-4526-be7c-b27a8059b296";
        public const string CompanyOneName = "CompanyOneName";

        public const string CompanyTwoKeyIdString = "e6e6e1aa-4bda-4313-9a4f-be6dd663b6c4";
        public const string CompanyTwoName = "CompanyTwoName";

        public List<Company> Companies { get; set; }

        public TestData()
        {
        
            CreateTestCompanies();
            //CreateTestEmployees();
        }

        private void CreateTestCompanies()
        {
            this.Companies = new List<Company>()
            {
                new Company()
                {
                    CompanyKeyId = Guid.Parse(TestData.CompanyOneKeyIdString), CompanyName = TestData.CompanyOneName
                    , Employees = new List<Employee>()
                    {
                        new Employee(){LastName = "Doe", FirstName = "John", EmployeeKeyId = Guid.NewGuid()}
                        ,new Employee(){LastName = "Dates", FirstName = "Kathy", EmployeeKeyId = Guid.NewGuid()}
                    }
                }


                ,new Company()
                {
                    CompanyKeyId = Guid.Parse(TestData.CompanyTwoKeyIdString), CompanyName = TestData.CompanyTwoName
                    , Employees = new List<Employee>()
                    {
                        new Employee(){LastName = "Smith", FirstName = "Robert", EmployeeKeyId = Guid.NewGuid()}
                        ,new Employee(){LastName = "Pearson", FirstName = "Linda", EmployeeKeyId = Guid.NewGuid()}
                    }
                }
            };
        }

        //private void CreateTestEmployees()
        //{
        //    this.Employees = 

        //}
    }
}

