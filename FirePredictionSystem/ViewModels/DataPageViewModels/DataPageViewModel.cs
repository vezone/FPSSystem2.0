using System.Windows.Controls;

using FirePredictionSystem.Commands;

namespace FirePredictionSystem.ViewModels.DataPageViewModels
{
    class DataPageViewModel : ViewModelBase
    {
        //
        private ContentControl m_ContentPage;

        private ContentControl m_DataView;
        private ContentControl m_DataModifyView;
        private ContentControl m_DataLoadView;
        private ContentControl m_DataGenerationView;

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

        public DataPageViewModel()
        {
            if (ContentPage == null)
            {
                m_DataView = new Views.DataPageViews.DataView();
                ContentPage = m_DataView;
            }
        }

        public RelayCommand btn_DataView_Click
        {
            get
            {
                return new RelayCommand((object obj)=>
                {
                    if (m_DataView == null)
                    {
                        m_DataView = new Views.DataPageViews.DataView();
                    }
                    ContentPage = m_DataView;
                });
            }
        }

        public RelayCommand btn_DataLoadView_Click
        {
            get
            {
                return new RelayCommand((object obj) =>
                {
                    if (m_DataLoadView == null)
                    {
                        m_DataLoadView = new Views.DataPageViews.DataLoadView();
                    }
                    ContentPage = m_DataLoadView;
                });
            }
        }

        public RelayCommand btn_DataModifyView_Click
        {
            get
            {
                return new RelayCommand((object obj) =>
                {
                    if (m_DataModifyView == null)
                    {
                        m_DataModifyView = new Views.DataPageViews.DataModifyView();
                    }
                    ContentPage = m_DataModifyView;
                });
            }
        }

        public RelayCommand btn_DataGenerationView_Click
        {
            get
            {
                return new RelayCommand((object obj) =>
                {
                    if (m_DataGenerationView == null)
                    {
                        m_DataGenerationView = new Views.DataPageViews.DataGenerationView();
                    }
                    ContentPage = m_DataGenerationView;
                });
            }
        }
        
    }
}
