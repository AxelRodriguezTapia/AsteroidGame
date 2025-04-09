using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class MainMenuPanel : ActiveOnlyDuringSomeGameStates
{
    public enum eMainMenuPanelState
    {
        none, idle, fadeIn, fadeIn2, fadeIn3, display
    }

    [Header("Set in Inspector")]
    public float fadeTime = 1f;

    [Header("Set Dynamically")]
    [SerializeField]
    private eMainMenuPanelState state = eMainMenuPanelState.none;

    private Image img;
    private Button startButton;
    Text infoText;
    private float stateStartTime, stateDuration;
    private eMainMenuPanelState nextState;

    override public void Awake()
    {
        img = GetComponent<Image>();
        Transform infoT = transform.Find("InfoText");
        if (infoT == null) {
            Debug.LogWarning("LevelAdvancePanel:Start() - LevelAdvancePanel lacks a child named InfoText.");
            return;
        }
        infoText = infoT.GetComponent<Text>();

        // Buscar el botón hijo
        Transform buttonT = transform.Find("StartButton");
        if (buttonT == null)
        {
            Debug.LogWarning("MainMenuPanel:Start() - Falta un hijo llamado StartButton.");
            return;
        }
        startButton = buttonT.GetComponent<Button>();
        if (startButton == null)
        {
            Debug.LogWarning("MainMenuPanel:Start() - El hijo StartButton necesita un componente Button.");
            return;
        }

        startButton.onClick.AddListener(OnStartButtonPressed);

        SetState(eMainMenuPanelState.idle);

        base.Awake();
    }

    protected override void DetermineActive()
    {
        base.DetermineActive();
        if (AsteraX.GAME_STATE == AsteraX.eGameState.mainMenu)
        {
            SetState(eMainMenuPanelState.fadeIn);
        }
    }

    void SetState(eMainMenuPanelState newState)
    {
        stateStartTime = realTime;

        switch (newState)
        {
            case eMainMenuPanelState.idle:
                gameObject.SetActive(false);
                break;

            case eMainMenuPanelState.fadeIn:
                gameObject.SetActive(true);
                img.color = Color.clear;
                stateDuration = fadeTime * 0.2f;
                nextState = eMainMenuPanelState.fadeIn2;
                infoText.text = "Level: "+AsteraX.levelIndex;
                break;

            case eMainMenuPanelState.fadeIn2:
                img.color = Color.black;
                stateDuration = fadeTime * 0.6f;
                nextState = eMainMenuPanelState.fadeIn3;
                break;

            case eMainMenuPanelState.fadeIn3:
                img.color = Color.black;
                stateDuration = fadeTime * 0.2f;
                nextState = eMainMenuPanelState.display;
                break;

            case eMainMenuPanelState.display:
                stateDuration = 999999;
                nextState = eMainMenuPanelState.none;
                break;
        }

        state = newState;
    }

    void Update()
    {
        if (state == eMainMenuPanelState.none) return;

        float u = (realTime - stateStartTime) / stateDuration;
        bool moveNext = false;
        if (u > 1)
        {
            u = 1;
            moveNext = true;
        }

        switch (state)
        {
            case eMainMenuPanelState.fadeIn:
                img.color = new Color(0, 0, 0, u);
                break;

            case eMainMenuPanelState.fadeIn2:
                break;

            case eMainMenuPanelState.fadeIn3:
                break;

            case eMainMenuPanelState.display:
                break;
        }

        if (moveNext)
        {
            SetState(nextState);
        }
    }

    void OnStartButtonPressed()
    {
        Debug.Log("Start button pressed! Changing game state...");

        AsteraX.StartGame();
    }

    float realTime
    {
        get { return Time.realtimeSinceStartup; }
    }
}
