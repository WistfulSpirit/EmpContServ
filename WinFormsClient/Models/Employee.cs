using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WinFormsClient.Models
{
    public class Employee
    {
        [Browsable(false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string   Address { get; set; }
        public string   About { get; set; }
        [Browsable(false)]
        public int      Dept_Id { get; set; }
        
    }
}
