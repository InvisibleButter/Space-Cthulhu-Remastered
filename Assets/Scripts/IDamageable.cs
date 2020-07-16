using UnityEngine;

public interface IDamageable
{
    void Hit(int amount);

    void IncreaseHealth(int amount);

    void Die();
}
