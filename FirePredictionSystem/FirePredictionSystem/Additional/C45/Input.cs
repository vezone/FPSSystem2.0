using System.Collections.Generic;

namespace FirePredictionSystem.Additional.C45
{

    public static class IO
    {
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

        public static string ReadFile(string path)
            => new System.IO.StreamReader(path, System.Text.Encoding.Default).ReadToEnd();

        public static string GenerateInput(int numberOfData)
        {
            string data = "",
                   type = "", gunType = "", answer = "",
                   angle = "", distance = "", score = "";

            System.Random rand = new System.Random();

            data += "Type GunType Angle Distance Score Answer" + "\r\n";

            for (int i = 1; i < numberOfData; i++)
            {
                type = "Стрелок" + rand.Next(1, 6);
                gunType = rand.Next(0, 10) > 5 ? "Нарезной" : "Гладкий";
                angle = rand.Next(1, 1000) < 300 ? "<60" :
                        rand.Next(1, 1000) < 600 ? "60<=x<85" : ">85";//rand.Next(50, 95);
                distance = rand.Next(1, 1000) < 300 ? "100" :
                           rand.Next(1, 1000) < 600 ? "150" : "200";
                score = rand.Next(1, 1000) < 300 ? "<600" :
                        rand.Next(1, 1000) < 600 ? "600<=x<=700" : ">700";//rand.Next(500, 800);

                if (score == "<600")
                {
                    answer = "No";
                }
                else if (
                        (score == ">700" && distance == "100")
                            ||
                        (score == ">700" && distance == "150")
                            ||
                        (gunType == "Нарезной" && distance == "100")
                            ||
                        (gunType == "Нарезной" && distance == "150")
                            ||
                        (gunType == "Нарезной" && angle == ">85" && distance != "200")
                            ||
                        (gunType == "Нарезной" && angle == "<60" && distance != "200")
                            ||
                        (gunType == "Нарезной" && angle == "60<=x<85" && distance != "200")
                            ||
                        (gunType == "Гладкий" && distance == "100")
                            ||
                        (gunType == "Гладкий" && angle == ">85")

                        )
                {
                    answer = "Yes";
                }
                else if (
                         (gunType == "Нарезной" && distance == "150")
                            ||
                         (gunType == "Нарезной" && distance == "200")
                            ||
                         (gunType == "Гладкий" && angle == "60<=x<85")
                            ||
                         (gunType == "Гладкий" && angle == "<60")
                            ||
                         (angle == "60<=x<85" && distance == "150")
                            ||
                         (angle == "60<=x<85" && distance == "200")
                            ||
                         (angle != "<60" && distance == "200")
                        )
                {
                    answer = "No";
                }
                else
                {
                    System.Console.WriteLine("неучтенный");
                    answer = "Yes";
                }
                data += $"{type} {gunType} {angle} {distance} {score} {answer}" + ((i != numberOfData - 1) ? "\r\n" : "");
            }

            return data;
        }

