using Bank.Forms.Controls;
using Bank.Models;
using System.Windows;
using System.Windows.Controls;

namespace Bank.Services
{
    /// <summary>
    /// Сервис для получения окна для пользователя
    /// </summary>
    public class WindowsSelectorService
    {
        public Window GetWindowByRole(Role role)
        {
            UserControl userControl = null;
            switch (role)
            {
                case Role.Admin:
                    userControl = new AdminControl();
                    break;
                case Role.Client:
                    userControl = new ClientControl();
                    break;
                case Role.Director:
                    userControl = new DirectorControl();
                    break;
                case Role.Worker:
                    userControl = new WorkerControl();
                    break;
            }

            return new MainWindow(userControl);
        }
    }
}
