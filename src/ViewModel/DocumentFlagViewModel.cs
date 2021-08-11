using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMSHelper.ViewModel
{
    public class DocumentFlagViewModel : ViewModelBase
    {
        private string _server;
        public string Server
        {
            get => _server;
            set
            {
                _server = value;
                OnPropertyChanged();
            }
        }
        
        private FlagAreaEnum _flagArea;
        public FlagAreaEnum FlagArea
        {
            get => _flagArea;
            set
            {
                _flagArea = value;
                OnPropertyChanged();
            }
        }

        public string FlagAreaString
        {
            get
            {
                if (_flagArea == FlagAreaEnum.Label)
                {
                    return "标签";
                }

                if (_flagArea == FlagAreaEnum.Background)
                {
                    return "背景";
                }

                throw new FormatException("标记区域格式错误");
            }
        }

        private string _backColor;
        public string BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                OnPropertyChanged();
            }
        }
    }
    public enum FlagAreaEnum
    {
        Label,
        Background
    }

}
