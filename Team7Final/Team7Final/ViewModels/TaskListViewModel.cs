using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Team7Final.Data;
using Team7Final.Models;
using Team7Final.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Team7Final.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        private ObservableCollection<Grouping<DateTime, TaskItem>> _groupedTaskItems;
        public ObservableCollection<Grouping<DateTime, TaskItem>> GroupedTaskItems
        {
            get => _groupedTaskItems;
            set
            {
                _groupedTaskItems = value;
                OnPropertyChanged();
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
            GroupedTaskItems = new ObservableCollection<Grouping<DateTime, TaskItem>>();
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
            var groupedItems = items
                .GroupBy(x => x.Date.Date)
                .Select(group => new Grouping<DateTime, TaskItem>(group.Key, group))
                .OrderBy(g => g.Key);

            GroupedTaskItems.Clear();
            foreach (var itemGroup in groupedItems)
            {
                GroupedTaskItems.Add(itemGroup);
            }

            SetItemColors();
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
            var groupedItems = items
                .GroupBy(x => x.Date.Date)
                .Select(group => new Grouping<DateTime, TaskItem>(group.Key, group))
                .OrderBy(g => g.Key);

            GroupedTaskItems.Clear();
            foreach (var itemGroup in groupedItems)
            {
                GroupedTaskItems.Add(itemGroup);
            }

            SetItemColors();
        }

        private void SetItemColors()
        {
            foreach (var itemGroup in GroupedTaskItems)
            {
                foreach (var taskItem in itemGroup)
                {
                    if (taskItem.Done)
                    {
                        taskItem.TextColor = Color.Green;
                    }
                    else if (taskItem.Date <= DateTime.Today)
                    {
                        taskItem.TextColor = Color.Red;
                    }
                    else
                    {
                        taskItem.TextColor = Color.Black;
                    }
                }
            }
        }

        private async Task CheckBoxChanged(object sender, CheckedChangedEventArgs args)
        {
            var checkBox = (CheckBox)sender;
            var taskItem = (TaskItem)checkBox.BindingContext;

            if (taskItem != null)
                {
                    taskItem.Done = checkBox.IsChecked;
                    taskItem.TextColor = taskItem.Done ? Color.Green : Color.Black;
                    TaskItemDatabase database = await TaskItemDatabase.Instance;
                    await database.SaveItemAsync(taskItem);
            }
        }

        private async Task ItemCheckChanged(object sender, CheckBox checkBox)
        {
            var taskItem = (TaskItem)checkBox.BindingContext;
            if (taskItem != null)
            {
                taskItem.Done = checkBox.IsChecked;
                TaskItemDatabase database = await TaskItemDatabase.Instance;
                if (database == null) return;
                await database.SaveItemAsync(taskItem);
            }
        }
    }
}
