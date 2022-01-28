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
using System.ComponentModel;
using System.Threading;

namespace Lab_3_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker _worker;
        public MainWindow()
        {
            InitializeComponent();

            _worker = new BackgroundWorker();
            _worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            _worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanger);
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            
        }

         void worker_DoWork(object sender, DoWorkEventArgs e)
        {
          for(int i=0; i<=100; ++i)
            {
                Thread.Sleep(50);

                if (_worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
               
                
                _worker.ReportProgress(i);
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Lbl.Content = "Успешное подключение к бд";
            this.Title = "Completed";
            if (e.Cancelled)
                Lbl.Content = "Подключение было отменено";

            Connect.IsEnabled = true;
        }

        void worker_ProgressChanger(object sender, ProgressChangedEventArgs e)
        {
            PGBar.Value = e.ProgressPercentage;
            Lbl.Content = "Подключение к бд";
            Connect.IsEnabled = false;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            _worker.RunWorkerAsync();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _worker.CancelAsync();
        }

        private void Erase_Click(object sender, RoutedEventArgs e)
        {
            TB.Text = String.Empty;
        }
    }
}
