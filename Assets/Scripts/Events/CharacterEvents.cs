using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    /*
    Collates character specific events to be called on by other game objects.
    */

    //Character that is taking damage and damage value.
    public static UnityAction<GameObject, int> characterDamaged;

    //Character that is being healed and amount healed.
    public static UnityAction<GameObject, int> characterHealed;
}