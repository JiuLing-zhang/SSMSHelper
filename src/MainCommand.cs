using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using SSMSHelper.ExtensionCommand;
using SSMSHelper.Forms;
using Task = System.Threading.Tasks.Task;

namespace SSMSHelper
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class MainCommand
    {

        public const int DocumentSignId = 0x0100;
        public const int AboutId = 0x0200;
        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("312c4a4e-e5bd-4d10-a34c-032312f73628");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private MainCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            //文档背景色设置
            var documentSignCommandId = new CommandID(CommandSet, DocumentSignId);
            var documentSignMenuItem = new MenuCommand(this.DocumentSignDo, documentSignCommandId);
            commandService.AddCommand(documentSignMenuItem);

            //关于
            var aboutCommandId = new CommandID(CommandSet, AboutId);
            var aboutMenuItem = new MenuCommand(this.AboutDo, aboutCommandId);
            commandService.AddCommand(aboutMenuItem);

            //扩展
            new DocumentFlagExtension(ServiceProvider).Extension();


        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static MainCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in MainCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new MainCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void DocumentSignDo(object sender, EventArgs e)
        {
            // ThreadHelper.ThrowIfNotOnUIThread();
            // string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            // string title = "Bingo!!!";
            //
            // // Show a message box to prove we were here
            // VsShellUtilities.ShowMessageBox(
            //     this.package,
            //     message,
            //     title,
            //     OLEMSGICON.OLEMSGICON_INFO,
            //     OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //     OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            //
            // var a = new FmSetting();
            // a.ShowModal();

            var fmSetting = new FmSetting();
            fmSetting.ShowModal();
        }

        private void AboutDo(object sender, EventArgs e)
        {
            var fmAbout = new FmAbout();
            fmAbout.ShowModal();
        }
    }
}
