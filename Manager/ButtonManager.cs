using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public RectTransform title;
    public RectTransform titleoption;
    public RectTransform gameoption;
    public RectTransform gamaover;
    public GameManager gm;

    public void OnClickStartButton()
    {
        Time.timeScale = 1;
        // Destroy(title);
        title.gameObject.SetActive(false);
        gm.coin=PlayerPrefs.GetInt("Coin");



    }

    public void GameOver() {
        gamaover.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnClickOptionButton()
    {
        titleoption.gameObject.SetActive(true);
    }

    public void OnClickOptionCloseButtion() {
        titleoption.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public void OnClickGameOptionCloseButtion()
    {
        titleoption.gameObject.SetActive(false);


    }

    public void OnClickGameOptionButton()
    {
        gameoption.gameObject.SetActive(true);
    }
    public void OnClickRestart() {

        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("Coin", gm.coin);
        if (gm.maxscore < gm.score) {
            PlayerPrefs.SetInt("MaxScore", gm.score);
        }


    }



    public void OnOptionGameOptionCloseButtion()
    {
        gameoption.gameObject.SetActive(false);
        Time.timeScale = 1;

    }
    public void OnClickTitle() {
        SceneManager.LoadScene(0);
    }


    public void OnClickExitButton()
    {

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
