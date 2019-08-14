using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Subject communicates with with observers but isn't coupled to them
public abstract class Subject : MonoBehaviour
{
    protected List<Observer> _observers;

    public void AddObserver(Observer observer) {
        if (_observers == null) {
            _observers = new List<Observer>();
        }
        _observers.Add(observer);
    }

    public void RemoveObserver(Observer observer) {
        if (_observers != null)
            _observers.Remove(observer);
    }

    public void Notify(GameEvent e) {
        foreach (Observer o in _observers) {
            o.ValidateAndNotify(gameObject, e);
        }
    }
}
