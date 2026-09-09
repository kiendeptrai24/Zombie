

using UnityEngine;

public class MenuScreenManager : ScreenManager
{
    [SerializeField] private GameObject m_MenuScreen;
    [SerializeField] private GameObject m_PlayScreen;
    [SerializeField] private GameObject m_SettingsScreen;
    protected override void Awake()
    {
        base.Awake();
        m_Screens.Add("Menu", m_MenuScreen);
        m_Screens.Add("Play", m_PlayScreen);
        m_Screens.Add("Settings", m_SettingsScreen);
    }
}