using System.Diagnostics;
using OneLakeIntegrationDemo.Models;

namespace OneLakeIntegrationDemo.Services {

  public class CustomerTenantBuilder {

    public static void CreateCustomerTenant(string WorkspaceName) {

      Console.WriteLine("Provision a new Fabric customer tenant");
      FabricWorkspace workspace = FabricUserApi.CreateWorkspace(WorkspaceName, AppSettings.PremiumCapacityId, "A demo workspace creating using Fabrc APIs");

      Console.WriteLine();
      Console.WriteLine("Mission complete");
      Console.WriteLine();

      Console.Write("Press ENTER to open workspace in the browser");
      Console.ReadLine();

      OpenWorkspaceInBrowser(workspace.id);

    }

    public static void CreateCustomerTenantWithUsers(string WorkspaceName) {

      Console.WriteLine("Provision a new Fabric custom tenant");
      FabricWorkspace workspace = FabricUserApi.CreateWorkspace(WorkspaceName, AppSettings.PremiumCapacityId, "A demo workspace creating using Fabrc APIs");

      FabricUserApi.AddWorkspaceUser(workspace.id, AppSettings.TestUser1Id, WorkspaceRole.Admin);
      FabricUserApi.AddWorkspaceUser(workspace.id, AppSettings.TestUser2Id, WorkspaceRole.Viewer);
      FabricUserApi.AddWorkspaceGroup(workspace.id, AppSettings.TestADGroup1, WorkspaceRole.Member);
      FabricUserApi.AddWorkspaceServicePrincipal(workspace.id, AppSettings.ServicePrincipalObjectId, WorkspaceRole.Admin);

      FabricUserApi.ViewWorkspaceRoleAssignments(workspace.id);

      Console.WriteLine();
      Console.WriteLine("Mission complete");
      Console.WriteLine();

      Console.Write("Press ENTER to open workspace in the browser");
      Console.ReadLine();

      OpenWorkspaceInBrowser(workspace.id);

    }

    public static void CreateCustomerTenantAndUploadCsvFiles(string WorkspaceName) {

      string LakehouseName = "sales";

      Console.WriteLine("Provision new customer tenant with Lakehouse");
      FabricWorkspace workspace = FabricUserApi.CreateWorkspace(WorkspaceName, AppSettings.PremiumCapacityId);

      FabricItem lakehouseItem = FabricUserApi.CreateLakehouse(workspace.id, LakehouseName);

      FabricLakehouse lakehouse = FabricUserApi.GetLakehouse(workspace.id, lakehouseItem.id);

      var oneLakeWriter = new OneLakeFileWriter(workspace.id, lakehouse.id);

      var folder = oneLakeWriter.CreateTopLevelFolder("landing_zone");

      Console.WriteLine(" - Uploading data files");

      string targetFolder = AppSettings.LocalDataFilesFolder;

      var csv_files = Directory.GetFiles(targetFolder, "*.csv", SearchOption.AllDirectories);

      foreach (var csv_file in csv_files) {
        var fileName = Path.GetFileName(csv_file);
        var tableName = fileName.Replace(".csv", "");
        Console.WriteLine("   > Uploading " + fileName);
        Stream content = new MemoryStream(File.ReadAllBytes(csv_file));
        oneLakeWriter.CreateFile(folder, fileName, content);

        Console.Write("   > Loading " + fileName);
        FabricUserApi.LoadLakehouseTableFromCsv(workspace.id, lakehouse.id, "Files/landing_zone/" + fileName, tableName);

      }
      Console.WriteLine();


      Console.Write(" - Getting SQL endpoint connection information for lakehouse");
      var sqlEndpoint = FabricUserApi.GetSqlEndpointForLakehouse(workspace.id, lakehouse.id);

      Console.WriteLine("   > Server: " + sqlEndpoint.server);
      Console.WriteLine("   > Database: " + sqlEndpoint.database);
      Console.WriteLine();

      var modelCreateRequest =
        FabricItemTemplateManager.GetDirectLakeSalesModelCreateRequest("Product Sales", sqlEndpoint.server, sqlEndpoint.database);

      var model = FabricUserApi.CreateItem(workspace.id, modelCreateRequest);

      Console.WriteLine(" - Preparing " + model.displayName + " semantic model");

      Console.WriteLine("   > Patching datasource credentials for semantic model");
      PowerBiUserApi.PatchDirectLakeDatasetCredentials(workspace.id, model.id);

      Console.Write("   > Refreshing semantic model");
      PowerBiUserApi.RefreshDataset(workspace.id, model.id);
      Console.WriteLine();

      FabricItemCreateRequest createRequestReport =
        FabricItemTemplateManager.GetSalesReportCreateRequest(model.id, "Product Sales");

      var report = FabricUserApi.CreateItem(workspace.id, createRequestReport);

      Console.WriteLine();
      Console.WriteLine("Customer tenant provisioning complete");
      Console.WriteLine();

      Console.Write("Press ENTER to open workspace in the browser");
      Console.ReadLine();

      WebPageGenerator.GenerateReportPageUserOwnsData(workspace.id, report.id);

      OpenWorkspaceInBrowser(workspace.id);

    }

