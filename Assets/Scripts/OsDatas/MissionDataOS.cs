using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "Game/Mission Data")]
public class MissionDataOS : ScriptableObject
{
    public string missionName;
    public string description;
    public float duration;
    public string sceneName;
}