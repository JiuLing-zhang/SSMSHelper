using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Newtonsoft.Json;
using SSMSHelper.Common;

namespace SSMSHelper.Forms
{
    /// <summary>
    /// FmSetting.xaml 的交互逻辑
    /// </summary>
    public partial class FmSetting
    {
        private readonly IniHelper _iniHelper = new IniHelper(GlobalArgs.AppConfigPath);
        public FmSetting()
        {
            InitializeComponent();
            LoadingAppConfig();
        }

        private void DialogWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void LoadingAppConfig()
        {
            if (!File.Exists(GlobalArgs.AppConfigPath))
            {
                return;
            }
            var s = _iniHelper.Read("Setting", "DocumentSign");
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var server = TxtServer.Text.Trim();
            if (string.IsNullOrEmpty(server))
            {
                //TODO tip
                return;
            }

            var color = TxtColor.Text.Trim();
            if (string.IsNullOrEmpty(color))
            {
                //TODO tip
                return;
            }

            if (!Regex.IsMatch(color, "^#[0-9a-fA-F]{6}$"))
            {
                //TODO tip
                return;
            }

            string a = "";
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
