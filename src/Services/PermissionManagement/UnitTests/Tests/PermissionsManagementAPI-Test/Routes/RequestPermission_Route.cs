/*
   This class defines a route for testing the Request Permission endpoint in the Permissions Management API.
   It contains a method to send a POST request to the Request Permissions endpoint and handle the response.

   Usage:
   1. Create an instance of RequestPermission_Route.
   2. Call the TestRequestPermission_Route method with a PostPermissionBodyRequest object as a parameter.
      This method sends a POST request to the Request Permissions endpoint with the provided permission data.
   3. Handle the response returned by the method to check if the operation was successful.
*/

using PermissionsManagementAPI_Test.Models.RequestPermission;
using System.Net;
using System.Text;

namespace PermissionsManagementAPI_Test.Routes
{
    public class RequestPermission_Route
    {
        public async Task<PostPermissionResponse> TestRequestPermission_Route(PostPermissionBodyRequest permission)
        {
            try
            {
                HttpClient client = new HttpClient();

                // Serialize the permission object to JSON
                string requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(new PostPermissionRequest { permission = permission });

                // Create a StringContent with the serialized JSON
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                // Make the POST request
                HttpResponseMessage response = await client.PostAsync("https://localhost:6060/RequestPermissions", content);

                // Handle the response
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    PostPermissionResponse apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<PostPermissionResponse>(responseBody);
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
