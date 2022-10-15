using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MainPannel;
    [SerializeField]
    private GameObject InstructionPannel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        SoundManager.Instance.StopAllSounds();
        SoundManager.Instance.Play("Background1");
    }


    public void OnStartBtn()
    {
        SoundManager.Instance.Play("ButtonClick");
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
    }

    public void OnInstructionBtn()
    {
        SoundManager.Instance.Play("ButtonClick");
        MainPannel.SetActive(false);
        InstructionPannel.SetActive(true);
    }

    public void OnExitBtn()
    {
        SoundManager.Instance.Play("ButtonClick");
    }

    public void OnBackBtn()
    {
        SoundManager.Instance.Play("ButtonClick");
        MainPannel.SetActive(true);
        InstructionPannel.SetActive(false);
    }
}
