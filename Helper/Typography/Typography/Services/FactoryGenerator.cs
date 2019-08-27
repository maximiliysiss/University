using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typography.Services
{
    public class FactoryGenerator<T, A, W> where T : class
    {
        public class FactoryPosition
        {
            private readonly Func<A, T> result;
            public T Result => result(default(A));
            public T ResultWithArguments(A arg) => result(arg);
            private Func<W, bool> where;

            public void Where(Func<W, bool> where) => this.where = where;
            public void Where(W where) => this.where = (x) => where.Equals(x);
            public bool If(W args) => where(args);

            public FactoryPosition(Func<T> elem)
            {
                this.result = (x) => elem();
            }

            public FactoryPosition(Func<A, T> elem)
            {
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

        public FactoryPosition AddFor(Func<A, T> elem)
        {
            var newPos = new FactoryPosition(elem);
            factoryPositions.Add(newPos);
            return newPos;
        }

        public T Build(W where)
        {
            foreach (var elem in factoryPositions)
                if (elem.If(where))
                    return elem.Result;
            return null;
        }

        public T BuildWithArguments(A args, W where)
        {
            foreach (var elem in factoryPositions)
                if (elem.If(where))
                    return elem.ResultWithArguments(args);
            return null;
        }
    }
}
