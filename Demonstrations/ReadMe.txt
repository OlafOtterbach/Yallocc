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
