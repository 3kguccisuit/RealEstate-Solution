using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UtilitiesLib.Helpers
{
    public static class UIHelper
    {
        // Method to recursively check for validation errors  
        public static bool HasValidationError(DependencyObject obj)
        {
            // Check if the current object has any validation errors  
            if (Validation.GetHasError(obj))
            {
                return true;
            }

            // Recursively check child elements  
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (HasValidationError(child))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
