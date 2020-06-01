using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpContServ.Models
{
    public class Dept
    {
        [Key]
        [Display(AutoGenerateField = false)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Название отдела")]
        public string Name { get; set; }

        [Display(Name = "Сотрудники")]
        public int EpmployeesCount { get; set; }
    }
}
