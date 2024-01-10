using UnityEngine;

public class RotationTest : MonoBehaviour
{
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    [SerializeField] private float _speed;

    private void Start()
    {
        transform.position = _start.position;
    }

    private void Update()
    {
        Vector3 position = Vector3.Slerp(transform.position, _end.position, Time.deltaTime * _speed);
        var target = position - transform.position;
        //var distance = target.magnitude;
        //var direction = target / distance;

        var move = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);
        transform.position = move;
        transform.forward = target;
    }
}
