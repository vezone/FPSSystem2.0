namespace FirePredictionSystem.ViewModels
{
    using NLog;
    using System.Windows;
    using System.Collections;
    using System.Collections.Generic;

    using FirePredictionSystem.Models;
    using FirePredictionSystem.Commands;
    using FirePredictionSystem.Additional;
    using FirePredictionSystem.Additional.C45;

    class MainViewModel : ViewModelBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private string m_ImagePath;
        private string m_FilePath;
        private List<Attribute> m_Data;
        private string[][] m_ReadedData;
        private Tree m_Tree;
        private string m_TestResult;
        private List<int> m_TreeLayersAnswers;
        private ArrayList m_TreeLayersLeafs;
        private List<TreeInfo> m_TreeLayersGridList;

        //additional
        private RelayCommand m_BrowseFileCommand;
        private RelayCommand m_RunCommand;
        private RelayCommand m_UpdateCommand;
        private RelayCommand m_TestCommand;

        public string ImagePath
        {
            get
            {
                return m_ImagePath;
            }
            set
            {
                m_ImagePath = value;
                OnPropertyChanged();
            }
        }

        public string FilePath
        {
            get
            {
                return m_FilePath;
            }
            set
            {
                m_FilePath = value;
                OnPropertyChanged();
            }
        }

        public List<Attribute> Data
        {
            get
            {
                return m_Data;
            }
            set
            {
                m_Data = value;
                OnPropertyChanged();
            }
        }

        public string TestResult
        {
            get
            {
                return m_TestResult;
            }
            set
            {
                m_TestResult = value;
                OnPropertyChanged();
            }
        }

        public List<int> TreeLayersAnswers
        {
            get
            {
                return m_TreeLayersAnswers;
            }
            set
            {
                m_TreeLayersAnswers = value;
                OnPropertyChanged();
            }
        }

        public ArrayList TreeLayersLeafs
        {
            get
            {
                return m_TreeLayersLeafs;
            }
            set
            {
                m_TreeLayersLeafs = value;
                OnPropertyChanged();
            }
        }

        public List<TreeInfo> TreeLayersGridList
        {
            get
            {
                return m_TreeLayersGridList;
            }
            set
            {
                m_TreeLayersGridList = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
        }
        
        public RelayCommand btn_BrowseFile_Click
        {
            get
            {
                return m_BrowseFileCommand == null ? m_BrowseFileCommand = new RelayCommand(
                    (object obj) =>
                    {
                        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                        if (openFileDialog.ShowDialog() == true)
                        {
                            FilePath = openFileDialog.FileName;
                            if (FilePath != null)
                            {
                                if (System.IO.File.Exists(FilePath))
                                {
                                    //Loading table
                                    m_ReadedData = IOClass.ReadTable(FilePath);
                                    MessageBox.Show("File is loaded!", "Success",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Invalid file path, use only valid file path!",
                                        "Path error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Не выбран путь для загрузки файла!",
                                        "FilePathError",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                            }
                        }
                    }) : m_BrowseFileCommand;
            }
        }

        public RelayCommand btn_Run_Click
        {
            get
            {
                return m_RunCommand == null ? m_RunCommand = new RelayCommand(
                    (object obj) =>
                    {
                        //С4.5 stuff there
                        if (m_ReadedData != null)
                        {
                            System.Console.WriteLine($"Кол-во строк: {m_ReadedData.Length}");
                            var input = new Input(m_ReadedData);
                            m_Tree = new Tree();
                            m_Tree.Build(input);
                            m_Tree.CountLeafInTree();

                            TreeInfo info = new TreeInfo();
                            info.LeafsOnLayer.AddRange(m_Tree.LayersAnswers);
                            info.AnswersOnLayer.AddRange(m_Tree.LayersLeaf);
                            List<int> leafIndecies = new List<int>();
                            for (int i = 0; i < info.AnswersOnLayer.Count; i++)
                            {
                                leafIndecies.Add(i);
                            }
                            info.LayerIndex.AddRange(leafIndecies);

                            System.Console.WriteLine("Id Leafs Answers");
                            System.Console.WriteLine(info.ToString());
                            System.Console.WriteLine($"Layers count: {info.LayersCount}");
                            System.Console.WriteLine($"Answers count: {info.AnswersCount}");
                            System.Console.WriteLine($"Leafs count: {info.LeafCount}");

                            var graph = new TreeGraph(m_Tree);
                            graph.BuildGraph();
                            graph.DrawGraph("graph.dot");
                            
                            string output_name = "C45_OutputImage_" + (m_ReadedData.Length-1) + ".png";
                            System.Diagnostics.Process.Start(@"get_image_1.bat", output_name);
                            ImagePath = System.Environment.CurrentDirectory + "\\Images\\" + output_name;
                        }
                        else
                        {
                            MessageBox.Show("Не введены данные для алгоритма!", "DataNullError", 
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }) : m_RunCommand;
            }
        }

        public RelayCommand btn_Update_Click
        {
            get
            {
                return m_UpdateCommand == null ? m_UpdateCommand = new RelayCommand(
                    (object obj) =>
                    {
                        ImagePath = ImagePath;
                    }) : m_UpdateCommand;
            }
        }

        public RelayCommand btn_Test_Click
        {
            get
            {
                return m_TestCommand == null ? m_TestCommand = new RelayCommand(
                    (object obj) =>
                    {
                        //Testing
                        string testPath;
                        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                        if (openFileDialog.ShowDialog() == true)
                        {
                            testPath = openFileDialog.FileName;
                            if (testPath != null)
                            {
                                if (System.IO.File.Exists(testPath))
                                {
                                    //Loading check data
                                    string[][] check = IOClass.ReadTable(testPath);
                                    TestResult = m_Tree.Check(check);
                                }
                                else
                                {
                                    MessageBox.Show("Invalid test path, use only valid test path!",
                                        "Path error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("testPath == null",
                                        "TestFilePathError",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                            }
                        }
                        
                    }) : m_TestCommand;
            }
        }

    }
}
