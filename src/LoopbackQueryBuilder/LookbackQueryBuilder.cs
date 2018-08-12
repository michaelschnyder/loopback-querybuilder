using System;
using System.Linq.Expressions;

namespace LoopbackQueryBuilder
{
    public class LookbackQueryBuilder<T>
    {
        public string Where(Expression<Func<T, bool>> expression)
        {
            var visitor = new Translator();

            visitor.Visit(expression);

            return visitor.ToString();
        }
    }
}