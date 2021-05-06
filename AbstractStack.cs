using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StackApplication
{
    public interface IStack<T>
    {
        int Count { get; }
        T Peak { get; }
        void Push(T value);
        T Pop();
    }

    static class StackExtension
    {
        public static void Reverse<T>(this AbstractStack<T> stack)
        {
            var array = new T[stack.Count];
            var index = 0;
            foreach (var item in stack)
            {
                array[index++] = item;
            }

            stack.Clear();
            foreach (var item in array)
            {
                stack.Push(item);
            }
        }
    }

    public abstract class AbstractStack<T> : ICloneable, IEnumerable<T>, IStack<T>, IEquatable<AbstractStack<T>>
    {
        public int Count { get; protected set; }
        public abstract T Peak { get; }

        public abstract void Push(T value);

        // public abstract T Pop();
        public virtual T Pop()
        {
            if (Count <= 0) throw new Exception("stack is empty");
            var peak = Peak;
            Count--;
            return peak;
        }

        public void PushAll(params T[] values) =>
            Array.ForEach(values, Push);

        public void PushAll(IEnumerable<T> values) =>
            PushAll(values.ToArray());

        public abstract object Clone();

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void Clear() => Count = 0;

        public bool Contains(T value)
        {
            foreach (var item in this)
            {
                if (item.Equals(value))
                    return true;
            }

            return false;
        }

        public void CheckIndex(int index)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException();
        }

        public virtual T this[int index]
        {
            get
            {
                CheckIndex(index);
                using var iterator = GetEnumerator();
                for (int i = 0; i <= index; i++)
                {
                    iterator.MoveNext();
                }

                return iterator.Current;
            }
        }

        public bool Equals(AbstractStack<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Count != other.Count)
                return false;
            using var iterator1 = GetEnumerator();
            using var iterator2 = other.GetEnumerator();
            for (int i = 0; i < Count; i++)
            {
                iterator1.MoveNext();
                iterator2.MoveNext();
                if (iterator1.Current != null && !iterator1.Current.Equals(iterator2.Current))
                    return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AbstractStack<T>) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Count, Peak);
        }

        public static bool operator ==(AbstractStack<T> stack1, AbstractStack<T> stack2) =>
            stack1?.Equals(stack2) ?? stack2 is null;

        public static bool operator !=(AbstractStack<T> stack1, AbstractStack<T> stack2) => !(stack1 == stack2);
    }
}