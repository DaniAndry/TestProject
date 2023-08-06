using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    private int _reward = 1;

    private Player _target;
    private int _currentHealth;

    public event UnityAction<int, int> HealthChanged;

    public Player Target => _target;
    public int Reward => _reward;

    private void Start()
    {
        _currentHealth = _health;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Target.AddPoints(_reward);
            Destroy(gameObject);
        }
    }

    public void Init(Player target)
    {
        _target = target;
    }
}