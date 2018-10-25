///////////////////////////////////////////////////////////////////////
////
// Module LexJS
////
///////////////////////////////////////////////////////////////////////
module.exports = {
    Token: Token,
    Pattern: Pattern,
    LexJS: LexJS
}

///////////////////////////////////////////////////////////////////////
// LexJS
///////////////////////////////////////////////////////////////////////
function LexJS(patterns, patternsToIgnore, invalidType)
{
    this.patterns = patterns;
    this.patternsToIgnore = patternsToIgnore;
    this.invalidType = invalidType;
}

LexJS.prototype.scan = function(text) {
    var textCursor = new TextCursor(text, this.patterns, this.invalidType);
    var validTokens = [];
    do
    {
         var token = textCursor.getNextToken();
         var validToken = token != null && this.patternsToIgnore.filter(tok => tok.tokenType == token.type).length == 0;
         if(validToken) validTokens.push(token);
    }
    while(textCursor.isNotFinished)

    return validTokens;
}




///////////////////////////////////////////////////////////////////////
// Token
///////////////////////////////////////////////////////////////////////
function Token(value, type, textIndex, length) {
    this.value = value;
    this.type = type;
    this.textIndex = textIndex
    this.length = length;
}

Token.prototype.toString = function() {
    var text = "Token('" + this.value + "', " + this.type + ", " + this.textIndex + ", " + this.length + ")";
    return text;
}




///////////////////////////////////////////////////////////////////////
// Pattern
///////////////////////////////////////////////////////////////////////
function Pattern(pattern, tokenType) {
    this.tokenPattern = pattern;
    this.tokenType = tokenType;
}




///////////////////////////////////////////////////////////////////////
// PatternAndMatch
///////////////////////////////////////////////////////////////////////
function PatternAndMatch() {
    this.patternIndex = 0;
    this.pattern = null;
    this.match = new RegExp;
}




///////////////////////////////////////////////////////////////////////
// TokenResultBuffer
///////////////////////////////////////////////////////////////////////
function TokenResultBuffer()
{
    this.buffer = null;
}
TokenResultBuffer.prototype.isEmpty = function () {
    return this.buffer == null;
}
TokenResultBuffer.prototype.setContent = function (token) {
    this.buffer = token;
}
TokenResultBuffer.prototype.getContent = function () {
    var content = this.buffer;
    this.buffer = null;
    return content;
}




///////////////////////////////////////////////////////////////////////
// TextCursor
///////////////////////////////////////////////////////////////////////
function TextCursor(text, patterns, invalidType)
{
    this.text = text;
    this.textLength = text.length;
    this.invalidType = invalidType;
    this.absCursorPos = 0;
    this.buffer = new TokenResultBuffer();
    this.isNotFinished = true;
    this.matches = patterns.map(function (p, i) {
        var item = new PatternAndMatch();
        item.patternIndex = i;
        item.pattern = p;
        item.match = p.tokenPattern.exec(text);

        return item;
    }).filter(function (item) {
        return item.match != null;
    });
}
TextCursor.prototype.getNextToken = function() 
{
    // Return buffer content if not empty
    if(!this.buffer.isEmpty())
    {
        return this.buffer.getContent();
    }

    // Get next matches
    this.matches = this.matches.filter(m => (m.match != null) && (m.match.index >= 0));
    var minimalPatternIndexAndLongestMininimalMatches = [];
    if(this.matches.length > 0)
    {
        var minIndex = this.matches.map(m => m.match.index).reduce((acc, x) => Math.min(acc, x) );
        var minMatches = this.matches.filter(match => match.match.index == minIndex);

        var maxLength = minMatches.map(m => m.match[0].length).reduce((acc, x) => Math.max(acc, x));
        var longestMinimalMatches = minMatches.filter(m => m.match[0].length == maxLength);

        var minPatternIndex = longestMinimalMatches.map(m => m.patternIndex).reduce((acc,x) => Math.min(acc, x));
        var minimalPatternIndexAndLongestMininimalMatches = longestMinimalMatches.filter(x => x.patternIndex == minPatternIndex);
    }

    if (minimalPatternIndexAndLongestMininimalMatches.length > 0)
    { // Matches are found
        var offset = this.absCursorPos;
        var tokens = minimalPatternIndexAndLongestMininimalMatches.map(x => new Token(x.match[0], x.pattern.tokenType, offset + x.match.index, x.match[0].length));
        var tokenResult = tokens[0];
        var index = minimalPatternIndexAndLongestMininimalMatches[0].match.index;
        var actualCursorPos = this.absCursorPos;
        var relCursorPos = index + ((tokenResult.length > 0) ? tokenResult.length : 1);
        this.absCursorPos = this.absCursorPos + relCursorPos;
        var originalText = this.text;

        // Scan next matches
        if(this.absCursorPos <= this.textLength)
        {
            this.text = this.text.substring(relCursorPos);//, this.text.length - relCursorPos + 1);
            if(this.text != "")
            {
                var text = this.text;
                this.matches.forEach(match => match.match = match.pattern.tokenPattern.exec(text));
            }
            else
            {
                this.isNotFinished = false;
            }
        }

        // Evaluate current result
        if (tokenResult.textIndex == actualCursorPos)
        {
            return tokenResult;
        }
        else
        {
            var length = tokenResult.textIndex - actualCursorPos;
            this.buffer.setContent(tokenResult);
            var untokenResult = new Token(originalText.substring(0, length), this.invalidType, actualCursorPos, length);
            return untokenResult;
        }
    }
    else
    { // No matches any more

        var length = this.textLength - this.absCursorPos;
        this.isNotFinished = length > 0;

        if (this.isNotFinished)
        {  // Any text to return at the end?
            //var restResult = new Token(this.text.substring(this.absCursorPos, length), this.absCursorPos, length);
            var restResult = new Token(this.text, this.invalidType, this.absCursorPos, this.text.length);
            this.absCursorPos = this.text.Length;
            this.isNotFinished = false;
            return restResult;
        }
        else
        { // No token any more
            var emptyToken = new Token();
            return emptyToken;
        }
    }
}
