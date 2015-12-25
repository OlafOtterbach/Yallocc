# Yallocc
Yet Another LL-One Compiler Compiler - A parser generator in c# using a syntax diagram decription for generating a parser. 

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

Yallocc.SyntaxTree
------------------
Almost always it is needed to get the structure for the syntax of
a text in a data structure. The syntax tree generator includes
the yallocc and transformates a text to a syntax tree data
structure.
In a grammar for the syntax tree generator the building of the tree 
is added to the grammar definition by the fluent interface.
Sveral demonstrations and tests are showing how to do this.

Olaf otterbach, 23.12.2015

Beside the unit tests several demonstrations are showing
the usage of the tokenizer, the parser and the syntaxtree generator.


SyntaxTreeDemo
--------------
Demonstrates creating a syntax tree throughout
a grammar.
A mathematical expression is parsed, converted
to a syntax tree then transformed to a
dot.net expression tree and then compiled and executed.
The result of the calculation is printed out.
This demonstration shows in a simple way how to use 
the full support to transoform a text to a
syntaxtree.


Demonstartions
--------------

BasicDemo
---------
Demonstrates a steam machine of a basic interpreter.
The demo basic program calculates an appleman graphices
with the incredible amount of 33.000.000 instances of
basic types in a long time of a several minutes.
Awful? But it does not matter.
It shows how to define a basic grammar, how
to create a syntax tree and how to convert it to
basic token commands.

Tokenizer Demo
--------------
Demonstrates use of a tokenizer.
Analyzes the tokens of a mathematical expression.
The result is represented in a list of tokens.

Parser Demo
-----------
Demonstrates use of yalloc parser by defining
a grammar for a mathematical expression.
The demonstration analyses the input and
marks errors if existing.

RawParser Demo
--------------
Demonstrates use of the raw parser without
integrated tokenizer by defining
a grammar for a mathematical expression.
The demonstration analyses the input and
marks errors if existing.


ScanningBitGroupsDemo
---------------------
Genrates a large text of bit groups of 000, 001, ... , 111 with
overlapping patterns. Then the text is scanned and analysed.

ScanningBasicDemo
-----------------
Genrates a large text of basic key words and symbols
Then the text is scanned and analysed.

Unit-Tests
----------
/Yallocc/BasicDemoTests/Grammar/ParallelGeneratingTest.cs

shows the paralleization of the parsing of a text.


 
