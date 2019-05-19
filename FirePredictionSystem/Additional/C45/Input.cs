namespace FirePredictionSystem.Additional.C45
{
    using FirePredictionSystem.Additional.ExcelApp.ExcelAPI;
    using System.Collections.Generic;

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
                   K21, K22, K23, K24, K25,
                   K26, K27, K28, K29, K30,
                   K31, K32, K33, K34, K35,
                   K36, K37, K38, K39, K40, K41;
            string answer = "No";
            System.Random rand = new System.Random();

            /*
             Weights/NeuralNetwork/Excel
             */
            double[] w1 = new double[]
            {
                 1.0, 1.0, 1.0, 1.0, 1.0,
                 1.0, 1.0, 1.0, 1.0, 0.0,
                 1.0, 0.0, 1.0, 0.0, 0.0,
                 1.0, 1.0, 1.0, 1.0, 1.0,
                 1.0, 1.0, 1.0, 1.0, 1.0,
                 0.0, 1.0, 0.1, 1.0, 0.0,
                 1.0, 0.0, 1.0, 1.0, 1.0,
                 1.0, 1.0, 1.0, 1.0, 1.0,
            };
            double[] w2 = new double[40];

            string path = @"E:\General\Programming\C_Sharp\WPF\FirePredictionSystem\FirePredictionSystem\bin\Debug\Data\new_table_of_fires_2.xlsx";
            if (System.IO.File.Exists(path))
            {
                using (ExcelAPIClass exApi = new ExcelAPIClass(
                        path, "Лист1", "B3", "I100"))
                {
                    exApi.ShowCategoriesWithAttributes();
                    int[][] x = exApi.GetUsefullAttributesValues();

                    int[] target = exApi.GetTarget();
                    Perceptron perceptron = new Perceptron(x, w1, target);
                    perceptron.Learn();
                    w2 = perceptron.Weights;
                }
            }
            else
            {
                for (int wi = 0; wi < w2.Length; wi++)
                {
                    w2[wi] = 0.0;
                }
            }

            data = new System.Text.StringBuilder(
                "K1 K2 K3 K4 K5 K6 K7 K8 K9 K10 K11 " +
                "K12 K13 K14 K15 K16 K17 K18 K19 " +
                "K20 K21 K22 K23 K24 K25 K26 K27 " +
                "K28 K29 K30 K31 K32 K33 K34 K35 " +
                "K36 K37 K38 K39 K40 K41 Answer" + 
                "\r\n");
            
            for (i = 0; i < numberOfData; i++)
            {
                answer = "No";
                //горючий потолок
                K1 = rand.Next(0, 1000) > 900 ? "Присутствует" : "Отсутствует";
                //горючий пол
                K2 = rand.Next(0, 1000) > 900 ? "Присутствует" : "Отсутствует";
                //горючая стена
                K3 = rand.Next(0, 1000) > 900 ? "Присутствует" : "Отсутствует";

                #region Печь
                p = rand.Next(1, 1000);
                K4 = (p <= 700) ? "90"  :
                     (p >  700 && p <= 750) ? "110" :
                     (p >  750 && p <= 950) ? "120" : "140";
                p = rand.Next(1, 1000);
                K5 = (p <= 666) ? "<=5"  :
                     (p > 666 && p <= 950) ? "5<x<=15" : ">15";
                K6 = rand.Next(0, 1000) < 950 ? "Присутствует" : 
                                                "Отсутствует";
                p = rand.Next(1, 1000);
                K7 = (p <= 500) ? "<500" :
                     (p > 500 && p <= 550) ? ">=500" :
                     (p > 550 && p <= 600) ? ">=конька_кровли_или_парапета" :
                     (p > 600 && p <= 650) ? "<=конька_кровли_или_парапета" :
                     (p > 650 && p <= 850) ?
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

                #region Воздуховоды
                p = rand.Next(1, 1000);
                K26 = p < 999 ? ">=3" : "<3";
                p = rand.Next(1, 1000);
                K27 = (p <= 250) ? "B1" :
                      (p >  250 && p <= 500) ? "B2" :
                      (p >  500 && p <= 750) ? "B3" : "B4";
                p = rand.Next(1, 1000);
                K28 = (p <= 400) ? "B1" :
                      (p > 400 && p <= 450) ? "B2" :
                      (p > 450 && p <= 500) ? "B3" :
                      (p > 500 && p <= 600) ? "B4" :
                      (p > 600 && p <= 650) ? "Отсутствует" : "Присутствует";
                p = rand.Next(1, 1000);
                K29 = p < 800 ? "Присутствует" : "Отсутствует";
                p = rand.Next(1, 1000);
                K30 = p < 800 ? "Присутствует" : "Отсутствует";
                p = rand.Next(1, 1000);
                K31 = p < 800 ? "Присутствует" : "Отсутствует";
                p = rand.Next(1, 1000);
                K32 = p < 800 ? "Присутствует" : "Отсутствует";
                p = rand.Next(1, 1000);
                #endregion

                #region Электрика
                K33 = p < 800 ? "Алюминий" : "Медь";
                p = rand.Next(1, 1000);
                if (K33.Equals("Алюминий"))
                {
                    K34 = (p <= 166) ? "<4" :
                      (p > 166 && p <= 332) ? "6" :
                      (p > 332 && p <= 498) ? "10" :
                      (p > 498 && p <= 664) ? "16-25" :
                      (p > 664 && p <= 830) ? "35-50" : "70";
                }
                else
                {
                    K34 = (p <= 166) ? "<2.5" :
                      (p > 166 && p <= 332) ? "-" :
                      (p > 332 && p <= 498) ? "4" :
                      (p > 498 && p <= 664) ? "6-10" :
                      (p > 664 && p <= 830) ? "16" : "25-35";
                }
                p = rand.Next(1, 1000);
                if (K34.Equals("<4"))
                {
                    K35 = (p <= 900) ? "Не_нормируется" :
                      (p > 910 && p <= 920) ? "2.5" :
                      (p > 920 && p <= 930) ? "2.8" :
                      (p > 930 && p <= 940) ? "3.2" :
                      (p > 940 && p <= 950) ? "3.5" : "4";
                }
                else if (K34.Equals("6"))
                {
                    K35 = (p <= 50) ? "Не_нормируется" :
                      (p > 50 && p <= 900) ? "2.5" :
                      (p > 900 && p <= 920) ? "2.8" :
                      (p > 920 && p <= 940) ? "3.2" :
                      (p > 940 && p <= 980) ? "3.5" : "4";
                }
                else if (K34.Equals("10"))
                {
                    K35 = (p <= 50) ? "Не_нормируется" :
                      (p > 50 && p <= 60) ? "2.5" :
                      (p > 60 && p <= 900) ? "2.8" :
                      (p > 900 && p <= 920) ? "3.2" :
                      (p > 920 && p <= 940) ? "3.5" : "4";
                }
                else if (K34.Equals("16-25"))
                {
                    K35 = (p <= 50) ? "Не_нормируется" :
                      (p > 50 && p <= 60) ? "2.5" :
                      (p > 60 && p <= 70) ? "2.8" :
                      (p > 70 && p <= 900) ? "3.2" :
                      (p > 900 && p <= 950) ? "3.5" : "4";
                }
                else if (K34.Equals("35-50"))
                {
                    K35 = (p <= 10) ? "Не_нормируется" :
                      (p > 10 && p <= 20) ? "2.5" :
                      (p > 30 && p <= 40) ? "2.8" :
                      (p > 40 && p <= 50) ? "3.2" :
                      (p > 50 && p <= 950) ? "3.5" : "4";
                }
                else // K34.Equals("70")
                {
                    K35 = (p <= 50) ? "Не_нормируется" :
                          (p > 60 && p <= 70) ? "2.5" :
                          (p > 70 && p <= 80) ? "2.8" :
                          (p > 80 && p <= 90) ? "3.2" :
                          (p > 90 && p <= 100) ? "3.5" : "4";
                }
                p = rand.Next(1, 1000);
                K36 = p > 500 ? "<50мм" : ">50мм";
                p = rand.Next(1, 1000);
                K37 = (p <= 333) ? "Изоляция_провода" :
                      (p > 333 && p <= 666) ? 
                      "Прибор_нагревательный_отопительный" :
                      "Водонагревательный_прибор";
                p = rand.Next(1, 1000);
                if (K37.Equals("Изоляция_провода"))
                {
                    K38 = (p <= 900) ? "65" :
                     (p > 900 && p <= 950) ? "85" : "90";
                }
                else if (K37.Equals("Прибор_нагревательный_отопительный"))
                {
                    K38 = (p <= 50) ? "65" :
                     (p > 50 && p <= 950) ? "85" : "90";
                }
                else
                {
                    K38 = (p <= 50) ? "65" :
                     (p > 50 && p <= 100) ? "85" : "90";
                }
                p = rand.Next(1, 1000);
                K39 = (p <= 111) ? "IP20" :
                      (p > 111 && p <= 222) ? "IP23" :
                      (p > 222 && p <= 333) ? "IP51" :
                      (p > 333 && p <= 444) ? "IP53" :
                      (p > 444 && p <= 555) ? "IP54" :
                      (p > 555 && p <= 666) ? "2.0" :
                      (p > 666 && p <= 777) ? "5.0" :
                      (p > 777 && p <= 888) ? "5.3" : "5.4";
                p = rand.Next(1, 1000);
                K40 = (p <= 333) ? "ЛЛ" :
                      (p > 333 && p <= 666) ? "ЛН" : "ГЛВД";
                p = rand.Next(1, 1000);
                K41 = (p <= 142) ? "Нормальные" :
                      (p > 142 && p <= 284) ? "Влажные" :
                      (p > 284 && p <= 426) ? "Сырые" :
                      (p > 426 && p <= 568) ? "Особо_сырые" :
                      (p > 568 && p <= 710) ? "Химически_активные" :
                      (p > 710 && p <= 852) ? "Пыльные" : "Жаркие";
                #endregion

                #region K1 - done Потолок из горючих материалов
                //k10 связан с K1
                //K12 - тип печи
                //K13 - расстояние_между_верхом_перекрытия_печи_и_потолком
                if (K1.Equals("Присутствует"))
                {
                    double res = w2[0] + w2[9] + w2[10] + w2[11] + w2[12];
                    if (!K11.Equals("Защищен"))
                    {
                        if (K10.Equals("Из_трех_рядов_кирпичей"))
                        {
                            if (K12.Equals("С_периодической_топкой"))
                            {
                                if (!K13.Equals("250"))
                                {
                                    answer = (res > 1.0) ? "Yes" : answer;
                                }
                            }
                            else if (K12.Equals("Длительного_горения"))
                            {
                                if (!K13.Equals("700"))
                                {
                                    answer = (res > 1.0) ? "Yes" : answer;
                                }
                            }
                        }
                        else if (K10.Equals("Из_двух_рядов_кирпичей"))
                        {
                            if (K12.Equals("С_периодической_топкой"))
                            {
                                if (!K13.Equals("375"))
                                {
                                    answer = (res > 1.0) ? "Yes" : answer;
                                }
                            }
                            else if (K12.Equals("Длительного_горения"))
                            {
                                if (!K13.Equals("1050"))
                                {
                                    answer = (res > 1.0) ? "Yes" : answer;
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
                                    answer = (res > 1.0) ? "Yes" : answer;
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
                                    answer = (res > 1.0) ? "Yes" : answer;
                                }
                            }
                            else if (K12.Equals("Длительного_горения"))
                            {
                                if (!K13.Equals("1000"))
                                {
                                    answer = (res > 1.0) ? "Yes" : answer;
                                }
                            }
                        }
                        else if (K10.Equals("Из_двух_рядов_кирпичей"))
                        {
                            if (K12.Equals("С_периодической_топкой"))
                            {
                                if (!K13.Equals("525"))
                                {
                                    answer = (res > 1.0) ? "Yes" : answer;
                                }
                            }
                            else if (K12.Equals("Длительного_горения"))
                            {
                                if (!K13.Equals("1500"))
                                {
                                    answer = (res > 1.0) ? "Yes" : answer;
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
                                    answer = (res > 1.0) ? "Yes" : answer;
                                }
                            }
                        }
                    }
                }
                #endregion

                #region K2 - done Пол из горючих материалов
                if (K2.Equals("Присутствует"))
                {
                    double res = w2[1] + w2[21] + w2[23] + w2[24];
                    if (K22.Equals("Металлический_лист_размером_700х500_мм"))
                    {
                        if (K24.Equals("По_асбестовому_картону_толщиной_8_мм"))
                        {
                            if (!K25.Equals("Длинная_сторона_вдоль_печи"))
                            {
                                answer = (res > 1.0) ? "Yes" : answer;
                            }
                        }
                        else
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K22.Equals("Листовая_сталь"))
                    {
                        if (K24.Equals("По_асбестовому_картону_толщиной_10_мм"))
                        {
                            if (!K25.Equals("В_пределах_горизонтальной_проекции_печи"))
                            {
                                answer = (res > 1.0) ? "Yes" : answer;
                            }
                        }
                        else
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                }
                #endregion

                #region K3 - done Стена из горючих материалов
                if (K3.Equals("Присутствует"))
                {
                    double res = w2[2] + w2[22] + w2[23] + w2[24];
                    if (K23.Equals("Металлический_лист_размером_700х500_мм"))
                    {
                        if (K24.Equals("По_асбестовому_картону_толщиной_8_мм"))
                        {
                            if (!K25.Equals("От_пола_до_уровня_на_250мм_выше_верха_топочной_дверки"))
                            {
                                answer = (res > 1.0) ? "Yes" : answer;
                            }
                        }
                        else
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K23.Equals("Штукатурка_толщиной_25_мм"))
                    {
                        if (K24.Equals("По_металлической_сетке"))
                        {
                            if (!K25.Equals("От_пола_до_уровня_на_250мм_выше_верха_топочной_дверки"))
                            {
                                answer = (res > 1.0) ? "Yes" : answer;
                            }
                        }
                        else
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                }
                #endregion

                #region K4 - done Температура печи
                if (K4.Equals("110") ||
                    K4.Equals("120"))
                {
                    double res = w2[3] + w2[4] + w2[5];
                    if (K5.Equals(">15"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                else if (K4.Equals("140"))
                {
                    double res = w2[3] + w2[4] + w2[5];
                    if (!K6.Equals("Присутствует"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                #endregion
                
                #region K7 - done Высота дымовых труб
                if (K7.Equals("<500"))
                {
                    double res = w2[6] + w2[7] + w2[8];
                    if (K8.Equals("Над_плоской_кровлей"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                    else if (K8.Equals("Над_коньком_кровли_или_парапетом"))
                    {
                        if (!K9.Equals("<1.5"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                }
                else if (K7.Equals("<=конька_кровли_или_парапета"))
                {
                    double res = w2[6] + w2[8];
                    if (!K9.Equals("1.5<=x<=3"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }

                else if (K7.Equals("<=линии_проведенной_от_конька_вниз_под_углом_10"))
                {
                    double res = w2[6] + w2[8];
                    if (!K9.Equals(">3"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                #endregion

                #region K15 Тепловая мощность
                if (K15.Equals("<3.5"))
                {
                    double res = w2[13] + w2[14];
                    if (!K14.Equals("140x140"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                else if (K15.Equals("5.2<=x<=7"))
                {
                    double res = w2[13] + w2[14];
                    if (!K14.Equals("140x200"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                else if (K15.Equals(">7"))
                {
                    double res = w2[13] + w2[14];
                    if (!K14.Equals("140x270"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                #endregion
                
                #region K16 Материал дымовых труб
                if (K16.Equals("Глиняный_кирпич"))
                {
                    double res = w2[15] + w2[16];
                    if (!K17.Equals(">=120"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                else if (K16.Equals("Жаростойкий_бетон"))
                {
                    double res = w2[15] + w2[16];
                    if (!K17.Equals("60<=x<=120"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                else if (K16.Equals("Хризотилоцементные_трубы"))
                {
                    double res = w2[15] + w2[17];
                    if (!K18.Equals("300"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                else if (K16.Equals("Нержавеющая_сталь_заводской_готовности"))
                {
                    double res = w2[15] + w2[17];
                    if (!K18.Equals("400"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                #endregion

                #region K19 Вид материала дымовой трубы
                if (K19.Equals("Кирпич") 
                    ||
                    K19.Equals("Бетон"))
                {
                    double res = w2[18] + w2[20];
                    if (!K21.Equals("130<=x<=250")
                        ||
                        !K21.Equals(">=250"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
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
                            double res = w2[18] + w2[19] + w2[20];
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K20.Equals("Отсутствует"))
                    {
                        if (!K21.Equals(">=250"))
                        {
                            double res = w2[19] + w2[20];
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                }
                #endregion
                
                #region K26 Воздуховоды
                if (K26.Equals("<3"))
                {
                    if (!K29.Equals("Присутствует") &&
                        !K30.Equals("Присутствует") &&
                        !K31.Equals("Присутствует") &&
                        !K32.Equals("Присутствует"))
                    {
                        double res = w2[25] + w2[28] + w2[29] + w2[30] + w2[31];
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }

                if (K27.Equals("B1"))
                {
                    if (!K28.Equals("B1") &&
                        !K28.Equals("Присутствует"))
                    {
                        double res = w2[26] + w2[27];
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                else if (K27.Equals("B2"))
                {
                    if (!K28.Equals("B1") &&
                        !K28.Equals("B2") &&
                        !K28.Equals("Присутствует"))
                    {
                        double res = w2[26] + w2[27];
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                else if (K27.Equals("B3"))
                {
                    if (!K28.Equals("B1") && 
                        !K28.Equals("B2") &&
                        !K28.Equals("B3") &&
                        !K28.Equals("Присутствует"))
                    {
                        double res = w2[26] + w2[27];
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                else if (K27.Equals("B4"))
                {
                    if (!K28.Equals("B1") &&
                        !K28.Equals("B2") &&
                        !K28.Equals("B3") &&
                        !K28.Equals("B4") &&
                        !K28.Equals("Присутствует"))
                    {
                        double res = w2[26] + w2[27];
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                #endregion
                
                #region K33 Материал проводки
                if (K33.Equals("Алюминий"))
                {
                    double res = w2[32] + w2[33] + w2[34];
                    if (K34.Equals("6"))
                    {
                        if (!K35.Equals("2.5"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K34.Equals("10"))
                    {
                        if (!K35.Equals("2.8"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K34.Equals("16-25"))
                    {
                        if (!K35.Equals("3.2"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K34.Equals("35-50"))
                    {
                        if (!K35.Equals("3.5"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K34.Equals("70"))
                    {
                        if (!K35.Equals("4.0"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                }
                else //Медь
                {
                    double res = w2[32] + w2[33] + w2[34];
                    if (K34.Equals("6"))
                    {
                        if (!K35.Equals("-"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K34.Equals("4"))
                    {
                        if (!K35.Equals("2.8"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K34.Equals("6-10"))
                    {
                        if (!K35.Equals("3.2"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K34.Equals("16"))
                    {
                        if (!K35.Equals("3.5"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K34.Equals("25-35"))
                    {
                        if (!K35.Equals("4.0"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                }
                #endregion

                #region 37 Наружная поверхность
                if (K37.Equals("Изоляция_провода"))
                {
                    double res = w2[36] + w2[37];
                    if (!K38.Equals("65"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                else if (K37.Equals("Прибор_нагревательный_отопительный"))
                {
                    double res = w2[36] + w2[37];
                    if (!K38.Equals("85"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                else if (K37.Equals("Водонагревательный_прибор"))
                {
                    double res = w2[36] + w2[37];
                    if (!K38.Equals("90"))
                    {
                        answer = (res > 1.0) ? "Yes" : answer;
                    }
                }
                #endregion
                
                #region K40 Степень защиты светильников
                if (K40.Equals("ЛЛ"))
                {
                    double res = w2[38] + w2[39] + w2[39];
                    if (K39.Equals("IP20"))
                    {
                        if (K41.Equals("Сырые"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                        else if (K41.Equals("Особо_сырые"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K39.Equals("IP23"))
                    {
                        if (K41.Equals("Нормальные"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K39.Equals("IP54"))
                    {
                        if (K41.Equals("Нормальные"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                        else if (K41.Equals("Влажные"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }

                    }
                    else if (K39.Equals("5.4"))
                    {
                        if (K41.Equals("Нормальные"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                        else if (K41.Equals("Влажные"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                }
                else if (K40.Equals("ЛН") || K40.Equals("ГЛВД"))
                {
                    double res = w2[38] + w2[39] + w2[39];
                    if (K39.Equals("IP23"))
                    {
                        if (K41.Equals("Нормальные"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K39.Equals("IP53"))
                    {
                        if (K41.Equals("Нормальные"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                        else if (K41.Equals("Влажные"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                    }
                    else if (K39.Equals("5.0") || 
                             K39.Equals("5.3") ||
                             K39.Equals("5.4"))
                    {
                        if (K41.Equals("Нормальные"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
                        }
                        else if (K41.Equals("Влажные"))
                        {
                            answer = (res > 1.0) ? "Yes" : answer;
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
                data.Append(K25 + " "); data.Append(K26 + " ");
                data.Append(K27 + " "); data.Append(K28 + " ");
                data.Append(K29 + " "); data.Append(K30 + " ");
                data.Append(K31 + " "); data.Append(K32 + " ");
                data.Append(K33 + " "); data.Append(K34 + " ");
                data.Append(K35 + " "); data.Append(K36 + " ");
                data.Append(K37 + " "); data.Append(K38 + " ");
                data.Append(K39 + " "); data.Append(K40 + " ");
                data.Append(K41 + " "); data.Append(answer);

                if (i != (numberOfData-1))
                {
                    data.Append("\r\n");
                }
                #endregion

            }

            return data.ToString();
        }

    } //99 - 1021

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
