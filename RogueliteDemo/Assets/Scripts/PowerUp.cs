using UnityEngine;
using MyBox;

public abstract class PowerUp : ScriptableObject
{
    public enum PowerUpType
    {
        BOOST,
        ELEMENTAL,
        CIRCLE
    }
    [ReadOnly] public PowerUpType p_type;
    public string p_name;
    [TextArea] public string p_description;
    public Sprite p_icon;

    public abstract void OnAdded(PlayerController player);
}
