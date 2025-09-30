// 代码生成时间: 2025-09-30 22:43:12
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Maui.Controls;

namespace SslTlsCertificateManager
{
    // The CertificateManager class provides functionality for managing SSL/TLS certificates.
    public class CertificateManager
    {
        public async Task InitializeCertificatesAsync()
        {
            // Initialize the certificate store.
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            try
            {
                // Load certificates from the store.
                var certificates = store.Certificates;
                // Display the certificates in the UI.
                DisplayCertificates(certificates);
            }
            catch (Exception ex)
            {
                // Handle exceptions and show error message.
                Console.WriteLine($"An error occurred: {ex.Message}");
                await DisplayAlert("Certificate Manager", ex.Message, "OK");
            }
            finally
            {
                store.Close();
            }
        }

        // Method to display certificates in the UI.
        private void DisplayCertificates(X509Certificate2Collection certificates)
        {
            foreach (var cert in certificates)
            {
                // Display each certificate's details.
                Console.WriteLine($"Certificate: {cert.Subject}");
            }
        }

        // Method to add a new certificate to the store.
        public async Task AddCertificateAsync(string filePath)
        {
            try
            {
                // Load the certificate from the file.
                var cert = new X509Certificate2(filePath);
                var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadWrite);
                try
                {
                    // Add the certificate to the store.
                    store.Add(cert);
                    await DisplayAlert("Certificate Manager", "Certificate added successfully.", "OK");
                }
                finally
                {
                    store.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and show error message.
                Console.WriteLine($"An error occurred: {ex.Message}");
                await DisplayAlert("Certificate Manager", ex.Message, "OK");
            }
        }

        // Method to remove a certificate from the store.
        public async Task RemoveCertificateAsync(string thumbprint)
        {
            try
            {
                var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadWrite);
                try
                {
                    // Find the certificate by thumbprint.
                    var cert = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false)[0];
                    // Remove the certificate from the store.
                    store.Remove(cert);
                    await DisplayAlert("Certificate Manager", "Certificate removed successfully.", "OK");
                }
                finally
                {
                    store.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and show error message.
                Console.WriteLine($"An error occurred: {ex.Message}");
                await DisplayAlert("Certificate Manager", ex.Message, "OK");
            }
        }

        // Generic method to display alert dialogs.
        private async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}