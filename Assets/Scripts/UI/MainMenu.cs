using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [Header("Pages")]
    [SerializeField] private GameObject mainPage;
    [SerializeField] private GameObject optionsPage;

    [Header("Slider")]
    private Slider volSlider;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);
        quitButton.onClick.AddListener(Quit);
        backButton.onClick.AddListener(OnBackClicked);
        if (volSlider)
        {
            volSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value));
        } 


    }

    private void OnPlayButtonClicked()
    {
        SceneTransitionManager.Instance.LoadScene("Level"); 
    }

    private void OnOptionsButtonClicked()
    {
        mainPage.SetActive(false);
        optionsPage.SetActive(true);
        if (volSlider)
        {
            float value = volSlider.value;
        } 
    }

    private void OnBackClicked()
    {
        optionsPage.SetActive(false);
        mainPage.SetActive(true); 

    }

    void OnSliderValueChanged(float value)
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif 
    }
}
