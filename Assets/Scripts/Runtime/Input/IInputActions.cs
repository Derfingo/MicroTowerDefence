using System;
using UnityEngine;

public interface IInputActions
{
    public event Action SelectPlaceEvent;
    public event Action GamePauseEvent;
    public event Action CancelSelectPlaceEvent;
    public event Action<float> ScrollEvent;
    public event Action<float> RotateCameraEvent;
    public event Action<bool> TurnCameraLeftEvent;
    public event Action<bool> TurnCameraRightEvent;

    void SetAllMaps();
    void SetPlayerMap();
    void SetUIMap();

    Vector3 MousePosition { get; }
}
