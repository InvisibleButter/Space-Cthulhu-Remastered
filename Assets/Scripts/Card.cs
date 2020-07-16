using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card 
{
    
    public enum CardType
    {
        Amunition=0,
        Healing=1,
        Light=2,
        Speed=3,
        Shield=4,
        Repair=5,
        Overclocking=6,
        DMG=7,
        ResDown=8,
        Slow=9,
        Stop=10
    }
    public CardType type;
    public bool positive;

    public void setCardType(CardType type)
    {
        this.type = type;
    }

    public CardType getCardType()
    {
        return type;
    }

    public abstract void Action();

    public void setPositive(bool p)
    {
        positive = p;
    }
    public bool isPositive()
    {
        return positive;
    }
}



public class AmunitionCard : Card
{
    
    public AmunitionCard()
    {
        setCardType(CardType.Amunition);
        setPositive(true);
    }

    public override void Action()
    {
        Debug.Log("Munition Nachladen, Animationen und Effekte abspielen");
        RessourceManager.Instance.AddMagazine();
    }
}
public class HealingCard : Card
{
    public HealingCard()
    {
        setCardType(CardType.Healing);
        setPositive(true);
    }

    public override void Action()
    {
        Debug.Log("Healing");
        RessourceManager.Instance.Heal();
    }
}
public class LightCard : Card
{
    public LightCard()
    {
        setCardType(CardType.Light);
        setPositive(true);
    }

    public override void Action()
    {
        Debug.Log("Light");
        RessourceManager.Instance.RefillLight();
    }
}
public class SpeedCard : Card
{
    public SpeedCard()
    {
        setCardType(CardType.Speed);
        setPositive(true);
    }

    public override void Action()
    {
        Debug.Log("Speed");
        RessourceManager.Instance.SpeedBoost();
    }
}
public class ShieldCard : Card
{
    public ShieldCard()
    {
        setCardType(CardType.Shield);
        setPositive(true);
        
    }

    public override void Action()
    {
        Debug.Log("Shieldup");
        RessourceManager.Instance.RefillShield();
    }
}
public class RepairCard : Card
{
    public RepairCard()
    {
        setCardType(CardType.Repair);
        setPositive(true);
    }

    public override void Action()
    {
        Debug.Log("repair");
        Stack.Instance.RemoveAllBadCard();
    }
}

public class OverclockingCard : Card
{
    public OverclockingCard()
    {
        setCardType(CardType.Overclocking);
        setPositive(true);
    }

    public override void Action()
    {
        Debug.Log("overclocking");
        RessourceManager.Instance.TickBoost();
    }
}



public class DMGCard : Card
{
    public DMGCard()
    {
        setCardType(CardType.DMG);
        setPositive(true);
    }

    public override void Action()
    {
        Debug.Log("DMG");
        RessourceManager.Instance.DealDamage();
    }
}

public class ResDownCard : Card
{
    public ResDownCard()
    {
        setCardType(CardType.ResDown);
        setPositive(true);
    }

    public override void Action()
    {
        Debug.Log("ResDown");
        RessourceManager.Instance.SteelResources();
    }
}

public class SlowCard : Card
{
    public SlowCard()
    {
        setCardType(CardType.Slow);
        setPositive(true);
    }

    public override void Action()
    {
        Debug.Log("Slow");
        RessourceManager.Instance.SpeedSlow();
    }
}

public class TickStopCard : Card
{
    public TickStopCard()
    {
        setCardType(CardType.Stop);
        setPositive(false);
    }

    public override void Action()
    {
        Debug.Log("tickStop");
        RessourceManager.Instance.TickStop();
    }
}
