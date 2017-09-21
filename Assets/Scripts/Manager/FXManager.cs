using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static FXManager Instance;

    [SerializeField]
    private ParticleSystem crystalCollect;

    void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple instances of SpecialEffectsHelper!");
        }

        Instance = this;
    }

    // Creation explosion in at the indicated location

    public void FX(Vector3 position)
    {
        // Smoke on the water
        instantiate(crystalCollect, position);
    }

    // Creating a particle effect from a prefab
    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as ParticleSystem;

        // Destruction
        Destroy(
          newParticleSystem.gameObject,
          newParticleSystem.main.startLifetimeMultiplier
        );

        return newParticleSystem;
    }
}
