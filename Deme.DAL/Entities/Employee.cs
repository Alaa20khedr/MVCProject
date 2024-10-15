using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deme.DAL.Entities
{
    public class Employee:BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
       [Column(TypeName ="money")]
        public double Salary {  get; set; }
        public DateTime HiringDate { get; set; }= DateTime.Now;
        public bool IsActive { get; set; }
        public Department Department { get; set; }
        public int DepartmentId {  get; set; }
        public string ImageUrl {  get; set; }

    }
}
