using System.Linq;

namespace Garage.Extensions
{
    /// <summary>
    /// Работа со строками
    /// </summary>
    public class StringUtils
    {
        /// <summary>
        /// Пусты ли строки
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(params string[] vs) => vs.Any(x => string.IsNullOrEmpty(x));
    }
}
