using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;

namespace LS_Diabetes_App.Interfaces
{
    public class PermissionsRequest
    {
        public async Task<PermissionStatus> Check_permissions(string type)
        {
            PermissionStatus status = PermissionStatus.Unknown;
            if (type == "Location")
            {
                status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                }
            }
            if (type == "Storage")
            {
                status = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {
                    }

                    status = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                }
            }
            return status;
        }
    }
}