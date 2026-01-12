using System;
using System.Collections;
using System.Collections.Generic;

namespace MyCollectionsList
{
    public class MyArrayList<T> : IEnumerable<T>
    {
        private T[] _items;
        private int _count;

        public MyArrayList(int capacity = 4)
        {
            if (capacity < 1)
                capacity = 4;

            _items = new T[capacity];
            _count = 0;
        }

        public int Count => _count;


        public T Get(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException();
            return _items[index];
        }

        public void Set(int index, T value)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException();
            _items[index] = value;
        }

        public void Add(T value)
        {
            if (_count == _items.Length)
                Resize();

            _items[_count++] = value;
        }

        public void Add(int index, T value)
        {
            if (index < 0 || index > _count)
                throw new IndexOutOfRangeException();

            if (_count == _items.Length)
                Resize();

            for (int i = _count; i > index; i--)
                _items[i] = _items[i - 1];

            _items[index] = value;
            _count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException();

            for (int i = index; i < _count - 1; i++)
                _items[i] = _items[i + 1];

            _items[_count - 1] = default!;
            _count--;
        }

        public bool Remove(T value)
        {
            int index = IndexOf(value);
            if (index == -1)
                return false;

            RemoveAt(index);
            return true;
        }

        public int IndexOf(T value)
        {
            for (int i = 0; i < _count; i++)
                if (Equals(_items[i], value))
                    return i;

            return -1;
        }

        public bool Contains(T value) => IndexOf(value) != -1;

        public void Clear()
        {
            _items = new T[4];
            _count = 0;
        }

        private void Resize()
        {
            T[] newArray = new T[_items.Length * 2];
            for (int i = 0; i < _items.Length; i++)
                newArray[i] = _items[i];

            _items = newArray;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
                yield return _items[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
