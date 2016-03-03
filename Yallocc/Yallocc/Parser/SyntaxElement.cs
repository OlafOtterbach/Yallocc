/// <summary>Yallocc</summary>
/// <author>Olaf Otterbach</author>
/// <url>https://github.com/OlafOtterbach/Yallocc</url>
/// <date>11.11.2015</date>

using System.Collections.Generic;
using System.Linq;

namespace Yallocc
{
   internal class SyntaxElement
   {
      private SyntaxElement _parentContext;

      public SyntaxElement(Transition transition, SyntaxElement parentContext)
      {
         _parentContext = parentContext;
         Transition = transition;
      }

      public Transition Transition { get; }

      public IEnumerable<SyntaxElement> GetSuccessors()
      {
         var successors = Transition.Successors.Any()
            ? Transition.Successors.Select(t => new SyntaxElement(t, _parentContext))
            : _parentContext != null
               ? _parentContext.GetSuccessors()
               : new List<SyntaxElement>();
         return successors;
      }
   }
}
