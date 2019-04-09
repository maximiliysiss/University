using BeatifulStudio.DataLayout.Models;
using BeatifulStudio.Forms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeatifulStudio.Services
{
    public class RegisterOrder
    {
        public static void Register(Service service, User user, DataLayout.DatabaseContext databaseContext)
        {
            var dataForm = new SelectDate();
            dataForm.ShowDialog();
            if (!dataForm.DateTime.HasValue)
            {
                MessageBox.Show("Select date", "Error");
                return;
            }

            User master = databaseContext.Users.Where(x => x.Role == Role.Master && x.IsFree(dataForm.DateTime.Value))
                    .Select(x => new { x, Count = x.MastersService.Count() }).OrderBy(x => x.Count).FirstOrDefault()?.x;

            if (master == null)
            {
                MessageBox.Show("Not free master", "Error");
                return;
            }

            databaseContext.UsersServices.Add(new UsersService
            {
                DateTime = dataForm.DateTime.Value,
                Service = service,
                User = user,
                Master = master
            });
            databaseContext.SaveChanges();
            MessageBox.Show($"Success ordering to {master.Login}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
