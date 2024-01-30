using System;
using UnityEngine;

public interface IInputActions
{
    public event Action<float> OnRotateCameraEvent;
    public event Action<float> OnMouseZoomEvent;
    public event Action OnTurnCameraLeftEvent;
    public event Action OnTurnCameraRightEvent;
    public event Action OnSelectContentEvent;
    public event Action OnBuildContentEvent;
    public event Action OnPauseGameEvent;

    Vector3 MousePosition { get; }
}
