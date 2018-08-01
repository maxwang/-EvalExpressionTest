using System;
using System.Collections.Generic;
using System.Dynamic;
using Z.Expressions;

namespace TestEvalExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic expando = new ExpandoObject();
            AddProperty(expando, "a", 5);

            int result = Eval.Execute<int>(@"
                var y = a;
                if ( y == 5 ) {
                    y += 5;
                }
                var list = new List<int>() {1, 2, 3, 4, 5};
                var filter = list.Where(x => x < 3);
                return filter.Sum(x => x) + y", expando);
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
    }
}
