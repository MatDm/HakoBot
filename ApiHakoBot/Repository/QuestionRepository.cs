using ApiHakoBot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiHakoBot.Repository
{
    public class QuestionRepository : IRepository<Question>
    {
        private HakoBotContext _context;

        public QuestionRepository(HakoBotContext context)
        {
            _context = context;
        }

        public Question Add(Question entity)
        {

          

            Question newQuestion = new Question();

            try
            {
                newQuestion = _context.Questions.Add(entity);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }

            return newQuestion;
        }
    }
}