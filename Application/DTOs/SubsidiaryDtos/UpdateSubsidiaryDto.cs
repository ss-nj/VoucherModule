using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SubsidiaryDtos
{
    public class UpdateSubsidiaryDto
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
        public int OwnerId { get; set; }

        [Required]
        public int MasterId { get; set; }

        public int? ParentSubsidiaryId { get; set; }

    }
}
