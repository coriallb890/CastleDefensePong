using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI leftWins;
    [SerializeField] private TextMeshProUGUI rightWins;
    [SerializeField] private TextMeshProUGUI txtHealthLeft;
    [SerializeField] private TextMeshProUGUI txtHealthRight;
    [SerializeField] private TextMeshProUGUI instructions;
    [SerializeField] private GameObject panel;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver){
            if (Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKeyDown(KeyCode.Q)){
                Application.Quit();
            }
        }
    }

    public void GameOverSequence()
    {
        isGameOver = true;
        panel.SetActive(true);
        if (txtHealthLeft.text == "0"){
            leftWins.enabled = false;
        }
        else{
            rightWins.enabled = false;
        }
    }
}
