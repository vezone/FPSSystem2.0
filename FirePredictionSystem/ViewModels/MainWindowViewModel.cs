using System;
using System.Windows.Controls;
using FirePredictionSystem.Commands;

namespace FirePredictionSystem.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private ContentControl m_ContentPage;

        private ContentControl m_DataPage;
        private ContentControl m_AlgorithmPage;
        private ContentControl m_SettingsPage;

        public ContentControl ContentPage
        {
            get
            {
                return m_ContentPage;
            }
            set
            {
                m_ContentPage = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            if (ContentPage == null)
            {
                if (m_DataPage == null)
                {
                    m_DataPage = new Views.DataPageView();
                }
                ContentPage = m_DataPage;
            }

        }

        public Action CloseAction { get; set; }

        public RelayCommand btn_DataPageSelect_Click
        {
            get
            {
                return new RelayCommand((object obj) =>
                {
                    ContentPage = m_DataPage;
                });
            }
        }

        public RelayCommand btn_AlgorithmPageSelect_Click
        {
            get
            {
                return new RelayCommand((object obj) =>
                {
                    if (m_AlgorithmPage == null)
                    {
                        m_AlgorithmPage = new Views.AlgorithmPageView();
                    }
                    ContentPage = m_AlgorithmPage;
                });
            }
        }

        public RelayCommand btn_SettingsPageSelect_Click
        {
            get
            {
                return new RelayCommand((object obj) =>
                {
                    if (m_SettingsPage == null)
                    {
                        m_SettingsPage = new Views.SettingsPageView();
                    }
                    ContentPage = m_SettingsPage;
                });
            }
        }
        
        public RelayCommand btn_Exit_Click
        {
            get
            {
                return new RelayCommand((object obj) =>
                {
                    System.Windows.MessageBoxResult result = 
                        System.Windows.MessageBox.Show("Do you really want to close this app?", "Closing a window", System.Windows.MessageBoxButton.YesNo);
                    if (result == System.Windows.MessageBoxResult.Yes)
                    {
                        CloseAction();
                    }
                });
            }
        }



    }
}
