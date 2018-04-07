using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState {

    void Awake()
    {
        Screen.SetResolution(576, 1024, false);
        stateID = StateID.Menu;
        AddTransition(Transition.StartButtonClick, StateID.Play);
    }

    public override void DoBeforeEntering()
    {
        // Menu状态初始化时自动调用View下的显示UI动画
        ctrl.view.ShowTitle();
        ctrl.view.ShowMenu();
        ctrl.cameraManager.ZoomOut();
    }

    public override void DoBeforeLeaving()
    {
        ctrl.view.HideTitle();
        ctrl.view.HideMenu();
    }

    public void OnStartButtonClick()
    {
        // 执行状态的转换
        fsm.PerformTransition(Transition.StartButtonClick);
        ctrl.audioManager.PlayCursor();
    }

    public void OnRankButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        ctrl.view.ShowRankUI(ctrl.model.Score, ctrl.model.BestScore, ctrl.model.NumbersGame);
    }

    public void OnRetryButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        ctrl.gameManager.ClearShape();
        ctrl.model.Retry();
        fsm.PerformTransition(Transition.StartButtonClick);
    }
}
