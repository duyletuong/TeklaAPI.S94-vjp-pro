using System;
using System.Reflection;
using Tekla.Structures.Internal;

namespace DLT.Plugin.Core.Dialog
{
    /// <summary>
    /// Resolves dialog defaults in one place for every plugin.
    /// After Tekla populates a StructuresData instance, call ApplyDefaults():
    /// any field left empty by the user (== DEFAULT_VALUE) is replaced by the
    /// value declared in its [Default(...)] attribute. Fields the user actually
    /// filled in are left untouched.
    /// </summary>
    public static class DialogDefaults
    {
        public static T ApplyDefaults<T>(this T data) where T : StructuresDataBase
        {
            if (data == null) return null;

            foreach (FieldInfo field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                DefaultAttribute def = field.GetCustomAttribute<DefaultAttribute>();
                if (def == null) continue;

                GuardDefaultType(field, def);

                if (IsUnset(field.GetValue(data)))
                {
                    field.SetValue(data, def.Value);
                }
            }

            return data;
        }

        private static bool IsUnset(object value)
        {
            switch (value)
            {
                case int i: return i == StructuresDataStorage.DEFAULT_VALUE;
                case double d: return d == (double)StructuresDataStorage.DEFAULT_VALUE;
                case string s: return string.IsNullOrEmpty(s);
                default: return false;
            }
        }

        private static void GuardDefaultType(FieldInfo field, DefaultAttribute def)
        {
            if (def.Value == null) return;

            if (def.Value.GetType() != field.FieldType)
            {
                throw new InvalidOperationException(
                    $"[Default] on '{field.DeclaringType?.Name}.{field.Name}' is {def.Value.GetType().Name}, " +
                    $"but the field is {field.FieldType.Name}. Use a matching literal (e.g. 0.0 for double, 0 for int).");
            }
        }
    }
}
