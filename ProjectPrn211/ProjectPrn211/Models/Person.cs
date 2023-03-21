using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPrn211.Models
{
    public partial class Person
    {
        public Person()
        {
            Rates = new HashSet<Rate>();
        }

        public int PersonId { get; set; }
        public string? Fullname { get; set; }
        public string? Gender { get; set; }
        [Required]
        [StringLength(255)]
        public string? Email { get; set; }
        [Required]
        [StringLength(255)]
        public string? Password { get; set; }
        public int? Type { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
    }
}
