using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private float _accelerationSpeed = 1.3f;
    [SerializeField] private float _decelerationSpeed = 2.0f;
    [SerializeField] private Rigidbody[] _rigidbodies;

    private Animator _animator;
    private float _velocity;
    private const string _clawAttackTrigger = "ClawAttackTrigger";
    private const string _specialAttackTrigger = "SpecialAttackTrigger";
    private const string _movementVelocity = "MovementVelocity";

    private void Start()
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = true;
        }
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Random.value < 0.5)
            {
                _animator.SetTrigger(_clawAttackTrigger);
            }
            else
            {
                _animator.SetTrigger(_specialAttackTrigger);
            }

        }

        if (Input.GetKey(KeyCode.W) && _velocity < 1.0f)
        {
            _velocity += Time.deltaTime * _accelerationSpeed;
        }
        else
        {
            _velocity -= Time.deltaTime * _decelerationSpeed;
        }
        if (_velocity < 0f)
        {
            _velocity = 0f;
        }
        _animator.SetFloat(_movementVelocity, _velocity);

        
        if (Input.GetKeyDown(KeyCode.D))
        {
            EnablePhysic();
        }
    }

    private void EnablePhysic()
    {
        _animator.enabled = false;
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = false;
        }
    }
}
