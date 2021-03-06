﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Samba.Domain.Models.Users;
using Samba.Presentation.Common;
using Samba.Services;

namespace Samba.Login
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>

    [Export]
    public partial class LoginView : UserControl
    {
        [ImportingConstructor]
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void LoginPadControl_PinSubmitted(object sender, string pinValue)
        {
            pinValue.PublishEvent(EventTopicNames.PinSubmitted);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UserControl_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Text)&& char.IsDigit(e.Text, 0))
                PadControl.UpdatePinValue(e.Text);
        }

        private void UserControl_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
               
                PadControl.SubmitPin();
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Uri u = new Uri(Localization.Properties.Resources.ClientServerConnectionHelpUrlString);
            Process.Start(new ProcessStartInfo(u.AbsoluteUri));
            e.Handled = true;
        }
        
    }
}
