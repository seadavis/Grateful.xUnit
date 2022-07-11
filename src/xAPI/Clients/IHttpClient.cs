using System.Net;

namespace xAPI.Clients
{
   public interface IHttpClient
   {

      /// <summary>
      /// Calls a GET method at the given route, 
      /// Authorizing it according to the data provided at
      /// 
      /// </summary>
      /// <typeparam name="T">the type of data contained in the response</typeparam>
      /// <param name="route">the name of the route we want to call in the HttpClient</param>
      /// <returns>the response to be used</returns>
      Task<HttpResponse<T>> GetAuthorized<T>(string route);

      Task<HttpStatusCode> DeleteAuthorized(string route);

      Task<HttpResponse<T>> PostAuthorized<T, P>(string route, P postData);

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

      /// <summary>
      /// Sends a Request of type delete
      /// to the server.
      /// </summary>
      /// <param name="route">the route we are sending data to, just appended to the path</param>
      /// <returns>the response data</returns>
      Task<HttpStatusCode> Delete(string route);
   }
}
