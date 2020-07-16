using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmunitionBar : MonoBehaviour
{
    public Slider AmoSlider;
    public float[] magazinelength;
    public float shotlength;
    public float speed;
    float oldV;
    float v;
    Coroutine corutine;
    // Start is called before the first frame update
    void Start()
    {
        v = magazinelength[RessourceManager.Instance.magazines]+ RessourceManager.Instance.shotsInMagazine*shotlength;
    }

    // Update is called once per frame
    void Update()
    {
        oldV = v;
        v = magazinelength[RessourceManager.Instance.magazines] + RessourceManager.Instance.shotsInMagazine * shotlength;
        if (v != oldV)
        {
            if (corutine != null)
                StopCoroutine(corutine);
            corutine = StartCoroutine(LowerHealth());
        }
    }

    IEnumerator LowerHealth()
    {
        float ov = oldV;
        float nv = v;
        for (float ft = 0f; ft >= 0; ft += 0.1f * speed)
        {
            AmoSlider.value = Mathf.Lerp(ov, nv, ft);

            yield return null;
        }
    }
}
