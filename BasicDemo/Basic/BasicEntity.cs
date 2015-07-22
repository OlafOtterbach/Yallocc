namespace BasicDemo.Basic
{
   public abstract class BasicEntity
   {
      public enum BasicType
      {
         e_boolean,
         e_integer,
         e_float,
         e_string,
         e_array,
         e_binary_operator,
         e_unary_operator
      }

      public abstract BasicEntity Createnew();

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

      public bool IsFloat
      { 
         get
         {
            return Type == BasicType.e_float;
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
