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
        if (currentLife < 0) return;

        currentLife--;
        
        if (currentLife <= 0)
        {
            GameManager.Instance.GameOver();
            return;
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            if (hearts[i].currentState == HeartController.HeartState.FULL
                && (i == hearts.Count - 1 || hearts[i + 1].currentState == HeartController.HeartState.EMPTY))
            {
                hearts[i].TakeOffLife();
                break;
            }
        }
    }
}
