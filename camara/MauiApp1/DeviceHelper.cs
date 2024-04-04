﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class DeviceHelper
    {
        //https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device-media/picker?view=net-maui-8.0&tabs=windows
        
        public async Task<string> TakePhoto(ContentPage pageContext)
        {
            if (MediaPicker.Default.IsCaptureSupported == false)
            {
                await pageContext.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return "1";
            }

            bool permisosConcedidos = await CheckForCameraAndGalleryPermission();
            if (permisosConcedidos)
            {
                FileResult? photo = await MediaPicker.Default.CapturePhotoAsync(
                    new MediaPickerOptions()
                    { 
                        Title="kk"
                    }
                );

                if (photo == null)
                    return "2";

                try
                {
                    var imageAsBase64String = Convert.ToBase64String(await ConvertFileToByteArray(photo));
                    return imageAsBase64String;
                } catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
            else
            {
                await pageContext.DisplayAlert("Habilitar permisos", "Debes habilitar los permisos para continuar.", "OK");
                return "3";
            }             
        }

        public async Task<ImageSource> TakePhoto1(ContentPage pageContext)
        {
            if (MediaPicker.Default.IsCaptureSupported == false)
            {
                await pageContext.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return null;
            }

            bool permisosConcedidos = await CheckForCameraAndGalleryPermission();
            if (permisosConcedidos)
            {
                

                try
                {
                    FileResult? photo = await MediaPicker.Default.CapturePhotoAsync(
                    new MediaPickerOptions()
                    {
                        Title = "kk",
                        
                    }
                    );

                    if (photo == null)
                        return null;

                    var stream = await photo.OpenReadAsync();
                    return ImageSource.FromStream(() => stream);
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
            else
            {
                await pageContext.DisplayAlert("Habilitar permisos", "Debes habilitar los permisos para continuar.", "OK");
                return null;
            }
        }

        private async Task<bool> CheckForCameraAndGalleryPermission()
        {
            #region verifica permisos
            var status = await Permissions.CheckStatusAsync<CameraAndStorage>();
            if (status == PermissionStatus.Granted)
            {
                return true;
            }
            #endregion

            #region los solicita!
            status = await Permissions.RequestAsync<CameraAndStorage>();

            if (status != PermissionStatus.Granted)
            {
                await Shell.Current.DisplayAlert("no me diste los permisos", "blabla!", "ok");
            }
            #endregion

            return false;
        }

        private async Task<byte[]> ConvertFileToByteArray(FileResult imageFile)
        {
            // Convert Image to bytes
            byte[] imageAsBytes;

            using (Stream sourceStream = await imageFile.OpenReadAsync())
            using (var memoryStream = new MemoryStream())
            {
                sourceStream.CopyTo(memoryStream);
                imageAsBytes = memoryStream.ToArray();
            }

            return imageAsBytes;
        }
    }
}
