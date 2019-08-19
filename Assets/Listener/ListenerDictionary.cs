using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerDictionary<T> 
{
    protected IDictionary<T, List<Action<T>>> _dict;

    public ListenerDictionary() {
        _dict = new Dictionary<T, List<Action<T>>>();
    }

    public void AddListener(T e, Action<T> a) {
        List<Action<T>> list;
        if (_dict.TryGetValue(e, out list)) {
            list.Add(a);
            _dict[e] = list;
        } else {
            list = new List<Action<T>>();
            list.Add(a);
            _dict.Add(e,list);
        }
    }

    public void RemoveListener(T e, Action<T> a) {
        List<Action<T>> list;
        if (_dict.TryGetValue(e, out list)) {
            list.Remove(a);
            _dict[e] = list;
        }
    }

    public void NotifyListeners(T e) {
        List<Action<T>> list;
        if (_dict.TryGetValue(e, out list)) {
            foreach(Action<T> a in list) {
                a(e);
            }
        }
    }
}
