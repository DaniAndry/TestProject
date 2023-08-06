using UnityEngine;

public class AttackState : State
{
    [SerializeField] private float _delay;
    private Animator _animator;

    private float _lastAttackTime;
    private int _damage = 5;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attack(Target);
            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(Player target)
    {
        target.ApplyDamage(_damage);

        _animator.Play("EnemyAttack", -1, 0f);
    }
}
