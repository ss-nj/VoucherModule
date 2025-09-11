using Application.DTOs.PersonDtos;
using Application.DTOs.SubsidiaryDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MasterDtos
{
    public class CreateMasterDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        public int OwnerId { get; set; }

    }
}
