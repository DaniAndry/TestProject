using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction PointsChanged;

    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Button _attackButton;

    private Weapon _currentWeapon;
    private int _currentHealth;
    private int _points;

    public int Points => _points;

    private void Start()
    {
        _currentHealth = _health;
        _currentWeapon = _weapons[0];
        _attackButton.onClick.AddListener(AttackButtonClicked);
    }

    private void AttackButtonClicked()
    {
        _currentWeapon.Atack();
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SelectPistol()
    {
        _currentWeapon = _weapons[1];
        _weapons[1].gameObject.SetActive(true);
        _weapons[0].gameObject.SetActive(false);
           }
    public void SelectSword()
    {
        _currentWeapon = _weapons[0];
        _weapons[0].gameObject.SetActive(true);
        _weapons[1].gameObject.SetActive(false);
    }

    public void AddPoints(int points)
    {
        _points += points;
        PointsChanged?.Invoke();
    }
}