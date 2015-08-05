﻿using LexSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicDemo.Basic
{
   public class ExpressionCommand
   {
      private Token<TokenType> _startToken;
      private List<BasicEntity> _postOrderExpression;

      public ExpressionCommand(Token<TokenType> startToken, IEnumerable<BasicEntity> expression)
      {
         _postOrderExpression = expression.ToList();
         _startToken = startToken;
      }

      public BasicEntity Execute()
      {
         BasicEntity result = null;
         try
         {
            var stack = new Stack<BasicEntity>();
            foreach (var entity in _postOrderExpression)
            {
               if (entity.IsVariable)
               {
                  stack.Push(entity);
               }
               else if (entity.IsUnaryOperator)
               {
                  stack.Push((entity as BasicUnaryOperator).Execute(stack.Pop()));
               }
               else
               {
                  var left = stack.Pop();
                  var right = stack.Pop();
                  stack.Push((entity as BasicBinaryOperator).Execute(left, right));
               }
            }
            result = stack.Pop();
         }
         catch(BasicTypeMissmatchException typeMissmatchException)
         {
            typeMissmatchException.StartPosition = _startToken.TextIndex;
            throw typeMissmatchException;
         }
         catch (InvalidOperationException e)
         {
            throw e;
         }

         return result;
      }


   }
}
