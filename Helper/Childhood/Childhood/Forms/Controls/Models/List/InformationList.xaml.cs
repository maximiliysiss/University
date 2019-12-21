using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Childhood.Extensions;
using Childhood.Models;
using Childhood.Services;

namespace Childhood.Forms.Controls.Models.List
{
    /// <summary>
    /// Вкладка информация
    /// </summary>
    public partial class InformationList : UserControl
    {
        /// <summary>
        /// Список информаций
        /// </summary>
        private List<Information> informations;
        /// <summary>
        /// БД
        /// </summary>
        private DatabaseContext context = App.Db;

        public InformationList()
        {
            InitializeComponent();
            Reload();
        }

        /// <summary>
        /// Перезагрузить информацию
        /// </summary>
        private void Reload()
        {
            informations = App.Db.Information.ToList();

            Menu.SetText(informations.FirstOrDefault(x => x.Name.ToLower() == "menu")?.Description);
        }

        /// <summary>
        /// Сохранить меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveMenu(object sender, RoutedEventArgs e)
        {
            var setting = informations.FirstOrDefault(x => x.Name.ToLower() == "menu");
            if (setting == null)
            {
                setting = new Information { Name = "Menu", Description = Menu.GetText() };
                context.Add(setting);
            }
            else
            {
                setting.Description = Menu.GetText();
                context.Update(setting);
            }
            context.SaveChanges();
        }
    }

    /// <summary>
    /// Информация, но только для чтения
    /// </summary>
    public class InformationReadOnlyList : InformationList
    {
        public InformationReadOnlyList()
        {
            Save.Visibility = Visibility.Collapsed;
            Menu.IsReadOnly = true;
        }
    }
}
