using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public int MaxHealth = 2;

    protected int _currenthealth;
    private bool _isDead;

    protected virtual void Start()
    {
        _currenthealth = MaxHealth;
    }

    public virtual void Hit(int amount)
    {
        _currenthealth -= amount;
        if(_currenthealth <= 0 && !_isDead)
        {
            Die();
        }
        else
        {
            PlaySound();
        }
    }

    public virtual void PlaySound() { }

    public void IncreaseHealth(int amount)
    {
        _currenthealth += amount;
        if(_currenthealth > MaxHealth)
        {
            _currenthealth = MaxHealth;
        }
    }

    public virtual void Die()
    {
        //override to handle enemies and players on their own
    }
}
