Olaf otterbach, 25.12.2015


Yallocc
-------
= [Y]et [A]nother [L][L]-[O]ne [C]ompiler [C]ompiler
Yallocc is a parser generator that generates the parser from a
syntax diagram (https://en.wikipedia.org/wiki/Syntax_diagram) definition.
IIt is easier to descripe a grammar in a syntax diagram as in
the Extended Backus–Naur Form (EBNF) the YACC is using.
A example for it is the definition of MODULA 2 from Niklaus Wirth
with syntax diagrams.
Thanks to the fluent interface it is possible to descripe
syntax diagrams in a simple way in c# without needing a
grammar definition file.
Also for the definition of the tokens a fluent interface is
used in the yallocc.
After defining the tokens and the grammer, the compiler with
tokenizer can be generated.
This generated compiler gets the text to parse, the included
tokenizer tranlates the text to tokens and the included
parser parses the token sequence.

Tokenizer
---------
The tokenizer project is the base of LeTok and LexSharp.
The tokenizer gets regular expressen patterns for every token
to define. For example it can has tokens for integer or
real numbers, for keywords of a programming language and so on.
By scanning a text the tokenizer matches all its
defined tokens in the text and transforms it to a sequence
of tokens. Every token includes the text it has matched
and the position in the text. 
The token sequence is the input of the parser.
The tokens are defined in a tokenizer creator and this
creator creates the tokenizer.
Unmatched tokens are included in the sequence as undefined tokens.

Tokenizer.LeTok
---------------
[Le]xical [Tok]enizer
The LeTok tokenizer matches the tokens by one combined
regular expression and returns the sequence of tokens.
The avantage is, that it should be a fast scanning by
regular expression.
The disadvantage is, that the matching depends on
the sequence of pattern definition.
If desired to get the longest match the "longer"
patterns have to defined before the "shorter" patterns.
For definitions like aa(b)*aa this could be difficult.

Tokenizer.LexSharp
------------------
The lex sharp tokenizer is independend from sequence
of defined patterns. It always matches the longest
patterns for tokens.

Tokenizer.LexJS
-------------------
The lex JavaScript tokenizer based on lex sharp is
a tokenizer for JavaScript.

Yallocc.SyntaxTree
------------------
Almost always it is needed to get the structure for the syntax of
a text in a data structure. The syntax tree generator includes
the yallocc and transformates a text to a syntax tree data
structure.
In a grammar for the syntax tree generator the building of the tree 
is added to the grammar definition by the fluent interface.
Sveral demonstrations and tests are showing how to do this.



 