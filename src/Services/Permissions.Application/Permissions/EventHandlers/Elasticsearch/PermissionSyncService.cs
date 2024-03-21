
using Elasticsearch.Net;
using Microsoft.EntityFrameworkCore;
using Nest;
using Permissions.Application.Data;
using Permissions.Domain.Models;

namespace Permissions.Application.Permissions.EventHandlers.Elasticsearch
{
    public class PermissionSyncService
    {
        private const string IndexName = "permissionregistry";
        
        public async Task SyncPermissionsAsync(IApplicationDbContext _dbContext, IElasticClient _elasticClient)
        {
            CreateIndexIfNotExists(_elasticClient);

            var permissions = await _dbContext.Permissions.ToListAsync();

            var documents = permissions.Select(p => new ELSPermissionDocument
            {
                id = p.Id.Value.ToString(),
                employeeid = p.EmployeeId.Value.ToString(),
                applicationname = p.ApplicationName,
                permissiontype = p.PermissionType.GetStringValue(),
                permissiongranted = p.PermissionGranted,
                permissiongrantedemployeeId = p.PermissionGrantedEmployeeId.Value.ToString()
            }).ToList(); // Materialize the LINQ query into a list

            var bulkRequest = new BulkRequest { Operations = new List<IBulkOperation>() };

            foreach (var doc in documents)
            {
                bulkRequest.Operations.Add(new BulkIndexOperation<ELSPermissionDocument>(doc)
                {
                    Index = IndexName
                });
            }

            var response = await _elasticClient.BulkAsync(bulkRequest);
        }

        public void CreateIndexIfNotExists(IElasticClient _elasticClient)
        {
            var indexState = new IndexState();

            // Check if the index exists
            var indexExistsResponse = _elasticClient.Indices.Exists(IndexName);
            if (!indexExistsResponse.Exists)
            {
                // Create the index
                var createIndexResponse = _elasticClient.Indices.Create(IndexName, d => d
                                            .Index(IndexName)
                                            .Settings(s => s
                                                .NumberOfShards(1)
                                                .NumberOfReplicas(2)
                                                .Analysis(SetupAnalysis()))
                                            .Map<ELSPermissionDocument>(m => m.AutoMap()));

                // Check if the index creation was successful
                if (!createIndexResponse.IsValid)
                {
                    // Handle index creation failure
                    Console.WriteLine($"Failed to create index: {createIndexResponse.DebugInformation}");
                }
            }
        }

        private static Func<AnalysisDescriptor, IAnalysis> SetupAnalysis()
        {
            Func<AnalysisDescriptor, IAnalysis> analysisConfigurator = analysis =>
            {
                analysis.Analyzers(analyzers => analyzers
                .Custom("custom_analyzer", ca => ca
                .Tokenizer("standard")
                .Filters("lowercase", "my_stemmer")));

                analysis.TokenFilters(tokenFilters => tokenFilters
                    .Stemmer("my_stemmer", s => s
                        .Language("english")));
                return analysis;
            };

            return analysisConfigurator;
        }
    }
}
