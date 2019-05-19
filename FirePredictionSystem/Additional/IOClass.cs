namespace FirePredictionSystem.Additional
{
    class IOClass
    {
        public static string[] ReadRow(string path)
        {
            return new System.IO.StreamReader(path, System.Text.Encoding.Default).ReadToEnd().Split(' ');
        }

        public static string[][] ReadTable(string path)
        {
            string readed = new System.IO.StreamReader(path, System.Text.Encoding.Default).ReadToEnd();

            readed = readed.Replace("\r\n", "|");
            string[] strArray = readed.Split(new char[] { '|' });
            string[][] result = new string[strArray.Length][];

            for (int i = 0; i < strArray.Length; i++)
            {
                result[i] = strArray[i].Split(' ');
            }
            
            return result;
        }

        public static void WriteFile(string path, string data)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding(1251).GetBytes(data);
            using (System.IO.FileStream fileStream = new System.IO.FileStream(
                    path,
                    System.IO.FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }

    }
}
