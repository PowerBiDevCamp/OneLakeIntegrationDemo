using OneLakeIntegrationDemo.Services;

string WorkspaceName = "Acme Corp";

// Demo 1
CustomerTenantBuilder.CreateCustomerTenantAndUploadParquetFiles(WorkspaceName);

// Demo 2
//CustomerTenantBuilder.CreateCustomerTenantAndUploadCsvFiles(WorkspaceName);