        public static string GenerateInput2(int numberOfData)
        {
            int    i, p;
            System.Text.StringBuilder data;
            string K1,  K2,  K3,  K4,  K5, 
                   K6,  K7,  K8,  K9,  K10,
                   K11, K12, K13, K14, K15,
                   K16, K17, K18, K19, K20,
                   K21, K22, K23, K24, K25;
            string answer = "No";
            System.Random rand = new System.Random();

            data = new System.Text.StringBuilder(
                "K1 K2 K3 K4 K5 K6 K7 K8 K9 K10 K11 " +
                "K12 K13 K14 K15 K16 K17 " +
                "K18 K19 K20 K21 K22 K23 K24 K25 Answer" + 
                "\r\n");
            
            for (i = 0; i < numberOfData; i++)
            {
                answer = "No";
                //горючий потолок
                K1 = rand.Next(0, 1000) > 500 ? "Присутствует" : "Отсутствует";
                //горючий пол
                K2 = rand.Next(0, 1000) > 500 ? "Присутствует" : "Отсутствует";
                //горючая стена
                K3 = rand.Next(0, 1000) > 500 ? "Присутствует" : "Отсутствует";

                #region Печь
                p = rand.Next(1, 1000);
                K4 = (p <= 250) ? "90"  :
                     (p >  250 && p <= 500) ? "110" :
                     (p >  500 && p <= 750) ? "120" : "140";
                p = rand.Next(1, 1000);
                K5 = (p <= 333) ? "<=5"  :
                     (p > 333 && p <= 666) ? "5<x<=15" : ">15";
                K6 = rand.Next(0, 10) > 5 ? 
                    "Присутствует" : 
                    "Отсутствует";

                p = rand.Next(1, 1000);
                K7 = (p <= 166) ? "<500" :
                     (p > 166 && p <= 332) ? ">=500" :
                     (p > 332 && p <= 498) ? ">=конька_кровли_или_парапета" :
                     (p > 498 && p <= 664) ? "<=конька_кровли_или_парапета" :
                     (p > 664 && p <= 830) ?
                     ">=линии_проведенной_от_конька_вниз_под_углом_10":
                     "<=линии_проведенной_от_конька_вниз_под_углом_10";
                K8 = rand.Next(0, 10) > 5 ? 
                    "Над_плоской_кровлей" :
                    "Над_коньком_кровли_или_парапетом";
                p = rand.Next(1, 1000);
                K9 = p < 333 ? "<1.5" : 
                     (p > 333 && p < 666) ? "1.5<=x<=3" : ">3";

                p = rand.Next(1, 1000);
                K10 =  p <= 250 ? "Из_трех_рядов_кирпичей" :
                      (p >  250 && p <= 500) ? "Из_двух_рядов_кирпичей" :
                      (p <  500 && p <= 750) ? 
                      "Металлическая_с_теплоизолированным_перекрытием" :
                      "Металлическая_с_нетеплоизолированным_перекрытием";
                K11 = rand.Next(0, 10) > 5 ? "Защищен" : "Не_защищен";
                K12 = rand.Next(0, 10) > 5 ? 
                    "С_периодической_топкой" :
                    "Длительного_горения";
                p = rand.Next(1, 1000);
                K13 = (p <= 100) ? "250"  :
                      (p > 100 && p <= 200) ? "350"  :
                      (p > 200 && p <= 300) ? "375"  :
                      (p > 300 && p <= 400) ? "525"  :
                      (p > 400 && p <= 500) ? "700"  :
                      (p > 500 && p <= 600) ? "800"  :
                      (p > 600 && p <= 700) ? "1000" :
                      (p > 700 && p <= 800) ? "1050" :
                      (p > 800 && p <= 900) ? "1200" : "1500";
                #endregion

                #region Дымовая_труба
                p = rand.Next(1, 1000);
                K14 = p <= 333 ? "140x140" :
                      (p > 333 && p <= 666) ? 
                        "140x200" : "140x270";
                p = rand.Next(1, 1000);
                K15 = p <= 250 ? "<3.5" : 
                      (p > 250 && p <= 500) ? "3.5<=x<=5.2" : 
                      (p < 500 && p <= 750) ? "5.2<=x<=7" : ">7";
                p = rand.Next(1, 1000);
                K16 = p <= 250 ? "Глиняный_кирпич" :
                      (p > 250 && p <= 500) ? "Жаростойкий_бетон" :
                      (p < 500 && p <= 750) ? 
                      "Хризотилоцементные_трубы" :
                      "Нержавеющая_сталь_заводской_готовности";
                p = rand.Next(1, 1000);
                K17 = p <= 333 ? "60<=x<=120" :
                      (p > 333 && p <= 666) ? 
                      ">=120" : "<60";
                K18 = rand.Next(1, 1000) < 300 ? "300" : "400";
                p = rand.Next(1, 1000);
                K19 = p <= 333 ? "Кирпич" :
                      (p > 333 && p <= 666) ? 
                        "Бетон" : "Керамика";
                K20 = rand.Next(1, 1000) <= 500 ? "Присутствует" : "Отсутствует";
                p = rand.Next(1, 1000);
                K21 = p <= 333 ? "<130" :
                      (p > 333 && p <= 666) ?  
                      "130<=x<=250" : ">=250";
                #endregion

                #region Защита_горючих_поверхностей
                p = rand.Next(1, 1000);
                K22 = p <= 333 ? "Металлический_лист_размером_700х500_мм" :
                      (p > 333 && p <= 666) ?
                        "Листовая_сталь"
                        : "Штукатурка_толщиной_25_мм";
                p = rand.Next(1, 1000);
                K23 = p <= 333 ? "Металлический_лист_размером_700х500_мм" :
                      (p > 333 && p <= 666) ?
                        "Штукатурка_толщиной_25_мм"
                        : "Листовая_сталь";
                p = rand.Next(1, 1000);
                K24 = p <= 333 ? "По_асбестовому_картону_толщиной_8_мм" :
                      (p > 333 && p <= 666) ?
                      "По_асбестовому_картону_толщиной_10_мм" :
                        "По_металлической_сетке";
                p = rand.Next(1, 1000);
                K25 = p <= 333 ? "Длинная_сторона_вдоль_печи" :
                      (p > 333 && p <= 666) ?
                      "От_пола_до_уровня_на_250мм_выше_верха_топочной_дверки" 
                      : "В_пределах_горизонтальной_проекции_печи";
                #endregion
                
                #region K1 - done Потолок из горючих материалов
                //k10 связан с K1
                //K12 - тип печи
                //K13 - расстояние_между_верхом_перекрытия_печи_и_потолком
                if (K1.Equals("Присутствует"))
                {
                    if (!K11.Equals("Защищен"))
                    {
                        if (K10.Equals("Из_трех_рядов_кирпичей"))
                        {
                            if (K12.Equals("С_периодической_топкой"))
                            {
                                if (!K13.Equals("250"))
                                {
                                    answer = "Yes";
                                }
                            }
                            else if (K12.Equals("Длительного_горения"))
                            {
                                if (!K13.Equals("700"))
                                {
                                    answer = "Yes";
                                }
                            }
                        }
                        else if (K10.Equals("Из_двух_рядов_кирпичей"))
                        {
                            if (K12.Equals("С_периодической_топкой"))
                            {
                                if (!K13.Equals("375"))
                                {
                                    answer = "Yes";
                                }
                            }
                            else if (K12.Equals("Длительного_горения"))
                            {
                                if (!K13.Equals("1050"))
                                {
                                    answer = "Yes";
                                }
                            }
                        }
                        else if (K10.Equals("Металлическая_с_теплоизолированным_перекрытием"))
                        {
                            if (K12.Equals("С_периодической_топкой")
                                || 
                                K12.Equals("Длительного_горения"))
                            {
                                if (!K13.Equals("800"))
                                {
                                    answer = "Yes";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (K10.Equals("Из_трех_рядов_кирпичей"))
                        {
                            if (K12.Equals("С_периодической_топкой"))
                            {
                                if (!K13.Equals("350"))
                                {
                                    answer = "Yes";
                                }
                            }
                            else if (K12.Equals("Длительного_горения"))
                            {
                                if (!K13.Equals("1000"))
                                {
                                    answer = "Yes";
                                }
                            }
                        }
                        else if (K10.Equals("Из_двух_рядов_кирпичей"))
                        {
                            if (K12.Equals("С_периодической_топкой"))
                            {
                                if (!K13.Equals("525"))
                                {
                                    answer = "Yes";
                                }
                            }
                            else if (K12.Equals("Длительного_горения"))
                            {
                                if (!K13.Equals("1500"))
                                {
                                    answer = "Yes";
                                }
                            }
                        }
                        else if (K10.Equals("Металлическая_с_нетеплоизолированным_перекрытием"))
                        {
                            if (K12.Equals("С_периодической_топкой")
                                ||
                                K12.Equals("Длительного_горения"))
                            {
                                if (!K13.Equals("1200"))
                                {
                                    answer = "Yes";
                                }
                            }
                        }
                    }
                }
                #endregion

                #region K2 - done Пол из горючих материалов
                if (K2.Equals("Присутствует"))
                {
                    if (K22.Equals("Металлический_лист_размером_700х500_мм"))
                    {
                        if (K24.Equals("По_асбестовому_картону_толщиной_8_мм"))
                        {
                            if (!K25.Equals("Длинная_сторона_вдоль_печи"))
                            {
                                answer = "Yes";
                            }
                        }
                        else
                        {
                            answer = "Yes";
                        }
                    }
                    else if (K22.Equals("Листовая_сталь"))
                    {
                        if (K24.Equals("По_асбестовому_картону_толщиной_10_мм"))
                        {
                            if (!K25.Equals("В_пределах_горизонтальной_проекции_печи"))
                            {
                                answer = "Yes";
                            }
                        }
                        else
                        {
                            answer = "Yes";
                        }
                    }
                }
                #endregion

                #region K3 - done Стена из горючих материалов
                if (K3.Equals("Присутствует"))
                {
                    if (K23.Equals("Металлический_лист_размером_700х500_мм"))
                    {
                        if (K24.Equals("По_асбестовому_картону_толщиной_8_мм"))
                        {
                            if (!K25.Equals("От_пола_до_уровня_на_250мм_выше_верха_топочной_дверки"))
                            {
                                answer = "Yes";
                            }
                        }
                        else
                        {
                            answer = "Yes";
                        }
                    }
                    else if (K23.Equals("Штукатурка_толщиной_25_мм"))
                    {
                        if (K24.Equals("По_металлической_сетке"))
                        {
                            if (!K25.Equals("От_пола_до_уровня_на_250мм_выше_верха_топочной_дверки"))
                            {
                                answer = "Yes";
                            }
                        }
                        else
                        {
                            answer = "Yes";
                        }
                    }
                }
                #endregion

                #region K4 - done Температура печи
                if (K4.Equals("110"))
                {
                    if (K5.Equals(">15"))
                    {
                        answer = "Yes";
                    }
                }
                else if (K4.Equals("120"))
                {
                    if (!K5.Equals("<=5"))
                    {
                        answer = "Yes";
                    }
                }
                else if (K4.Equals("140"))
                {
                    if (!K6.Equals("Присутствует"))
                    {
                        answer = "Yes";
                    }
                }
                else
                {
                    if (!K4.Equals("90"))
                    {
                        answer = "Yes";
                    }
                }
                #endregion
                
                #region K7 ~done Высота дымовых труб
                if (K7.Equals("<500"))
                {
                    if (K8.Equals("Над_плоской_кровлей"))
                    {
                        answer = "Yes";
                    }
                    else if (K8.Equals("Над_коньком_кровли_или_парапетом"))
                    {
                        if (!K9.Equals("<1.5"))
                        {
                            answer = "Yes";
                        }
                    }
                }
                else if (K7.Equals("<=конька_кровли_или_парапета"))
                {
                    //здесь всегда Yes for data111.txt
                    if (!K9.Equals("1.5<=x<=3"))
                    {
                        answer = "Yes";
                    }
                }

                else if (K7.Equals("<=линии_проведенной_от_конька_вниз_под_углом_10"))
                {
                    if (!K9.Equals(">3"))
                    {
                        answer = "Yes";
                    }
                }
                #endregion

                #region K15 Тепловая мощность
                if (K15.Equals("<3.5"))
                {
                    if (!K14.Equals("140x140"))
                    {
                        answer = "Yes";
                    }
                }
                else if (K15.Equals("5.2<=x<=7"))
                {
                    if (!K14.Equals("140x200"))
                    {
                        answer = "Yes";
                    }
                }
                else if (K15.Equals(">7"))
                {
                    if (!K14.Equals("140x270"))
                    {
                        answer = "Yes";
                    }
                }
                #endregion
                
                #region K16 Материал дымовых труб
                if (K16.Equals("Глиняный_кирпич"))
                {
                    if (!K17.Equals(">=120"))
                    {
                        answer = "Yes";
                    }
                }
                else if (K16.Equals("Жаростойкий_бетон"))
                {
                    if (!K17.Equals("60<=x<=120"))
                    {
                        answer = "Yes";
                    }
                }
                else if (K16.Equals("Хризотилоцементные_трубы"))
                {
                    if (!K18.Equals("300"))
                    {
                        answer = "Yes";
                    }
                }
                else if (K16.Equals("Нержавеющая_сталь_заводской_готовности"))
                {
                    if (!K18.Equals("400"))
                    {
                        answer = "Yes";
                    }
                }
                #endregion

                #region K19 Вид материала дымовой трубы
                if (K19.Equals("Кирпич") 
                    ||
                    K19.Equals("Бетон"))
                {
                    if (!K21.Equals("130<=x<=250")
                        ||
                        !K21.Equals(">=250"))
                    {
                        answer = "Yes";
                    }
                }
                else if (K19.Equals("Керамика"))
                {
                    if (K20.Equals("Присутствует"))
                    {
                        if (!K21.Equals("130<=x<=250")
                            ||
                            !K21.Equals(">=250"))
                        {
                            answer = "Yes";
                        }
                    }
                    else if (K20.Equals("Отсутствует"))
                    {
                        if (!K21.Equals(">=250"))
                        {
                            answer = "Yes";
                        }
                    }
                }
                #endregion

                #region data_Append
                data.Append(K1  + " "); data.Append(K2  + " ");
                data.Append(K3  + " "); data.Append(K4  + " ");
                data.Append(K5  + " "); data.Append(K6  + " ");
                data.Append(K7  + " "); data.Append(K8  + " ");
                data.Append(K9  + " "); data.Append(K10 + " ");
                data.Append(K11 + " "); data.Append(K12 + " ");
                data.Append(K13 + " "); data.Append(K14 + " ");
                data.Append(K15 + " "); data.Append(K16 + " ");
                data.Append(K17 + " "); data.Append(K18 + " ");
                data.Append(K19 + " "); data.Append(K20 + " ");
                data.Append(K21 + " "); data.Append(K22 + " ");
                data.Append(K23 + " "); data.Append(K24 + " ");
                data.Append(K25 + " ");
                data.Append(answer);
                if (i != (numberOfData-1))
                {
                    data.Append("\r\n");
                }
                #endregion

            }

            return data.ToString();
        }

    } //99 - 592 

    public class Input
    {
        public string[][] Data { get; set; }

        public Input()
        {

        }

        public Input(string[][] data)
        {
            int rLength = data.Length,
                cLength = data[0].Length; ;
            Data = new string[rLength][];
            for (int i = 0; i < rLength; i++)
            {
                Data[i] = new string[cLength];
            }
            for (int r = 0; r < rLength; r++)
            {
                for (int c = 0; c < cLength; c++)
                {
                    Data[r][c] = data[r][c];
                }
            }
        }

        public string[] GetAttributeColumn(int attributeNumber)
        {
            string[] result = new string[Data.Length];

            for (int i = 0; i < Data.Length; i++)
                result[i] = Data[i][attributeNumber];

            return result;
        }
        
        public Input GetSubset(string attribute,
                               string attributeValue)
        {
            int attributeNumber;

            for (attributeNumber = 1;
                 attributeNumber < Data[0].Length - 1;
                 attributeNumber++)
            {
                if (Data[0][attributeNumber] == attribute)
                    break;
            }

            var data = new List<string[]>();
            data.Add(Data[0]);

            for (int i = 1; i < Data.Length; i++)
            {
                if (Data[i][attributeNumber] == attributeValue)
                    data.Add(Data[i]);
            }

            var subset = new Input();
            subset.Data = data.ToArray();

            return subset;
        }

    }
}

//NOTE:
/*
Возможно, что для упрощения вероятности здесь требуется рассчитывать случайное число только
один раз. То есть вместо:
    
    K4 = rand.Next(1, 1000) < 300 ? "90"  :
         rand.Next(1, 1000) < 300 ? "110" :
         rand.Next(1, 1000) < 300 ? "120" : "140";
Сделать так:
    int p;
    K4 = (p = rand.Next(1, 1000)) <= 300 ? "90"  :
         (p > 300 && p < 600) ? "110" :
         p >= 600 ? "120" : "140";
         
    
*/
