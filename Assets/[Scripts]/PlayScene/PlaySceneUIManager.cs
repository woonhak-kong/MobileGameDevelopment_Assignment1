using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneUIManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI CoinText;
    public TMPro.TextMeshProUGUI ScoreText;
    public TMPro.TextMeshProUGUI PowerUpCoinText;
    public TMPro.TextMeshProUGUI GameOverScoreText;

    public GameObject Pause;
    public GameObject Resume;

    [SerializeField]
    private GameObject PlayPannel;
    [SerializeField]
    private GameObject GameEndPannel;

    public int PowerUpPrice = 1;
    public int FireRateUpPrice = 1;

    // Start is called before the first frame update
    void Start()
    {
        ReviewUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSettingBtn()
    {
        GameEndPannel.SetActive(true);
        GameOverScoreText.text = "Score : " + LevelManager.Instance.Score.ToString();
        Time.timeScale = 0;
    }

    public void OnClickBackBtn()
    {
        GameEndPannel.SetActive(false);
        
        Time.timeScale = 1;
    }

    public void OnPauseBtn()
    {
        Pause.SetActive(false);
        Resume.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnResumBtn()
    {
        Pause.SetActive(true);
        Resume.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnToMainBtn()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void OnClickPowerUp()
    {
        if (PowerUpPrice > LevelManager.Instance.Coin)
            return;
        LevelManager.Instance.Coin -= PowerUpPrice;
        PowerUpPrice++;
        PowerUpCoinText.text = PowerUpPrice.ToString();
        FindObjectOfType<Player>().WeaponPowerUp();
        ReviewUI();
    }

    public void OnClickFireRateUp()
    {
        if (FireRateUpPrice > LevelManager.Instance.Coin)
            return;
        LevelManager.Instance.Coin -= FireRateUpPrice;
        FireRateUpPrice++;

        FindObjectOfType<Player>().WeaponFireRateUp();
        ReviewUI();
    }

    public void SetCoinText(int coin)
    {
        CoinText.text = coin.ToString();
    }

    public void SetScoreText(int score)
    {
        ScoreText.text = "Score : " + score.ToString();
    }

    public void ReviewUI()
    {
        CoinText.text = LevelManager.Instance.Coin.ToString();
        ScoreText.text = LevelManager.Instance.Score.ToString();
        PowerUpCoinText.text = PowerUpPrice.ToString();
    }
}
