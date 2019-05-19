namespace FirePredictionSystem.Additional.ExcelApp.ExcelAPI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Excel = Microsoft.Office.Interop.Excel;

    public class FireTable
    {
        public int Length { get; private set; }

        public string[] BuildingName { get; set; }

        public int[] BuildingArea { get; set; }

        public short[] Floors { get; set; }

        public string[] Purpose { get; set; }

        public string[] DateOfFire { get; set; }

        public string[] CauseOfFire { get; set; }

        public string[] City { get; set; }

        public int[] FireArea { get; set; }

        public int[][] AttributeValues { get; set; }

        public int[] ConsoleColors { get; set; }

        public string[] ShortCause { get; set; }

        public FireTable(int length)
        {
            Length = length;
            BuildingName = new string[length];
            BuildingArea = new int[length];
            Floors = new short[length];
            Purpose = new string[length];
            DateOfFire = new string[length];
            CauseOfFire = new string[length];
            City = new string[length];
            FireArea = new int[length];
            AttributeValues = new int[length][];
            ShortCause = new string[length];
            ConsoleColors = new int[length];
        }

        public void Print()
        {
            int length = BuildingName.Length;
            StringBuilder sb = new StringBuilder(150);
            for (int index = 0; index < length; index++)
            {
                sb.Clear();

                sb.Append(BuildingName[index]); sb.Append(" ");
                sb.Append(BuildingArea[index]); sb.Append(" ");
                sb.Append(Floors[index]); sb.Append(" ");
                sb.Append(Purpose[index]); sb.Append(" ");
                sb.Append(DateOfFire[index]); sb.Append(" ");
                sb.Append(CauseOfFire[index]); sb.Append(" ");
                sb.Append(City[index]); sb.Append(" ");
                sb.Append(FireArea[index]);
                
                Console.WriteLine(sb.ToString());
            }
        }

        public void Print(int index)
        {
            StringBuilder sb = new StringBuilder(150);
            sb.Append(BuildingName[index]); sb.Append(" ");
            sb.Append(BuildingArea[index]); sb.Append(" ");
            sb.Append(Floors[index]); sb.Append(" ");
            sb.Append(Purpose[index]); sb.Append(" ");
            sb.Append(DateOfFire[index]); sb.Append(" ");
            sb.Append(CauseOfFire[index]); sb.Append(" ");
            sb.Append(City[index]); sb.Append(" ");
            sb.Append(FireArea[index]);
            Console.WriteLine(sb.ToString());
        }

        public string ToStringByIndex(int index)
        {
            StringBuilder sb = new StringBuilder(150);
            
            sb.Append(BuildingName[index]); sb.Append(" ");
            sb.Append(BuildingArea[index]); sb.Append(" ");
            sb.Append(Floors[index]); sb.Append(" ");
            sb.Append(Purpose[index]); sb.Append(" ");
            sb.Append(DateOfFire[index]); sb.Append(" ");
            sb.Append(CauseOfFire[index]); sb.Append(" ");
            sb.Append(City[index]); sb.Append(" ");
            sb.Append(FireArea[index]);
            
            return sb.ToString();
        }

        public override string ToString()
        {
            int length = BuildingName.Length;
            StringBuilder sb = new StringBuilder(1000);
            for (int index = 0; index < length; index++)
            {
                sb.Append(BuildingName[index]); sb.Append(" ");
                sb.Append(BuildingArea[index]); sb.Append(" ");
                sb.Append(Floors[index]); sb.Append(" ");
                sb.Append(Purpose[index]); sb.Append(" ");
                sb.Append(DateOfFire[index]); sb.Append(" ");
                sb.Append(CauseOfFire[index]); sb.Append(" ");
                sb.Append(City[index]); sb.Append(" ");
                sb.Append(FireArea[index]);
            }
            return sb.ToString();
        }

    }

    class ExcelAPIClass : IDisposable
    {
        public Excel.Application ExcelApplication
        {
            get; set;
        }

        public Excel.Workbook ExcelWorkbook
        {
            get; set;
        }

        public Excel.Worksheet ExcelWorksheet
        {
            get; set;
        }

        public Excel.Range ExcelRange
        {
            get; set;
        }

        public int NumberOfRows
        {
            get; set;
        }

        public int NumberOfColumns
        {
            get; set;
        }

        public FireTable TableOfFires
        {
            get; set;
        }

        public ExcelAPIClass() { }

        public ExcelAPIClass(string workbook_path, string sheet_name, string from, string to)
        {
            ExcelApplication = new Excel.Application();
            ExcelWorkbook = ExcelApplication.Workbooks.Open(workbook_path);
            if (ExcelWorkbook != null)
            {
                ExcelWorksheet = ExcelWorkbook.Worksheets[sheet_name];
                ExcelRange = ExcelWorksheet.Range[from +":"+ to];
                
                NumberOfRows = int.Parse(to.Substring(1)) - int.Parse(from.Substring(1)) + 1;
                NumberOfColumns = to[0] - from[0] + 1;
                TableOfFires = new FireTable(NumberOfRows);
                ParseExcelInTable();

                ExcelWorkbook.Close(false);
                ExcelApplication.Quit();
            }
        }

        public void Dispose()
        {
            ReleaseComObject(ExcelWorksheet);
            ReleaseComObject(ExcelWorkbook);
            ReleaseComObject(ExcelApplication);
        }

        public void ParseExcelInTable()
        {
            int r, c, fr = 0;
            var dataArray = (object[,])ExcelRange.Value;
            for (r = 1; r <= NumberOfRows; r++, fr++)
            {
                TableOfFires.BuildingName[fr] = dataArray[r, 1].ToString();
                TableOfFires.BuildingArea[fr] = int.Parse(dataArray[r, 2].ToString().Replace("\"", ""));
                TableOfFires.Floors[fr] = short.Parse(dataArray[r, 3].ToString());
                TableOfFires.Purpose[fr] = dataArray[r, 4].ToString();
                TableOfFires.DateOfFire[fr] = dataArray[r, 5].ToString().Split(' ')[0];
                TableOfFires.CauseOfFire[fr] = dataArray[r, 6].ToString();
                TableOfFires.City[fr] = dataArray[r, 7].ToString();
                TableOfFires.FireArea[fr] = int.Parse(dataArray[r, 8].ToString());

                string cause = TableOfFires.CauseOfFire[fr];
                if (!cause.Contains("по вине") &&
                    (cause.Contains("загор") || cause.Contains("возгор") ||
                     cause.Contains("плам") || cause.Contains("восплам")))
                {
                    TableOfFires.AttributeValues[fr] = new int[] {
                        0, 0, 0,                      //горючий потолок, пол, стена
                        1, 0, 1, 0, 1, 0, 1, 0, 1, 1, //печь [4-13]
                        1, 0, 1, 0, 1, 0, 1, 0,       //дымовая труба [14-21]
                        0, 0, 0, 0,                   //горючие повернхности [22-25]
                        0, 0, 0, 0, 0, 0, 0,          //воздуховоды [26-32]
                        0, 0, 0, 0, 0, 0, 0, 0        //электрика [33-40]
                    };
                    TableOfFires.ShortCause[fr] = "возгорание";
                    TableOfFires.ConsoleColors[fr] = (int) ConsoleColor.Red;
                }
                else if (cause.Contains("поджог") || cause.Contains("неосторожное обращение") ||
                         cause.Contains("искры") || cause.Contains("по вине") ||
                        (cause.Contains("халатн") && cause.Contains("отношен")) ||
                         cause.Contains("петард") || cause.Contains("занесение открытого источника огня") ||
                         cause.Contains("внесенный источник") ||
                         cause.Contains("пиротехник") || cause.Contains("курени"))
                {
                    TableOfFires.AttributeValues[fr] = new int[] {
                        1, 1, 1,                      //горючий потолок, пол, стена
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //печь [4-13]
                        0, 0, 0, 0, 0, 0, 0, 0,       //дымовая труба [14-21]
                        0, 0, 0, 0,                   //горючие повернхности [22-25]
                        0, 0, 0, 0, 0, 0, 0,          //воздуховоды [26-32]
                        0, 0, 0, 0, 0, 0, 0, 0        //электрика [33-40]
                    };
                    TableOfFires.ShortCause[fr] = "поджог";
                    TableOfFires.ConsoleColors[fr] = (int) ConsoleColor.Yellow;
                }
                else if (cause.Contains("электро") || cause.Contains("напряжени")
                    || cause.Contains("замыкание") || cause.Contains("проводк"))
                {
                    TableOfFires.AttributeValues[fr] = new int[] {
                        0, 0, 0,                      //горючий потолок, пол, стена
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //печь [4-13]
                        0, 0, 0, 0, 0, 0, 0, 0,       //дымовая труба [14-21]
                        0, 0, 0, 0,                   //горючие повернхности [22-25]
                        0, 0, 0, 0, 0, 0, 0,          //воздуховоды [26-32]
                        1, 1, 1, 1, 1, 1, 1, 1        //электрика [33-40]
                    };
                    TableOfFires.ShortCause[fr] = "проводка";
                    TableOfFires.ConsoleColors[fr] = (int) ConsoleColor.Cyan;
                }
                else if (cause.Contains("неизвестна"))
                {
                    TableOfFires.AttributeValues[fr] = new int[] {
                        0, 0, 0,                      //горючий потолок, пол, стена
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //печь [4-13]
                        0, 0, 0, 0, 0, 0, 0, 0,       //дымовая труба [14-21]
                        0, 0, 0, 0,                   //горючие повернхности [22-25]
                        0, 0, 0, 0, 0, 0, 0,          //воздуховоды [26-32]
                        0, 0, 0, 0, 0, 0, 0, 0        //электрика [33-40]
                    };
                    TableOfFires.ShortCause[fr] = "неизвестна";
                    TableOfFires.ConsoleColors[fr] = (int) ConsoleColor.White;
                }
                else if (cause.Contains("нарушен") || cause.Contains("по вине") ||
                    cause.Contains("неисправность") || cause.Contains("несоблюдение"))
                {
                    TableOfFires.AttributeValues[fr] = new int[] {
                        0, 0, 0,                      //горючий потолок, пол, стена
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //печь [4-13]
                        0, 0, 0, 0, 0, 0, 0, 0,       //дымовая труба [14-21]
                        0, 0, 0, 0,                   //горючие повернхности [22-25]
                        0, 0, 0, 0, 0, 0, 0,          //воздуховоды [26-32]
                        0, 0, 0, 0, 0, 0, 0, 0        //электрика [33-40]
                    };
                    TableOfFires.ShortCause[fr] = "нарушение";
                    TableOfFires.ConsoleColors[fr] = (int) ConsoleColor.DarkMagenta;
                }
                else if (cause.Contains("в вентиляции"))
                {
                    TableOfFires.AttributeValues[fr] = new int[] {
                        0, 0, 0,                      //горючий потолок, пол, стена
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //печь [4-13]
                        0, 0, 0, 0, 0, 0, 0, 0,       //дымовая труба [14-21]
                        0, 0, 0, 0,                   //горючие повернхности [22-25]
                        1, 1, 1, 1, 1, 1, 1,          //воздуховоды [26-32]
                        0, 0, 0, 0, 0, 0, 0, 0        //электрика [33-40]
                    };
                    TableOfFires.ShortCause[fr] = "вентиляция";
                    TableOfFires.ConsoleColors[fr] = (int) ConsoleColor.DarkGreen;
                }
                else
                {
                    TableOfFires.AttributeValues[fr] = new int[] {
                        0, 0, 0,                      //горючий потолок, пол, стена
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //печь [4-13]
                        0, 0, 0, 0, 0, 0, 0, 0,       //дымовая труба [14-21]
                        0, 0, 0, 0,                   //горючие повернхности [22-25]
                        0, 0, 0, 0, 0, 0, 0,          //воздуховоды [26-32]
                        0, 0, 0, 0, 0, 0, 0, 0        //электрика [33-40]
                    };
                    TableOfFires.ShortCause[fr] = "не горел";
                    TableOfFires.ConsoleColors[fr] = (int) ConsoleColor.Green;
                }
            }
        }

        private void ReleaseComObject(object com)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(com);
                com = null;
            }
            catch
            {
                com = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        public int[][] GetUsefullAttributesValues()
        {
            List<int[]> result = new List<int[]>();
            for (int r = 0; r < TableOfFires.Length; r++)
            {
                string cause = TableOfFires.ShortCause[r];
                if (cause.Equals("возгорание") ||
                    cause.Equals("поджог") ||
                    cause.Equals("проводка") ||
                    cause.Equals("вентиляция"))
                {
                    result.Add(TableOfFires.AttributeValues[r]);
                }
            }

            int[][] table = result.ToArray();
            List<int> list = new List<int>(table.Length);
            result.Clear();

            for (int c = 0; c < table[0].Length; c++)
            {
                for (int r = 0; r < table.Length; r++)
                {
                    list.Add(table[r][c]);
                }
                result.Add(list.ToArray());
                list.Clear();
            }

            return result.ToArray();
        }

        public int[] GetTarget()
        {
            int[][] table = GetUsefullAttributesValues();
            int[] target = new int[table.Length];

            for (int r = 0; r < table.Length; r++)
            {
                for (int c = 0; c < table[0].Length; c++)
                {
                    target[r] |= table[r][c];
                }
            }

            return target;
        }

        public void ShowCategories()
        {
            int r;

            for (r = 0; r < NumberOfRows; r++)
            {
                Console.ForegroundColor = (ConsoleColor) TableOfFires.ConsoleColors[r]; //green
                Console.WriteLine(TableOfFires.ToStringByIndex(r) + " " + $"{TableOfFires.ShortCause[r]}");
            }
        }

        public void ShowCategoriesWithAttributes()
        {
            int r;

            for (r = 0; r < NumberOfRows; r++)
            {
                Console.ForegroundColor = (ConsoleColor)TableOfFires.ConsoleColors[r]; //green
                Console.Write(TableOfFires.ToStringByIndex(r) + " " + $"{TableOfFires.ShortCause[r]} ");
                for (int i = 0; i < TableOfFires.AttributeValues[r].Length; i++)
                {
                    Console.Write($"{TableOfFires.AttributeValues[r][i]} ");
                }
                Console.Write("\n");
            }
        }

    }
}
