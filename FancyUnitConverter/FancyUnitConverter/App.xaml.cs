﻿using Microsoft.Maui.Controls;

namespace FancyUnitConverter
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
    }
}