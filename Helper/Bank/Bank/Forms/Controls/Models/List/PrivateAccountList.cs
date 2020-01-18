using Bank.Forms.Controls.Models.Model;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bank.Forms.Controls.Models.List
{
    /// <summary>
    /// Список лс
    /// </summary>
    public class PrivateAccountList : BaseModelListControl
    {
        protected override void AddNew() => Open(new PrivateAccount());

        protected override List<object> Load() => App.Db.PrivateAccounts.Cast<object>().ToList();

        protected override void Open(object obj) => new PrivateAccountControl(obj as PrivateAccount).ShowDialog();
    }

    /// <summary>
    /// Список лс клиентов
    /// </summary>
    public class ClientPrivateAccountList : PrivateAccountList
    {
        protected override List<object> Load() => App.Db.PrivateAccounts.Where(x => x.User.Role == Role.Client).Cast<object>().ToList();

        protected override void Open(object obj) => new PrivateAccountControl(obj as PrivateAccount,
            new PrivateAccountControlContent(obj as PrivateAccount, App.Db.Currencies.ToList(), App.Db.Users.Where(x => x.Role == Role.Client).ToList())).ShowDialog();
    }

    /// <summary>
    /// Список лс клиента
    /// </summary>
    public class ClientsAccountList : PrivateAccountList
    {
        public ClientsAccountList()
        {
            this.grid.RowDefinitions.RemoveAt(this.grid.RowDefinitions.Count - 1);
            this.Add.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override List<object> Load() => App.Db.PrivateAccounts.Where(x => x.UserId == App.user.Id).Cast<object>().ToList();

        protected override void Open(object obj) => new ClientPrivateAccountControl(obj as PrivateAccount).ShowDialog();
    }
}
