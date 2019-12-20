using Childhood.Forms;
using Childhood.Forms.Controls;
using Childhood.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Childhood.Services
{
    public class UserWindowService
    {
        public Window OpenUserWindow(UserType userRole)
        {
            UserControl control = null;
            string name = string.Empty;

            switch (userRole)
            {
                case UserType.Director:
                    control = new DirectorControl();
                    break;
                case UserType.Tutor:
                    control = new TutorControl();
                    break;
                case UserType.Parent:
                    control = new ParentControl();
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
