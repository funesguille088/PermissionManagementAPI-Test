using PermissionsManagementAPI_Test.Models.ModifyPermission;
using System.Net;
using System.Text;

namespace PermissionsManagementAPI_Test.Routes
{
    public class ModifyPermission_Route
    {
        public async Task<PutPermissionResponse> TestModifyPermission_Route(PutPermissionBodyRequest permission)
        {
            try
            {
                HttpClient client = new HttpClient();

                // Serialize the permission object to JSON
                string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(new PutPermissionRequest { permission = permission });

                // Create a StringContent with the serialized JSON
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                // Make the POST request
                HttpResponseMessage response = await client.PutAsync("https://localhost:6060/ModifyPermissions", content);

                // Handle the response
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    PutPermissionResponse apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<PutPermissionResponse>(responseBody);
                    return apiResponse;
                }
                else
                {
                    Console.WriteLine($"Unexpected response status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
                return null;
            }
        }
    }
}
