using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Code_For_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker bgWorker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
            InitializeBgWorker();
            InitializeBarProcess();
        }

        private void InitializeBarProcess()
        {
            BarProcess.Maximum = 1000;
        }

        private void InitializeBgWorker()
        {
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (bgWorker.IsBusy)
            {
                Log(1, "Busy !");
            }
            else
            {
                bgWorker.RunWorkerAsync();
            }
        }
        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            bgWorker.CancelAsync();
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (bgWorker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    bgWorker.ReportProgress(i + 1, "Processing");
                    System.Threading.Thread.Sleep(10);
                }                
            }
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BarProcess.Value = e.ProgressPercentage;
            LbProcess.Content = e.UserState.ToString() + " " + e.ProgressPercentage.ToString() + " %";
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Log(1, "Process Cancelled !");
            }
            else
            {
                if (e.Error != null)
                {
                    MessageBox.Show(e.Error.ToString());
                }
                else
                {
                    Log(1, "Process Finished !");
                }
            }

        }

        private void Log(int _re_print, string _log)
        {
            switch (_re_print)
            {
                case 0:
                    TbLog.Clear();
                    TbLog.Text = DateTime.Now.ToString("yy-mm-dd hh-mm-ss") + "-> " + _log + "\n";
                    break;
                case 1:
                    TbLog.AppendText(DateTime.Now.ToString("yy-mm-dd hh-mm-ss") + "-> " + _log + "\n");
                    break;
                default:
                    TbLog.AppendText(DateTime.Now.ToString("yy-mm-dd hh-mm-ss") + "-> " + _log + "\n");
                    break;
            }
        }

    }
}
