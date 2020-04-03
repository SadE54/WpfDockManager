﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDockManagerDemo.ViewModel
{
    public class DemoTwoViewModel : WpfDockManagerDemo.DockManager.IDocument
    {
        public DemoTwoViewModel()
        {
            Title = "Demo Two View Model";
        }

        public long ID { get; set; }

        public string Title { get; set; }

        public event EventHandler Close;

        public void Closing()
        {

        }

        public bool CanClose
        {
            get
            {
                return true;
            }
        }

        public bool CanFloat
        {
            get
            {
                return true;
            }
        }
    }
}
