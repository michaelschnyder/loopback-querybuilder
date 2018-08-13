namespace LoopbackQueryBuilder
{
    internal class CompareOperation : EqualityOperation
    {
        private readonly ComparisionMode _mode;

        public CompareOperation(SerializationSettings serializationSettings, ComparisionMode mode) : base(serializationSettings)
        {
            _mode = mode;
        }

        public override string ToString()
        {
            var rawValue = string.Empty;
            var operationString = string.Empty;

            if (_mode == ComparisionMode.Contains)
            {
                operationString = "like";
                rawValue = $"%{Value}%";
            }

            var saveValue = !IsSaveValue ? $"{SerializationSettings.UnsafeValueEscape}{rawValue}{SerializationSettings.UnsafeValueEscape}" : rawValue;

            return $"{{ {SerializationSettings.PropertyEscape}{ColumnName}{SerializationSettings.PropertyEscape}: {{ {SerializationSettings.OperationEscape}{operationString}{SerializationSettings.OperationEscape}: {saveValue} }} }}";
        }
    }
}