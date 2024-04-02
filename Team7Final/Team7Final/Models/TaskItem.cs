using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace Team7Final.Models
{
    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public bool Done { get; set; }
    }
}
