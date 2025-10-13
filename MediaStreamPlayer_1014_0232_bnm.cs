// 代码生成时间: 2025-10-14 02:32:23
using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;

namespace MauiApp
{
    /// <summary>
    /// A simple media stream player for MAUI applications.
    /// </summary>
    public class MediaStreamPlayer : ContentView
    {
        private MediaElement mediaElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaStreamPlayer"/> class.
        /// </summary>
        public MediaStreamPlayer()
        {
            // Initialize the MediaElement
            mediaElement = new MediaElement
            {
                // Set the aspect ratio of the media element
                Aspect = Aspect.AspectFit
            };

            // Add the media element to the layout
            this.Content = mediaElement;
        }

        /// <summary>
        /// Loads and plays a media stream from a given URL.
        /// </summary>
        /// <param name="mediaUrl">The URL of the media stream to play.</param>
        public async Task PlayAsync(string mediaUrl)
        {
            try
            {
                // Set the source of the media element to the provided URL
                mediaElement.Source = new UriMediaSource { Uris = { new Uri(mediaUrl) } };

                // Play the media stream
                await mediaElement.Play();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during media playback
                Console.WriteLine($"Error playing media: {ex.Message}");
            }
        }

        /// <summary>
        /// Stops the media stream playback.
        /// </summary>
        public void Stop()
        {
            mediaElement.Stop();
        }

        /// <summary>
        /// Pauses the media stream playback.
        /// </summary>
        public void Pause()
        {
            mediaElement.Pause();
        }

        /// <summary>
        /// Resumes the media stream playback.
        /// </summary>
        public void Resume()
        {
            mediaElement.Play();
        }
    }
}
