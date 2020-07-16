using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemSpawner : MonoBehaviour
{
    public float spawnTime;
    public Sprite[] itemSprites;
    public GameObject effects;
    public SpriteRenderer sr;
    private Transform player;

    private Collider c;
    private float timer;
    private bool isActive;
    int type;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Collider>();
        c.isTrigger = true;
        isActive = true;
        timer = spawnTime;
        int r = Random.Range(0, itemSprites.Length);
        type = r;
        sr.sprite = itemSprites[r];
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            timer -= Time.deltaTime;
        if (timer < 0)
        {
            effects.SetActive(true);
            sr.enabled = true;
            int r = Random.Range(0, itemSprites.Length);
            sr.sprite = itemSprites[r];
            type = r;
            isActive = true;
            sr.transform.LookAt(player);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isActive = false;
            effects.SetActive(false);
            sr.enabled = false;
            switch (type)
            {
                case 0:
                    //schild
                    Stack.Instance.AddNewCardToBelt(new ShieldCard());
                    break;
                case 1:
                    //reperatur
                    Stack.Instance.AddNewCardToBelt(new RepairCard());
                    break;
                case 2:
                    //übertakten
                    Stack.Instance.AddNewCardToBelt(new OverclockingCard());
                    break;
            }
            timer = spawnTime;
        }
    }
}
