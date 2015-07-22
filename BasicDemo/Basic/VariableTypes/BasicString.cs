﻿namespace BasicDemo.Basic
{
   public class BasicString : BasicEntity
   {
      public BasicString()
      {
      }

      public BasicString(string value)
      {
         Value = value;
      }

      public abstract BasicEntity CreateNew()
      {
         return new BasicString(string.Empty);
      }

      public override BasicType Type
      {
         get
         {
            return BasicType.e_string;
         }
      }

      public string Value { get; set; }
   }
}
