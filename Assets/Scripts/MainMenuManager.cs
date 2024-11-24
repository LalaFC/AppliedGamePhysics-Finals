using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject optionsPanel;
    [SerializeField] Volumes sounds;
    [SerializeField] Slider volumeSlider;

    private AudioSource audiosource;
    void Start()
    {
        if (optionsPanel == null) optionsPanel = GameObject.Find("OptionsPanel");
        GameObject.Find("OptionsPanel").transform.DOMoveX(-500, 0f);
        audiosource = GetComponent<AudioSource>();
        volumeSlider.value = sounds.volume;
    }

    // Update is called once per frame
    void Update()
    {
        audiosource.volume = sounds.volume;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowOptions()
    {
        optionsPanel.transform.DOMoveX(500, 1);
    }
    public void HideOptions()
    {
        optionsPanel.transform.DOMoveX(-500, 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeVolume()
    {
        sounds.volume = volumeSlider.value;
    }
}
