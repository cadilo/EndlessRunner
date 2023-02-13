using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LosePanel : MonoBehaviour
{
    [SerializeField] Text recordText;

    private void Start()
    {
        int lasrRunScore = PlayerPrefs.GetInt("lastRunScore");
        int recordScore = PlayerPrefs.GetInt("recordScore");

        if(lasrRunScore > recordScore)
        {
            recordScore = lasrRunScore;
            PlayerPrefs.SetInt("recordScore", recordScore);
            recordText.text = recordScore.ToString();
        }
        else
        {
            recordText.text = recordScore.ToString();
        }
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(1); 
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
