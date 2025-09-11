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
    public class MasterDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public GetPersonDto Owner { get; set; }

        public ICollection<GetSubsidiaryDto> Subsidiaries { get; set; } = new List<GetSubsidiaryDto>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
