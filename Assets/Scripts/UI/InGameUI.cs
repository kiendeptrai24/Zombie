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
    [SerializeField] private GameObject endPage;
    [SerializeField] private Button quitGame;

    protected override void Awake()
    {
        base.Awake();
        swapWeaponButton.onClick.AddListener(() =>
        {
            weaponController.SwitchWeapon();
        });
        quitGame.onClick.AddListener(() =>
        {
            SceneLoadManager.Instance.LoadRegularScene("Menu");
        });
        OnHealthChanged(1, 1);
    }
    private void OnEnable()
    {
        GameManger.Instance.OnTimeChange += UpdateTimer;
        GameManger.Instance.OnGameEnded += OnEndGame;
    }
    private void OnDisable()
    {
        if (GameManger.Instance != null)
        {
            GameManger.Instance.OnTimeChange -= UpdateTimer;
            GameManger.Instance.OnGameEnded -= OnEndGame;
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
        resultText.text = GameManger.Instance.GameResult_ == GameManger.GameResult.Win ? "You Won" : "You lost";
        endPage.SetActive(true);
    }

    private void OnHealthChanged(float max, float cur)
    {
        healthbar.value = cur / max;
        healthtxt.text = $"{cur}/{max}";
    }
}
