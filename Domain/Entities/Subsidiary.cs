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
    public class Subsidiary : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string? Title { get; set; } 

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } 

        public long DebitAmount { get; set; } = 0;//rial

        public long CreditAmount { get; set; } = 0;//rial

        [Required]
        public bool IsLastLevel { get; set; } = true;

        [Required]
        [ForeignKey(nameof(Person))]
        public int CreatorId { get; set; }
        public Person Creator { get; set; } 

        [ForeignKey(nameof(Master))]
        [Required]
        public int MasterId { get; set; }
        public Master Master { get; set; } 

        [ForeignKey(nameof(Subsidiary))]
        public int? ParentSubsidiaryId { get; set; }
        public Subsidiary? ParentSubsidiary { get; set; }

        public ICollection<Subsidiary> ChildSubsidiaries { get; set; } = new List<Subsidiary>();


        [Required]
        [MaxLength(500)]
        public string ParentPath { get; set; } = "-";

        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
