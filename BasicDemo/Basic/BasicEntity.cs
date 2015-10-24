namespace BasicDemo.Basic
{
   public abstract class BasicEntity
   {
      public enum BasicType
      {
         e_boolean,
         e_integer,
         e_real,
         e_string,
         e_array,
         e_binary_operator,
         e_unary_operator
      }

      public abstract BasicType Type { get; }

      public bool IsBoolean 
      { 
         get
         {
            return Type == BasicType.e_boolean;
         }
      }

      public bool IsInteger
      { 
         get
         {
            return Type == BasicType.e_integer;
         }
      }

      public bool IsReal
      { 
         get
         {
            return Type == BasicType.e_real;
         }
      }

      public bool IsString
      { 
         get
         {
            return Type == BasicType.e_string;
         }
      }

      public bool IsArray
      {
         get
         {
            return Type == BasicType.e_array;
         }
      }

      public bool IsBinaryOperator
      { 
         get
         {
            return Type == BasicType.e_binary_operator;
         }
      }

      public bool IsUnaryOperator
      {
         get
         {
            return Type == BasicType.e_unary_operator;
         }
      }

      public bool IsVariable
      {
         get
         {
            return (!IsBinaryOperator) && (!IsUnaryOperator);
         }
      }
   }
}
