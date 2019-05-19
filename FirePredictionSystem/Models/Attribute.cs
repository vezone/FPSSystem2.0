
namespace FirePredictionSystem.Models
{
    class Attribute
    {
        public string K1 { get; set; }
        public string K2 { get; set; }
        public string K3 { get; set; }
        public string K4 { get; set; }
        public string K5 { get; set; }
        public string K6 { get; set; }
        public string K7 { get; set; }
        public string K8 { get; set; }
        public string K9 { get; set; }
        public string K10 { get; set; }
        public string K11 { get; set; }
        public string K12 { get; set; }
        public string K13 { get; set; }
        public string K14 { get; set; }
        public string K15 { get; set; }
        public string K16 { get; set; }
        public string K17 { get; set; }
        public string K18 { get; set; }
        public string K19 { get; set; }
        public string K20 { get; set; }
        public string K21 { get; set; }
        public string K22 { get; set; }
        public string K23 { get; set; }
        public string K24 { get; set; }
        public string K25 { get; set; }
        public string Answer { get; set; }

        public Attribute(string[] values)
        {
            if (values != null)
            {
                K1 = values[0];
                K2 = values[1];
                K3 = values[2];
                K4 = values[3];
                K5 = values[4];
                K6 = values[5];
                K7 = values[6];
                K8 = values[7];
                K9 = values[8];
                K10 = values[9];
                K11 = values[10];
                K12 = values[11];
                K13 = values[12];
                K14 = values[13];
                K15 = values[14];
                K16 = values[15];
                K17 = values[16];
                K18 = values[17];
                K19 = values[18];
                K20 = values[19];
                K21 = values[20];
                K22 = values[21];
                K23 = values[22];
                K24 = values[23];
                K25 = values[24];
                Answer = values[25];
            }
        }
    }
}
