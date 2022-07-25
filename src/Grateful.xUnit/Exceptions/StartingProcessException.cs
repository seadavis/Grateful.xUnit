namespace Grateful.xUnit.Exceptions
{
   /// <summary>
   /// Use this exception when a process is 
   /// started but it doesn't even work in the first place.
   /// </summary>
   public class StartingProcessException : Exception
   {

      public StartingProcessException(string msg) : base(msg)
      {

      }

   }
}
