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

        protected override async void OnStartup(StartupEventArgs e)
        {
            this.InitialiseApp();
            Configuration.Init();
            LogWriter.Init();
            DbRepository.Init();

            DbRepository.ClearSessions();

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
                this.MainWindow = new MainWindow
                                  {
                                      DataContext = new MainWindowViewModel()
                                  };
                this.MainWindow.Show();

                LogWriter.LogInfo("ACC Server Results Companion started");

#if RELEASE
            }
#endif
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