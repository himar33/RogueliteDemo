using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxLife;
    [SerializeField] private float heartSeparation;
    public int currentLife;

    [Space]

    [Header("References")]
    [SerializeField] private GameObject heartGO;

    private List<HeartController> hearts;

    private void Start()
    {
        currentLife = maxLife;
        hearts = new List<HeartController>();

        for (int i = 0; i < maxLife; i++)
        {
            Vector3 pos = transform.position;
            pos.x += i * heartSeparation;
            GameObject obj = Instantiate(heartGO, pos, transform.rotation, transform);
            hearts.Add(obj.GetComponent<HeartController>());
        }
    }

    public void TakeOff()
    {
        if (currentLife > 0)
        {
            currentLife--;
            for (int i = 0; i < hearts.Count; i++)
            {
                Debug.Log("aquiiiiiiiiiii");
                if (i == hearts.Count - 1 && hearts[i].currentState == HeartController.HeartState.FULL)
                {
                    Debug.Log("xd");
                    hearts[i].TakeOffLife();
                }
                else if (hearts[i + 1].currentState == HeartController.HeartState.EMPTY && hearts[i].currentState == HeartController.HeartState.FULL)
                {
                    Debug.Log("AAAAAAAAAAA");
                    hearts[i].TakeOffLife();
                }
            }
        }
    }
}
