using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class CarGearboxIndicator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Text text;
    
    [SerializeField] private AudioSource gearboxAudioSource;

    private void Start()
    {
        car.GearChanged += OnGearChanged;     
    }
    private void OnDestroy()
    {
        car.GearChanged -= OnGearChanged; 
        //gearboxAudioSource.Stop();
    }
    private void OnGearChanged(string gearName)
    {
        text.text = gearName;
        gearboxAudioSource.Play();
    }
}
