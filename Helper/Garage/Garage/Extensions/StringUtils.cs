using System.Linq;

namespace Garage.Extensions
{
    public class StringUtils
    {
        public static bool IsNullOrEmpty(params string[] vs) => vs.Any(x => string.IsNullOrEmpty(x));
    }
}
