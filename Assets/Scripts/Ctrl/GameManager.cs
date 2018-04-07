using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool isPause = true;
    private Shape currentShape = null;
    private Ctrl ctrl;
    private Transform blockHolder;

    public Shape[] shapes;
    public Color[] colors;

    void Awake()
    {
        Screen.SetResolution(576, 1024, false);
        ctrl = GetComponent<Ctrl>();
        blockHolder = transform.Find("BlockHolder");
    }

    void Update()
    {
        if (isPause)
            return;
        if(currentShape == null)
        {
            SpawnShape();
        }
    }

    public void StartGame()
    {
        isPause = false;
        if (currentShape != null)
            currentShape.ResumeFall();
    }

    public void PauseGame()
    {
        isPause = true;
        if (currentShape != null)
            currentShape.PauseFall();
    }

    void SpawnShape()
    {
        int index = Random.Range(0, shapes.Length);
        int indexColor = Random.Range(0, colors.Length);
        currentShape = GameObject.Instantiate(shapes[index]);
        currentShape.transform.parent = blockHolder;
        currentShape.Init(colors[indexColor], ctrl, this);
    }

    // 方块已经到底了 置空当前方块
    public void FallDown()
    {
        currentShape = null;
        if (ctrl.model.isDataUpdate)
        {
            ctrl.view.UpdateUpUI(ctrl.model.Score, ctrl.model.BestScore);
        }
        foreach(Transform t in blockHolder)
        {
            if (t.childCount <= 1)
                Destroy(t.gameObject);  // 清理垃圾
        }
        if (ctrl.model.IsGameOver())
        {
            PauseGame();
            ctrl.view.ShowGameOverUI(ctrl.model.Score);
        }
    }

    public void ClearShape()
    {
        if(currentShape != null)
        {
            Destroy(currentShape.gameObject);
            currentShape = null;
        }
    }
}
