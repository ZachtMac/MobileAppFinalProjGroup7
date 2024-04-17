using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Team7Final.Data;
using Team7Final.Models;
using Team7Final.Views;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Team7Final.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {

        private ObservableCollection<TaskItem> _taskItems;
        public ObservableCollection<TaskItem> TaskItems
        {
            get => _taskItems;
            set
            {
                _taskItems = value;
                OnPropertyChanged();
                //_ = LoadItemsAsync();
            }
        }

        private bool _isToggleSwitchOn = false;
        public bool IsToggleSwitchOn
        {
            get => _isToggleSwitchOn;
            set
            {
                if (SetProperty(ref _isToggleSwitchOn, value))
                {
                    _ = ToggleSwitch(value);
                }
            }
        }



        public ICommand AddTaskCommand { get; }
        public ICommand ItemSelectedCommand { get; }
        public ICommand CheckBoxChangedCommand { get; }
        public ICommand ItemCheckChangeCommand { get; }

        public TaskListViewModel()
        {
            var capturedSender = this;
            TaskItems = new ObservableCollection<TaskItem>();
            AddTaskCommand = new Command(async () => await AddTask());
            ItemSelectedCommand = new Command<TaskItem>(async (taskItem) => await SelectTask(taskItem));
            CheckBoxChangedCommand = new Command<CheckedChangedEventArgs>(async (args) => await CheckBoxChanged(capturedSender, args));
            ItemCheckChangeCommand = new Command<CheckBox>(async (checkBox) =>
            {
                await ItemCheckChanged(capturedSender, checkBox);
            });
            _ = LoadItemsAsync();
        }

        public async Task LoadItemsAsync()
        {
            TaskItemDatabase database = await TaskItemDatabase.Instance;
            var items = await database.GetItemsAsync();
            TaskItems.Clear();
            foreach (var item in items)
            {
                TaskItems.Add(item);
            }
        }

        private async Task AddTask()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TaskItemPage
            {
                BindingContext = new TaskItemViewModel()
            });
        }

        private async Task SelectTask(TaskItem taskItem)
        {
            await LoadItemsAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new TaskItemPage
            {
                BindingContext = new TaskItemViewModel(taskItem)
            });
            await LoadItemsAsync();
        }

        private async Task ToggleSwitch(bool value)
        {
            TaskItemDatabase database = await TaskItemDatabase.Instance;
            var items = await database.GetItemsAsync();
            TaskItems.Clear();
            if (value)
            {
                foreach (var item in items.Where(item => !item.Done))
                {
                    TaskItems.Add(item);
                }
            }
            else
            {
                foreach (var item in items)
                {
                    TaskItems.Add(item);
                }
            }
        }

        private async Task CheckBoxChanged(object sender, CheckedChangedEventArgs args)
        {
            var checkBox = (CheckBox)sender;
            var taskItem = (TaskItem)checkBox.BindingContext;

            if (taskItem != null)
            {
                TaskItemDatabase database = await TaskItemDatabase.Instance;
                await database.SaveItemAsync(taskItem);
            }
        }

        private async Task ItemCheckChanged(object sender, CheckBox checkBox)
        {
            //var checkBox = (CheckBox)sender;
            /*var taskItem = (TaskItem)checkBox.BindingContext;
            taskItem.Done = checkBox.IsChecked;

            if (taskItem != null)
            {
                TaskItemDatabase database = await TaskItemDatabase.Instance;
                await database.SaveItemAsync(taskItem);
            }*/
        }

        //protected async virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

    }
}
