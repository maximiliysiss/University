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
    /// Interaction logic for InformationList.xaml
    /// </summary>
    public partial class InformationList : UserControl
    {
        private List<Information> informations;
        private DatabaseContext context = App.Db;

        public InformationList()
        {
            InitializeComponent();
            Reload();
        }

        private void Reload()
        {
            informations = App.Db.Information.ToList();

            Menu.SetText(informations.FirstOrDefault(x => x.Name.ToLower() == "menu")?.Description);
        }

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

    public class InformationReadOnlyList : InformationList
    {
        public InformationReadOnlyList()
        {
            Save.Visibility = Visibility.Collapsed;
            Menu.IsReadOnly = true;
        }
    }
}
