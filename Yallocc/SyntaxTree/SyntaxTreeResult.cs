using Yallocc;

namespace Yallocc.SyntaxTree
{
   public struct SyntaxTreeResult
   {
      public SyntaxTreeResult(SyntaxTreeNode root, ParserResult result ) : this()
      {
         Root = root;
         ParserResult = result;
      }
  
      public SyntaxTreeNode Root { get; set; }
      public ParserResult ParserResult { get; set; }
      public bool Success
      {
         get
         {
            return ParserResult.Success && (Root != null);
         }
      }
   }
}
