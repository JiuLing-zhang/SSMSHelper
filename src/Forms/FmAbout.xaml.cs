using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using SSMSHelper.ViewModel;

namespace SSMSHelper.Forms
{
    /// <summary>
    /// FmAbout.xaml 的交互逻辑
    /// </summary>
    public partial class FmAbout
    {
        private readonly string _appVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        private readonly FmAboutViewModel _myModel = new FmAboutViewModel();
        public FmAbout()
        {
            InitializeComponent();
            DataContext = _myModel;
            _myModel.Version = $"版本：{_appVersion}";
        }

        private void DialogWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void GoWebsite_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            OpenUrl(e.Uri.AbsoluteUri);
            e.Handled = true;
        }

        private void BtnCheckUpdate_Click(object sender, RoutedEventArgs e)
        {
            // _myModel.UpdateMessage = "正在查找更新....";
            // Task.Run(() =>
            // {
            //     try
            //     {
            //         var (isNewVersion, version, link) = new CheckForUpdates().Check();
            //         if (isNewVersion == false)
            //         {
            //             _myModel.UpdateMessage = "当前版本已经是最新版本！";
            //             return;
            //         }
            //         _myModel.UpdateMessage = $"发现新版本：{version}";
            //         var notify = new NotifyIcon
            //         {
            //             BalloonTipText = $"发现新版本：{version}{System.Environment.NewLine}点击更新",
            //             Icon = new Icon($"{GlobalArgs.AppPath}Images\\icon.ico"),
            //             Tag = link,
            //             Visible = true
            //         };
            //         notify.BalloonTipClicked += notifyIcon_BalloonTipClicked;
            //         notify.ShowBalloonTip(5000);
            //     }
            //     catch (Exception ex)
            //     {
            //         _myModel.UpdateMessage = $"检查更新失败，{ex.Message}";
            //     }
            // });
        }
        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            var notify = sender as NotifyIcon;
            if (notify == null)
            {
                return;
            }
            OpenUrl(notify.Tag.ToString());
        }

        private void OpenUrl(string url)
        {
            using (Process compiler = new Process())
            {
                compiler.StartInfo.FileName = url;
                compiler.StartInfo.UseShellExecute = true;
                compiler.Start();
            }
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
