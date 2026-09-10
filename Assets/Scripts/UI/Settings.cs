using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : KienMonoBehaviour
{
    [Header("Master")]
    [SerializeField] private Slider allSd;
    [SerializeField] private TextMeshProUGUI alltext;

    [Header("Music")]
    [SerializeField] private Slider musicSd;
    [SerializeField] private TextMeshProUGUI musictext;

    [Header("SFX")]
    [SerializeField] private Slider sfxSd;
    [SerializeField] private TextMeshProUGUI sfxtext;

    protected override void Awake()
    {
        base.Awake();

        allSd.onValueChanged.AddListener(OnAllValueChanged);
        musicSd.onValueChanged.AddListener(OnMusicValueChanged);
        sfxSd.onValueChanged.AddListener(OnSfxValueChanged);
    }

    protected override void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        float master = PlayerPrefs.GetFloat(
            GameConstantsUtils.MASTER_VOL_PARAM,
            10f
        );

        float music = PlayerPrefs.GetFloat(
            GameConstantsUtils.MUSIC_VOL_PARAM,
            10f
        );

        float sfx = PlayerPrefs.GetFloat(
            GameConstantsUtils.SFX_VOL_PARAM,
            10f
        );

        allSd.SetValueWithoutNotify(master);
        musicSd.SetValueWithoutNotify(music);
        sfxSd.SetValueWithoutNotify(sfx);

        UpdateMasterText(master);
        UpdateMusicText(music);
        UpdateSfxText(sfx);
    }

    private void OnAllValueChanged(float value)
    {
        MusicManager.Instance.ApplyVolumeToMixer(GameConstantsUtils.MASTER_VOL_PARAM, value);
        UpdateMasterText(value);
    }

    private void OnMusicValueChanged(float value)
    {
        MusicManager.Instance.ApplyVolumeToMixer(GameConstantsUtils.MUSIC_VOL_PARAM, value);
        UpdateMusicText(value);
    }
    private void OnSfxValueChanged(float value)
    {
        SFXManager.Instance.SetSfxVolume( value);
        UpdateSfxText(value);
    }

    private void UpdateMasterText(float value)
    {
        alltext.text = $"{value * 10:F0}%";
    }

    private void UpdateMusicText(float value)
    {
        musictext.text = $"{value * 10:F0}%";
    }

    private void UpdateSfxText(float value)
    {
        sfxtext.text = $"{value * 10:F0}%";
    }
    private void OnDisable()
    {
        SaveData(GameConstantsUtils.MASTER_VOL_PARAM, allSd.value);
        SaveData(GameConstantsUtils.MUSIC_VOL_PARAM, musicSd.value);
        SaveData(GameConstantsUtils.SFX_VOL_PARAM, sfxSd.value);
    }
    protected void OnDestroy()
    {
        allSd.onValueChanged.RemoveListener(OnAllValueChanged);
        musicSd.onValueChanged.RemoveListener(OnMusicValueChanged);
        sfxSd.onValueChanged.RemoveListener(OnSfxValueChanged);
    }
    private void SaveData(string id, float value)
    {
        PlayerPrefs.SetFloat(
            id,
            value
        );
        PlayerPrefs.Save();
    }
}