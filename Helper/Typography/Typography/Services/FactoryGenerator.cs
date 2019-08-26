using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typography.Services
{
    public class FactoryGenerator<T> where T : class, new()
    {
        public class FactoryPosition
        {
            private readonly T result;
            public T Result => result;
            private Func<List<object>, bool> where;

            public void Where(Func<List<object>, bool> where) => this.where = where;
            public bool If(List<object> args) => where(args);

            public FactoryPosition(T elem)
            {
                this.result = elem;
            }
        }

        protected List<FactoryPosition> factoryPositions = new List<FactoryPosition>();

        public FactoryPosition AddFor(T elem)
        {
            var newPos = new FactoryPosition(elem);
            factoryPositions.Add(newPos);
            return newPos;
        }

        public T Build(params object[] list)
        {
            foreach (var elem in factoryPositions)
                if (elem.If(list.ToList()))
                    return elem.Result;
            return null;
        }
    }
}
