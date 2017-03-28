using System;
using System.Linq;

namespace Eventing
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class StreamCategoryAttribute : Attribute
    {
        public StreamCategoryAttribute(string categoryName)
        {
            this.CategoryName = categoryName;
        }

        public string CategoryName { get; }

        public static string GetCategory(Type type)
        {
            var att = GetCustomAttributes(type)
                        .FirstOrDefault(a => a is StreamCategoryAttribute);

            return ((StreamCategoryAttribute)att)?.CategoryName;
        }
    }
}