    public static void CreateCustomerTenantAndUploadParquetFiles(string WorkspaceName) {

      string LakehouseName = "sales";

      Console.WriteLine("Provision new customer tenant with Lakehouse");
      FabricWorkspace workspace = FabricUserApi.CreateWorkspace(WorkspaceName, AppSettings.PremiumCapacityId);

      FabricItem lakehouseItem = FabricUserApi.CreateLakehouse(workspace.id, LakehouseName);

      FabricLakehouse lakehouse = FabricUserApi.GetLakehouse(workspace.id, lakehouseItem.id);

      var oneLakeWriter = new OneLakeFileWriter(workspace.id, lakehouse.id);

      var folder = oneLakeWriter.CreateTopLevelFolder("landing_zone");

      Console.WriteLine(" - Uploading data files");
      string dataFilesFolder = AppSettings.LocalDataFilesFolder;
      var parquet_files = Directory.GetFiles(dataFilesFolder, "*.parquet", SearchOption.AllDirectories);

      foreach (var parquet_file in parquet_files) {
        var fileName = Path.GetFileName(parquet_file);
        var tableName = fileName.Replace(".parquet", "");
        Console.WriteLine("   > Uploading " + fileName);
        Stream content = new MemoryStream(File.ReadAllBytes(parquet_file));
        oneLakeWriter.CreateFile(folder, fileName, content);

        Console.Write("   > Loading " + fileName);
        FabricUserApi.LoadLakehouseTableFromParquet(workspace.id, lakehouse.id, "Files/landing_zone/" + fileName, tableName);

      }
      Console.WriteLine();


      Console.Write(" - Getting SQL endpoint connection information for lakehouse");
      var sqlEndpoint = FabricUserApi.GetSqlEndpointForLakehouse(workspace.id, lakehouse.id);

      Console.WriteLine("   > Server: " + sqlEndpoint.server);
      Console.WriteLine("   > Database: " + sqlEndpoint.database);
      Console.WriteLine();

      var modelCreateRequest =
        FabricItemTemplateManager.GetDirectLakeSalesModelCreateRequest("Product Sales", sqlEndpoint.server, sqlEndpoint.database);

      var model = FabricUserApi.CreateItem(workspace.id, modelCreateRequest);

      Console.WriteLine(" - Preparing " + model.displayName + " semantic model");

      Console.WriteLine("   > Patching datasource credentials for semantic model");
      PowerBiUserApi.PatchDirectLakeDatasetCredentials(workspace.id, model.id);

      Console.Write("   > Refreshing semantic model");
      PowerBiUserApi.RefreshDataset(workspace.id, model.id);
      Console.WriteLine();

      FabricItemCreateRequest createRequestReport =
        FabricItemTemplateManager.GetSalesReportCreateRequest(model.id, "Product Sales");

      var report = FabricUserApi.CreateItem(workspace.id, createRequestReport);

      Console.WriteLine();
      Console.WriteLine("Customer tenant provisioning complete");
      Console.WriteLine();

      Console.Write("Press ENTER to open workspace in the browser");
      Console.ReadLine();

      WebPageGenerator.GenerateReportPageUserOwnsData(workspace.id, report.id);

      OpenWorkspaceInBrowser(workspace.id);


    }

    private static void OpenWorkspaceInBrowser(string WorkspaceId) {

      string url = "https://app.powerbi.com/groups/" + WorkspaceId;

      var process = new Process();
      process.StartInfo = new ProcessStartInfo(@"C:\Program Files\Google\Chrome\Application\chrome.exe");
      process.StartInfo.Arguments = url + " --profile-directory=\"Profile 1\" ";
      //process.StartInfo.Arguments = url + " --profile-directory=\"Profile 9\" ";
      process.Start();

    }

    public static void CreateCustomerTenantWithAllTypes(string WorkspaceName) {

      Console.WriteLine("Provision a new Fabric customer tenant");
      FabricWorkspace workspace = FabricUserApi.CreateWorkspace(WorkspaceName, AppSettings.PremiumCapacityId);

      foreach (string ItemType in FabricItemType.AllTypes) {

        FabricItemCreateRequest createRequest = new FabricItemCreateRequest {
          displayName = ItemType + "Test",
          type = ItemType
        };

        Console.WriteLine("Creating " + createRequest.displayName);

        try {
          FabricUserApi.CreateItem(workspace.id, createRequest);
          Console.WriteLine(ItemType + " created");
        }
        catch (Exception ex) {
          Console.WriteLine(" > Error creating " + ItemType);
          Console.WriteLine(" > " + ex.Message);
          Console.WriteLine();
        }

        Thread.Sleep(6000);
      }

      Console.WriteLine();
      Console.WriteLine("Mission complete");
      Console.WriteLine();

      Console.Write("Press ENTER to open workspace in the browser");
      Console.ReadLine();

      OpenWorkspaceInBrowser(workspace.id);

    }

  }

}
