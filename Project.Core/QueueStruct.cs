using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    public class QueueStruct<T> : DynamicStructure<T> where T : IComparable<T>
    {
        public QueueStruct()
        {
            items = new LinkedListStruct<T>();
        }

        public override void Push(T elem)
        {
            items.Add(elem);
        }

        public override T Top()
        {
            if (IsEmpty()) throw new ArgumentOutOfRangeException("The queue is empty");

            return items.ElementAt(0);
        }

        public override T Pop()
        {
            var item = Top();
            items.Remove(item);
            return item;
        }

        public override bool IsEmpty()
        {
            return items.Count() == 0;
        }
       
        public override void Print()
        {
            for(int i = 0; i < items.Count(); i++)
            {
                var item = items.ElementAt(i);
                Console.Write($"{item} ");
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