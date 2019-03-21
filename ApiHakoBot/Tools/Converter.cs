using ApiHakoBot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiHakoBot.Tools
{
    public class Converter
    {
        public Question StringToQuestionType(string question)
        {
            return new Question()
            {
                
                QuestionToReview = question,
                
            };
        }
    }
}