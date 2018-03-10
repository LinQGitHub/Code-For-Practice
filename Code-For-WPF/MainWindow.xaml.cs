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
            //初始化worker属性，订阅事件
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            //点击事件里检查进程是否忙，如果不忙测异步调用进程
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
            //异步取消请求，需在耗时程序内检测取消请求
            bgWorker.CancelAsync();
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //耗时程序需要在DoWork事件里进行，并检测异步取消请求，禁止在DoWork事件里操作UI
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
            //在ProgressChanged事件里处理，状态上报，进程百分比和用户状态
            BarProcess.Value = e.ProgressPercentage;
            LbProcess.Content = e.UserState.ToString() + " " + e.ProgressPercentage.ToString() + " %";
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //在completed事件里，处理异步取消、处理结束错误、处理正常结束逻辑
            if (e.Cancelled)
            {                
                Log(1, "Process Cancelled !");
                BarProcess.Value = 0;
                LbProcess.Content = "Processing 0 %";
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //在窗口关闭逻辑内，取消事件订阅
            bgWorker.DoWork -= BgWorker_DoWork;
            bgWorker.ProgressChanged -= BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted -= BgWorker_RunWorkerCompleted;
        }
    }
}
