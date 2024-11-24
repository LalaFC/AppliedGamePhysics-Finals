using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] Slider heightSlider;
    [SerializeField] GameObject endPanel;
    [SerializeField] Volumes sounds;
    [SerializeField] TextMeshProUGUI statsText;
    [SerializeField] Level2Changes levelMarker;

    private PlayerMovement playerMove;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        heightSlider.maxValue = transform.position.y - playerTransform.position.y;
        playerMove = playerTransform.GetComponent<PlayerMovement>();
        PlayerMovement.endGame.AddListener(GameEnd);
        GetComponent<AudioSource>().volume = sounds.volume;
    }

    // Update is called once per frame
    void Update()
    {
        heightSlider.value = heightSlider.maxValue - (transform.position.y - playerTransform.position.y);
        statsText.text = levelMarker.GetLevel() + "\n" + playerMove.GetStatsJumpForce();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
    void GameEnd()
    {
        endPanel.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            heightSlider.gameObject.SetActive(false);
        }
    }
}
