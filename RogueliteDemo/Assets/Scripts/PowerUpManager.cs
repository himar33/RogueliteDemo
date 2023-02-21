using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using System.Linq;

public class PowerUpManager : MonoBehaviour
{
    public List<PowerUp> powerUps;

    private PlayerController player;
    private List<PowerUp> allPowerUps;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        allPowerUps = Resources.LoadAll("Power Ups", typeof(PowerUp)).Cast<PowerUp>().ToList();
    }

    private void Update()
    {
        //PARA DEBUGGEAR SOLO
        if (Input.GetKeyDown(KeyCode.Alpha1)) AddPowerUp(PowerUp.PowerUpType.B_ATTACK);
        if (Input.GetKeyDown(KeyCode.Alpha2)) AddPowerUp(PowerUp.PowerUpType.B_ATTACKSPEED);
        if (Input.GetKeyDown(KeyCode.Alpha3)) AddPowerUp(PowerUp.PowerUpType.B_SPEED);
    }

    public void AddPowerUp(PowerUp.PowerUpType pType)
    {
        PowerUp powerUp = allPowerUps.Find(x => x.p_type == pType);
        powerUp.OnAdded(player);
        powerUps.Add(powerUp);
        Debug.Log("Added a " + powerUp.p_name + " power up!");
    }
}
