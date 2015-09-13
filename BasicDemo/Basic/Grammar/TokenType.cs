namespace BasicDemo.Basic
{
   public enum TokenType
   {
      program_keyword, // PROGRAM
      end_keyword,     // END
      plus,            // +
      minus,           // -
      mult,            // *
      div,             // /
      equal,           // =
      greater,         // >
      less,            // <
      open,            // (
      close,           // )
      comma,           // ,
      colon,           // :
      Return,          // \n
      integer,         // 1, 2, 3, 12, 123, ...
      real,            // 1.0, 12.0, 1.0, 0.2, .4, ...
      text,            // "Hallo", ...
      dim_keyword,     // DIM
      let_keyword,     // LET
      if_keyword,      // IF
      then_keyword,    // THEN
      else_keyword,    // ELSE
      goto_keyword,    // GOTO
      label,           // Label:
      name,            // x, y, index, ...
      white_space      // _, TAB
   }                   
}                      
                       
                       
                       
                       
                       
                       