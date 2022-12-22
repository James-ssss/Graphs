using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    public class StackStruct<T> : DynamicStructure<T> where T : IComparable<T>
    {
        public StackStruct()
        {
            items = new LinkedListStruct<T>();
        }

        public override void Push(T elem)
        {
            items.Add(elem);
        }

        public override T Pop()
        {
            if (IsEmpty()) throw new ArgumentOutOfRangeException("The stack is empty");

            T elem = items.ElementAt(items.Count() - 1);
            items.RemoveAt(items.Count() - 1);
            return elem;
        }

        public override T Top()
        {
            if (IsEmpty()) throw new ArgumentOutOfRangeException("The stack is empty");

            return items.ElementAt(items.Count() - 1);
        }

        public override bool IsEmpty()
        {
            return items.Count() == 0;
        }

        public override void Print()
        {
            for (int i = 0; i < items.Count(); i++)
            {
                Console.Write($"{items.ElementAt(i)} ");
            }
            Console.WriteLine();
        }

        public override void Clear()
        {
            items.Clear();
        }

        public override int Count()
        {
            return items.Count();
        }
    }
}