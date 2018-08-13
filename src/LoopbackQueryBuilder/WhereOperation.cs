using System.Linq;
using System.Text;

namespace LoopbackQueryBuilder
{
    public class WhereOperation : OperationBase
    {
        public WhereOperation(SerializationSettings serializationSerialization) : base(serializationSerialization)
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (this.Children.Count == 1)
            {
                sb.Append($"{{ {SerializationSettings.OperationEscape}where{SerializationSettings.OperationEscape}: ");
                sb.Append(this.Children.First());
                sb.Append(" }");
            }

            return sb.ToString();
        }

    }
}