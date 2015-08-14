namespace BasicDemo.Basic
{
   public abstract class BasicVariable : BasicEntity
   {
      public abstract BasicEntity CreateNew();

      public void Set(BasicEntity second)
      {
         if(second is BasicInteger)
         {
            Set(second as BasicInteger);
         }
         else if(second is BasicReal)
         {
            Set(second as BasicReal);
         }
         else if (second is BasicString)
         {
            Set(second as BasicString);
         }
         else if (second is BasicBoolean)
         {
            Set(second as BasicBoolean);
         }
         else
         {
            throw new BasicTypeMissmatchException();
         }
      }

      public virtual void Set(BasicInteger second)
      {
         throw new BasicTypeMissmatchException();
      }

      public virtual void Set(BasicReal second)
      {
         throw new BasicTypeMissmatchException();
      }

      public virtual void Set(BasicString second)
      {
         throw new BasicTypeMissmatchException();
      }

      public virtual void Set(BasicBoolean second)
      {
         throw new BasicTypeMissmatchException();
      }
   }
}
