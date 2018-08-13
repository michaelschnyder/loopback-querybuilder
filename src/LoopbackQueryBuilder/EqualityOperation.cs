namespace LoopbackQueryBuilder
{
    public class EqualityOperation : OperationBase
    {
        public string ColumnName { get; set; }

        public string Value { get; set; }

        public bool IsSaveValue { get; set; }


        public EqualityOperation(string name, string foo) : base(new SerializationSettings())
        {
            ColumnName = name;
            Value = foo;
        }

        public EqualityOperation(SerializationSettings serializationSerialization) : base(serializationSerialization)
        {
        }

        public override string ToString()
        {
            var value = !this.IsSaveValue ? $"{SerializationSettings.UnsafeValueEscape}{Value}{SerializationSettings.UnsafeValueEscape}" : Value;

            return $"{{ {SerializationSettings.PropertyEscape}{ColumnName}{SerializationSettings.PropertyEscape}: {value} }}";
        }
    }
}