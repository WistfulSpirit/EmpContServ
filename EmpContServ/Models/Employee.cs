using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpContServ.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [MaxLength(25)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [MaxLength(30)]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; }
        [MaxLength(60)]
        [Display(Name = "Адрес")]
        public string   Address { get; set; }
        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "О себе")]
        public string   About { get; set; }
        [Display(Name = "Принадлежит отделу")]
        public int      Dept_Id { get; set; }
        
    }
}
