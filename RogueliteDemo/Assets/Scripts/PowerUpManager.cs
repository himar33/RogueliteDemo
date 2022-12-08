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
        if (Input.GetKeyDown(KeyCode.Alpha1)) AddPowerUp(allPowerUps[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2)) AddPowerUp(allPowerUps[1]);
        if (Input.GetKeyDown(KeyCode.Alpha3)) AddPowerUp(allPowerUps[2]);
    }

    public void AddPowerUp(PowerUp powerUp)
    {
        if (powerUp)
        {
            powerUp.OnAdded(player);
            powerUps.Add(powerUp);
            Debug.Log("Added a " + powerUp.p_name + " power up!");
        }
    }
}
