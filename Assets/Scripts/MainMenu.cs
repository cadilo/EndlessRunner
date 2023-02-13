using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text coinsText;

    private void Start()
    {
        
        int coints = PlayerPrefs.GetInt("coins");
        coinsText.text = coints.ToString(); 
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
