using Bank.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Bank.Forms.Controls.Models.Model
{
    /// <summary>
    /// Форма лс
    /// </summary>
    public class PrivateAccountControl : BaseModelControl<PrivateAccount>
    {
        public PrivateAccountControl(PrivateAccount obj) : base(obj, new PrivateAccountControlContent(obj))
        {
        }

        public PrivateAccountControl(PrivateAccount obj, UserControl content) : base(obj, content)
        {
        }

        public override bool IsEdit(PrivateAccount obj) => obj.Id != 0;
    }

    /// <summary>
    /// Interaction logic for PrivateAccountControlContent.xaml
    /// </summary>
    public partial class PrivateAccountControlContent : UserControl
    {
        public PrivateAccountControlContent(PrivateAccount obj)
            : this(obj, App.Db.Currencies.ToList(), App.Db.Users.ToList())
        {

        }

        public PrivateAccountControlContent(PrivateAccount obj, List<Currency> currencies, List<User> users)
        {
            InitializeComponent();
            this.Currency.ItemsSource = currencies;
            this.User.ItemsSource = users;
            this.DataContext = obj;
        }
    }
}
