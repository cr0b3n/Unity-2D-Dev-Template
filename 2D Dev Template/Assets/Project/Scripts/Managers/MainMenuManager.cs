﻿using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class MainMenuManager : MonoBehaviour {

    //public TextMeshPro highScoreText;

    public CanvasGroup title;
    private float desiredAlpha;

    private bool isTransitioning;
    //public const string HIGH_SCORE_TEXT = "HIGH SCORE: ";
    private bool isActive;

    private IEnumerator Start() {

        desiredAlpha = 1f;
        //highScoreText.text = string.Format("{0:n0}", PlayerData.GetHighScore());
        Time.timeScale = 1f;

        SceneController.RemoveTransition();
        SceneController.OnSceneAddetiveClose += OnSceneAddetiveClose;
        AudioListener.pause = false;
        AudioManager.PlayBGM(BGMType.Menu);

        yield return new WaitForSecondsRealtime(1f);

        isActive = true;
    }

    private void Update() {
        title.alpha = Mathf.Lerp(title.alpha, desiredAlpha, 0.1f);
    }

    private void OnDisable() {
        SceneController.OnSceneAddetiveClose -= OnSceneAddetiveClose;
    }

    private void OnSceneAddetiveClose() {
        desiredAlpha = 1f;
    }

    public void OpenSceneAddetive(int index) {

        if (isTransitioning || !isActive) return;

        PlayUIAudio();

        switch (index) {
            default:
            case 2: //how to play
                SceneController.LoadSceneAddetive(2);
                break;
            case 3: //credits        
                SceneController.LoadSceneAddetive(3);              
                break;
        }
        desiredAlpha = 0f;
    }

    private void PlayUIAudio() {
        AudioManager.PlayUIAudio(GUIAudio.RegButton);
    }

    public void StartGame() {

        if (isTransitioning || !isActive) return;

        isTransitioning = true;
        SceneController.BeginTransitionAndLoadScene(1);
        AudioManager.PlayUIAudio(GUIAudio.StartButton);
    }
}
