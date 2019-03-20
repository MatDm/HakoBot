using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiHakoBot.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionToReview { get; set; }
    }
}