using UnityEngine;

namespace MicroTowerDefence
{
    public class TargetRadiusView : MonoBehaviour
    {
        public void SetRadius(float radius)
        {
            var diameter = radius * 2f;
            transform.localScale = new Vector3(diameter, diameter, 1f);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}