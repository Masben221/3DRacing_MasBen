using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] [Range(0.0f, 1.0f)] private float normalizeSparksEffect;
    [SerializeField] private float speedSparksEffect;
    [SerializeField] private float amountSparksEffect;

    private new ParticleSystem particleSystem;
    private ParticleSystemRenderer particleSystemRenderer;
        
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        var partSys = particleSystem.main;
    }
       
    private void Update()
    {        
        var partSys = particleSystem.main;        

        if (car.NormalizeLinearVelocity >= normalizeSparksEffect)
        {
            particleSystemRenderer.enabled = true;            
            partSys.simulationSpeed = car.NormalizeLinearVelocity * speedSparksEffect;
            partSys.maxParticles = (int)(car.NormalizeLinearVelocity * amountSparksEffect*10);            
        }
        else 
        {
            particleSystemRenderer.enabled = false;          
            partSys.simulationSpeed = 0;
            partSys.maxParticles = 0;           
        }

    }
}
