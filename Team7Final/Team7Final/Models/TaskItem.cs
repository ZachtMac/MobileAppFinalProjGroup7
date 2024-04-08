using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using Team7Final.Data;
using Team7Final.Models;
using Xamarin.Forms;
using System.ComponentModel;

namespace Team7Final.Models
{

    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string notes;
        public string Notes
        {
            get => notes;
            set
            {
                notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }


        private DateTime date;
        public DateTime Date 
        {
            get => date;
            set 
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            } 
        }
        
        private bool done;
        public bool Done 
        {
            get => done;
            set 
            { 
                done = value;
                OnPropertyChanged(nameof(Done));
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected async virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            TaskItemDatabase database = await TaskItemDatabase.Instance;
            await database.SaveItemAsync(this);
        }
    }
    
}