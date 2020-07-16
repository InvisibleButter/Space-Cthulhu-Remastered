using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController Instance;
    public Transform BulletHolder;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        {
            Instance = this;
        }
    }


    // if we want different weapons somehow
    public Gun CurrentGun;

    public void Shoot()
    {
        if(CurrentGun != null)
        {
            CurrentGun.Shoot();
        }
    }

    public void Reload()
    {
        CurrentGun.Reload();
    }
}
