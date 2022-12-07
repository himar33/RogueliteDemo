using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public List<PowerUp> powerUps;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BoostPowerUp item = ScriptableObject.CreateInstance<BoostPowerUp>();
            item.OnAdded(GetComponent<PlayerController>());

            powerUps.Add(item);
        }
    }
}
