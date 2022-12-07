using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boost_PowerUp", menuName = "Power Ups/Boost", order = 1)]
public class BoostPowerUp : PowerUp
{
    public enum BoostType
    {
        ATTACK,
        ATTACKSPEED,
        SPEED
    }
    public BoostType boostType;
    [Range(0, 1)] public float boostPercentage;
    private void OnEnable()
    {
        p_type = PowerUpType.BOOST;
    }

    public override void OnAdded(PlayerController player)
    {
        switch (boostType)
        {
            case BoostType.ATTACK:
                player.multAttack += boostPercentage;
                break;
            case BoostType.ATTACKSPEED:
                player.multFireRate += boostPercentage;
                break;
            case BoostType.SPEED:
                player.multSpeed += boostPercentage;
                break;
            default:
                break;
        }
    }
}
