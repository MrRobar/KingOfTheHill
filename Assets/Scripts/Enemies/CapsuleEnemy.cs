using UnityEngine;

public class CapsuleEnemy : Enemy
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private int _force = 5;
    private Vector3 _initialPos;

    private void Awake()
    {
        _initialPos = transform.position;
    }

    public override void Activate()
    {
        _rigidbody.AddForce(Vector3.back * _force, ForceMode.Impulse);
    }

    private void Update()
    {
        if (transform.position.y < -5f)
        {
            transform.position = _initialPos;
            _enemiesPool.Add(gameObject);
            gameObject.SetActive(false);
        }
    }
}