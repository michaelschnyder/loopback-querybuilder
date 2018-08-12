namespace LoopbackQueryBuilder
{
    public class EqualityOperation : OperationBase
    {
        public string ColumnName { get; set; }

        public string Value { get; set; }

        public bool IsSaveValue { get; set; }

        public EqualityOperation()
        {
            
        }

        public EqualityOperation(string name, string foo)
        {
            ColumnName = name;
            Value = foo;
        }

        public override string ToString()
        {
            var value = !this.IsSaveValue ? $"'{Value}'" : Value;

            return $"{{ {ColumnName}: {value} }}";
        }
    }
}