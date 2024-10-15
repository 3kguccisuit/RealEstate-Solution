using CommunityToolkit.Mvvm.ComponentModel;
using DTO.Enums;

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
