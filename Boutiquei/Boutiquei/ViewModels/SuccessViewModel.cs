using System;
using System.Windows.Input;
using Boutiquei.Views;
using MvvmHelpers;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Boutiquei.ViewModels
{
    public class SuccessViewModel : BaseViewModel
    {
        public string ImageSource { get; set; }
        public ICommand GetBackCommand { get; set; }
        public ICommand TrackCommand { get; set; }
        public SuccessViewModel()
        {
            ImageSource = "https://firebasestorage.googleapis.com/v0/b/boutiquei-54faf.appspot.com/o/Imgs%2FSuccess.png?alt=media&token=30273a94-17bf-4a18-95fd-f67257d4d1d7";
            GetBackCommand = new Command(OnGetBackTapped);
            TrackCommand = new Command(OnTrackTapped);

            ChickWifiOnStart();
            ChickWifiContinuously();
        }

        private bool _imgIsVisible;

        public bool ImgIsVisible
        {
            get => _imgIsVisible;
            set
            {
                _imgIsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _contentIsVisible;

        public bool ContentIsVisible
        {
            get => _contentIsVisible;
            set
            {
                _contentIsVisible = value;
                OnPropertyChanged();
            }
        }

        private string _connection;

        public string Connection
        {
            get => _connection;
            set
            {
                _connection = value;
                OnPropertyChanged();
            }
        }

        public void ChickWifiOnStart()
        {

            if (CrossConnectivity.Current.IsConnected)
            {

                ContentIsVisible = true;
                ImgIsVisible = false;
            }
            else
            {
                Connection = "Nointernetconnection.png";
                ContentIsVisible = false;
                ImgIsVisible = true;
            }
        }
        public void ChickWifiContinuously()
        {
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {

                if (args.IsConnected)
                {

                    ContentIsVisible = true;
                    ImgIsVisible = false;
                }
                else
                {
                    Connection = "Nointernetconnection.png";
                    ContentIsVisible = false;
                    ImgIsVisible = true;
                }
            };
        }
        private void OnGetBackTapped(object obj)
        {
            Application.Current.MainPage = new AppShell();
        }

        private async void OnTrackTapped(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new OrdersPage());
        }
    }
}
