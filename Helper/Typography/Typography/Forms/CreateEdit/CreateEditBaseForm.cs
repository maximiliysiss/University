using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Typography.Services;

namespace Typography.Forms.CreateEdit
{
    public class CreateEditBaseForm<T> : Form where T : class, new()
    {
        protected System.Windows.Forms.Button deleteBtn;
        protected System.Windows.Forms.Button actionBtn;

        protected readonly IDatabaseContext databaseContext;
        public Func<List<T>> dbSet;

        private Type _type;
        public Type Type => _type ?? (_type = typeof(T));

        private Dictionary<string, AttributeGoForm> _customAttrs;
        private Dictionary<string, AttributeGoForm> CustomAttrs => _customAttrs ??
                                    (_customAttrs = PropertyInfos.ToDictionary(x => x.Key, x => x.Value.GetCustomAttribute<AttributeGoForm>()));

        private Dictionary<string, PropertyInfo> _propertyInfos;
        public Dictionary<string, PropertyInfo> PropertyInfos => _propertyInfos ??
                                    (_propertyInfos = Type.GetProperties().ToDictionary(x => x.Name, x => x));

        public CreateEditBaseForm(IDatabaseContext databaseContext, Func<List<T>> dbSet, string name = null)
        {
            InitializeComponent();
            this.FormClosed += CreateEditBaseForm_FormClosed;
            this.actionBtn.Text = "Add";
            this.actionBtn.Click += Add_Click;
            this.databaseContext = databaseContext;
            this.dbSet = dbSet;
            this.Name = name ?? Type.Name;
            this.Context = new T();
        }

        private void CreateEditBaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var control in Controls.Cast<Control>())
                control.Text = string.Empty;
        }

        public CreateEditBaseForm(T elem, Func<List<T>> dbSet, string name = null)
        {
            InitializeComponent();
            this.FormClosed += CreateEditBaseForm_FormClosed;
            this.actionBtn.Text = "Edit";
            this.actionBtn.Click += Edit_Click;
            this.deleteBtn.Visible = true;
            this.deleteBtn.Click += Delete_Click;
            this.dbSet = dbSet;
            this.Name = name ?? Type.Name;
            this.Context = elem;
        }

        protected virtual void InitializeComponent()
        {
            this.actionBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateEditBaseForm
            // 
            this.ClientSize = new System.Drawing.Size(452, 516);
            this.Name = "CreateEditBaseForm";
            this.ResumeLayout(false);

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            this.databaseContext.Remove(_context);
            databaseContext.SaveChanges();
            this.Close();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            databaseContext.Add(_context);
            databaseContext.SaveChanges();
            this.Close();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            databaseContext.Update(_context);
            databaseContext.SaveChanges();
            this.Close();
        }

        public T _context;
        public T Context
        {
            get => _context;
            set
            {
                if (_context == value)
                    return;
                _context = value;
                foreach (var control in Controls.Cast<Control>())
                {
                    this.PropertyInfos.TryGetValue(control.Name, out var prop);
                    if (prop == null)
                        continue;
                    this.CustomAttrs.TryGetValue(control.Name, out var goTo);

                    EventHandler changeAction = (s, e) => { prop.SetValue(_context, Convert.ChangeType(control.Text, prop.PropertyType)); };

                    if (goTo != null)
                    {
                        MouseEventHandler goToDblClick = (s, e) => { GlobalContext.FactoryGeneratorList.Build(goTo.NextForm).ShowDialog(); };
                        control.MouseDoubleClick -= goToDblClick;
                        control.MouseDoubleClick += goToDblClick;
                    }

                    control.Text = prop.GetValue(_context)?.ToString() ?? string.Empty;
                    if (control.Enabled)
                    {
                        control.TextChanged -= changeAction;
                        control.TextChanged += changeAction;
                    }
                }
            }
        }
    }
}
