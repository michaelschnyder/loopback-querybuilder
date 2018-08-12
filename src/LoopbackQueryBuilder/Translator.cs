using System;
using System.Linq.Expressions;

namespace LoopbackQueryBuilder
{
    class Translator : ExpressionVisitor
    {
        private OperationBase _currentOperation = new WhereOperation();

        public override string ToString()
        {
            return _currentOperation.ToString();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var propertyName = node.Member.Name;

            var firstChar = propertyName[0];

            if (char.IsUpper(firstChar))
            {
                propertyName = $"{char.ToLower(firstChar)}{propertyName.Substring(1)}";
            }

            (this._currentOperation as EqualityOperation).ColumnName = propertyName;

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            var binaryOperation = (_currentOperation as EqualityOperation);
            binaryOperation.Value = node.Value.ToString();

            if (node.Type == typeof(int) || node.Type == typeof(double))
            {
                binaryOperation.IsSaveValue = true;
            }

            if (node.Type == typeof(string))
            {
                binaryOperation.IsSaveValue = false;
            }

            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            throw new NotSupportedException($"The node-type '{node.NodeType}' is not supported!");
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            OperationBase operationForThisNode = null;

            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    operationForThisNode = new EqualityOperation();
                    break;
                case ExpressionType.NotEqual:
                    // _sql.Append(" <> ");
                    break;
                case ExpressionType.AndAlso:
                case ExpressionType.And:
                    operationForThisNode = new AndOperation();
                    break;
                case ExpressionType.Or:
                    // _sql.Append(" OR ");
                    break;
            }

            var previousOperation = this._currentOperation;

            previousOperation.Add(operationForThisNode);

            this._currentOperation = operationForThisNode;

            Visit(node.Left);

            Visit(node.Right);

            this._currentOperation = previousOperation;

            return node;
        }
    }
}