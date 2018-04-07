using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : FSMState {

    void Awake()
    {
        Screen.SetResolution(576, 1024, false);
        stateID = StateID.Play;
        AddTransition(Transition.PauseButtonClick, StateID.Menu);
    }

    public override void DoBeforeEntering()
    {
        ctrl.view.ShowUpUI(ctrl.model.Score, ctrl.model.BestScore);
        ctrl.cameraManager.ZoomIn();
        ctrl.gameManager.StartGame();
    }

    public override void DoBeforeLeaving()
    {
        ctrl.view.HideUpUI();
        ctrl.view.ShowRetryButton();
        ctrl.gameManager.PauseGame();
    }

    public void OnPauseButtonClick()
    {
        fsm.PerformTransition(Transition.PauseButtonClick);
        ctrl.audioManager.PlayCursor();
    }

    public void OnRetryButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        ctrl.view.HideGameOverUI();
        ctrl.model.Retry();
        ctrl.gameManager.StartGame();
        ctrl.view.UpdateUpUI(0, ctrl.model.BestScore);
    }
}
