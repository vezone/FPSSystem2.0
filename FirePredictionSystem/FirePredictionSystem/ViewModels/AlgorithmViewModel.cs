using System;

namespace FirePredictionSystem.ViewModels
{
    class AlgorithmViewModel : ViewModelBase
    {

        private string m_ImagePath;

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

        public AlgorithmViewModel()
        {
            //set it like this for now
            ImagePath = 
                "E:/General/Programming/C_Sharp/WPF/FirePredictionSystem/FirePredictionSystem/bin/Debug/Images/C45.png";
        }



    }
}
