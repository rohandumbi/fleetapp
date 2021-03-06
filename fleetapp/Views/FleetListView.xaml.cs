﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fleetapp.Views
{
    /// <summary>
    /// Interaction logic for FleetListView.xaml
    /// </summary>
    public partial class FleetListView : UserControl
    {
        public FleetListView()
        {
            InitializeComponent();
        }

        private void FleetFile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                FleetFile.Text = openFileDialog.FileName;
        }
    }
}
