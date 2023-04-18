using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Models
{
    public partial class kitchen
    {
        [Key]
        [StringLength(50)]
        public string user_name { get; set; }
        [Required]
        [StringLength(50)]
        public string pass_word { get; set; }
    }
}
