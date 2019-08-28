using Microsoft.EntityFrameworkCore;
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
    /// <summary>
    /// Форма для создания и изменения объекта
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CreateEditBaseForm<T> : Form where T : class, new()
    {
        /// <summary>
        /// Кнопка удалить
        /// </summary>
        protected System.Windows.Forms.Button deleteBtn;
        /// <summary>
        /// Кнопка добавить или изменить
        /// </summary>
        protected System.Windows.Forms.Button actionBtn;

        /// <summary>
        /// Окно для изменения
        /// </summary>
        public bool IsEdit { get; set; } = false;
        /// <summary>
        /// Доступ к БД
        /// </summary>
        protected readonly IDatabaseContext databaseContext;

        /// <summary>
        /// Тип объекта (кэш)
        /// </summary>
        private Type _type;
        public Type Type => _type ?? (_type = typeof(T));

        /// <summary>
        /// Аттрибуты у свойств объекта (кэш)
        /// </summary>
        private Dictionary<string, AttributeGoForm> _customAttrs;
        private Dictionary<string, AttributeGoForm> CustomAttrs => _customAttrs ??
                                    (_customAttrs = PropertyInfos.ToDictionary(x => x.Key, x => x.Value.GetCustomAttribute<AttributeGoForm>()));

        /// <summary>
        /// Свойства объекта (кэш)
        /// </summary>
        private Dictionary<string, PropertyInfo> _propertyInfos;
        public Dictionary<string, PropertyInfo> PropertyInfos => _propertyInfos ??
                                    (_propertyInfos = Type.GetProperties().ToDictionary(x => x.Name, x => x));

        /// <summary>
        /// Конструктор для создания
        /// </summary>
        /// <param name="databaseContext"></param>
        /// <param name="name"></param>
        public CreateEditBaseForm(IDatabaseContext databaseContext, string name = null)
        {
            InitializeComponent();
            this.actionBtn.Text = "Добавить";
            this.actionBtn.Click += Add_Click;
            this.databaseContext = databaseContext;
            this.Name = name ?? Type.Name;
            this.Context = new T();
        }

        /// <summary>
        /// Конструктор для удаления и изменения
        /// </summary>
        /// <param name="databaseContext"></param>
        /// <param name="elem"></param>
        /// <param name="name"></param>
        public CreateEditBaseForm(IDatabaseContext databaseContext, T elem, string name = null)
        {
            InitializeComponent();
            this.actionBtn.Text = "Изменить";
            this.actionBtn.Click += Edit_Click;
            this.deleteBtn.Text = "Удалить";
            this.deleteBtn.Visible = true;
            this.deleteBtn.Click += Delete_Click;
            this.Name = name ?? Type.Name;
            this.Context = elem;
            this.databaseContext = databaseContext;
        }

        /// <summary>
        /// Создать элементы
        /// </summary>
        protected virtual void InitializeComponent()
        {
            this.actionBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.deleteBtn.Visible = false;
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Удалить объект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, EventArgs e)
        {
            this.databaseContext.Remove(_context);
            databaseContext.SaveChanges();
            this.Close();
        }

        /// <summary>
        /// Добавить объект в БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, EventArgs e)
        {
            databaseContext.Add(_context);
            databaseContext.SaveChanges();
            this.Close();
        }

        /// <summary>
        /// Изменить объект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, EventArgs e)
        {
            databaseContext.Update(_context);
            databaseContext.SaveChanges();
            this.Close();
        }

        /// <summary>
        /// Объект с которым работаем форма
        /// </summary>
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

                    EventHandler changeAction = (s, e) =>
                    {
                        try
                        {
                            prop.SetValue(_context, Convert.ChangeType(control.Text, prop.PropertyType));
                        }
                        catch
                        { }
                    };
                    control.Text = prop.GetValue(_context)?.ToString() ?? string.Empty;

                    if (goTo != null && control is TextBox textBox)
                    {
                        textBox.ReadOnly = true;
                        MouseEventHandler goToDblClick = (s, e) =>
                        {
                            var form = GlobalContext.FactoryGeneratorSelectListForm.Build(goTo.NextForm);
                            form.ShowDialog();
                            if (form.SelectedContext != null)
                            {
                                textBox.Text = form.SelectedContext.ToString();
                                prop.SetValue(_context, form.SelectedContext);
                            }
                        };
                        control.MouseDoubleClick -= goToDblClick;
                        control.MouseDoubleClick += goToDblClick;
                    }
                    else
                    {
                        control.TextChanged -= changeAction;
                        control.TextChanged += changeAction;
                    }
                }
            }
        }
    }
}
