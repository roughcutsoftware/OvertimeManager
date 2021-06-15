using System;
using System.Collections.Generic;

public class Company
{
    public Guid CompanyKeyId { get; set; }
    public string CompanyName { get; set; }
    public List<Employee> Employees { get; set; }
}