using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{ 

    public abstract class DynamicStructure<T> where T : IComparable<T>
    {
        protected LinkedListStruct<T> items { get; set; }

        abstract public void Push(T elem);
        abstract public T Pop();
        abstract public T Top();
        abstract public bool IsEmpty();
        abstract public void Print();
        abstract public void Clear();
        abstract public int Count();

        public void Call(int id, bool print, T elem = default(T))
        {
            switch (id)
            {
                case 1:
                    if (elem is null) throw new ArgumentNullException();
                    Push(elem);     if (print) Print();                         break;
                case 2: Pop();      if (print) Print();                         break;
                case 3: if (print) Console.WriteLine(Top()); Top();             break;
                case 4: if (print) Console.WriteLine(IsEmpty()); IsEmpty();     break;
                case 5: if (print) Print();                                     break;
                default: throw new Exception("Такой команды не существует!");
            }
        }
    }
}
