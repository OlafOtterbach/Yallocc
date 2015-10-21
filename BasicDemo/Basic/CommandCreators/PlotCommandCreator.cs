﻿using SyntaxTree;
using System.Linq;

namespace BasicDemo.Basic
{
   public class PlotCommandCreator : CommandCreator
   {
      public PlotCommandCreator(BasicEngine engine) : base(engine)
      { }

      public override bool CanCreate(SyntaxTreeNode node)
      {
         var result = (node is TokenTreeNode) && ((node as TokenTreeNode).Token.Type == TokenType.plot_keyword);
         return result;
      }

      public override void Create(SyntaxTreeNode node)
      {
         var tokNode = (node as TokenTreeNode);
         var children = node.Children.OfType<TokenTreeNode>().ToList();

         var expressionCreator = new ExpressionCommandCreator(Engine);
         var xExpression = expressionCreator.Create(children[0]);
         var yExpression = expressionCreator.Create(children[1]);
         var colorExpression = expressionCreator.Create(children[2]);
         var command = new PlotCommand(tokNode.Token, Engine, xExpression, yExpression, colorExpression);
         Engine.Add(command);
      }
   }
}
