﻿namespace DlaViewer
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel mainViewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this.mainViewModel;
            PagePlotView.SizeChanged += PagePlotView_SizeChanged;
        }

        private void PagePlotView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Debug.Print(e.PreviousSize.ToString() + "->" + e.NewSize);
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            var data = e.Data as DataObject;
            if (data != null && data.ContainsFileDropList())
            {
                foreach (var file in data.GetFileDropList())
                {
                    this.Title = Path.GetFileName(file);
                    this.mainViewModel.OpenDocument(file);
                    break; // take only one file
                }
            }
        }

        private void buttonPrev_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.CurrentPageNumber--;
            e.Handled = true;
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.CurrentPageNumber++;
            e.Handled = true;
        }
    }
}
