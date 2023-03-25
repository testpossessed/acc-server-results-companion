using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Acc.Server.Results.Companion.Database.Entities;
using Acc.Server.Results.Companion.ServerManagement.ServerEditor;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Acc.Server.Results.Companion
{
    internal class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel()
        {
            this.AddServer = new RelayCommand(this.HandleAddServer);
        }

        private void HandleAddServer()
        {
            var serverEditor = new ServerEditor()
                               {
                                   Owner = Application.Current.MainWindow
                               };
            var viewModel = new ServerEditorViewModel(serverEditor);

            serverEditor.DataContext = viewModel;
            var dialogResult = serverEditor.ShowDialog();
        }

        public ICommand AddServer { get; }

        public ObservableCollection<Session> Sessions { get; } = new();

    }
}