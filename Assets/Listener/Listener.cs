using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ListenerList<T>
{
    private List<Action<T>> _list;
    public ListenerList() {
        _list = new List<Action<T>>();
    }

    public void AddListener(Action<T> a) {
        _list.Add(a);
    }

    public void RemoveListener(Action<T> a) {
        if (_list.Contains(a)) _list.Remove(a);
    }

    public void NotifyListeners(T e) {
        foreach(Action<T> a in _list) {
            a(e);
        }
    }
}
