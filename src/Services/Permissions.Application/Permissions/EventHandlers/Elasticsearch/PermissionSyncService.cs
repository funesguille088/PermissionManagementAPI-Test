
using Elasticsearch.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Nest;
using Permissions.Application.Data;
using Permissions.Domain.Models;

namespace Permissions.Application.Permissions.EventHandlers.Elasticsearch
{
    public class PermissionSyncService
    {
        private const string IndexName = "permissionregistry";
        private readonly IElasticLowLevelClient _elasticClient;

        public PermissionSyncService(IElasticLowLevelClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task SyncPermissionsAsync(IApplicationDbContext _dbContext)
        {
            var createIndexResult = await CreateIndex(IndexName);

            var bulkInsertResult = await BulkIndexAsync(_dbContext);

        }

       
        public async Task<StringResponse> BulkIndexAsync(IApplicationDbContext _dbContext)
        {
            var permissions = await _dbContext.Permissions.ToListAsync();

            var bulkRequest = new List<object>();

            foreach (var permission in permissions)
            {
                bulkRequest.Add(new { index = new { _index = IndexName } });
                bulkRequest.Add(new ELSPermissionDocument
                {
                    permissionid = permission.Id.Value.ToString(),
                    employeeid = permission.EmployeeId.Value.ToString(),
                    applicationname = permission.ApplicationName,
                    permissiontype = permission.PermissionType.GetStringValue(),
                    permissiongranted = permission.PermissionGranted,
                    permissiongrantedemployeeId = permission.PermissionGrantedEmployeeId.Value.ToString()
                });
            }

            return await _elasticClient.BulkAsync<StringResponse>(PostData.MultiJson(bulkRequest));
        }

        public async Task<IElasticsearchResponse> CreateIndex(string indexName)
        {
            
            if (_elasticClient.Indices.Exists<StringResponse>(indexName).ApiCall.HttpStatusCode == StatusCodes.Status200OK)
            {
                return null;
            }

            var mappings = new
            {
                mappings = new
                {
                    dynamic = "strict", 
                    properties = new
                    {
                        permissionid = new { type = "keyword" },
                        employeeid = new { type = "keyword" },
                        applicationname = new { type = "text" },
                        permissiontype = new { type = "text" },
                        permissiongranted = new { type = "boolean" },
                        permissiongrantedemployeeId = new { type = "keyword" }
                    }
                }
            };

            var postData = PostData.Serializable(mappings);

            return await _elasticClient.Indices.CreateAsync<DynamicResponse>(indexName, postData);
        }

       
    }
}
