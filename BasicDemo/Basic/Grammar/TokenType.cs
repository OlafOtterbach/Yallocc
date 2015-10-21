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
      greaterEqual,    // >=
      less,            // <
      lessEqual,       // <
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
      while_keyword,   // WHILE
      for_keyword,     // FOR
      to_keyword,      // TO
      step_keyword,    // STEP
      do_keyword,      // DO
      goto_keyword,    // GOTO
      plot_keyword,    // PLOT
      label,           // Label:
      name,            // x, y, index, ...
      white_space      // _, TAB
   }                   
}                      
                       
                       
                       
                       
                       
                       