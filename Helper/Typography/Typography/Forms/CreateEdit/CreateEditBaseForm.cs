﻿using Microsoft.EntityFrameworkCore;
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

        public bool IsEdit { get; set; } = false;
        protected readonly IDatabaseContext databaseContext;
        public DbSet<T> dbSet;

        private Type _type;
        public Type Type => _type ?? (_type = typeof(T));

        private Dictionary<string, AttributeGoForm> _customAttrs;
        private Dictionary<string, AttributeGoForm> CustomAttrs => _customAttrs ??
                                    (_customAttrs = PropertyInfos.ToDictionary(x => x.Key, x => x.Value.GetCustomAttribute<AttributeGoForm>()));

        private Dictionary<string, PropertyInfo> _propertyInfos;
        public Dictionary<string, PropertyInfo> PropertyInfos => _propertyInfos ??
                                    (_propertyInfos = Type.GetProperties().ToDictionary(x => x.Name, x => x));

        public CreateEditBaseForm(IDatabaseContext databaseContext, DbSet<T> dbSet, string name = null)
        {
            InitializeComponent();
            this.actionBtn.Text = "Add";
            this.actionBtn.Click += Add_Click;
            this.databaseContext = databaseContext;
            this.dbSet = dbSet;
            this.Name = name ?? Type.Name;
            this.Context = new T();
        }

        public CreateEditBaseForm(IDatabaseContext databaseContext, T elem, DbSet<T> dbSet, string name = null)
        {
            InitializeComponent();
            this.actionBtn.Text = "Edit";
            this.actionBtn.Click += Edit_Click;
            this.deleteBtn.Text = "Delete";
            this.deleteBtn.Visible = true;
            this.deleteBtn.Click += Delete_Click;
            this.dbSet = dbSet;
            this.Name = name ?? Type.Name;
            this.Context = elem;
            this.databaseContext = databaseContext;
        }

        protected virtual void InitializeComponent()
        {
            this.actionBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.deleteBtn.Visible = false;
            this.SuspendLayout();
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
