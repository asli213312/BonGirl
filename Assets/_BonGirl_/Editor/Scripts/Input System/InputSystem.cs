using System;
using UnityEngine;

public class InputSystem : IUpdateable
{
    private const int LEFT_MOUSE = 0;
    public event Action OnClick;
    
    public void Tick()
    {
        if (Input.GetMouseButtonDown(LEFT_MOUSE))
        {
            OnClick?.Invoke();
        }
    }
}
