using Childhood.Forms.Controls.Models.List;
using System;
using System.Collections.Generic;
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

namespace Childhood.Forms.Controls
{
    /// <summary>
    /// Interaction logic for DirectorControl.xaml
    /// </summary>
    public partial class DirectorControl : UserControl
    {
        public DirectorControl()
        {
            InitializeComponent();
            Users.Content = new UsersList();
            Groups.Content = new GroupList();
            Child.Content = new ChildList();
        }
    }
}
