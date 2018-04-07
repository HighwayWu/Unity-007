using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour {

    [HideInInspector]
    public Model model;
    [HideInInspector]
    public View view;
    [HideInInspector]
    public CameraManager cameraManager;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public AudioManager audioManager;

    private FSMSystem fsm;

    void Awake()
    {
        Screen.SetResolution(576, 1024, false);
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        cameraManager = GetComponent<CameraManager>();
        gameManager = GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();
    }

	void Start () {
        // 游戏开始时创建FSM状态机
        MakeFSM();
    }
	
	void Update () {
		
	}

    void MakeFSM()
    {
        fsm = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();
        foreach(FSMState state in states)
        {
            fsm.AddState(state, this);
        }
        // 状态机创建初始状态: Menu状态
        MenuState s = GetComponentInChildren<MenuState>();
        fsm.SetCurrentState(s);
    }
}
