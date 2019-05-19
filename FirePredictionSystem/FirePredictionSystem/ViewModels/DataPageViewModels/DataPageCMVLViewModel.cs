using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

using FirePredictionSystem.Additional;
using FirePredictionSystem.Commands;
using FirePredictionSystem.Models;
using FirePredictionSystem.Additional.C45;

namespace FirePredictionSystem.ViewModels.DataPageViewModels
{
    class DataPageCMVLViewModel : ViewModelBase
    {
        private string m_txt_NumGeneratedData;
        private string tb_MainTextBlock = "No table is created or loaded!";
        //TODO: delete this
        private string tb_PathToDownload;
        //TODO: delete this
        private List<Attribute> is_GridSource;


        //property
        public string NumGeneratedData
        {
            get
            {
                return m_txt_NumGeneratedData;
            }
            set
            {
                m_txt_NumGeneratedData = value;
                OnPropertyChanged();
            }
        }

        //TODO: delete this
        public string MainTextBlock
        {
            get
            {
                return tb_MainTextBlock;
            }
            set
            {
                tb_MainTextBlock = value;
                OnPropertyChanged();
            }
        }

        //TODO: delete this
        public string PathToDownload
        {
            get
            {
                return tb_PathToDownload;
            }
            set
            {
                tb_PathToDownload = value;
                OnPropertyChanged();
            }
        }

        //TODO: delete this
        public List<Attribute> GridSource
        {
            get
            {
                return is_GridSource;
            }
            set
            {
                is_GridSource = value;
                OnPropertyChanged();
            }
        }

        public DataPageCMVLViewModel()
        {
            //.ctor
        }

        //TODO: delete this
        public RelayCommand btn_LoadTable_Click
        {
            get
            {
                return new RelayCommand(
                    (object obj) => 
                {
                    if (System.IO.File.Exists(PathToDownload))
                    {
                        //Loading table
                        string[][] table = IOClass.ReadTable(PathToDownload);
                        GridSource = new List<Attribute>();

                        int rLength = table.Length;
                        for (int r = 0; r < rLength; r++)
                        {
                            GridSource.Add(new Attribute(table[r]));
                        }
                        
                        MessageBox.Show("File is loaded!", "Success", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Invalid file path, use only valid file path!", "Path error", MessageBoxButton.OK);
                    }
                });
            }
        }

        //TODO: delete this
        public RelayCommand btn_FileSearch_Click
        {
            get
            {
                return new RelayCommand(
                    (object obj) => 
                {
                    //
                    Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        PathToDownload = openFileDialog.FileName;
                    }
                });
            }
        }

        public RelayCommand btn_Generate_Click
        {
            get
            {
                return new RelayCommand(
                    (object obj) =>
                {
                    //Generating data
                    if (NumGeneratedData != null)
                    {
                        if (NumGeneratedData.Length > 0)
                        {
                            int numGenData;
                            bool isNumGenDataValid = int.TryParse(NumGeneratedData, out numGenData);
                            if (isNumGenDataValid)
                            {
                                if (numGenData > 0)
                                {
                                    string generatedData = IO.GenerateInput2(numGenData);
                                    IO.WriteFile(@"Data\data" + numGenData + ".txt", 
                                        generatedData);
                                    MessageBox.Show("Genrated data is loaded to the file!", 
                                        "Success",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                                }
                                else
                                {
                                    //
                                }
                            }
                            else
                            {
                                //
                            }
                        }
                    }
                    else
                    {

                    }
                });
            }
        }


    }
}
