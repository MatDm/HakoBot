using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiHakoBot.Repository
{
    public interface IQuestionRepository<T> where T : class
    {
        //TEntity Add(TEntity entity);
        void Add(T entity);
    }
}
