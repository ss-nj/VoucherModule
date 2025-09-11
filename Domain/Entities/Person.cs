using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } 

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } 

        [Required]
        [StringLength(10)]
        public string NationalId { get; set; } 

        [Required]
        [Phone]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } 
  

        public ICollection<Master> MastersCreated { get; set; } = new List<Master>();
        public ICollection<Subsidiary> SubsidiariesCreated { get; set; } = new List<Subsidiary>();
    }
}

