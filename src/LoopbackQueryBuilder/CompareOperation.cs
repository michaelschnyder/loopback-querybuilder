namespace LoopbackQueryBuilder
{
    internal class CompareOperation : EqualityOperation
    {
        private readonly ComparisionMode _mode;

        public CompareOperation(ComparisionMode mode)
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

            var saveValue = !this.IsSaveValue ? $"'{rawValue}'" : rawValue;

            return $"{{ {ColumnName}: {{ '{operationString}': {saveValue} }} }}";
        }
    }
}