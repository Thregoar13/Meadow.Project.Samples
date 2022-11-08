﻿using MobileBle.ViewModel;

namespace MobileBle.View
{
    public partial class ServoControllerPage : ContentPage
    {
        public ServoControllerPage()
        {
            InitializeComponent();
            BindingContext = new ServoControllerViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as BaseViewModel).CmdSearchForDevices.Execute(null);
        }
    }
}