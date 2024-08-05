using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    public void Awake()
    {
        if (PlayerPrefs.GetInt("volumeChangedBool") == 1)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        }
    }
    public void PlayGame(int levelNo){
        SceneManager.LoadScene(levelNo);
    }
    public void standardButtonFunc(){
        PlayGame(1);
    }
    public void endlessButtonFunc(){
        PlayGame(2);
    }
    public void restartButtonFunc(){
        PlayGame(1);
        General.resetAll();
    }
    public void mainMenuButtonFunc(){
        SceneManager.LoadScene(0);
    }
    public void changeVolume()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.SetInt("volumeChangedBool", 1);
    }
}
