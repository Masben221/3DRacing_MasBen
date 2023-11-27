using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WindSound : MonoBehaviour
{
    [SerializeField] private Car car;

    [SerializeField] private float pitchModifier;
    [SerializeField] private float volumeModifier;
    [SerializeField] private float velosityModifier;

    [SerializeField] private float basePitch = 1.0f;
    [SerializeField] private float baseVolume = 0.4f;

    [SerializeField] private AudioSource windAudioSource;
    private void Start()
    {
        windAudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        windAudioSource.pitch = basePitch + pitchModifier * (car.NormalizeLinearVelocity * velosityModifier);
        windAudioSource.volume = baseVolume + volumeModifier * car.NormalizeLinearVelocity;
    }
}