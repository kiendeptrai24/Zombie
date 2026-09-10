using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : KienMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Slider healthbar;
    [SerializeField] private TextMeshProUGUI healthtxt;
    [SerializeField] private Button swapWeaponButton;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private GameManger gameManger;
    [SerializeField] private GameObject endPage;
    [SerializeField] private Button quitGame;
    [SerializeField] private GameObject settingsPage;
    [SerializeField] private Button openSetting;
    [SerializeField] private Button exitSetting;
    protected override void Awake()
    {
        base.Awake();
        swapWeaponButton.onClick.AddListener(() =>
        {
            weaponController.SwitchWeapon();
        });
        quitGame.onClick.AddListener(() =>
        {
            SFXManager.Instance.Stop();
            SceneLoadManager.Instance.LoadRegularScene("Menu");
        });
        openSetting.onClick.AddListener(() =>
        {
            settingsPage.SetActive(true);
            Time.timeScale = 0;
        });
        exitSetting.onClick.AddListener(() =>
        {
            settingsPage.SetActive(false);
            Time.timeScale = 1;
        });
        OnHealthChanged(100, 100);
    }
    private void OnEnable()
    {
        if (gameManger != null)
        {
            gameManger.OnTimeChange += UpdateTimer;
            gameManger.OnGameEnded += OnEndGame;
        }
    }
    private void OnDisable()
    {
        if (gameManger != null)
        {
            gameManger.OnTimeChange -= UpdateTimer;
            gameManger.OnGameEnded -= OnEndGame;
        }
    }
    private void UpdateTimer(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
    protected override void Start()
    {
        base.Start();
        playerHealth.OnHealthChanged += OnHealthChanged;
    }
    private void OnEndGame()
    {
        resultText.text = gameManger.GameResult_ == GameManger.GameResult.Win ? "You Won" : "You lost";
        endPage.SetActive(true);
    }

    private void OnHealthChanged(float max, float cur)
    {
        healthbar.value = cur / max;
        healthtxt.text = $"{cur}/{max}";
    }
}
