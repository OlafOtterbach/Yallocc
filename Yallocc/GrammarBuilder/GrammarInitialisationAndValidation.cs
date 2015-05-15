using System.Collections.Generic;
using System.Linq;

namespace Yallocc
{
   public class GrammarInitialisationAndValidation
   {
      public void ReplaceAndValidateProxiesWithLabels(Transition start)
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
               throw new MissingGotoLabelException(string.Format("Missing target label \"{0}\" for goto command.", missingLabel)) { Label = missingLabel };
            }
            trans.RemoveSuccessors(proxies);
            trans.AddSuccessors(replaces);
            trans.Successors.Where(x => !visited.Contains(x)).ToList().ForEach(t => stack.Push(t));
         }
      }

      private List<Transition> GetNamedTransitions(Transition start)
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
