namespace xAPI.Clients
{
   public interface IHttpClient
   {

      /// <summary>
      /// 
      /// </summary>
      /// <typeparam name="T">The type of data to deserialize into</typeparam>
      /// <param name="route">The name of the route we ant to call in the HttpClient</param>
      Task<HttpResponse<T>> Get<T>(string route);

   }
}
