using Yallocc;

namespace SyntaxTree
{
   public struct SyntaxTreeBuilderResult
   {
      public SyntaxTreeBuilderResult(SyntaxTreeNode root, ParserResult result ) : this()
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
