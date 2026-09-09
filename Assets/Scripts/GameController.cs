using Unity.VisualScripting;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private MissionData missionData;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    public void SetMisstionData(MissionData missionData) => this.missionData = missionData;
    public MissionData GetMissionData() => missionData;
}
