using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class PausePanel : ActiveOnlyDuringSomeGameStates
{
    public enum ePausePanelState
    {
        none, idle, fadeIn, fadeIn2, fadeIn3, display
    }

    [Header("Set in Inspector")]
    public float fadeTime = 1f;

    [Header("Set Dynamically")]
    [SerializeField]
    private ePausePanelState state = ePausePanelState.none;

    private Image img;
    private float stateStartTime, stateDuration;
    private ePausePanelState nextState;

    override public void Awake()
    {
        img = GetComponent<Image>();

        SetState(ePausePanelState.idle);

        base.Awake();
    }

    protected override void DetermineActive()
    {
        base.DetermineActive();
        if (AsteraX.GAME_STATE == AsteraX.eGameState.preLevel)
        {
            SetState(ePausePanelState.fadeIn);
        }
    }

    void SetState(ePausePanelState newState)
    {
        stateStartTime = realTime;

        switch (newState)
        {
            case ePausePanelState.idle:
                gameObject.SetActive(false);
                break;

            case ePausePanelState.fadeIn:
                gameObject.SetActive(true);
                img.color = Color.clear;
                stateDuration = fadeTime * 0.2f;
                nextState = ePausePanelState.fadeIn2;
                break;

            case ePausePanelState.fadeIn2:
                img.color = Color.black;
                stateDuration = fadeTime * 0.6f;
                nextState = ePausePanelState.fadeIn3;
                break;

            case ePausePanelState.fadeIn3:
                img.color = Color.black;
                stateDuration = fadeTime * 0.2f;
                nextState = ePausePanelState.display;
                break;

            case ePausePanelState.display:
                stateDuration = 999999;
                nextState = ePausePanelState.none;
                break;
        }

        state = newState;
    }

    void Update()
    {
        if (state == ePausePanelState.none) return;

        float u = (realTime - stateStartTime) / stateDuration;
        bool moveNext = false;
        if (u > 1)
        {
            u = 1;
            moveNext = true;
        }

        switch (state)
        {
            case ePausePanelState.fadeIn:
                img.color = new Color(0, 0, 0, u);
                break;

            case ePausePanelState.fadeIn2:
                break;

            case ePausePanelState.fadeIn3:
                break;

            case ePausePanelState.display:
                break;
        }

        if (moveNext)
        {
            SetState(nextState);
        }
    }

    float realTime
    {
        get { return Time.realtimeSinceStartup; }
    }
}
