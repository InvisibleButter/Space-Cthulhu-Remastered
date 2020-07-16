using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    private static GunAnimation _instance;

    public static GunAnimation Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject ammopackPrefab;
    public Transform ammoSpawnpoint;
    public float aDespwanTime;
    AmmoPackDespawn apd;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void StartShooting()
    {
        animator.SetBool("Fire",true);
        
    }
    public void StopShooting()
    {
        animator.SetBool("Fire", false);
    }
    public void StartReloading()
    {
        animator.SetBool("Relode", true);
        apd = Instantiate(ammopackPrefab,ammoSpawnpoint).GetComponent<AmmoPackDespawn>();
        apd.Initialise(aDespwanTime);
    }
    public void StopReloaing()
    {
        animator.SetBool("Relode", false);
    }
    public void StartWalking()
    {
        animator.SetBool("Walking", true);
    }
    public void StopWalking()
    {
        animator.SetBool("Walking", false);
    }

}


