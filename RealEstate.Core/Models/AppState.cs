using RealEstate.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Models
{
    public class AppState
    {
        public bool IsDirty { get; set; }
        public string FileName { get; set; }
        public FileFormats Format { get; set; }

        public AppState()
        {
            Format = FileFormats.Unknown;
            IsDirty = false;
            FileName = "";
        }
    }
}
