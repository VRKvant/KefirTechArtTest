using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : StateMachineBehaviour
{
    public event Action OnAttackExit;
    public event Action OnAttackEnter;


    private List<string> _attackTriggers;

    private const string _clawAttackTrigger = "ClawAttackTrigger";
    private const string _specialAttackTrigger = "SpecialAttackTrigger";



    private void Awake()
    {
        _attackTriggers = new List<string>();
        _attackTriggers.Add(_clawAttackTrigger);
        _attackTriggers.Add(_specialAttackTrigger);
    }
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnAttackEnter?.Invoke();
        string attackTrigger = _attackTriggers[UnityEngine.Random.Range(0, _attackTriggers.Count)];
        animator.SetTrigger(attackTrigger);

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnAttackExit?.Invoke();
    }


}
