using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WRCClient
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        public MainWindow()
        {
            this.InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(typeof(EmptyPage));
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(typeof(DirectXPage));
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PageFrame.Navigate(typeof(DirectNPage));
        }
        //private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        //{
        //    RootFrame.Navigate(typeof(DirectXPage), null, new SuppressNavigationTransitionInfo());
        //}
        //u–ß‚évƒ{ƒ^ƒ“‚Ìˆ—
        private void Button_Click_Prev(object sender, RoutedEventArgs e)
        {
            if (PageFrame.CanGoBack) PageFrame.GoBack();
        }

        //ui‚Þvƒ{ƒ^ƒ“‚Ìˆ—
        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            if (PageFrame.CanGoForward) PageFrame.GoForward();
        }
    }
}
