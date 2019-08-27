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
            private readonly Func<List<object>, T> result;
            private readonly bool isArguments = false;
            public T Result => result(null);
            public T ResultWithArguments(List<object> list) => result(list);
            private Func<List<object>, bool> where;

            public void Where(Func<List<object>, bool> where) => this.where = where;
            public bool If(List<object> args) => where(args);

            public FactoryPosition(Func<T> elem)
            {
                this.result = (x) => elem();
            }

            public FactoryPosition(Func<List<object>, T> elem)
            {
                this.isArguments = true;
                this.result = elem;
            }
        }

        protected List<FactoryPosition> factoryPositions = new List<FactoryPosition>();

        public FactoryPosition AddFor(Func<T> elem)
        {
            var newPos = new FactoryPosition(elem);
            factoryPositions.Add(newPos);
            return newPos;
        }

        public FactoryPosition AddFor(Func<List<object>, T> elem)
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

        public T BuildWithArguments(List<object> args, List<object> where)
        {
            return null;
        }
    }
}
