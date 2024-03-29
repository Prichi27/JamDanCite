﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runtime/Set", order = 22)]
public class RuntimeSet<T> : ScriptableObject
{
    private List<T> Items = new List<T>();

    public void Add(T item)
    {
        if (!Items.Contains(item)) Items.Add(item);
    }

    public void Remove(T item)
    {
        if (Items.Contains(item)) Items.Remove(item);
    }

    public int Count()
    {
        return Items.Count;
    }
}
