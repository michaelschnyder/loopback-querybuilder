﻿using System;
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

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
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

            if ((_mode == ComparisionMode.Contains))
            {
                operationString = "like";
                rawValue = $"%{Value}%";
            }

            var saveValue = !this.IsSaveValue ? $"'{rawValue}'" : rawValue;

            return $"{{ {ColumnName}: {{ '{operationString}': {saveValue} }} }}";
        }
    }

    public enum ComparisionMode
    {
        Contains
    }
}