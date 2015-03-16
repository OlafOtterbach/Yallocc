using System;

namespace CSharpLex
{
   public interface ITokenType : IEquatable<ITokenType>
   {
      int GetHashCode();
   }
}
