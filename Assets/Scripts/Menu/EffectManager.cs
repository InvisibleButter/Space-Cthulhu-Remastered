using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public List<Effect> Effects = new List<Effect>();
    public Transform EffectHolder;

    [Serializable]
    public enum EffectType
    {
        EnemyDead
    }
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

    public void ShowEffect(EffectType type, Vector3 pos)
    {
        StartCoroutine(PlayEffect(type, pos));
    }

    private IEnumerator PlayEffect(EffectType type, Vector3 pos)
    {
        Effect e = Effects.FirstOrDefault(each=> each.Type == type);
        GameObject go = Instantiate(e.Prefab, pos, Quaternion.identity, EffectHolder);

        float duration = e.Duration + 0.5f;
        yield return new WaitForSeconds(duration);

        Destroy(go);
    }

    [Serializable]
    public class Effect
    {
        public EffectType Type;
        public GameObject Prefab;
        public float Duration;
    }
}
