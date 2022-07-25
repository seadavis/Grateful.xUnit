namespace Grateful.xUnit.Config
{
   /// <summary>
   /// Represents the configuration
   /// for a "Client" in the ASP.Net
   /// collection
   /// </summary>
   public class ClientConfig
   {

      /// <summary>
      /// The client id of the 
      /// application, that is authenticatiing
      /// via MSAL
      /// </summary>
      public string AppClientId { get; set; }

      /// <summary>
      /// The client id of the API
      /// </summary>
      public string APIClientId { get; set; }

      /// <summary>
      /// The client secret of the application
      /// authentication via MSAL
      /// </summary>
      public string ClientSecret { get; set; }

      /// <summary>
      /// The id of the Azure directory
      /// authenticating via MSAL.
      /// </summary>
      public string TenantId { get; set; }

   }
}
