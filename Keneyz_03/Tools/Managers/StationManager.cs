using System;
using System.Windows;
using System.Windows.Controls;
using Keneyz_03.Model;
using Keneyz_03.Tools.DataStorage;
using Keneyz_03.ViewModel;

namespace Keneyz_03.Tools.Managers
{
    internal static class StationManager
    {
        internal static Person CurrentPerson { get; set; }
        internal static Person TestPerson { get; set; }
        internal static DataGrid PersonTable { get; set; }

        internal static EditViewModel EditVM { get; set; }
        internal static ListViewModel DataVM { get; set; }

        private static IDataStorage _dataStorage;

        internal static IDataStorage DataStorage => _dataStorage;

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(1);
        }

    }
}