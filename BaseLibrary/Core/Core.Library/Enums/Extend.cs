using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Library.Enums
{
    public static class Extend
    {
        public static string GetDescription(this Enum obj)
        {
            DescriptionAttribute attribute = obj.GetType()
                .GetField(obj.ToString())
                .GetCustomAttributes(typeof(CodeAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? obj.ToString() : attribute.Description;
        }

        public static string GetCode(this Enum obj)
        {
            CodeAttribute attribute = obj.GetType()
                .GetField(obj.ToString())
                .GetCustomAttributes(typeof(CodeAttribute), false)
                .SingleOrDefault() as CodeAttribute;
            return attribute == null ? obj.ToString() : attribute.Code;
        }

        public static string GetFolder(this Enum obj)
        {
            FolderAttribute attribute = obj.GetType()
                .GetField(obj.ToString())
                .GetCustomAttributes(typeof(FolderAttribute), false)
                .SingleOrDefault() as FolderAttribute;
            return attribute == null ? obj.ToString() : attribute.Folder;
        }

        public static T GetEnumFromCode<T>(this string code)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException();

            FieldInfo[] fields = type.GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(
                                typeof(CodeAttribute), false), (
                                    f, a) => new { Field = f, Att = a })
                            .Where(a => ((CodeAttribute)a.Att)
                                .Code == code).SingleOrDefault();
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }
    }

    public class CodeAttribute : Attribute
    {
        public string Code { get; protected set; }

        public CodeAttribute(string value)
        {
            this.Code = value;
        }
    }

    public class FolderAttribute : Attribute
    {
        public string Folder { get; protected set; }

        public FolderAttribute(string value)
        {
            this.Folder = value;
        }
    }
}
