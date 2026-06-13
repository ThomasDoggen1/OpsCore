using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using OpsCore.Core.Models;
using OpsCore.Core.Services;
using System;
using Windows.UI;

namespace OpsCore.UI.Converters
{
    public class MaintenanceStatusColorConverter : IValueConverter
    {
        private readonly MaintenanceService _service = new();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is not Asset asset)
                return new SolidColorBrush(Colors.Transparent);

            if (_service.IsOverdue(asset))
                return new SolidColorBrush(Color.FromArgb(255, 255, 204, 204)); // rood

            if (_service.IsDueSoon(asset))
                return new SolidColorBrush(Color.FromArgb(255, 255, 229, 204)); // oranje

            return new SolidColorBrush(Color.FromArgb(255, 204, 255, 204)); // groen
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}