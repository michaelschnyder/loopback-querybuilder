using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace LoopbackQueryBuilder
{
    class Translator : ExpressionVisitor
    {
        private OperationBase _rootOperation = new WhereOperation();
        private OperationBase _currentOperation = new WhereOperation();

        public Translator()
        {
            _currentOperation = _rootOperation;
        }

        public override string ToString()
        {
            return _rootOperation.ToString();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Trace.WriteLine($"VisitMember for {node}");

            var propertyName = node.Member.Name;

            propertyName = MakeLowerCamelCase(propertyName);

            (this._currentOperation as EqualityOperation).ColumnName = propertyName;

            return node;
        }

        private static string MakeLowerCamelCase(string propertyName)
        {
            var firstChar = propertyName[0];

            if (char.IsUpper(firstChar))
            {
                propertyName = $"{char.ToLower(firstChar)}{propertyName.Substring(1)}";
            }

            return propertyName;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            Trace.WriteLine($"VisitConstant for {node}");

            var binaryOperation = (_currentOperation as EqualityOperation);

            if (node.Type == typeof(int) || node.Type == typeof(double))
            {
                binaryOperation.Value = node.Value.ToString();
                binaryOperation.IsSaveValue = true;
            }

            if (node.Type == typeof(string))
            {
                binaryOperation.Value = node.Value.ToString();
                binaryOperation.IsSaveValue = false;
            }

            if (node.Type == typeof(bool))
            {
                binaryOperation.Value = MakeLowerCamelCase(node.Value.ToString());
                binaryOperation.IsSaveValue = true;
            }

            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Trace.WriteLine($"VisitMethodCall for {node}");

            if (node.Method.Name == "Contains")
            {
                var operation = new CompareOperation(ComparisionMode.Contains);

                this._currentOperation.Add(operation);
                this._currentOperation = operation;
            }
            
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            Trace.WriteLine($"VisitUnary for {node}");

            throw new NotSupportedException($"The node-type '{node.NodeType}' is not supported!");
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            Trace.WriteLine($"VisitBinary for {node}");

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

    public enum ComparisionMode
    {
        Contains
    }
}