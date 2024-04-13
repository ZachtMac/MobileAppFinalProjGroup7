using System;
using Team7Final.ViewModels;
using Team7Final.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Team7Final
{
    public partial class App : Application
    {
        private TaskListViewModel _taskListViewModel;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TaskListPage(_taskListViewModel))
            {
                BarTextColor = Color.White,
                BarBackgroundColor = (Color)App.Current.Resources["primaryColor"]
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
