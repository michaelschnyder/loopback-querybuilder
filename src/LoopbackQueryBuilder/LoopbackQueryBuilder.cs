using System;
using System.Linq.Expressions;
using System.Text;

namespace LoopbackQueryBuilder
{
    public class LoopbackQueryBuilder<T>
    {
        private string _whereResult;
        private string _limitPart;
        private string _skipPart;

        public SerializationSettings SerializationSettings { get; set; } = new SerializationSettings();

        public LoopbackQueryBuilder<T> Where(Expression<Func<T, bool>> expression)
        {
            var visitor = new Translator(this.SerializationSettings);

            visitor.Visit(expression);

            _whereResult = visitor.ToString();

            return this;
        }

        public LoopbackQueryBuilder<T> Take(int count)
        {
            _limitPart = $"{SerializationSettings.OperationEscape}limit{SerializationSettings.OperationEscape}: {count}";

            return this;
        }

        public LoopbackQueryBuilder<T> Skip(int count)
        {
            _skipPart = $"{SerializationSettings.OperationEscape}skip{SerializationSettings.OperationEscape}: {count}";

            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(_whereResult))
            {
                sb.Append(_whereResult);
            }

            if (!string.IsNullOrWhiteSpace(_limitPart))
            {
                AddSeparatorIfRequired(sb);
                sb.Append(_limitPart);
            }

            if (!string.IsNullOrWhiteSpace(_skipPart))
            {
                AddSeparatorIfRequired(sb);
                sb.Append(_skipPart);
            }

            return $"{{ {sb} }}";
        }

        private void AddSeparatorIfRequired(StringBuilder sb)
        {
            if (sb.Length > 0)
            {
                sb.Append(", ");
            }
        }
    }

    public class SerializationSettings
    {
        public char? PropertyEscape { get; set; } = '"';

        public char? OperationEscape { get; set; } = '"';

        public char? UnsafeValueEscape { get; set; } = '"';
    }
}