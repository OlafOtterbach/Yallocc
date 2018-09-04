var lexJs = require("./LexJS")

let Pattern = lexJs.Pattern;
let LexJS = lexJs.LexJS;


console.log("BEGIN DEMO");
console.log("");
console.log("");
demo01();
console.log("");
console.log("");
demo02();
console.log("");
console.log("");
demo03();
console.log("");
console.log("END DEMO");





function demo01() {
    var tokenType = {
        invalid : 0,
        plus : 1,
        minus : 2,
        mult : 3,
        div : 4,
        open : 5,
        close : 6,
        real : 7,
        integer : 8,
        white_space : 9
    }

    var pat1 = new Pattern(/\+/, tokenType.plus);
    var pat2 = new Pattern(/\-/, tokenType.minus);
    var pat3 = new Pattern(/\*/, tokenType.mult);
    var pat4 = new Pattern(/\//, tokenType.div);
    var pat5 = new Pattern(/\(/, tokenType.open);
    var pat6 = new Pattern(/\)/, tokenType.close);
    var pat7 = new Pattern(/(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+/, tokenType.real);
    var pat8 = new Pattern(/(0|1|2|3|4|5|6|7|8|9)+/, tokenType.integer);
    var ignorePat = new Pattern(/( |\t)+/, tokenType.white_space);

    var patterns = [pat1, pat2, pat3, pat4, pat5, pat6, pat7, pat8, ignorePat];
    var ignorePatterns = [ignorePat];

    var lex = new LexJS(patterns, ignorePatterns, tokenType.invalid);
    var tokens = lex.scan("####1 * (2.34##+1)## ####");

    console.log("scan'('####1 * (2.34##+1)## ####')");
    tokens.forEach(x => console.log(x.toString()))
}




function demo02() {
    var code0 = new Pattern(/000/, 0);
    var code1 = new Pattern(/001/, 1);
    var code2 = new Pattern(/010/, 2);
    var code3 = new Pattern(/011/, 3);
    var code4 = new Pattern(/100/, 4);
    var code5 = new Pattern(/101/, 5);
    var code6 = new Pattern(/110/, 6);
    var code7 = new Pattern(/111/, 7);

    var codePatterns = [code0, code1, code2, code3, code4, code5, code6, code7];
    var codeIgnorePatterns = [];

    var codeLex = new LexJS(codePatterns, codeIgnorePatterns, null);

    console.log("scan('101111110110010000110001111110')");
    var codeSequence = codeLex.scan("101111110110010000110001111110");
    codeSequence.forEach(x => console.log(x.toString()))

    console.log("");
    console.log("scan('10111111#110010000110001111110')");
    var codeSequence2 = codeLex.scan("10111111#110010000110001111110");
    codeSequence2.forEach(x => console.log(x.toString()))
}




function demo03() {
    var tokenType = {
        program_keyword : 0, // PROGRAM
        end_keyword : 1,     // END
        plus : 2,            // +
        minus : 3,           // -
        mult : 4,            // *
        div : 5,             // /
        equal : 6,           // =
        greater : 7,         // >
        greaterEqual : 8,    // >=
        less : 9,            // <
        lessEqual : 10,       // <=
        open : 11,            // (
        close : 12,           // )
        comma : 13,           // ,
        colon : 14,           // :
        Return : 15,          // \n
        integer : 16,         // 1, 2, 3, 12, 123, ...
        real : 17,            // 1.0, 12.0, 1.0, 0.2, .4, ...
        text : 18,            // "Hallo", ...
        and_keyword : 19,     // AND
        or_keyword : 20,      // OR
        not_keyword : 21,     // NOT
        mod_keyword : 22,     // MOD
        dim_keyword : 23,     // DIM
        let_keyword : 24,     // LET
        if_keyword : 25,      // IF
        then_keyword : 26,    // THEN
        else_keyword : 27,    // ELSE
        while_keyword : 28,   // WHILE
        for_keyword : 29,     // FOR
        to_keyword : 30,      // TO
        step_keyword : 31,    // STEP
        do_keyword : 32,      // DO
        goto_keyword : 33,    // GOTO
        plot_keyword : 34,    // PLOT
        label : 35,           // Label:
        name : 36,            // x, y, index, ...
        white_space : 37      // _, TAB
     }
 
     var pat01 = new Pattern(/PROGRAM/, tokenType.program_keyword);
     var pat02 = new Pattern(/END/, tokenType.end_keyword);
     var pat03 = new Pattern(/\+/, tokenType.plus);
     var pat04 = new Pattern(/\-/, tokenType.minus);
     var pat05 = new Pattern(/\*/, tokenType.mult);
     var pat06 = new Pattern(/\//, tokenType.div);
     var pat07 = new Pattern(/=/, tokenType.equal);
     var pat08 = new Pattern(/\>/, tokenType.greater);
     var pat09 = new Pattern(/\>=/, tokenType.greaterEqual);
     var pat10 = new Pattern(/\</, tokenType.less);
     var pat11 = new Pattern(/\<=/, tokenType.lessEqual);
     var pat12 = new Pattern(/\(/, tokenType.open);
     var pat13 = new Pattern(/\)/, tokenType.close);
     var pat14 = new Pattern(/,/, tokenType.comma);
     var pat15 = new Pattern(/:/, tokenType.colon);
     var pat16 = new Pattern(/r\n/, tokenType.Return);
     var pat17 = new Pattern(/(0|1|2|3|4|5|6|7|8|9)*\.(0|1|2|3|4|5|6|7|8|9)+/, tokenType.real);
     var pat18 = new Pattern(/(0|1|2|3|4|5|6|7|8|9)+/, tokenType.integer);
     var pat19 = new Pattern(/".*\"/, tokenType.text);
     var pat20 = new Pattern(/DIM/, tokenType.dim_keyword);
     var pat21 = new Pattern(/LET/, tokenType.let_keyword);
     var pat22 = new Pattern(/IF/, tokenType.if_keyword);
     var pat23 = new Pattern(/THEN/, tokenType.then_keyword);
     var pat24 = new Pattern(/ELSE/, tokenType.else_keyword);
     var pat25 = new Pattern(/WHILE/, tokenType.while_keyword);
     var pat26 = new Pattern(/FOR/, tokenType.for_keyword);
     var pat27 = new Pattern(/TO/, tokenType.to_keyword);
     var pat28 = new Pattern(/STEP/, tokenType.step_keyword);
     var pat29 = new Pattern(/DO/, tokenType.do_keyword);
     var pat30 = new Pattern(/GOTO/, tokenType.goto_keyword);
     var pat31 = new Pattern(/PLOT/, tokenType.plot_keyword);
     var pat32 = new Pattern(/NOT/, tokenType.not_keyword);
     var pat33 = new Pattern(/AND/, tokenType.and_keyword);
     var pat34 = new Pattern(/OR/, tokenType.or_keyword);
     var pat35 = new Pattern(/MOD/, tokenType.mod_keyword);
     var pat36 = new Pattern(/(\w)+(\w|\d)*/, tokenType.name);
     var ignorePat = new Pattern(/( |\t|\r\n)+/, tokenType.white_space);

     var patterns = [pat01, pat02, pat03, pat04, pat05, pat06, pat07, pat08, pat09, pat10,
        pat11, pat12, pat13, pat14, pat15, pat16, pat17, pat18, pat19, pat20,
        pat21, pat22, pat23, pat24, pat25, pat26, pat27, pat28, pat29, pat30,
        pat31, pat32, pat33, pat34, pat35, pat36, ignorePat];
     var ignorePatterns = [ignorePat];

     var text = basic();
     var lex = new LexJS(patterns, ignorePatterns, tokenType.invalid);
     var tokens = lex.scan(text);
 
     console.log("Basic Program");
     tokens.forEach(x => console.log(x.toString()))
      
 }


function basic() {
var text = 
"LET xmin = 0.763\r\n\
LET xmax = 0.768\r\n\
LET ymin = 0.0999\r\n\
LET ymax = 0.103\r\n\
LET depthMax = 254\r\n\
\r\n\
DIM map(160, 200)\r\n\
LET dx = (xmax - xmin) / 159.0\r\n\
LET dy = (ymax - ymin) / 199.0\r\n\
LET cx = xmin\r\n\
LET cy = ymax\r\n\
\r\n\
FOR row = 0 TO 199 DO\r\n\
  FOR col = 0 TO 159 DO\r\n\
\r\n\
    LET depth = 0\r\n\
    LET xval = 0.0\r\n\
    LET yval = 0.0\r\n\
    LET xquad = 0.0\r\n\
    LET yquad = 0.0\r\n\
\r\n\
    WHILE (depth < depthMax) AND (xquad + yquad < 8) DO\r\n\
      LET yval = 2 * xval * yval - cy\r\n\
      LET xval = xquad - yquad - cx\r\n\
      LET xquad = xval * xval\r\n\
      LET yquad = yval * yval\r\n\
      LET depth = depth + 1\r\n\
    END\r\n\
\r\n\
    IF depth = depthMax THEN\r\n\
      LET color = 0\r\n\
    ELSE\r\n\
      LET color = (depth MOD 3) + 1\r\n\
    END\r\n\
    LET map(col, row) = color\r\n\
\r\n\
    LET cx = cx + dx\r\n\
  END\r\n\
  LET cx = xmin\r\n\
  LET cy = cy - dy\r\n\
END\r\n\
\r\n\
FOR y = 0 TO 199 DO\r\n\
  FOR x = 0 TO 159 DO\r\n\
    PLOT x, y, map(x,y)\r\n\
  END\r\n\
END\r\n\
END";

return text;
}
















