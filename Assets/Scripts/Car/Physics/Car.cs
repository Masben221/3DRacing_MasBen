using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    public event UnityAction<string> GearChanged;

    [SerializeField] private float maxSteerAngle; // максимальный угол поворота колес
    [SerializeField] private float maxBrakeTorque; // максимальный тормозящий момент

    [Header("Engine")]
    [SerializeField] private AnimationCurve engineTorqueCurve; // кривая показывающая зависимость крутящего момента от оборотов двигателя
    [SerializeField] private float engineMaxTorque; // максимальный крутящий момент

    //Debug
    [SerializeField] private float engineTorque; // текущий крутящий момент
    [SerializeField] private float engineRpm; // текущие обороты двигателя

    [SerializeField] private float engineMinRpm; // минимальные обороты двигателя
    [SerializeField] private float engineMaxRpm; // максимальные обороты двигателя

    [Header("Gearbox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio;

    //Debug
    [SerializeField] private int selectedGearIndex;
    [SerializeField] private float selectedGear;
    [SerializeField] private float rearGear;

    [SerializeField] private float upShiftEngineRpm;
    [SerializeField] private float downShiftEngineRpm;

    [SerializeField] private int maxSpeed;

    public float LinearVelocity => chassis.LinearVelocity;
    public float NormalizeLinearVelocity => chassis.LinearVelocity / maxSpeed;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;

    public float EngineRpm => engineRpm;
    public float EngineMaxRpm => engineMaxRpm;

    public float SelectedGear => selectedGear;

    private CarChassis chassis; //Шасси
    public Rigidbody Rigidbody => chassis == null ? GetComponent<CarChassis>().Rigidbody :  chassis.Rigidbody;

    //Debug
    [SerializeField] public float linearVelocity;

    public float ThrottleControl; //Педаль газа
    public float SteerControl; //Поворот
    public float BrakeControl; //Тормоз

    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }
    private void Update()
    {
        linearVelocity = LinearVelocity;

        UpdateEngineTorque();

        AutoGearShift();

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.MotorTorque = engineTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTorque = maxBrakeTorque * BrakeControl;  
    }
    public void Reset()
    {
        chassis.Reset();

        chassis.MotorTorque = 0;
        chassis.BrakeTorque = 0;
        chassis.SteerAngle = 0;

        ThrottleControl = 0;
        BrakeControl = 0;
        SteerControl = 0;
    }
    public void Respawn(Vector3 position, Quaternion rotation)
    {
        Reset();

        transform.position = position;
        transform.rotation = rotation;
    }

    #region Gearbox

    public string GetSelectedGearName()
    {
        if (selectedGear == 0) return "N";

        if (selectedGear == rearGear) return "R";        

        return (selectedGearIndex + 1).ToString();
    }
    public void AutoGearShift()
    {
        if (selectedGear < 0) return;

        if (engineRpm >= upShiftEngineRpm)
            UpGear();

        if (engineRpm < downShiftEngineRpm && selectedGearIndex > 0)
            DownGear();
    }
    public void UpGear()
    {
        ShiftGear(selectedGearIndex + 1);
    }
    public void DownGear()
    {
        ShiftGear(selectedGearIndex - 1);
    }
    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;
        GearChanged?.Invoke(GetSelectedGearName());
    }
    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }
    public void ShiftToNeutral()
    {
        selectedGear = 0;
        GearChanged?.Invoke(GetSelectedGearName());
    }
    private void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;

        GearChanged?.Invoke(GetSelectedGearName());
    }
    private void UpdateEngineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * selectedGear * finalDriveRatio);
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear) * gears[0];
    }

    #endregion
}
