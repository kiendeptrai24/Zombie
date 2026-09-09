using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class MissionData
{
    public MissionData(string missionName, string des, float dur, string sceName)
    {
        this.missionName = missionName;
        this.description = des;
        this.duration = dur;
        this.sceneName = sceName;
    }
    public string missionName;
    public string description;
    public float duration;
    public string sceneName;
}
public class HomePagePresenter : KienMonoBehaviour
{
    [SerializeField] private HomePageView view;
    public List<MissionDataOS> missionDataOs;
    public List<MissionData> missionDatas = new();
    protected override void Awake()
    {
        base.Awake();
        foreach (var dataOs in missionDataOs)
        {
            missionDatas.Add(new MissionData(dataOs.missionName, dataOs.description, dataOs.duration, dataOs.sceneName));
        }
    }
    protected override void Start()
    {
        base.Start();
        view.Show(missionDatas);
    }
}
