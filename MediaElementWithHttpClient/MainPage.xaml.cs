using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaElementWithHttpClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MediaElement mediaPlayer;
        private SystemMediaTransportControls systemControls;

        public MainPage()
        {
            this.InitializeComponent();
            StartMediaElement();
        }


        private async void StartMediaElement()
        {
            // To use AudioCategory.BackgroundCapableMedia:
            // * OnWindows 8 set MediaControl.PlayPressed, MediaControl.PausePressed,
            //   MediaControl.PlayPauseTogglePressed and MediaControl.StopPressed.
            // * On Windows 8.1 set SystemMediaTransportControls.ButtonPressed.
            systemControls = SystemMediaTransportControls.GetForCurrentView();
            systemControls.ButtonPressed += OnButtonPressed;
            systemControls.IsPlayEnabled = true;
            systemControls.IsPauseEnabled = true;
            systemControls.PlaybackStatus = MediaPlaybackStatus.Playing;

            mediaPlayer = new MediaElement();
            mediaPlayer.AudioCategory = AudioCategory.BackgroundCapableMedia;
            mediaPlayer.AutoPlay = true;
            mediaPlayer.CurrentStateChanged += OnCurrentStateChanged;
            this.Content = mediaPlayer;

            HttpClient client = new HttpClient();

            // Add custom headers or credentials.
            client.DefaultRequestHeaders.Add("Foo", "Bar");

            //Uri uri = new Uri("http://localhost/song.mp3?slow=1000&?bufferlength=100000&lastModified=true");
            Uri uri = new Uri("http://video.ch9.ms/ch9/70cc/83e17e76-8be8-441b-b469-87cf0e6a70cc/ASPNETwithScottHunter_high.mp4");

            HttpRandomAccessStream stream = await HttpRandomAccessStream.CreateAsync(client, uri);

            // If you need to use HttpClient, use MediaElement.SetSource() instead of MediaElement.Source.
            mediaPlayer.SetSource(stream, "audio/mpeg");
        }

        private async void OnButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                switch (args.Button)
                {
                    case SystemMediaTransportControlsButton.Play:
                        mediaPlayer.Play();
                        break;
                    case SystemMediaTransportControlsButton.Pause:
                        mediaPlayer.Pause();
                        break;
                    default:
                        break;
                }
            });
        }

        private void OnCurrentStateChanged(object sender, RoutedEventArgs e)
        {
            switch (mediaPlayer.CurrentState)
            {
                case MediaElementState.Playing:
                    systemControls.PlaybackStatus = MediaPlaybackStatus.Playing;
                    break;
                case MediaElementState.Paused:
                    systemControls.PlaybackStatus = MediaPlaybackStatus.Paused;
                    break;
                case MediaElementState.Stopped:
                    systemControls.PlaybackStatus = MediaPlaybackStatus.Stopped;
                    break;
                case MediaElementState.Closed:
                    systemControls.PlaybackStatus = MediaPlaybackStatus.Closed;
                    break;
                default:
                    break;
            }
        }

    }
}
