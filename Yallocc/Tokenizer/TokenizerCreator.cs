﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Yallocc.Tokenizer
{
   public abstract class TokenizerCreator<T> where T : struct
   {
      private List<Pattern<T>> _patterns;
      private List<Pattern<T>> _patternsToIgnore;
      private RegexOptions _regexOptions;


      public TokenizerCreator()
      {
         _patterns = new List<Pattern<T>>();
         _patternsToIgnore = new List<Pattern<T>>();
         _regexOptions = RegexOptions.CultureInvariant | RegexOptions.Compiled;
      }


      public bool IsToCompile
      {
         get
         {
            return (_regexOptions & RegexOptions.Compiled) == RegexOptions.Compiled;
         }
         set
         {
            _regexOptions = value ? RegexOptions.Compiled : (~RegexOptions.Compiled) & _regexOptions;
         }
      }
      

      public void Register(string patternText, T tokenType)
      {
         _patterns.Add(CreatePattern(patternText, tokenType));
      }


      public void RegisterIgnorePattern(string patternText, T tokenType)
      {
         _patternsToIgnore.Add(CreatePattern(patternText, tokenType));
      }


      public bool IsComplete()
      {
         var allPatterns = _patterns.Concat(_patternsToIgnore);
         if (!allPatterns.Any())
         {
            return false;
         }

         bool isEnum = allPatterns.First().TokenType is Enum;
         if (isEnum)
         {
            var isComplete = Enum.GetValues(typeof(T)).OfType<T>().All(tokType => allPatterns.Any(x => x.TokenType.Equals(tokType)));
            return isComplete;
         }
         else
         {
            throw new TokenIsNotAnEnumTypeException("Can not test on completeness. Type is not enum type");
         }
      }


      public abstract Tokenizer<T> Create();


      private Pattern<T> CreatePattern(string patternText, T tokenType)
      {
         if (_patterns.Any(p => p.TokenType.Equals(tokenType)))
         {
            throw new TokenRegisteredMoreThanOneTimeException<T>(tokenType, "Not allowed to register Token more than one time");
         }

         var regexPattern = new Regex(patternText, _regexOptions);
         var pattern = new Pattern<T>(regexPattern, tokenType);
         return pattern;
      }
   }
}
