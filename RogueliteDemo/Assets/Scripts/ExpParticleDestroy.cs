using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpParticleDestroy : MonoBehaviour
{
    private ParticleSystem ps;
    private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        if (numEnter >= 1)
        {
            Destroy(gameObject);
        }
    }
}
