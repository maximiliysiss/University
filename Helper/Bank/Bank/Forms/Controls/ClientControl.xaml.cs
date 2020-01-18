using Bank.Forms.Controls.Models.List;
using Bank.Models;
using Bank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bank.Forms.Controls
{
    /// <summary>
    /// Interaction logic for ClientControl.xaml
    /// </summary>
    public partial class ClientControl : UserControl
    {
        readonly DatabaseContext context = App.Db;

        public ClientControl()
        {
            InitializeComponent();
            ReloadTransactionInfo();
        }

        /// <summary>
        /// Загрузим данные для транзакций
        /// </summary>
        private void ReloadTransactionInfo()
        {
            this.Accounts.Content = new ClientsAccountList();
            this.Account.ItemsSource = context.PrivateAccounts.Where(x => x.UserId == App.user.Id).ToList();
        }

        /// <summary>
        /// Создадим транзакцию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateTransactionClick(object sender, RoutedEventArgs e)
        {
            if (!CreateTransaction())
                MessageBox.Show("Невозможно провести перевод. Есть ошибки");
            else
                MessageBox.Show("Успешный перевод");
        }

        /// <summary>
        /// Проведем транзакцию
        /// </summary>
        /// <returns></returns>
        private bool CreateTransaction()
        {
            var text = Guid.Text.Trim();
            if (text.Length > 0)
            {
                try
                {
                    // Куда
                    var account = context.PrivateAccounts.FirstOrDefault(x => x.Guid.Contains(text));
                    var selectedAccount = Account.SelectedItem as PrivateAccount;
                    if (account == null || selectedAccount == null)
                        return false;

                    // Сколько
                    decimal countTransitionFrom = decimal.Parse(Sum.Text.Trim());
                    if (selectedAccount.Sum < countTransitionFrom)
                        return false;

                    decimal countTransitionTo = countTransitionFrom;
                    if (countTransitionFrom <= 0)
                        return false;

                    // Если валюты счетов отличаются
                    if (account.CurrencyId != selectedAccount.CurrencyId)
                    {
                        var convert = context.ConvertCurrencies.FirstOrDefault(x => (x.CurrencyToId == account.CurrencyId && x.CurrencyFromId == selectedAccount.CurrencyId)
                                                    || (x.CurrencyFromId == account.CurrencyId && x.CurrencyToId == selectedAccount.CurrencyId));
                        if (convert == null)
                            return false;

                        bool isAnotherWay = convert.CurrencyToId != selectedAccount.CurrencyId;

                        // Перевод в валюту счета
                        countTransitionTo *= (isAnotherWay ? (1 / convert.Convert) : convert.Convert);
                    }

                    // Перевод и сохранение
                    account.Sum += countTransitionTo;
                    selectedAccount.Sum -= countTransitionFrom;

                    context.Update(account);
                    context.Update(selectedAccount);
                    context.SaveChanges();

                    ReloadTransactionInfo();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Поиск по Guid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeGuid(object sender, TextChangedEventArgs e)
        {
            var text = Guid.Text.Trim();
            if (text.Length > 0)
            {
                var account = context.PrivateAccounts.FirstOrDefault(x => x.Guid.Contains(text));
                if (account != null)
                    Fio.Text = $"{account.User.FIO}|{account.Currency.Name}";
                else
                    Fio.Text = "Не найдено";
            }
            else
            {
                Fio.Text = string.Empty;
            }
        }
    }
}
