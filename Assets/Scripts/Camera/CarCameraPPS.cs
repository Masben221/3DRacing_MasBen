using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CarCameraPPS : CarCameraComponent
{
    //[SerializeField] private Car car;
    [SerializeField] [Range(0.0f, 1.0f)] private float normalizePPS;
    [SerializeField] [Range(0.0f, 1.0f)] private float amountPPS;

    private PostProcessVolume postProcessVolume;

    private void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.weight = 0;
    }

    private void Update()
    {
        if (car.NormalizeLinearVelocity >= normalizePPS)
        {
           postProcessVolume.weight = car.NormalizeLinearVelocity * amountPPS;
        }
        else
            postProcessVolume.weight = 0;
    }
}
