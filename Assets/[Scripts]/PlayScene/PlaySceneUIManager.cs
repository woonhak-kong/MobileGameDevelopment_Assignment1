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
    public GameObject BackButton;
    public GameObject GameoverText;

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
        SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.Play("Background2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSettingBtn()
    {
        SoundManager.Instance.Play("ButtonClick");
        GameEndPannel.SetActive(true);
        GameOverScoreText.text = "Score : " + LevelManager.Instance.Score.ToString();
        BackButton.SetActive(true);
        GameoverText.SetActive(false);
        Time.timeScale = 0;
    }

    public void OnClickBackBtn()
    {
        SoundManager.Instance.Play("ButtonClick");
        GameEndPannel.SetActive(false);
        BackButton.SetActive(false);
        GameoverText.SetActive(true);
        Time.timeScale = 1;
    }

    public void OnPauseBtn()
    {
        SoundManager.Instance.Play("ButtonClick");
        Pause.SetActive(false);
        Resume.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnResumBtn()
    {
        SoundManager.Instance.Play("ButtonClick");
        Pause.SetActive(true);
        Resume.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnToMainBtn()
    {
        SoundManager.Instance.Play("ButtonClick");
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
        SoundManager.Instance.Play("PowerUp");
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
        ScoreText.text = "Score : " + LevelManager.Instance.Score.ToString();
        PowerUpCoinText.text = PowerUpPrice.ToString();
    }

    public void GameOver()
    {
        //SoundManager.Instance.Play("ButtonClick");
        GameEndPannel.SetActive(true);
        GameOverScoreText.text = "Score : " + LevelManager.Instance.Score.ToString();
        //BackButton.SetActive(true);
        GameoverText.SetActive(true);
        Time.timeScale = 0;
    }
}
