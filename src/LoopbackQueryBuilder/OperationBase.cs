using System.Collections.Generic;

namespace LoopbackQueryBuilder
{
    public class OperationBase
    {
        protected List<OperationBase> Children { get; } = new List<OperationBase>();

        public void Add(OperationBase operation)
        {
            this.Children.Add(operation);
        }
    }
}