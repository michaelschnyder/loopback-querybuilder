using System;
using System.Linq.Expressions;

namespace LoopbackQueryBuilder
{
    public class LoopbackQueryBuilder<T>
    {
        private string _whereResult;

        public SerializationSettings SerializationSettings { get; set; } = new SerializationSettings();

        public LoopbackQueryBuilder<T> Where(Expression<Func<T, bool>> expression)
        {
            var visitor = new Translator(this.SerializationSettings);

            visitor.Visit(expression);

            _whereResult = visitor.ToString();

            return this;
        }

        public override string ToString()
        {
            return $"{{ {_whereResult} }}";
        }
    }

    public class SerializationSettings
    {
        public char? PropertyEscape { get; set; } = '"';

        public char? OperationEscape { get; set; } = '"';

        public char? UnsafeValueEscape { get; set; } = '"';
    }
}