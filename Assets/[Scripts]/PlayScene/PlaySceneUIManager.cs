using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneUIManager : MonoBehaviour
{

    public TMPro.TextMeshProUGUI CoinText;

    [SerializeField]
    private GameObject PlayPannel;
    [SerializeField]
    private GameObject GameEndPannel;

    public int PowerUpPrice = 1;
    public int FireRateUpPrice = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSettingBtn()
    {
        GameEndPannel.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickBackBtn()
    {
        GameEndPannel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnPauseBtn()
    {

    }

    public void OnResumBtn()
    {

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
    
    public void ReviewUI()
    {
        CoinText.text = LevelManager.Instance.Coin.ToString();
    }
}
