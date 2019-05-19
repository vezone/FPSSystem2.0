using System;

using FirePredictionSystem.Commands;
using FirePredictionSystem.Additional;

namespace FirePredictionSystem.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        private string m_Text;

        public string BText
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
                OnPropertyChanged();
            }
        }


        public SettingsViewModel()
        {
            if (BText == null)
            {
                BText = "Settings";
            }

        }

        public DelegateDeclaration.ResizeAction ResizeAction { get; set; }

        public RelayCommand btn_FullScreen_Click
        {
            get
            {
                return new RelayCommand((object obj) =>
                {
                    ResizeAction(1900, 960);
                });
            }
        }

        public RelayCommand btn_SmallScreen_Click
        {
            get
            {
                return new RelayCommand((object obj) =>
                {
                    ResizeAction(960, 640);
                });
            }
        }

    }
}
