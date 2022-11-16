using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private float _accelerationSpeed = 1.3f;
    [SerializeField] private float _decelerationSpeed = 2.0f;

    private Rigidbody[] _rigidbodies;

    private Animator _animator;
    private float _velocity;

    private const string _movementVelocity = "MovementVelocity";
    private const string _attackTrigger = "AttackTrigger";

    private bool _isAttacking = false;

    private AttackController _attackController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _attackController = _animator.GetBehaviour<AttackController>();
    }
    private void OnEnable()
    {
        _attackController.OnAttackExit += AttackEndCallback;
        _attackController.OnAttackEnter += AttackStartCallback;
    }
    private void Start()
    {

        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = true;
        }

    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            _animator.SetTrigger(_attackTrigger);

        }

        if (Input.GetKey(KeyCode.W))
        {
            _velocity += Time.deltaTime * _accelerationSpeed;
            if (_velocity > 1f)
            {
                _velocity = 1f;
            }
        }
        else
        {
            _velocity -= Time.deltaTime * _decelerationSpeed;
            if (_velocity < 0f)
            {
                _velocity = 0f;
            }
        }

        _animator.SetFloat(_movementVelocity, _velocity);

        if (Input.GetKeyDown(KeyCode.D))
        {
            EnablePhysic();
        }
    }
    private void OnDisable()
    {
        _attackController.OnAttackExit -= AttackEndCallback;
        _attackController.OnAttackEnter -= AttackStartCallback;

    }

    private void EnablePhysic()
    {
        _animator.enabled = false;
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = false;
        }
    }

    private void AttackStartCallback()
    {
        _isAttacking = true;
    }
    private void AttackEndCallback()
    {
        _isAttacking = false;
    }

}
