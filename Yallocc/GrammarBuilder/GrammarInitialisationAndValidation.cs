﻿using System.Collections.Generic;
using System.Linq;

namespace Yallocc
{
   public static class GrammarInitialisationAndValidation
   {
      public static void ReplaceProxiesInGrammarTransitions(GrammarDictionary grammars)
      {
         foreach (var transition in grammars.GetStartTransitions())
         {
            var stack = new Stack<Transition>();
            var visited = new List<Transition>();
            stack.Push(transition);
            while (stack.Count() > 0)
            {
               var trans = stack.Pop();
               visited.Add(trans);
               trans.Successors
                    .OfType<GrammarTransition>()
                    .Where(x => x.Start is ProxyTransition)
                    .Where(x => grammars.Contains((x.Start as ProxyTransition).TargetName))
                    .ToList()
                    .ForEach(x => x.Start = grammars.GetGrammar((x.Start as ProxyTransition).TargetName));
               trans.Successors.Where(x => !visited.Contains(x)).ToList().ForEach(t => stack.Push(t));
            }
         }
      }

      public static bool AnyProxyTransitions(GrammarDictionary grammars)
      {
         var found = false;
         foreach (var transition in grammars.GetStartTransitions())
         {
            var stack = new Stack<Transition>();
            var visited = new List<Transition>();
            stack.Push(transition);
            while ((!found) && (stack.Count() > 0))
            {
               var trans = stack.Pop();
               visited.Add(trans);
               found = (trans is ProxyTransition) || ((trans is GrammarTransition) && ((trans as GrammarTransition).Start is ProxyTransition));
               trans.Successors.Where(x => !visited.Contains(x)).ToList().ForEach(t => stack.Push(t));
            }

            if(found)
            {
               break;
            }
         }
         return found;
      }

      public static void ReplaceAndValidateProxiesWithLabels(Transition start)
      {
         var namedTransitions = GetNamedTransitions(start);
         var stack = new Stack<Transition>();
         var visited = new List<Transition>();
         stack.Push(start);
         while (stack.Count() > 0)
         {
            var trans = stack.Pop();
            visited.Add(trans);
            var proxies = trans.Successors.OfType<ProxyTransition>().Cast<ProxyTransition>().ToList();
            var replaces = namedTransitions.Where(x => proxies.Where(p => p.TargetName == x.Name).Any()).ToList();
            if (replaces.Count != proxies.Count)
            {
               var missingLabel = proxies.Where(proxy => replaces.All(x => x.Name != proxy.TargetName)).First().TargetName;
               throw new GrammarBuildingException(string.Format("Missing target label \"{0}\" for goto command.", missingLabel)) { Label = missingLabel, HasUndefinedGotoLabel = true };
            }
            trans.RemoveSuccessors(proxies);
            trans.AddSuccessors(replaces);
            trans.Successors.Where(x => !visited.Contains(x)).ToList().ForEach(t => stack.Push(t));
         }
      }

      private static List<Transition> GetNamedTransitions(Transition start)
      {
         var namedTransitions = new List<Transition>();
         var stack = new Stack<Transition>();
         var visited = new List<Transition>();
         stack.Push(start);
         while (stack.Count() > 0)
         {
            var trans = stack.Pop();
            visited.Add(trans);
            if (trans.Name != string.Empty)
            {
               namedTransitions.Add(trans);
            }
            trans.Successors.Where(x => !visited.Contains(x)).ToList().ForEach(t => stack.Push(t));
         }
         return namedTransitions;
      }
   }
}