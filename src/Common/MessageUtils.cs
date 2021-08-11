using System.Windows;

namespace SSMSHelper.Common
{
    public class MessageUtils
    {
        private const string Title = "SSMS辅助工具";
        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, Title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
