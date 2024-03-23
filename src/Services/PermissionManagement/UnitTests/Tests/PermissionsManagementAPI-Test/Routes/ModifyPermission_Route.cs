/*
   This class defines a route for testing the Modify Permission endpoint in the Permissions Management API.
   It contains a method to send a PUT request to the Modify Permission endpoint and handle the response.

   Usage:
   1. Create an instance of ModifyPermission_Route.
   2. Call the TestModifyPermission_Route method with a PutPermissionBodyRequest object as a parameter.
      This method sends a PUT request to the Modify Permissions endpoint with the provided permission data.
   3. Handle the response returned by the method to check if the operation was successful.
*/

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
