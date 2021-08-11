using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SSMSHelper.Common;
using SSMSHelper.ViewModel;

namespace SSMSHelper.Forms
{
    /// <summary>
    /// FmSetting.xaml 的交互逻辑
    /// </summary>
    public partial class FmSetting
    {
        private readonly IniHelper _iniHelper = new IniHelper(GlobalArgs.AppConfigPath);
        private readonly FmDocumentFlagViewModel _myModel = new FmDocumentFlagViewModel();
        public FmSetting()
        {
            InitializeComponent();
            LoadingAppConfig();

            ListBoxSearchFolders.ItemsSource = _myModel.DocumentFlags;
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

            string setting = _iniHelper.IniReadValue("Setting", "DocumentFlags");
            if (string.IsNullOrEmpty(setting))
            {
                return;
            }


            var configRows = setting.Split(';').ToList();
            foreach (var row in configRows)
            {
                if (string.IsNullOrEmpty(row))
                {
                    continue;
                }
                var rowSetting = row.Split(',');
                if (rowSetting.Length != 3)
                {
                    MessageUtils.ShowError("读取配置文件失败，请重新配置背景");
                    return;
                }
                _myModel.DocumentFlags.Add(new DocumentFlagViewModel() { Server = rowSetting[0], FlagArea = (FlagAreaEnum)Enum.Parse(typeof(FlagAreaEnum), rowSetting[1]), BackColor = rowSetting[2] });
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var server = TxtServer.Text.Trim();
            if (string.IsNullOrEmpty(server))
            {
                MessageUtils.ShowError("服务器地址不能为空");
                return;
            }

            if (_myModel.DocumentFlags.Any(x => x.Server == server))
            {
                MessageUtils.ShowError("服务器地址已存在");
                return;
            }

            var color = TxtColor.Text.Trim();
            if (string.IsNullOrEmpty(color))
            {
                MessageUtils.ShowError("颜色不能为空");
                return;
            }

            if (!Regex.IsMatch(color, "^#[0-9a-fA-F]{6}$"))
            {
                MessageUtils.ShowError("颜色格式不正确");
                return;
            }

            FlagAreaEnum flagArea;
            if (RdoLabel.IsChecked == true)
            {
                flagArea = FlagAreaEnum.Label;
            }
            else if (RdoBackground.IsChecked == true)
            {
                flagArea = FlagAreaEnum.Background;
            }
            else
            {
                MessageUtils.ShowError("请选择标记区域");
                return;
            }
            _myModel.DocumentFlags.Add(new DocumentFlagViewModel() { Server = server, BackColor = color, FlagArea = flagArea });
            WriteConfig();
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)?.Tag;
            if (tag == null)
            {
                return;
            }
            string server = tag.ToString();
            for (int i = 0; i < _myModel.DocumentFlags.Count; i++)
            {
                if (_myModel.DocumentFlags[i].Server == server)
                {
                    _myModel.DocumentFlags.RemoveAt(i);
                    break;
                }
            }
            WriteConfig();
        }

        private void WriteConfig()
        {
            string info = "";
            foreach (var item in _myModel.DocumentFlags)
            {
                info = $"{info}{item.Server},{item.FlagArea},{item.BackColor};";
            }

            _iniHelper.IniWriteValue("Setting", "DocumentFlags", info);
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
