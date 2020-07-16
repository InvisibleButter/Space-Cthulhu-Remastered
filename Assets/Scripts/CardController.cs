using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    Sprite[] sprites;
    Vector2 start;
    Vector2 goal;
    [HideInInspector]
    public int CardId;
    int type;
    Image imageComponent;
    [HideInInspector]
    public bool isPositive;

    float position;

    public void Initialise(Sprite[] sprites,Vector2 start, Vector2 goal,int cardID)
    {
        this.start = start;
        this.goal = goal;
        this.sprites = sprites;

        this.CardId = cardID;
        
        imageComponent = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void AddToBelt(int type, bool isPositive)
    {
        this.type = type;
        this.isPositive = isPositive;
        position = 0;
        imageComponent.sprite = sprites[type];
        transform.position = start;
        gameObject.SetActive(true);
    }

    public void RemoveFromBelt()
    {
        gameObject.SetActive(false);
    }

    public void CardControlerUpdate()
    {
        //Debug.Log(position);
        position+=Time.deltaTime* RessourceManager.Instance.tickSpeed;
        if (position > 1)
        {
            finish();
        }
        else
        {
            transform.position = Vector3.Lerp(start,goal,position);
        }
    }

    private void finish()
    {
        AudioController.Instance.PlaySound(AudioController.Sounds.Collect_1);
        Stack.Instance.ResolveCardFromBelt(CardId);
        gameObject.SetActive(false);
    }
 
}
