using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typography.Services.Templates
{
    /// <summary>
    /// Колонка для отчета
    /// </summary>
    public class ReportColumn
    {
        public ReportColumn(string name)
        {
            Name = name;
            Title = name;
            Type = typeof(string);
            Width = GetPixelFromInch(1);
            Expression = string.Format("=Fields!{0}.Value", name);
            HeaderBackColor = Color.LightPink;
        }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Тип
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// Ширина
        /// </summary>
        public int Width { get; set; }
        public float WidthInInch
        {
            get { return GetInchFromPixel(Width); }
        }
        /// <summary>
        /// Выражение для шаблона
        /// </summary>
        public string Expression { get; set; }
        /// <summary>
        /// Цвет заголовка
        /// </summary>
        public Color HeaderBackColor { get; set; }
        public string HeaderBackColorInHtml
        {
            get { return ColorTranslator.ToHtml(HeaderBackColor); }
        }
        private int GetPixelFromInch(float inch)
        {
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
                return (int)(g.DpiY * inch);
        }
        private float GetInchFromPixel(int pixel)
        {
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
                return (float)pixel / g.DpiY;
        }
    }
}
