using PermissionsManagementAPI_Test.Models.GetPermissionsModel;
using PermissionsManagementAPI_Test.Models.ModifyPermission;
using PermissionsManagementAPI_Test.Models.RequestPermission;
using PermissionsManagementAPI_Test.Routes;
using System.Text.Json;

namespace PermissionsManagementAPI_Test
{
    [TestClass]
    public class APITests
    {
        [TestMethod]
        public async Task TestRequestPermission_Route()
        {
            PostPermissionBodyRequest permission = new PostPermissionBodyRequest
            {
                employeeId = "741f06d2-258d-4b4e-af61-c1b5fe7a129f",
                applicationName = "Pega",
                permissionType = 0,
                permissionGranted = true,
                permissionGrantedEmployeeId = "397d6c28-f8c9-4788-8640-dd7590748998"
            };

            // Make the POST request
            RequestPermission_Route apiClient = new RequestPermission_Route();
            PostPermissionResponse response = await apiClient.TestRequestPermission_Route(permission);

            // Check if response is not null and print the ID
            if (response != null)
            {
                Console.WriteLine($"Created permission ID: {response.id}");
            }
            else
            {
                Console.WriteLine("Failed to create permission.");
            }
        }

        [TestMethod]
        public async Task TestModifyPermission_Route()
        {
            PutPermissionBodyRequest permission = new PutPermissionBodyRequest
            {
                id = "9b0d9b57-6203-4bf3-b186-434802f0f7b3",
                employeeId = "741f06d2-258d-4b4e-af61-c1b5fe7a129f",
                applicationName = "Pega Cosmos",
                permissionType = 5,
                permissionGranted = true,
                permissionGrantedEmployeeId = "397d6c28-f8c9-4788-8640-dd7590748998"
            };

            // Make the PUT request
            ModifyPermission_Route apiClient = new ModifyPermission_Route();
            PutPermissionResponse response = await apiClient.TestModifyPermission_Route(permission);

            // Check if response is not null and print the result
            if (response != null)
            {
                Console.WriteLine($"Permission Modified Success: {response.isSuccess}");
            }
            else
            {
                Console.WriteLine("Failed to create permission.");
            }
        }

        [TestMethod]
        public async Task TestGetPermissions_Route()
        {
            try
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync("https://localhost:6060/GetPermissions");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();


                GetPermissionResponse permissionResponse = JsonSerializer.Deserialize<GetPermissionResponse>(responseBody);

                if (permissionResponse != null && permissionResponse.permissions != null)
                {

                    // Verify the expected result
                    Console.WriteLine($"pageIndex: {permissionResponse.permissions.pageIndex}");
                    Console.WriteLine($"pageSize: {permissionResponse.permissions.pageSize}");
                    Console.WriteLine($"count: {permissionResponse.permissions.count}");
                    foreach (var data in permissionResponse.permissions.data)
                    {
                        Console.WriteLine($"id: {data.id}");
                        Console.WriteLine($"employeeId: {data.employeeId}");
                        Console.WriteLine($"applicationName: {data.applicationName}");
                        Console.WriteLine($"permissionType: {data.permissionType}");
                        Console.WriteLine($"permissionGranted: {data.permissionGranted}");
                        Console.WriteLine($"permissionGrantedEmployeeId: {data.permissionGrantedEmployeeId}");
                    }
                }
                else
                {
                    Console.WriteLine("Permission response or its properties are null.");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
            }
        }
    }
}