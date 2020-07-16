using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceManager : MonoBehaviour
{
    private static RessourceManager _instance;

    public static RessourceManager Instance { get { return _instance; } }


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

    public int MaxMagizines;
    public int StartMagazines;
    public int MagazinesPerCard;
    public int ShotsPerMagazines;

    public int MaxHealth;
    public int HealthPerCard;

    public float MaxSpeed;
    public float NormalSpeed;
    public float SlowSpeed;
    public float SlowSpeedTime;
    public float SpeedBoostTime;

    public float ShieldTime;

    public float LightTime;

    public float MaxTickSpeed;
    public float NormalTickSpeed;
    public float TickBoostTime;
    public float TickStopTime;

    public int DMG;

    public int ResDownMagazines;
    public int ResDownLight;
    public int ResDownShield;

    [HideInInspector]
    public int health;
    [HideInInspector]
    public int magazines;
    [HideInInspector]
    public float shieldTimer;
    [HideInInspector]
    public float speedBoostTimer;
    [HideInInspector]
    public float tickBoostTimer;
    [HideInInspector]
    public float lightTimer;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float tickSpeed;
    [HideInInspector]
    public float slowSpeedTimer;
    [HideInInspector]
    public float tickStopTimer;
    [HideInInspector]
    public int shotsInMagazine; 

    public void Initiate()
    {
        health = MaxHealth;
        magazines = StartMagazines;
        shieldTimer = 0;
        speedBoostTimer = 0;
        tickBoostTimer = 0;
        lightTimer = LightTime;
        speed = NormalSpeed;
        tickSpeed = NormalTickSpeed;
        slowSpeedTimer = 0;
        tickStopTimer = 0;
        shotsInMagazine = ShotsPerMagazines;
    }

    public void RessourceUpdate()
    {
        //speed
        if (slowSpeedTimer > 0)
        {
            slowSpeedTimer -= Time.deltaTime;
            speedBoostTimer = 0;
            speed = SlowSpeed;
        }
        else
        {
            slowSpeedTimer = 0;
            if (speedBoostTimer > 0)
            {
                speedBoostTimer -= Time.deltaTime;
                speed = MaxSpeed;
            }
            if (speedBoostTimer == 0 && speed == MaxSpeed)
                speed = NormalSpeed;
            if (speedBoostTimer < 0)
            {
                speed = NormalSpeed;
                speedBoostTimer = 0;
            }
        }



        //tick
        if (tickStopTimer > 0)
        {
            slowSpeedTimer -= Time.deltaTime;
            tickBoostTimer = 0;
            tickSpeed = 0;
        }
        else
        {
            tickStopTimer = 0;
            if (tickBoostTimer > 0)
            {
                tickBoostTimer -= Time.deltaTime;
                tickSpeed = MaxTickSpeed;
            }
            if (tickBoostTimer == 0 && tickSpeed == MaxTickSpeed)
                tickSpeed = NormalTickSpeed;
            if (tickBoostTimer < 0)
            {
                tickSpeed = NormalTickSpeed;
                tickBoostTimer = 0;
            }
        }

        Debug.Log(magazines);
    }

    public void SpeedBoost()
    {
        speedBoostTimer = SpeedBoostTime;
    }
    public void TickBoost()
    {
        tickBoostTimer = TickBoostTime;
    }
    public void SpeedSlow()
    {
        slowSpeedTimer = SlowSpeedTime;
    }
    public void TickStop()
    {
        tickStopTimer = TickStopTime;
    }
    public void DealDamage()
    {
        if(shieldTimer<=0)
            health -= DMG;

        if (health <= 0)
        {
            GameController.Instance.GameOver();
        }
    }
    public void DealDamage(int amount)
    {
        if (shieldTimer <= 0)
        {
            health -= amount;
            Stack.Instance.AddNegativeEffect();
            shieldTimer = 0.5f;
        }
        if(health <= 0)
        {
            GameController.Instance.GameOver();
        }
    }
    public void Heal()
    {
        if (health + HealthPerCard > MaxHealth)
            health = MaxHealth;
        else
            health += HealthPerCard;
    }
    public void AddMagazine()
    {
        if ((magazines + MagazinesPerCard > MaxMagizines))
        {
            //if(shotsInMagazine == 0)
            //{
            //    shotsInMagazine = ShotsPerMagazines;
            //}
            magazines = MaxMagizines;
        }

        else
            magazines += MagazinesPerCard;
    }
    public void RemoveMagazine()
    {
        magazines--;
        if(magazines <= 0)
        {
            magazines = shotsInMagazine = 0;
        }
        shotsInMagazine = ShotsPerMagazines;
    }

    public void RefillLight()
    {
        lightTimer += LightTime;
    }
    public void RefillShield()
    {
        shieldTimer += ShieldTime;
    }
    public void SteelResources()
    {
        if (magazines - ResDownMagazines > 1)
            magazines -= ResDownMagazines;
        else
            if (magazines < 1)
                magazines = 0;
        else
            magazines = 1;


        if (lightTimer - ResDownLight > 0)
            lightTimer -= ResDownLight;
        else
            lightTimer = 0;


        if (shieldTimer - ResDownShield > 0)
            shieldTimer -= ResDownShield;
        else
            shieldTimer = 0;
    }

    public void Shoot()
    {
        shotsInMagazine--;
        if(shotsInMagazine <= 0)
        {
            shotsInMagazine = 0;
        }
    }
}
