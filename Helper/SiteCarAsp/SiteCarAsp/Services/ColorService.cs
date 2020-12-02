namespace SiteCarAsp.Services
{
    public class ColorService
    {
        private readonly string[] colors = new[] {
            "#E24938, #A30F22",
            "#6CD96A, #00986F",
            "#4795D1, #006EB8",
            "#292a2f, #131519"
        };

        public string GetColor(int index) => colors[index];
    }
}
