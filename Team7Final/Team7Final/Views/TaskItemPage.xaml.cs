using System;
using Team7Final.Data;
using Team7Final.Models;
using Team7Final.ViewModels;
using Xamarin.Forms;

namespace Team7Final.Views
{
    public partial class TaskItemPage : ContentPage
    {
        public TaskItemPage()
        {
            InitializeComponent();
            BindingContext = new TaskItemViewModel();
        }

        
    }
}