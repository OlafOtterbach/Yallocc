﻿using System.Linq;
using SyntaxTree;

namespace BasicDemo.Basic
{
   public class DimCommandCreator : CommandCreator
   {
      public DimCommandCreator(BasicEngine engine) : base(engine)
      {}

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode) && ((node as TokenTreeNode).Token.Type == TokenType.dim_keyword);
         return result;
      }

      public override BasicCommand Create(SyntaxTreeNode node)
      {
         var tokNode = (node as TokenTreeNode);
         var children = node.Children.OfType<TokenTreeNode>().ToList();
         var name = children.First().Token.Value;
         var expressions = children.Skip(1)
                                    .TakeWhile(x => x.Token.Type != TokenType.close)
                                    .Select(n => new ExpressionCommandCreator(Engine).Create(n))
                                    .ToArray();
         var command = new DimCommand(tokNode.Token, Engine, name, expressions);

         return command;
      }
   }
}