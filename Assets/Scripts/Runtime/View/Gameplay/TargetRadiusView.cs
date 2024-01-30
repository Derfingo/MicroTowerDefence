using UnityEngine;

public class TargetRadiusView : ViewBase
{
    public void SetRadiusView(Vector3 position, float radius)
    {
        transform.position = position;
        transform.localScale = new Vector3(radius * 2, 0f, radius * 2);
    }

    public void SetRadiusView(Vector3 position)
    {
        transform.position = position;
    }
}
