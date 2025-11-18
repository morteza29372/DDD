using Taban.AssetInventory.Core.Permissions;
using Taban.AssetInventory.DomainClasses.Configs;

namespace Taban.AssetInventory.Application.Resources;

[AppPermission]
public class ResourcePermissions
{
    private const string ApplicationName = "Scm.";
    private const string ControllerName = "Resource.";

    [PermissionOperation(ApplicationName + ControllerName + nameof(View), "نمایش فهرست منبع", ResourcePermissionsCategory.Id)]
    public const long View = 5787407540359168;

    [PermissionOperation(ApplicationName + ControllerName + nameof(Create), "ایجاد منبع", ResourcePermissionsCategory.Id)]
    public const long Create = 5787670569525248;

    [PermissionOperation(ApplicationName + ControllerName + nameof(Update), "ویرایش منبع", ResourcePermissionsCategory.Id)]
    public const long Update = 5787712143466496;

    [PermissionOperation(ApplicationName + ControllerName + nameof(Delete), "حذف منبع", ResourcePermissionsCategory.Id)]
    public const long Delete = 5787746603868160;
}

[PermissionCategory("منبع", ProductionPermissionCategory.Id)]
public class ResourcePermissionsCategory
{
    public const long Id = 5787782041542656;
}
