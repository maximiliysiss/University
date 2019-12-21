using Childhood.Models;
using System.Linq;
using System.Windows;

namespace Childhood.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма ребенка для воспитателя
    /// </summary>
    public partial class TutorChildControl : Window
    {
        public TutorChildControl(Child child)
        {
            InitializeComponent();

            var lastAction = App.Db.ChildChecks.OrderByDescending(x => x.Date).FirstOrDefault(x => x.ChildId == child.ID);
            if ((lastAction?.CheckType ?? CheckType.Out) == CheckType.Out)
            {
                Action.Content = "Пришел";
                Action.Click += (s, e) => Handler(CheckType.In);
            }
            else
            {
                Action.Content = "Ушел";
                Action.Click += (s, e) => Handler(CheckType.Out);
            }

            this.DataContext = child;
        }

        /// <summary>
        /// Обработка изменения положения ребенка в садике
        /// </summary>
        /// <param name="in"></param>
        private void Handler(CheckType @in)
        {
            var child = this.DataContext as Child;
            var db = App.Db;
            db.Add(new ChildCheck { CheckType = @in, ChildId = child.ID });
            db.SaveChanges();
            Close();
        }
    }
}
