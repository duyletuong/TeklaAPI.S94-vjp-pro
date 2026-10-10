using System;

namespace DLT.Plugin.Core.Dialog
{
    /// <summary>
    /// Declares the default value to apply when a dialog field is left empty
    /// (i.e. Tekla fills it with StructuresDataStorage.DEFAULT_VALUE).
    /// Put it right next to the [StructuresField] attribute on the same field.
    /// The CLR type of <paramref name="value"/> must match the field type exactly
    /// (use 0.0 for double, 0 for int, "" for string).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DefaultAttribute : Attribute
    {
        public object Value { get; }

        public DefaultAttribute(object value)
        {
            this.Value = value;
        }
    }
}
