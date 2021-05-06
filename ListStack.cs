using System;
using System.Collections;
using System.Collections.Generic;

namespace StackApplication
{
    class ListStack<T> : AbstractStack<T>
    {
        class Link
        {
            public T Value;
            public Link Next;
        }

        private Link _top;

        public override T Peak =>
            Count > 0 ? _top.Value : throw new Exception("stack is empty");

        public override void Push(T value)
        {
            _top = new Link {Value = value, Next = _top};
            Count++;
        }

        public override T Pop()
        {
            var peak = base.Pop();
            _top = _top.Next;
            return peak;
        }

        public override object Clone()
        {
            var clone = new ListStack<T>();
            var current = _top;
            HandleRest();
            return clone;

            void HandleRest()
            {
                if (current != null)
                {
                    var value = current.Value;
                    current = current.Next;
                    HandleRest();
                    clone.Push(value);
                }
            }
        }

        // simpler approach
        // public override IEnumerator<T> GetEnumerator()
        // {
        // var current = top;
        // for (int i = 0; i < Count; i++)
        // {
        //     yield return current.Value;
        //     current = current.Next;
        // }
        // }


        // trickest approach
        public override IEnumerator<T> GetEnumerator() =>
            new ListStackIterator(this);

        class ListStackIterator : IEnumerator<T>
        {
            private readonly ListStack<T> _stack;
            private Link _current;

            public ListStackIterator(ListStack<T> stack)
            {
                _stack = stack;
                Reset();
            }

            public bool MoveNext()
            {
                if (_current.Next != null)
                {
                    _current = _current.Next;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                // initial position -- before top
                _current = new Link {Next = _stack._top};
            }

            public T Current => _current.Value;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}