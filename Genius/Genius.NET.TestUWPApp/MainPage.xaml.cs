﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Genius.NET.TestUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Authenticator.ClientId = "CLIENT_ID";
            Authenticator.RedirectUri = "https://genius.com";
            Authenticator.Scope = "me create_annotation manage_annotation vote";
            Authenticator.State = "default_state";
            Authenticator.ClientSecret = "ENTER_CLIENT_SECRET";
            var url = Authenticator.GetAuthenticationUrl();
            var unescapedUrl = Uri.EscapeUriString(url.ToString());
            WebView.FrameContentLoading += web_FrameContentLoading;
            WebView.NavigationCompleted += web_NavigationCompleted;
            WebView.NavigateToString(unescapedUrl);
        }

        private static void web_FrameContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        {
            Debug.WriteLine("Yo");
        }

        private static async void web_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            try
            {
                var uri = args.Uri.ToString();
                if (uri.Contains("code="))
                {
                    await Authenticator.GetAccessToken(args.Uri);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
