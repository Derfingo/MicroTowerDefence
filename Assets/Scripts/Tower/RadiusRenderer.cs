using UnityEngine;

public class RadiusRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _stepAmount = 1;
    [SerializeField] private float _radius;

    private void Start()
    {
        DrawCircle(_stepAmount, _radius);
    }

    private void DrawCircle(int steps, float radius)
    {
        _lineRenderer.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);
            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currenPosition = new(x, y, 0);
            _lineRenderer.SetPosition(currentStep, currenPosition);
        }
    }
}
