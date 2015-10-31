using System.Collections.Generic;

namespace Yallocc
{
   public class GrammarDictionary
   {
      private Dictionary<string, Transition> _grammars;

      public GrammarDictionary()
      {
         _grammars = new Dictionary<string, Transition>();
         MasterGrammar = string.Empty;
      }

      public string MasterGrammar { get; set; }

      public bool Contains(string name)
      {
         return _grammars.ContainsKey(name);
      }

      public void AddGrammar(string name, Transition startTransition)
      {
         _grammars.Add(name, startTransition);
      }

      public Transition GetGrammar(string name)
      {
         return _grammars[name];
      }

      public bool HasMasterGrammar()
      {
         return (MasterGrammar != string.Empty) && Contains(MasterGrammar);
      }

      public Transition GetMasterGrammar()
      {
         return _grammars[MasterGrammar];
      }

      public IEnumerable<Transition> GetStartTransitions()
      {
         return _grammars.Values;
      }
   }
}
