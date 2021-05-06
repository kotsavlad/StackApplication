using System;
using System.Collections.Generic;

namespace StackApplication
{
    public class ArrayStack<T> : AbstractStack<T>
    {
        private readonly T[] _data;

        private const int DefaultCapacity = 100;

        public int Capacity { get; private set; }

        public override T Peak =>
            Count > 0 ? _data[Count - 1] : throw new Exception("stack is empty");


        public ArrayStack(int capacity = DefaultCapacity)
        {
            if (capacity <= 0) throw new Exception("invalid capacity");
            Capacity = capacity;
            _data = new T[Capacity];
        }

        public ArrayStack(ICollection<T> collection, int capacity = DefaultCapacity) : this(capacity)
        {
            if (collection.Count > capacity) throw new Exception("capacity is too small");
            collection.CopyTo(_data, 0);
            Count += collection.Count;
        }

        public override void Push(T value)
        {
            if (Count >= Capacity) throw new Exception("stack overflow");
            _data[Count++] = value;
        }

        public override object Clone()
        {
            var clone = new ArrayStack<T>(Capacity);
            Array.Copy(_data, clone._data, Count);
            clone.Count = Count;
            return clone;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return _data[i];
            }
        }

        public override T this[int index]
        {
            get
            {
                CheckIndex(index);
                return _data[Count - 1 - index];
            }
        }
        
        public static ArrayStack<T> operator +(ArrayStack<T> stack1, ArrayStack<T> stack2)
        {
            if (stack1 is null || stack2 is null)
                throw new Exception("Addition is not supported for null operand");
            var sum = new ArrayStack<T>(stack1.Capacity + stack2.Capacity);
            Array.Copy(stack1._data, sum._data, stack1.Count);
            Array.Copy(stack2._data, 0, sum._data, stack1.Count, stack2.Count);
            sum.Count = stack1.Count + stack2.Count;
            return sum;
        }

    }
}