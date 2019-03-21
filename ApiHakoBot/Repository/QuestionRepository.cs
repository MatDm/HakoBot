using ApiHakoBot.Context;
using ApiHakoBot.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiHakoBot.Repository
{
    public class QuestionRepository<T> : IQuestionRepository<T> where T : class
    {
        protected DbSet<T> DbSet;

        public QuestionRepository(HakoBotDbContext context)
        {
            DbSet = context.Set<T>();
        }
        #region IRepository<T> Members

        //public T Add(Question entity)
        //{

          

        //    Question newQuestion = new Question();

        //    try
        //    {
        //        newQuestion = context.Questions.Add(entity);
        //        context.SaveChanges();
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return newQuestion;
        //}

        //public T Add(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Add2(string entity)
        //{
        //    DbSet.Add(entity);
        //}

        //public Add2(string question)
        //{
        //    DbSet.Add(question);
        //}

        public void Add(T question)
        {
             DbSet.Add(question);
        }
        #endregion
    }
}