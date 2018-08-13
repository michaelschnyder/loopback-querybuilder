﻿using System;
using System.Linq.Expressions;

namespace LoopbackQueryBuilder
{
    public class LookbackQueryBuilder<T>
    {
        public SerializationSettings SerializationSettings { get; set; } = new SerializationSettings();

        public string Where(Expression<Func<T, bool>> expression)
        {
            var visitor = new Translator(this.SerializationSettings);

            visitor.Visit(expression);

            return visitor.ToString();
        }
    }

    public class SerializationSettings
    {
        public char? PropertyEscape { get; set; } = '"';

        public char? OperationyEscape { get; set; } = '"';

        public char? UnsafeValueEscape { get; set; } = '"';
    }
}