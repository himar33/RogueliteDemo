using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boost_PowerUp", menuName = "Power Ups/Boost", order = 1)]
public class BoostPowerUp : PowerUp
{
    [Range(0, 1)] public float boostPercentage;

    public override void OnAdded(PlayerController player)
    {
        switch (p_type)
        {
            case PowerUpType.B_ATTACK:
                player.multAttack += boostPercentage;
                break;
            case PowerUpType.B_ATTACKSPEED:
                player.multFireRate += boostPercentage;
                break;
            case PowerUpType.B_SPEED:
                player.multSpeed += boostPercentage;
                break;
            default:
                break;
        }
    }
}
