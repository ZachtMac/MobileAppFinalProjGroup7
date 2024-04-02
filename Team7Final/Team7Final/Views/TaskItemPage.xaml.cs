using System;
using Team7Final.Data;
using Team7Final.Models;
using Xamarin.Forms;

namespace Team7Final.Views
{
    public partial class TaskItemPage : ContentPage
    {
        public TaskItemPage()
        {
            InitializeComponent();
        }

        async void SaveClicked(object sender, EventArgs e)
        {
            var TaskItem = (TaskItem)BindingContext;
            TaskItemDatabase database = await TaskItemDatabase.Instance;
            await database.SaveItemAsync(TaskItem);
            await Navigation.PopAsync();
        }

        async void DeleteClicked(object sender, EventArgs e)
        {
            var TaskItem = (TaskItem)BindingContext;
            TaskItemDatabase database = await TaskItemDatabase.Instance;
            await database.DeleteItemAsync(TaskItem);
            await Navigation.PopAsync();
        }

        async void CancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}