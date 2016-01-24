using Yallocc;

namespace Yallocc.SyntaxTree
{
   public struct SyntaxTreeResult<T> where T : struct
   {
      public SyntaxTreeResult(SyntaxTreeNode root, ParserResult<T> result ) : this()
      {
         Root = root;
         ParserResult = result;
      }
  
      public SyntaxTreeNode Root { get; }

      public ParserResult<T> ParserResult { get; }

      public bool Success
      {
         get
         {
            return ParserResult.Success && (Root != null);
         }
      }
   }
}
