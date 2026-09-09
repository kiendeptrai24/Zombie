

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : KienMonoBehaviour
{
    private MissionData missionData;
    [SerializeField] private TextMeshProUGUI misName;
    [SerializeField] private TextMeshProUGUI misDes;
    [SerializeField] private Button play;
    private string nameScene;
    protected override void Awake()
    {
        base.Awake();
        play.onClick.AddListener(() =>
        {
            GameController.Instance.SetMisstionData(missionData);
            SceneLoadManager.Instance.LoadRegularScene(nameScene);
        });
    }
    public void Show(MissionData data)
    {
        missionData = data;
        misName.text = data.missionName;
        misDes.text = data.description;
        nameScene = data.sceneName;
    }
}