using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Windows10DemoKing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();


            MainFrame.Navigate(typeof(RelativePage));
            RegisterVCD();

        }
        private async void RegisterVCD()
        {         

            //if (isPresent)
            //{
            //    StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            //    await statusBar.HideAsync();
            //}
            

            var storageFile =
              await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Commands.xml"));
            await
              VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(storageFile);
        }



        private void btnContent_Click(object sender, RoutedEventArgs e)
        {
            MyStackPanel.IsPaneOpen = !MyStackPanel.IsPaneOpen;



        }

        private void btnMap_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(MapPage));
            MyStackPanel.IsPaneOpen = false;
        }

        private void btnInk_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(InkPage));
            MyStackPanel.IsPaneOpen = false;
        }

        private void btnApp2App_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(App2AppPage));
            MyStackPanel.IsPaneOpen = false;
        }

        private void btnBtAds_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(BtAdsPage));
            MyStackPanel.IsPaneOpen = false;
        }

        private void btnRelative_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(RelativePage));
            MyStackPanel.IsPaneOpen = false;
        }

        private void btnLFR_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(LaunchForResultsPage));
            MyStackPanel.IsPaneOpen = false;
        }

        private void btnLaunchSpec_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(LaunchUri));
            MyStackPanel.IsPaneOpen = false;
        }

        private void btnDragDrop_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(DropPage));
            MyStackPanel.IsPaneOpen = false;
        }

        private void btnCortana_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(CortanaRecognition));
            MyStackPanel.IsPaneOpen = false;
        }

        private void btnWifi_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(wifi));
            MyStackPanel.IsPaneOpen = false;
        }
    }
}
