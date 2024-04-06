using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class DeviceHelper
    {
        //https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device-media/picker?view=net-maui-8.0&tabs=windows
        
        public async Task<MediaFile> TakePhoto(ContentPage pageContext)
        {
            if (MediaPicker.Default.IsCaptureSupported == false)
            {
                //await pageContext.DisplayAlert("No Camera", ":( No camera available.", "OK");
                await Shell.Current.DisplayAlert("ahí va calorina", ":( No camera available.", "ok", "cancel");
                return null;
            }

            bool permisosConcedidos = await CheckForCameraAndGalleryPermission();
            if (permisosConcedidos)
            {

                try
                {
                    var options = new StoreCameraMediaOptions {
                        Directory = "prueba",
                        SaveToAlbum = true,
                        CompressionQuality = 75,
                        CustomPhotoSize = 50,
                        PhotoSize = PhotoSize.Medium,
                        MaxWidthHeight = 1000,
                    };
                    var photo = await CrossMedia.Current.TakePhotoAsync(options);

                    await Shell.Current.DisplayAlert("Path", photo.Path, "ok", "cancel");
                    return photo;
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error interno", ex.Message.ToString(), "ok","cancel");
                    return null;
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("No has dado tu permiso!", "Debes habilitar los permisos para tomar una foto", "ok", "cancel");
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

        private byte[]ConvertFileToByteArray(MediaFile imageFile)
        {
            // Convert Image to bytes
            byte[] imageAsBytes= new byte[0];
            using (var memoryStream = new MemoryStream())
            {
                imageFile.GetStream().CopyTo(memoryStream);
                imageFile.Dispose();
                imageAsBytes = memoryStream.ToArray();
            }

            return imageAsBytes;
        }
    }
}
