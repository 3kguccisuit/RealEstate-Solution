using RealEstate.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RealEstate.Models
{
    public partial class AppState : ObservableObject
    {
        [ObservableProperty]
        private bool isDirty;
        [ObservableProperty]
        private string fileName;
        [ObservableProperty]
        private FileFormats format;

        public AppState()
        {
            Format = FileFormats.Unknown;
            IsDirty = false;
            FileName = "";
        }
    }
}
