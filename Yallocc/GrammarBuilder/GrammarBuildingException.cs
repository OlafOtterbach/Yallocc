using System;

namespace Yallocc
{
   public class GrammarBuildingException : Exception
   {
      private enum Reasons
      {
         unknown = 0,
         goto_label_not_defined = 1,
         grammar_name_already_exist = 2,
         transition_is_not_a_action_transition = 4,
         one_or_more_subgrammars_are_bot_defined = 8,
         master_grammar_not_defined= 16,
         master_grammar_already_defined = 32
      }

      private Reasons _reasons;

      public GrammarBuildingException()
      {
         _reasons = Reasons.unknown;
      }

      public GrammarBuildingException(string message) : base(message)
      {
         _reasons = Reasons.unknown;
      }

      public GrammarBuildingException(string label, string message) : base(message)
      {
         Label = label;
         _reasons = Reasons.unknown;
      }

      public GrammarBuildingException(string message, Exception inner) : base(message, inner)
      {
         _reasons = Reasons.unknown;
      }

      public GrammarBuildingException(string label, string message, Exception inner) : base(message, inner)
      {
         Label = label;
         _reasons = Reasons.unknown;
      }

      public string Label { get; set; }

      public bool HasUnknownReason 
      { 
         get
         {
            return _reasons == Reasons.unknown;
         }
      }

      public bool HasUndefinedGotoLabel 
      { 
         get
         {
            return (_reasons & Reasons.goto_label_not_defined) == Reasons.goto_label_not_defined;
         }
         set
         {
            if(value)
            {
               _reasons |= Reasons.goto_label_not_defined;
            }
            else
            {
               _reasons &= ~Reasons.goto_label_not_defined;
            }
         }
      }

      public bool HasAlreadyExistingGrammarName
      { 
         get
         {
            return (_reasons & Reasons.grammar_name_already_exist) == Reasons.grammar_name_already_exist;
         }
         set
         {
            if(value)
            {
               _reasons |= Reasons.grammar_name_already_exist;
            }
            else
            {
               _reasons &= ~Reasons.grammar_name_already_exist;
            }
         }
      }

      public bool HasUndefinedSubgrammars
      { 
         get
         {
            return (_reasons & Reasons.one_or_more_subgrammars_are_bot_defined) == Reasons.one_or_more_subgrammars_are_bot_defined;
         }
         set
         {
            if(value)
            {
               _reasons |= Reasons.one_or_more_subgrammars_are_bot_defined;
            }
            else
            {
               _reasons &= ~Reasons.one_or_more_subgrammars_are_bot_defined;
            }
         }
      }

      public bool HasSettedActionInNonActionTransition
      { 
         get
         {
            return (_reasons & Reasons.transition_is_not_a_action_transition) == Reasons.transition_is_not_a_action_transition;
         }
         set
         {
            if(value)
            {
               _reasons |= Reasons.transition_is_not_a_action_transition;
            }
            else
            {
               _reasons &= ~Reasons.transition_is_not_a_action_transition;
            }
         }
      }

      public bool MasterGrammarIsNotDefined
      {
         get
         {
            return (_reasons & Reasons.master_grammar_not_defined) == Reasons.master_grammar_not_defined;
         }
         set
         {
            if (value)
            {
               _reasons |= Reasons.master_grammar_not_defined;
            }
            else
            {
               _reasons &= ~Reasons.master_grammar_not_defined;
            }
         }
      }

      public bool MasterGrammarAlreadyDefined
      {
         get
         {
            return (_reasons & Reasons.master_grammar_already_defined) == Reasons.master_grammar_already_defined;
         }
         set
         {
            if (value)
            {
               _reasons |= Reasons.master_grammar_already_defined;
            }
            else
            {
               _reasons &= ~Reasons.master_grammar_already_defined;
            }
         }
      }
   }
}
