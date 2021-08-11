using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using SSMSHelper.Common;

namespace SSMSHelper.ExtensionCommand
{
    public class DocumentFlagExtension
    {
        private EnvDTE80.DTE2 dte;
        private IAsyncServiceProvider _serviceProvider;
        public DocumentFlagExtension(IAsyncServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Extension()
        {
            dte = Package.GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;
            dte.Events.WindowEvents.WindowActivated += OnWindowCreated;
        }

        private void OnWindowCreated(Window gotFocus, Window lostFocus)
        {
       
            // MessageUtils.ShowError($"{gotFocus.LinkedWindowFrame.Caption}");
        }
    }
}
