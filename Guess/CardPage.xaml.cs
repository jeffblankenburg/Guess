using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.IO.IsolatedStorage;

namespace Guess
{
    public partial class CardPage : PhoneApplicationPage
    {
        bool IsGameRunning = false;
        CaptureSource captureSource;
        FileSink fileSink;
        VideoCaptureDevice videoCaptureDevice;
        VideoBrush videoRecorderBrush;
        string isoVideoFileName = "GuessItRecording.mp4";

        public CardPage()
        {
            InitializeComponent();
            //InitializeVideoRecorder();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            OrientationChanged += CardPage_OrientationChanged;
        }

        void CardPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            PageOrientation orientation = e.Orientation;
            TextValue.Text = orientation.ToString();
            //if (IsGameRunning)
            //{
            //    if (orientation == PageOrientation.LandscapeLeft)
            //    {
            //        string r = null;
            //    }
            //}
            //else
            //{
            //    if (orientation == PageOrientation.Landscape)
            //    {
            //        StartGame();
            //    }
            //}
        }

        private void StartGame()
        {
            TextValue.Text = "3";
        }

        private void InitializeVideoRecorder()
        {
            if (captureSource == null)
            {
                captureSource = new CaptureSource();
                fileSink = new FileSink();

                var devices = CaptureDeviceConfiguration.GetAvailableVideoCaptureDevices();
                captureSource.VideoCaptureDevice = devices[0];
                captureSource.CaptureFailed += captureSource_CaptureFailed;

                if (videoCaptureDevice != null)
                {
                    videoRecorderBrush = new VideoBrush();
                    videoRecorderBrush.SetSource(captureSource);

                    ViewfinderRectangle.Fill = videoRecorderBrush;
                    captureSource.Start();
                }
            }
        }

        private void StartVideoRecording()
        {
            try
            {
                if (captureSource.VideoCaptureDevice != null && captureSource.State == CaptureState.Started)
                {
                    captureSource.Stop();
                    fileSink.CaptureSource = captureSource;
                    fileSink.IsolatedStorageFileName = isoVideoFileName;
                }

                if (captureSource.VideoCaptureDevice != null && captureSource.State == CaptureState.Stopped)
                {
                    captureSource.Start();
                }
            }
            catch
            {

            }
        }

        private void StopVideoRecording()
        {
            try
            {
                if (captureSource.VideoCaptureDevice != null && captureSource.State == CaptureState.Started)
                {
                    captureSource.Stop();

                    fileSink.CaptureSource = null;
                    fileSink.IsolatedStorageFileName = null;

                    StartVideoPreview();
                }
            }
            catch
            {
                
            }
        }

        private void StartVideoPreview()
        {
            try
            {
                if (captureSource.VideoCaptureDevice != null && captureSource.State == CaptureState.Stopped)
                {
                    videoRecorderBrush.SetSource(captureSource);

                    ViewfinderRectangle.Fill = videoRecorderBrush;

                    captureSource.Start();
                }
            }
            catch (Exception e)
            {

            }
        }

        private void DisposeVideoPlayer()
        {
            //if (VideoPlayer != null)
            //{
            //    VideoPlayer.Stop();

            //    VideoPlayer.Source = null;
            //    isoVideoFile = null;

            //    VideoPlayer.MediaEnded -= VideoPlayerMediaEnded;
            //}
        }

        private void DisposeVideoRecorder()
        {
            if (captureSource != null)
            {
                if (captureSource.VideoCaptureDevice != null
                    && captureSource.State == CaptureState.Started)
                {
                    captureSource.Stop();
                }

                // Remove the event handler for captureSource.
                captureSource.CaptureFailed -= captureSource_CaptureFailed;

                // Remove the video recording objects.
                captureSource = null;
                videoCaptureDevice = null;
                fileSink = null;
                videoRecorderBrush = null;
            }
        }


        void captureSource_CaptureFailed(object sender, ExceptionRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}