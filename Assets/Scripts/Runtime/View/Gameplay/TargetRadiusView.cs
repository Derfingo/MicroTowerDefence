using UnityEngine;

public class TargetRadiusView : ViewBase
{
    public void SetRadiusView(Vector3 position, float radius)
    {
        position.y += 0.01f;
        transform.position = position;
        transform.localScale = new Vector3(radius * 2, 0f, radius * 2);
    }
}
