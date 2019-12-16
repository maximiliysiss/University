using Garage.Forms;
using Garage.Forms.Controls;
using Garage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Garage.Services
{
    /// <summary>
    /// Сервис для выбора окна для пользователя
    /// </summary>
    public class UserWindowService
    {
        public Window OpenUserWindow(UserRole userRole)
        {
            UserControl control = null;
            string name = string.Empty;

            switch (userRole)
            {
                case Models.UserRole.User:
                    control = new UsersControl();
                    break;
                case Models.UserRole.HomeKeeper:
                    control = new HomeKeeperControl();
                    break;
            }

            var wnd = new MainWindow(control)
            {
                Title = name
            };

            return wnd;
        }
    }
}
