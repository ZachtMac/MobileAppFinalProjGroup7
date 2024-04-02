using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7Final.Models;
using Team7Final.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Team7Final.Views
{
    public partial class TaskListPage : ContentPage
    {
        public TaskListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            TaskItemDatabase database = await TaskItemDatabase.Instance;
            listView.ItemsSource = await database.GetItemsAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskItemPage
            {
                BindingContext = new TaskItem()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new TaskItemPage
                {
                    BindingContext = e.SelectedItem as TaskItem
                });
            }
        }

        private async void OnToggleSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                TaskItemDatabase database = await TaskItemDatabase.Instance;
                listView.ItemsSource = await database.GetItemsAsync();
                listView.ItemsSource = listView.ItemsSource.Cast<TaskItem>().Where(item => !item.Done).ToList();
            }
            else
            {
                TaskItemDatabase database = await TaskItemDatabase.Instance;
                listView.ItemsSource = await database.GetItemsAsync();
            }
        }
    }
}