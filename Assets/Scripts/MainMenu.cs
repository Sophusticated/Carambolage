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
    public void ChallengeButtonFunc(){
        PlayGame(2);
    }
    public void EndlessButtonFunc(){
        PlayGame(3);
    }
    public void TutorialButtonFunc()
    {
        PlayGame(1);
    }
    public void MainMenuButtonFunc(){
        SceneManager.LoadScene(0);
    }
    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.SetInt("volumeChangedBool", 1);
    }
}
