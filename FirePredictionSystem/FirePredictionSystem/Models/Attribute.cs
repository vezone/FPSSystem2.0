
namespace FirePredictionSystem.Models
{
    class Attribute
    {
        public string Parametr1 { get; set; }
        public string Parametr2 { get; set; }
        public string Parametr3 { get; set; }

        public Attribute(string parametr1, string parametr2, string parametr3)
        {
            Parametr1 = parametr1;
            Parametr2 = parametr2;
            Parametr3 = parametr3;
        }
        public Attribute(string[] parametrs)
        {
            if (parametrs != null && parametrs.Length > 2)
            {
                Parametr1 = parametrs[0];
                Parametr2 = parametrs[1];
                Parametr3 = parametrs[2];
            }
        }
    }
}
