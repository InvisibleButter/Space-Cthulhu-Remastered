using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public Transform Muzzle;
	public Bullet Projectile;
	public float MsBetweenShots = 200f;
	public float MuzzleVelocity = 35f;

	public float ReloadTime = 0.1f;

	private float _nextShotTime;

	private bool _isReloading;
	private int _currentMaxAmmo ;


	private void Start()
	{
		_currentMaxAmmo = RessourceManager.Instance.magazines * RessourceManager.Instance.ShotsPerMagazines;
	}

	private int GetMagCount()
	{
		if( _currentMaxAmmo - RessourceManager.Instance.magazines >= 0)
		{
			_currentMaxAmmo -= RessourceManager.Instance.ShotsPerMagazines;
			return RessourceManager.Instance.ShotsPerMagazines;
		}
		int max = _currentMaxAmmo;
		_currentMaxAmmo = 0;

		return max;
	}

	public void Shoot()
	{
		if (!_isReloading && Time.time > _nextShotTime && RessourceManager.Instance.shotsInMagazine > 0)
		{
			RessourceManager.Instance.Shoot();
			AudioController.Instance.PlaySound(AudioController.Sounds.Shoot);
			_nextShotTime = Time.time + MsBetweenShots / 1000;
			Bullet bullet = Instantiate(Projectile, Muzzle.position, Muzzle.rotation, GunController.Instance.BulletHolder) as Bullet;
			bullet.SetSpeed(MuzzleVelocity);
		}
		GameController.Instance.ToggleReload(RessourceManager.Instance.shotsInMagazine <= 0);
	}

	public void Reload() 
	{ 
		if(!_isReloading)
		{
			StartCoroutine(WaitToReload());
		}
	}

	private IEnumerator WaitToReload()
	{
		AudioController.Instance.PlaySound(AudioController.Sounds.Reload);
		_isReloading = true;
		yield return new WaitForSeconds(ReloadTime);

		RessourceManager.Instance.RemoveMagazine();

		_isReloading = false;
	}

	public void IncreaseMaxAmmo(int val)
	{
		_currentMaxAmmo += val;
	}

	public void DecreaseMaxAmmo(int val)
	{
		_currentMaxAmmo -= val;
		if(_currentMaxAmmo <= 0)
		{
			_currentMaxAmmo = 0;
		}
	}
}
