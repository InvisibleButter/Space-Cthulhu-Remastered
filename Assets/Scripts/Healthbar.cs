using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider healthSlider;
    public float[] steplength;
    public float speed;
    float oldV;
    float v;
    Coroutine corutine;
    // Start is called before the first frame update
    void Start()
    {
        v = steplength[RessourceManager.Instance.health];
    }

    // Update is called once per frame
    void Update()
    {
        oldV = v;
        v = steplength[RessourceManager.Instance.health];
        if (v != oldV)
        {
            if(corutine!=null)
                StopCoroutine(corutine);
            corutine = StartCoroutine(LowerHealth());
        }
    }

    IEnumerator LowerHealth()
    {
        float ov = oldV;
        float nv = v;
        for (float ft = 0f; ft >= 0; ft += 0.1f*speed)
        {
            healthSlider.value = Mathf.Lerp(ov,nv,ft);
            
            yield return null;
        }
    }
}
