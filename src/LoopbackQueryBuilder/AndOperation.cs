using System.Text;

namespace LoopbackQueryBuilder
{
    internal class AndOperation : OperationBase
    {
        public AndOperation(SerializationSettings serializationSettings): base(serializationSettings)
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{{ {SerializationSettings.OperationyEscape}and{SerializationSettings.OperationyEscape}: [ ");

            for (var index = 0; index < Children.Count; index++)
            {
                var child = Children[index];
                sb.Append(child);

                if (index < Children.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append(" ] }");

            return sb.ToString();
        }
    }
}