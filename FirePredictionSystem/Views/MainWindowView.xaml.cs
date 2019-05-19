using System;
using System.Windows;

using FirePredictionSystem.Additional;

namespace FirePredictionSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        ViewModels.MainWindowViewModel m_MainWindowViewModel;

        public MainWindowView()
        {
            InitializeComponent();

            m_MainWindowViewModel = new ViewModels.MainWindowViewModel();
            DataContext = m_MainWindowViewModel;
            if (m_MainWindowViewModel.CloseAction == null)
            {
                m_MainWindowViewModel.CloseAction = new Action(Close);
            }
        }

        private void Event_MouseLeftButtonDown(object sender, RoutedEventArgs @event)
        {
            DragMove();
        }
    }
}
