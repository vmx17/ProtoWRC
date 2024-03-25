using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using ProtoWRC;

namespace WRCClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DirectXPage : Page
    {
        static Dx11BackEnd m_back = null;

        public DirectXPage()
        {
            this.InitializeComponent();
            if (m_back == null)
            {
                m_back = new Dx11BackEnd();
                m_back.Initialize(1366, 768);
                m_back.SetSwapChainToBackEnd(mySwapChainPanel);
                m_back.StartRendering();
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs args)
        {
            if (m_back != null)
            {
                m_back.StartRendering();
				StartButton.IsEnabled = false;
				StopButton.IsEnabled = true;
			}
		}
		private void StopButton_Click(object sender, RoutedEventArgs args)
		{
			if (m_back != null)
			{
				m_back.StopRendering();
                StopButton.IsEnabled = false;
				StartButton.IsEnabled = true;
			}
		}
        /// <summary>
        /// 1. 
        /// 2. SwapChainPanelŽ©‘Ì‚ÌÝ’è’l
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void SCPSizeChanged(object sender, SizeChangedEventArgs e)
        {
            m_back.Width = mySwapChainPanel.ActualWidth;
            m_back.Height = mySwapChainPanel.ActualHeight;
        }

        private void SCPPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint(mySwapChainPanel as UIElement);
            m_back.SetPointer(p);
        }

        private void SCPPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint(mySwapChainPanel as UIElement);
            m_back.SetPointer(p);
        }

        private void SCPPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint(mySwapChainPanel as UIElement);
            m_back.SetPointer(p);
        }

        private void SCPCompositionScaleChanged(SwapChainPanel sender, object args)
        {
            ;
        }
    }
}
