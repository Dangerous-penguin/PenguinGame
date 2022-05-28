using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DangerousPenguin
{

public class LoopedArray<T> : IEnumerable<T>
{
    private readonly T[] _items;

    private int _startIndex = 0;
    private int _itemsCount = 0;

    public int Length => _itemsCount;

    public LoopedArray(int size)
    {
        _items = new T[size];
    }

    public void Push(T item)
    {
        if (_itemsCount < _items.Length)
        {
            _items[_itemsCount++] = item;
        }
        else
        {
            _items[_startIndex] = item;
            _startIndex         = (_startIndex + 1) % Length;
        }
    }

    public T this[int index] => _items[GetIndex(index)];
    
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _itemsCount; ++i)
        {
            yield return _items[GetIndex(i)];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private int GetIndex(int i) => (_startIndex + i) % Length;

    public void DebugPrint(Func<T,object> selector = null)
    {
        var sb = new StringBuilder($"{this} => {Length} items:");
        foreach (var item in this)
        {
            sb.AppendLine((selector?.Invoke(item) ?? item).ToString());
        }
        Debug.Log(sb.ToString());
    }
}

}