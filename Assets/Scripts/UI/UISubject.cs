using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UISubject : MonoBehaviour
{
    public List<I_UIObserver> _UIObservers = new List<I_UIObserver>();

    public void AddObserver(I_UIObserver obs) { _UIObservers.Add(obs); }
    public void RemoveObserver(I_UIObserver obs) { _UIObservers.Remove(obs); }

    public void UpdatePlayerUIObserver()
    {
        for (int i = 0; i < _UIObservers.Count; i++)
        {
            _UIObservers[i].UpdatePlayerUI();
        }
    }
}
