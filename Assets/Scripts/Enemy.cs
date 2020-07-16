using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    private NavMeshAgent _navMashAgent;
    private Transform _target;
    private float _updateTime = 0.5f;

    public event EventHandler Kill;
    public int Damage = 1;

    private bool _isAttacking;
    private float _attackCooldown = 1.5f;
    private float _attackDist = 0.5f;
    private float _nextAttackTime;

    protected override void Start()
    {
        base.Start();

        _navMashAgent = GetComponent<NavMeshAgent>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _navMashAgent.isStopped = false;
        StartCoroutine(RefreshTargetPos());
    }

    private IEnumerator RefreshTargetPos()
    {
        while(_target != null || !GameController.Instance.IsRunning)
        {
            _navMashAgent.SetDestination(_target.position);
            yield return new WaitForSeconds(_updateTime);
        }
    }

    public override void Die()
    {
        base.Die();
       
        gameObject.SetActive(false);
      
        if (Kill != null)
        {
            Kill.Invoke(this, null);
        }

        AudioController.Instance.PlaySound(AudioController.Sounds.EnemyDead);
        EffectManager.Instance.ShowEffect(EffectManager.EffectType.EnemyDead, new Vector3(transform.position.x, 58f, transform.position.z));
    }

    public override void PlaySound()
    {
        AudioController.Instance.PlaySound(AudioController.Sounds.Hit);
    }

    public void Respawn()
    {
        Start();
    }

    private void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            float sqrDstToTarget = (_target.position - transform.position).sqrMagnitude;
            if (sqrDstToTarget < Mathf.Pow(_attackDist + 2.1f, 2))
            {
                _nextAttackTime = Time.time + _attackCooldown * 2;
                StartCoroutine(WaitToRecover());
            }

        }
    }

    private IEnumerator WaitToRecover()
    {
        _navMashAgent.isStopped = true;

        yield return new WaitForSeconds(0.5f);

        _target.GetComponent<IDamageable>().Hit(Damage);
        Die();
    }
}
