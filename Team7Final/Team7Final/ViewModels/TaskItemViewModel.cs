using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Team7Final.Data;
using Team7Final.Models;
using Xamarin.Forms;

namespace Team7Final.ViewModels
{
    public class TaskItemViewModel : BaseViewModel
    {
        private TaskItem _taskItem;
        public TaskItem TaskItem
        {
            get => _taskItem;
            set { SetProperty(ref _taskItem, value); }
        }

        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
        public Command CancelCommand { get; }

        public TaskItemViewModel()
        {
            TaskItem = new TaskItem();
            SaveCommand = new Command(async () => await SaveTaskItemAsync());
            DeleteCommand = new Command(async () => await DeleteTaskItemAsync());
            CancelCommand = new Command(async () => await CancelTaskItemAsync());
        }

        public TaskItemViewModel(TaskItem taskItem)
        {
            TaskItem = taskItem;
            SaveCommand = new Command(async () => await SaveTaskItemAsync());
            DeleteCommand = new Command(async () => await DeleteTaskItemAsync());
            CancelCommand = new Command(async () => await CancelTaskItemAsync());
        }

        private async Task SaveTaskItemAsync()
        {
            if (TaskItem.Name == null || TaskItem.Name.Trim() == string.Empty)
            {
                await Application.Current.MainPage.DisplayAlert("Notice", "Please name your task", "Ok");
                return;
            }

            TaskItemDatabase database = await TaskItemDatabase.Instance;
            await database.SaveItemAsync(TaskItem);

            await Application.Current.MainPage.Navigation.PopAsync();

        }

        private async Task DeleteTaskItemAsync()
        {
            TaskItemDatabase database = await TaskItemDatabase.Instance;
            await database.DeleteItemAsync(TaskItem);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task CancelTaskItemAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
