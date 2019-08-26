using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Typography.Forms.CreateEdit
{
    public class CreateEditBaseForm<T> : Form where T : class, new()
    {
        public Func<List<T>> dbSet;
        public T _context;
        public T Context
        {
            get => _context;
            set
            {
                if (_context == value)
                    return;
                _context = value;
                if (Controls.Cast<Control>().Any(x => x.Name))
            }
        }

        private Type _type;
        public Type Type => _type ?? (_type = typeof(T));

        private Dictionary<string, PropertyInfo> _propertyInfos;
        public Dictionary<string, PropertyInfo> PropertyInfos => _propertyInfos ??
                                    (_propertyInfos = Type.GetProperties().ToDictionary(x => x.Name, x => x));

        private CreateEditBaseForm(Func<List<T>> dbSet, string name = null)
        {
            this.dbSet = dbSet;
            this.Name = name ?? Type.Name;
            this.Context = new T();
        }

        private CreateEditBaseForm(T elem, Func<List<T>> dbSet, string name = null)
        {
            this.dbSet = dbSet;
            this.Name = name ?? Type.Name;
            this.Context = elem;
        }
    }
}
