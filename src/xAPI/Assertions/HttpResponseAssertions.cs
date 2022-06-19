using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System.Net;
using xAPI.Clients;

namespace xAPI.Assertions
{
   /// <summary>
   /// This calss is used to 
   /// assert different properties of 
   /// data returned from the HttpClient
   /// </summary>
   /// <typeparam name="T">The type of data returned from the HttpClient</typeparam>
   public class HttpResponseAssertions<T> : ReferenceTypeAssertions<HttpResponse<T>, HttpResponseAssertions<T>>
   {
      public HttpResponseAssertions(HttpResponse<T> subject) : base(subject)
      {
      }

      protected override string Identifier => "HttpResonse";

      public AndConstraint<HttpResponseAssertions<T>> BeOkWithData(T data)
      {
         Subject.Should().HaveCodeAndData(HttpStatusCode.OK, data);
         return new AndConstraint<HttpResponseAssertions<T>>(this);
      }

      public AndConstraint<HttpResponseAssertions<T>> HaveCodeAndData(HttpStatusCode code, T data)
      {
         Subject.Data.Should()
                     .BeEquivalentTo(data);

         Subject.Status.Should().Be(code);

         return new AndConstraint<HttpResponseAssertions<T>>(this);
      }

   }
}
