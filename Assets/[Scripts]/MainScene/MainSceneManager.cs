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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartBtn()
    {
        SceneManager.LoadScene("PlayScene", LoadSceneMode.Additive);
    }

    public void OnInstructionBtn()
    {
        MainPannel.SetActive(false);
        InstructionPannel.SetActive(true);
    }

    public void OnExitBtn()
    {

    }

    public void OnBackBtn()
    {
        MainPannel.SetActive(true);
        InstructionPannel.SetActive(false);
    }
}
