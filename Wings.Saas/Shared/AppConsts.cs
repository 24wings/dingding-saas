using System.Collections.Generic;

namespace Wings.Saas.Shared
{
  public class AppConsts
  {
    static readonly string tenancyNamePlaceHolderInUrl = "{TENANCY_NAME}";

    public static string remoteServiceBaseUrl = "http://localhost:5000";
    static string remoteServiceBaseUrlFormat;
    public static string appBaseUrl;
    public static string AppBaseHref; // returns angular's base-href parameter value if used during the publish
    static string appBaseUrlFormat;
    static int recaptchaSiteKey { get; set; }
    static int subscriptionExpireNotifyDayCount;

    static Dictionary<string, string> localeMappings { get; set; }

    public static readonly UserManagement userManagement = new UserManagement
    {
      DefaultAdminUserName = "admin"
    };

    static readonly Localization localization = new Localization
    {
      DefaultLocalizationSourceName = "eShopLinker"
    };

    static readonly Authorization authorization = new Authorization
    {
      EncryptedAuthTokenName = "enc_auth_token"
    };

  }

  public class UserManagement
  {
    public string DefaultAdminUserName { get; set; }
  }
  public class Localization
  {
    public string DefaultLocalizationSourceName { get; set; }
  }
  public class Authorization
  {
    public string EncryptedAuthTokenName { get; set; }
  }

}