using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMSHelper.ViewModel
{
    public class FmDocumentFlagViewModel : ViewModelBase
    {
        public FmDocumentFlagViewModel()
        {
            DocumentFlags = new ObservableCollection<DocumentFlagViewModel>();
        }

        private ObservableCollection<DocumentFlagViewModel> _documentFlags;
        /// <summary>
        /// 已配置的服务器
        /// </summary>
        public ObservableCollection<DocumentFlagViewModel> DocumentFlags
        {
            get => _documentFlags;
            set
            {
                _documentFlags = value;
                OnPropertyChanged();
            }
        }

    }
}
