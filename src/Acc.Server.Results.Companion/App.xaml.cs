using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Acc.Server.Results.Companion.Core.Services;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Installer;
using NLog;
using NuGet.Versioning;
using Squirrel;
using Squirrel.Sources;
using Syncfusion.SfSkinManager;

// Do not remove dimmed usings without first switching to release mode

namespace Acc.Server.Results.Companion
{
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs eventArgs)
        {
            LogManager.Shutdown();
            base.OnExit(eventArgs);
        }

        protected override async void OnStartup(StartupEventArgs eventArgs)
        {
            // this.Reset();

            SfSkinManager.ApplyStylesOnApplication = true;
            this.InitialiseApp();
            Configuration.Init();
            LogWriter.Init();
            DbRepository.Init();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTQ4MjExMUAzMjMxMmUzMTJlMzMzNVJZSVc0MVJZZUo1aGhpOU04Tnc1SHRVYm5XZkJIdFNYQ1JMa2JCb2k3ckk9");


#if RELEASE
            SquirrelAwareApp.HandleEvents(this.OnAppInstall,
                onAppUninstall: this.OnAppUninstall,
                onEveryRun: this.OnAppRun);

            var updateSource =
                new GithubSource(@"https://github.com/testpossessed/acc-server-results-companion",
                    null,
                    true);

            var updateManager = new UpdateManager(updateSource);

            LogWriter.LogDebug("Checking for updates");
            var updateInfo = await updateManager.CheckForUpdate();
            if(updateInfo?.ReleasesToApply?.Any() is true)
            {
                var updateProgressDialog = new UpdateProgressDialog();
                var updateProgressViewModel =
                    new UpdateProgressDialogViewModel(updateProgressDialog,
                        updateManager,
                        updateInfo);
                updateProgressDialog.DataContext = updateProgressViewModel;

                this.MainWindow = updateProgressDialog;
                this.MainWindow.Show();
                updateProgressViewModel.Update();
            }
            else
            {
                var splashScreen = new SplashScreen("Splash.png");
                splashScreen.Show(false, true);
                Thread.Sleep(TimeSpan.FromSeconds(3));
                splashScreen.Close(TimeSpan.FromMilliseconds(500));
#endif
            var mainWindowViewModel = new MainWindowViewModel();
            this.MainWindow = new MainWindow
                              {
                                  DataContext = mainWindowViewModel
                              };
            SfSkinManager.SetTheme(this.MainWindow, new Theme("Blend"));
                this.MainWindow.Show();
                mainWindowViewModel.Init();

                LogWriter.LogInfo("ACC Server Results Companion started");

#if RELEASE
            }
#endif
        }

        private void Reset()
        {
            var dbFilePaths = Directory.GetFiles(PathProvider.AppDataFolderPath,
                "AccServerResultsCompanion.db*");
            
            foreach(var dbFilePath in dbFilePaths)
            {
                File.Delete(dbFilePath);
            }

            Directory.Delete(PathProvider.DownloadedResultsFolderPath, true); 
        }

        private void EnsureAppDataFoldersExist()
        {
            if(!Directory.Exists(PathProvider.AppDataFolderPath))
            {
                Directory.CreateDirectory(PathProvider.AppDataFolderPath);
            }

            if(!Directory.Exists(PathProvider.DownloadedResultsFolderPath))
            {
                Directory.CreateDirectory(PathProvider.DownloadedResultsFolderPath);
            }
        }

        private void InitialiseApp()
        {
            this.SetupExceptionHandling();
            this.EnsureAppDataFoldersExist();
        }

        private void LogUnhandledException(Exception exception, string source)
        {
            var message = $"Unhandled exception ({source})";
            try
            {
                var assemblyName = Assembly.GetExecutingAssembly()
                                           .GetName();
                message = $"Unhandled exception in {assemblyName.Name} v{assemblyName.Version}";
            }
            catch(Exception innerException)
            {
                LogWriter.LogError(innerException, "Unexpected error in error handler.");
            }
            finally
            {
                LogWriter.LogError(exception, message);
                MessageBox.Show($"An unexpected error occurred: {exception?.Message}");
            }
        }

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException +=
                (s, e) => this.LogUnhandledException((Exception)e.ExceptionObject,
                    "AppDomain.CurrentDomain.UnhandledException");

            this.DispatcherUnhandledException += (s, e) =>
                                                 {
                                                     this.LogUnhandledException(e.Exception,
                                                         "Application.Current.DispatcherUnhandledException");
                                                     e.Handled = true;
                                                 };

            TaskScheduler.UnobservedTaskException += (s, e) =>
                                                     {
                                                         this.LogUnhandledException(e.Exception,
                                                             "TaskScheduler.UnobservedTaskException");
                                                         e.SetObserved();
                                                     };
        }

#if RELEASE
        private void OnAppInstall(SemanticVersion version, IAppTools tools)
        {
            tools.CreateShortcutForThisExe();
        }

        private void OnAppRun(SemanticVersion version, IAppTools tools, bool firstRun) { }

        private void OnAppUninstall(SemanticVersion version, IAppTools tools)
        {
            tools.RemoveShortcutForThisExe();
        }
#endif
    }
}