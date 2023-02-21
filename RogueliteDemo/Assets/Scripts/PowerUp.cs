using UnityEngine;
using MyBox;

public abstract class PowerUp : ScriptableObject
{
    public enum PowerUpType
    {
        B_ATTACK,
        B_ATTACKSPEED,
        B_SPEED,
        ELEMENTAL,
        CIRCLE
    }
    public PowerUpType p_type;
    public string p_name;
    [TextArea] public string p_description;
    public Sprite p_icon;

    public abstract void OnAdded(PlayerController player);
}
