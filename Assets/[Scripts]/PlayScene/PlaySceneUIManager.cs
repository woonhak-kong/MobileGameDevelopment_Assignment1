using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneUIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject PlayPannel;
    [SerializeField]
    private GameObject GameEndPannel;

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
}
