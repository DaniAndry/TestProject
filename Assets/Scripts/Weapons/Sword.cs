using UnityEngine;

public class Sword : Weapon
{
    public Animator _animator;

    private int _damage = 15;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Atack()
    {
        _animator.Play("SwordAttack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
        }
    }
}
