using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslateChatter.ChatAPI;

namespace TranslateChatter.ViewModels
{
    /// <summary>
    /// Комната
    /// </summary>
    public class RoomViewModel
    {
        public Room Room { get; set; }
        public bool IsAction { get; set; }
    }
}
