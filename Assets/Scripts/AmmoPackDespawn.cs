using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPackDespawn : MonoBehaviour
{
    float timer;
    public void Initialise(float time)
    {
        timer = time;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
            Destroy(gameObject);
    }
}
