using UnityEngine;
using System;

public class GameEvent<T> : ScriptableObject
{
    public event EventHandler<T> Fired;
    public void Invoke(object sender,  T t)
    {
        if (Application.isPlaying)
        {
            Fired.Invoke(sender, t);
        }
    }
}
