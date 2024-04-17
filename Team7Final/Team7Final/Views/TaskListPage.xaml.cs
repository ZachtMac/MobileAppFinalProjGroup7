using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7Final.Models;
using Team7Final.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Team7Final.ViewModels;

namespace Team7Final.Views
{
    public partial class TaskListPage : ContentPage
    {
        private TaskListViewModel _taskListViewModel = new TaskListViewModel();
        public TaskListPage(TaskListViewModel taskListViewModel)
        {
            InitializeComponent();
            BindingContext = _taskListViewModel;
        }

        public void OnItemSelectedChanged(object sender, SelectedItemChangedEventArgs e)
        {
            _taskListViewModel?.ItemSelectedCommand?.Execute(e.SelectedItem);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = _taskListViewModel.LoadItemsAsync();
        }

        public void OnItemCheckChange(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox && BindingContext is TaskListViewModel viewModel)
            {
                viewModel.ItemCheckChangeCommand.Execute(checkBox);
            }
        }
    }
}