using UnityEngine;
using UnityEngine.UI;

public class UIRaceMyResultPanel : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceStateTracker>
{
    //������� ����� ������
    [Header("Statistics")]
    [SerializeField] private UnityEngine.GameObject goldTimeObject;
    [SerializeField] private UnityEngine.GameObject recordTimeObject;
    [SerializeField] private UnityEngine.GameObject currentTimeObject;
    [SerializeField] private Text goldTime;
    [SerializeField] private Text recordTime;
    [SerializeField] private Text currentTime;

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime obj) => raceResultTime = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceResultTime.ResultUpdated += OnRaceCompleted;             

        //���������� ���������� ������ � ������
        goldTimeObject.SetActive(false);
        recordTimeObject.SetActive(false);
        currentTimeObject.SetActive(false);
    }

    private void OnDestroy()
    {
        raceResultTime.ResultUpdated -= OnRaceCompleted;
    }
    
    private void OnRaceCompleted()
    {
        //���� �����������
        goldTime.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
        recordTime.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        currentTime.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime);

        //��������� ���������� ������ � �����
        goldTimeObject.SetActive(true);
        recordTimeObject.SetActive(true);
        currentTimeObject.SetActive(true);
    }
}
