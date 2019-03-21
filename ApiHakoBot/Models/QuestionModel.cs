using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiHakoBot.Models
{
    public class QuestionModel
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string question { get; set; }
    }
}