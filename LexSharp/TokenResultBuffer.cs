namespace LexSharp
{
   public class TokenResultBuffer<T>
   {
      private TokenResult<T> _buffer;

      public TokenResultBuffer()
      {
         _buffer = default(TokenResult<T>);
         IsEmpty = true;
      }

      public bool IsEmpty { get; private set; }

      public TokenResult<T> Content
      {
         set
         {
            _buffer = value;
            IsEmpty = false;
         }
         get
         {
            IsEmpty = true;
            var result = _buffer;
            _buffer = default(TokenResult<T>);
            return result;
         }
      }
   }
}
