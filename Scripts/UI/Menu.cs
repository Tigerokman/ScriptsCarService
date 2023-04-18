using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    [SerializeField] private AudioSource _start;

    private string _language = "Language";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(_language) == false)
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                PlayerPrefs.SetInt(_language, 1);
            else
                PlayerPrefs.SetInt(_language, 0);
        }
        Translator.SelectLanguage(PlayerPrefs.GetInt(_language));
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void PauseOff()
    {
        Time.timeScale = 1;
        _start.Play();
    }

    public void Restart()
    {
        PauseOff();
        SceneManager.LoadScene(0);
    }

    public void LangeaugeChange(int IDlanguage)
    {
        PlayerPrefs.SetInt(_language, IDlanguage);
        Translator.SelectLanguage(PlayerPrefs.GetInt(_language));
        Debug.Log(PlayerPrefs.GetInt(_language));
    }

    public void OpenAnimationPanel(GameObject panel)
    {
        float time = 2f;
        panel.SetActive(true);
        StartCoroutine(PanelOff(panel, time));
    }

    public void SoundOff(bool isOff)
    {
        AudioListener.pause = isOff;
        AudioListener.volume = isOff ? 0f : 1f;
    }

    private IEnumerator PanelOff(GameObject panel,float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        panel.SetActive(false);
    }
}

