using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    public class LinkedListStruct<T> : ILinkedList<T> where T : IComparable<T>
    {
        Elem Top { get; set; }
        Elem Tail { get; set; }

        class Elem
        {
            public Elem next { get; set; }
            public Elem prev { get; set; }
            public T data { get; set; }

            public Elem(T data) => this.data = data;
        }

        public void Add(T elem)
        {
            if (IsEmpty())
            {
                Top = new Elem(elem);
                Tail = Top;
                return;
            }

            Elem top = Top;
            Top = new Elem(elem);
            top.next = Top;
            Top.prev = top;
        }

        public void Clear()
        {
            Top = null;
            Tail = null;
        }

        public bool Remove(T elem)
        {
            bool ret = false;
            Elem top = Top;
            while (top is not null)
            {
                if (top.data.Equals(elem))
                {
                    if (top.next is not null) top.next.prev = top.prev;
                    else Top = top.prev;
                    if (top.prev is not null) top.prev.next = top.next;
                    else Tail = top.next;
                    ret = true;
                    break;
                }
                top = top.prev;
            }
            return ret;
        }

        public void RemoveAt(int index)
        {
            Elem tail = Tail;
            int count = 0;
            while (tail is not null)
            {
                if (count == index)
                {
                    if (tail.next is not null) tail.next.prev = tail.prev;
                    else Top = tail.prev;
                    if (tail.prev is not null) tail.prev.next = tail.next;
                    else Tail = tail.next;
                    return;
                }
                count++;
                tail = tail.next;
            }

            throw new IndexOutOfRangeException("Index is out of range");
        }

        public int Count()
        {
            Elem tail = Tail;
            int count = 0;
            while (tail is not null)
            {
                tail = tail.next;
                count++;
            }
            return count;
        }

        public bool IsEmpty()
        {
            return Top is null;
        }

        public void Print()
        {
            Elem tail = Tail;
            while (tail is not null)
            {
                Console.Write($"{tail.data} ");
                tail = tail.next;
            }
            Console.WriteLine();
        }

        public T ElementAt(int index)
        {
            Elem tail = Tail;
            int count = 0;
            while (tail is not null)
            {
                if (count == index) return tail.data;
                count++;
                tail = tail.next;
            }

            throw new IndexOutOfRangeException("Index is out of range");
        }

        public void Insert(int index, T elem)
        {
            Elem tail = Tail;
            Elem newElem = new Elem(elem);
            int count = 0;
            while (tail is not null)
            {
                if (count == index)
                {
                    if (tail.prev is not null) { 
                        tail.prev.next = newElem; newElem.prev = tail.prev;
                        newElem.next = tail; tail.prev = newElem;
                    }
                    else Tail = newElem;
                    newElem.next = tail;
                    return;
                }
                count++;
                tail = tail.next;
            }

            throw new IndexOutOfRangeException("Index is out of range");
        }

        public void Sort(bool order = true)
        {
            if(IsEmpty()) return;

            Elem currentI = Tail;
            while (currentI.next is not null) {
                Elem currentJ = currentI.next;
                while (currentJ is not null) {
                    if(order && currentI.data.CompareTo(currentJ.data) > 0
                        || !order && currentI.data.CompareTo(currentJ.data) < 0) 
                    {
                        T data = currentI.data;
                        currentI.data = currentJ.data;
                        currentJ.data = data;
                    }
                    currentJ = currentJ.next;
                }
                currentI = currentI.next;
            }
        }

        public int IndexOf(T elem)
        {
            Elem tail = Tail;
            int count = 0;
            while (tail is not null)
            {
                if (tail.data.Equals(elem)) return count;
                count++;
                tail = tail.next;
            }

            throw new ArgumentException("This element is not found");
        }

        public string ToString()
        {
            Elem tail = Tail;

            StringBuilder sb = new StringBuilder();
            while (tail is not null)
            {
                sb.Append(tail.data + " ");
                tail = tail.next;
            }

            return sb.ToString();
        }
    }
}
