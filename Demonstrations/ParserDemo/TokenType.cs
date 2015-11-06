namespace ParserDemo
{
   public enum TokenType
   {
      plus,            // +
      minus,           // -
      mult,            // *
      div,             // /
      open,            // (
      close,           // )
      integer,         // 1, 2, 3, 12, 123, ...
      real,            // 1.0, 12.0, 1.0, 0.2, .4, ...
      white_space      // _, TAB
   }
}
