using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace FancyUnitConverter
{
    public partial class MainPage : ContentPage
    {
        private readonly Dictionary<string, string[]> _unitTypes;
        private readonly Dictionary<(string, string), Func<double, double>> _conversions;

        public MainPage()
        {
            InitializeComponent();

            _unitTypes = new Dictionary<string, string[]>
            {
                { "Length", new [] { "Meter", "Feet" } }, // You can add more units here  
                { "Temperature", new [] { "Celsius", "Fahrenheit" } }, // You can add more units here  
                { "Weight", new [] { "Kilogram", "Pound" } }, // You can add more units here  
            };

            _conversions = new Dictionary<(string, string), Func<double, double>>
            {
                { ("Meter", "Feet"), meters => meters * 3.28084 },
                { ("Feet", "Meter"), feet => feet / 3.28084 },
                { ("Celsius", "Fahrenheit"), celsius => (celsius * 9 / 5) + 32 },
                { ("Fahrenheit", "Celsius"), fahrenheit => (fahrenheit - 32) * 5 / 9 },
                { ("Kilogram", "Pound"), kilogram => kilogram * 2.20462 },
                { ("Pound", "Kilogram"), pound => pound / 2.20462 },  
                // ... other conversions  
            };

            unitTypePicker.SelectedIndexChanged += OnUnitTypeSelected;
            fromUnitPicker.SelectedIndexChanged += OnUnitSelected;
            toUnitPicker.SelectedIndexChanged += OnUnitSelected;
        }

        private void OnUnitTypeSelected(object sender, EventArgs e)
        {
            if (unitTypePicker.SelectedItem is null) return;

            string selectedType = unitTypePicker.SelectedItem.ToString();
            PopulatePickerItems(fromUnitPicker, _unitTypes[selectedType]);
            PopulatePickerItems(toUnitPicker, _unitTypes[selectedType]);
        }

        private void PopulatePickerItems(Picker picker, string[] items)
        {
            picker.Items.Clear();
            foreach (var item in items)
            {
                picker.Items.Add(item);
            }
        }

        private void OnUnitSelected(object sender, EventArgs e)
        {
            // No implementation needed here for now  
        }

        private void OnConvertClicked(object sender, EventArgs e)
        {
            if (fromUnitPicker.SelectedItem is null || toUnitPicker.SelectedItem is null)
            {
                resultLabel.Text = "Please select units to convert.";
                return;
            }

            if (double.TryParse(inputEntry.Text, out double inputValue))
            {
                string fromUnit = fromUnitPicker.SelectedItem.ToString();
                string toUnit = toUnitPicker.SelectedItem.ToString();

                var conversionKey = (fromUnit, toUnit);

                if (_conversions.TryGetValue(conversionKey, out Func<double, double> conversionFunc))
                {
                    double result = conversionFunc(inputValue);
                    resultLabel.Text = $"{inputValue} {fromUnit} is {result:N2} {toUnit}";
                }
                else
                {
                    resultLabel.Text = "Conversion not available.";
                }
            }
            else
            {
                resultLabel.Text = "Please enter a valid number.";
            }
        }
    }
}