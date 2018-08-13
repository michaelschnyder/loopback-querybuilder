using System.Collections.Generic;
using System.Threading;

namespace LoopbackQueryBuilder
{
    public class OperationBase
    {
        protected internal SerializationSettings SerializationSettings { get; }

        protected List<OperationBase> Children { get; } = new List<OperationBase>();

        public OperationBase(SerializationSettings serializationSerialization)
        {
            SerializationSettings = serializationSerialization;
        }

        public void Add(OperationBase operation)
        {
            this.Children.Add(operation);
        }
    }
}