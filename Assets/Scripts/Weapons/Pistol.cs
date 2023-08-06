using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;

    public override void Atack()
    {
        Vector3 shootDirection = (_shootPoint.position - transform.position).normalized;
        Bullet newBullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
        newBullet.Shoot(shootDirection);
    }
}
