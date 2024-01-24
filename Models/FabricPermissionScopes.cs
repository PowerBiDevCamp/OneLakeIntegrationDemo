using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneLakeIntegrationDemo.Models {

  public class FabricPermissionScopes {

    public const string resourceUri = "https://api.fabric.microsoft.com/";

    // used for service principal token acquisition
    public static readonly string[] PowerBiDefault = new string[] {
      "https://analysis.windows.net/powerbi/api/.default"
    };

    // used for user token acquisition
    public static readonly string[] TenantProvisioning = new string[] {
      "https://api.fabric.microsoft.com/Capacity.ReadWrite.All",
      "https://api.fabric.microsoft.com/Workspace.ReadWrite.All",
      "https://api.fabric.microsoft.com/Lakehouse.ReadWrite.All",
      "https://api.fabric.microsoft.com/Lakehouse.Execute.All",
      "https://api.fabric.microsoft.com/Item.ReadWrite.All",
      "https://api.fabric.microsoft.com/Item.Read.All",
      "https://api.fabric.microsoft.com/Item.Execute.All",
      "https://api.fabric.microsoft.com/Content.Create",
      "https://api.fabric.microsoft.com/Dataset.ReadWrite.All ",
      "https://api.fabric.microsoft.com/Report.ReadWrite.All",
    };

    // used for user token acquisition
    public static readonly string[] TenantProvisioningPowerBi = new string[] {
      "https://analysis.windows.net/powerbi/api/Capacity.ReadWrite.All",
      "https://analysis.windows.net/powerbi/api/Workspace.ReadWrite.All",
      "https://analysis.windows.net/powerbi/api/Lakehouse.ReadWrite.All",
      "https://analysis.windows.net/powerbi/api/Lakehouse.Execute.All",
      "https://analysis.windows.net/powerbi/api/Item.ReadWrite.All",
      "https://analysis.windows.net/powerbi/api/Item.Read.All",
      "https://analysis.windows.net/powerbi/api/Item.Execute.All",
      "https://analysis.windows.net/powerbi/api/Content.Create",
      "https://analysis.windows.net/powerbi/api/Dataset.ReadWrite.All ",
      "https://analysis.windows.net/powerbi/api/Report.ReadWrite.All",
    };

    // used for service principal token acquisition
    public static readonly string[] Default = new string[] {
      "https://api.fabric.microsoft.com/.default"
    };

    // not used in this sample application
    public static readonly string[] AdminScopes = new string[] {
      "https://api.fabric.microsoft.com/Capacity.ReadWrite.All",
      "https://api.fabric.microsoft.com/Workspace.ReadWrite.All",
      "https://api.fabric.microsoft.com/Item.ReadWrite.All",
      "https://api.fabric.microsoft.com/Content.Create",
      "https://api.fabric.microsoft.com/Dashboard.ReadWrite.All",
      "https://api.fabric.microsoft.com/Dataflow.ReadWrite.All ",
      "https://api.fabric.microsoft.com/Dataset.ReadWrite.All ",
      "https://api.fabric.microsoft.com/Report.ReadWrite.All",
      "https://api.fabric.microsoft.com/UserState.ReadWrite.All",
      "https://api.fabric.microsoft.com/Tenant.ReadWrite.All"
    };

  }
}




