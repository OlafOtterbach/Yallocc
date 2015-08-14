namespace BasicDemo.Basic
{
   public enum TokenType
   {
      program,       // PROGRAM
      plus,          // +
      minus,         // -
      mult,          // *
      div,           // /
      equal,         // =
      greater,       // >
      less,          // <
      open,          // (
      close,         // )
      comma,         // ,
      Return,        // \n
      integer,       // 1, 2, 3, 12, 123, ...
      real,          // 1.0, 12.0, 1.0, 0.2, .4, ...
      text,          // "Hallo", ...
      dim,           // DIM
      let,           // LET
      name,          // x, y, index, ...
      white_space    // _, TAB
   }
}
