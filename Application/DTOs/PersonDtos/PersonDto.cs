using Application.DTOs.MasterDtos;
using Application.DTOs.SubsidiaryDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PersonDtos
{
    public class PersonDto
    {
        public int Id { get; set; }
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
        [MaxLength(20)]
        public string PhoneNumber { get; set; }


        public ICollection<GetMasterDto> MastersCreated { get; set; } = new List<GetMasterDto>();
        public ICollection<GetSubsidiaryDto> SubsidiariesCreated { get; set; } = new List<GetSubsidiaryDto>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
