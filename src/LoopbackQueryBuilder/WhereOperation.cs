using System.Linq;
using System.Text;

namespace LoopbackQueryBuilder
{
    public class WhereOperation : OperationBase
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (this.Children.Count == 1)
            {
                sb.Append("{ where: ");
                sb.Append(this.Children.First());
                sb.Append(" }");
            }

            return sb.ToString();
        }
    }
}