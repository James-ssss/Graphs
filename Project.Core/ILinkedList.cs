using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    public interface ILinkedList<T>
    {
        void Add(T elem);
        void Clear();
        bool Remove(T elem);
        void RemoveAt(int index);
        int Count();
        bool IsEmpty();
        int IndexOf(T elem);
        void Print();
        T ElementAt(int index);
        void Insert(int index, T elem);
        /// <summary>Упорядочивает список по возрастанию (убыванию)</summary>
        void Sort(bool order);
        string ToString();
    }
}
