using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Master: BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } 

        [ForeignKey(nameof(Person))]
        [Required]
        public int CreatorId { get; set; }

        public Person Creator { get; set; } 

        public ICollection<Subsidiary> Subsidiaries { get; set; } = new List<Subsidiary>();
    }

}
