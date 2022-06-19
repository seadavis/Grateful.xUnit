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

      /// <summary>
      /// 
      /// </summary>
      /// <typeparam name="T">the type of data being returned</typeparam>
      /// <typeparam name="P">the tyoe of data we are posting</typeparam>
      /// <param name="route">the route we are posting to</param>
      /// <returns>the response from the server.</returns>
      Task<HttpResponse<T>> Post<T, P>(string route, P postData);
   }
}
