using System;
using System.Windows.Forms;
using System.Windows.Input;
using Acc.Server.Results.Companion.Database;
using Acc.Server.Results.Companion.Database.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Acc.Server.Results.Companion.ServerManagement.ServerEditor
{
    internal class ServerEditorViewModel : ObservableObject
    {
        private const string FolderServerType = "Folder";
        private const string FtpServerType = "FTP";
        private readonly ServerEditor serverEditor;

        private string folderPath = null!;
        private string hostName = null!;
        private string hostPort = null!;
        private string name = null!;
        private string password = null!;
        private string serverType = null!;
        private string username = null!;

        public ServerEditorViewModel(ServerEditor serverEditor)
        {
            this.serverEditor = serverEditor;
            this.Save = new RelayCommand(this.HandleSave, this.CanExecuteSave);
            this.SelectFolder = new RelayCommand(this.HandleSelectFolder);
            this.ServerType = FtpServerType;
        }

        public ICommand Save { get; }

        public ICommand SelectFolder { get; }

        public string FolderPath
        {
            get => this.folderPath;
            set
            {
                this.SetProperty(ref this.folderPath, value);
                this.NotifyCanExecuteSaveChanged();
            }
        }

        public string HostName
        {
            get => this.hostName;
            set
            {
                this.SetProperty(ref this.hostName, value);
                this.NotifyCanExecuteSaveChanged();
            }
        }

        public string HostPort
        {
            get => this.hostPort;
            set
            {
                this.SetProperty(ref this.hostPort, value);
                this.NotifyCanExecuteSaveChanged();
            }
        }

        public string Name
        {
            get => this.name;
            set
            {
                this.SetProperty(ref this.name, value);
                this.NotifyCanExecuteSaveChanged();
            }
        }

        public string Password
        {
            get => this.password;
            set => this.SetProperty(ref this.password, value);
        }

        public string ServerType
        {
            get => this.serverType;
            set => this.SetProperty(ref this.serverType, value);
        }

        public string Username
        {
            get => this.username;
            set => this.SetProperty(ref this.username, value);
        }

        private bool CanExecuteSave()
        {
            return !string.IsNullOrWhiteSpace(this.Name)
                   && (this.HasValidFtpSettings() || this.HasValidFolderSettings());
        }

        private string GetAddress()
        {
            return this.ServerType == FolderServerType
                       ? this.FolderPath
                       : $"ftp://{this.HostName}:{this.HostPort}";
        }

        private void HandleSave()
        {
            var serverDetails = new ServerDetails
                                {
                                    Name = this.Name,
                                    Address = this.GetAddress(),
                                    Username = this.Username,
                                    Password = this.Password,
                                    IsLocalFolder = this.ServerType == FolderServerType
                                };

            DbRepository.AddServerDetails(serverDetails);
            this.serverEditor.DialogResult = true;
            this.serverEditor.Close();
        }

        private void HandleSelectFolder()
        {
            var selectFolderDialog = new FolderBrowserDialog();
            var dialogResult = selectFolderDialog.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                this.FolderPath = selectFolderDialog.SelectedPath;
            }
        }

        private bool HasValidFolderSettings()
        {
            return this.ServerType == FolderServerType
                   && !string.IsNullOrWhiteSpace(this.FolderPath);
        }

        private bool HasValidFtpSettings()
        {
            return this.ServerType == FtpServerType && (!string.IsNullOrWhiteSpace(this.HostName)
                                                        && !string
                                                            .IsNullOrWhiteSpace(this.HostPort));
        }

        private void NotifyCanExecuteSaveChanged()
        {
            ((RelayCommand)this.Save).NotifyCanExecuteChanged();
        }
    }
}