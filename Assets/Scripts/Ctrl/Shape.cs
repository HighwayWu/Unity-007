using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {

    private Ctrl ctrl;
    private GameManager gameManager;
    private Transform pivot;
    private bool isPause = false;
    private float timer = 0;
    private float stepTime = 0.8f;
    private int multiple = 16;
    private bool isSpeedUp = false;

    enum slideVector { nullVector, up, down, left, right };
    private Vector2 touchFirst = Vector2.zero; //手指开始按下的位置
    private Vector2 touchSecond = Vector2.zero; //手指拖动的位置
    private slideVector currentVector = slideVector.nullVector;//当前滑动方向
    private float timeCount;//时间计数器  
    public float offsetTime = 0.1f;//判断的时间间隔 
    public float SlidingDistance = 80f;

    void Awake()
    {
        Screen.SetResolution(576, 1024, false);
        pivot = transform.Find("Pivot");
    }

    void Update()
    {
        if (isPause)
            return;
        timer += Time.deltaTime;
        if(timer > stepTime)
        {
            timer = 0;
            Fall();
        }
        InputControl(Slide());
    }

    public void Init(Color color, Ctrl ctrl, GameManager gameManager)
    {
        foreach(Transform t in transform)
        {
            if(t.tag == "Block")
            {
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }
        this.ctrl = ctrl;
        this.gameManager = gameManager;
    }

    private void Fall()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
        if(ctrl.model.IsValidMapPosition(this.transform) == false)
        {
            pos.y += 1;
            transform.position = pos;
            isPause = true;
            if (ctrl.model.PutShapeInMap(this.transform)) // 返回true表示有某行被销毁
                ctrl.audioManager.PlayLineClear();
            gameManager.FallDown();
            return;
        }
        ctrl.audioManager.PlayDrop();
    }

    private void InputControl(int slide)
    {
        // if (isSpeedUp) return; // 加速时是否可控
        int h = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || slide == 1)
            h = -1;
        else if (Input.GetKeyDown(KeyCode.RightArrow) || slide == 2)
            h = 1;
        if(h != 0)
        {
            Vector3 pos = transform.position;
            pos.x += h;
            transform.position = pos;
            if (ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                pos.x -= h;
                transform.position = pos;
            }
            else
                ctrl.audioManager.PlayControl();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || slide == 3)
        {
            transform.RotateAround(pivot.position, Vector3.forward, -90);
            if (ctrl.model.IsValidMapPosition(this.transform) == false)
                transform.RotateAround(pivot.position, Vector3.forward, 90);
            else
                ctrl.audioManager.PlayControl();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || slide == 4)
        {
            isSpeedUp = true;
            stepTime /= multiple;
        }
    }

    public void PauseFall()
    {
        isPause = true;
    }

    public void ResumeFall()
    {
        isPause = false;
    }

    int Slide()   // 滑动方法
    {
        if (Input.touchCount == 1)
        //判断当前手指是按下事件 
        {
            touchFirst = Input.GetTouch(0).position;//记录开始按下的位置
        }
        else
        {
            return 0;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        //判断当前手指是拖动事件
        {
            touchSecond = Input.GetTouch(0).position;

            timeCount += Time.deltaTime;  //计时器

            if (timeCount > offsetTime)
            {
                touchSecond = Input.GetTouch(0).position; //记录结束下的位置
                Vector2 slideDirection = touchFirst - touchSecond;
                float x = slideDirection.x;
                float y = slideDirection.y;

                if (y + SlidingDistance < x && y > -x - SlidingDistance)
                {

                    if (currentVector == slideVector.left)
                    {
                        return 0;
                    }

                    Debug.Log("right");
                    return 2;
                }
                else if (y > x + SlidingDistance && y < -x - SlidingDistance)
                {
                    if (currentVector == slideVector.right)
                    {
                        return 0;
                    }

                    Debug.Log("left");
                    return 1;
                }
                else if (y > x + SlidingDistance && y - SlidingDistance > -x)
                {
                    if (currentVector == slideVector.up)
                    {
                        return 0;
                    }

                    Debug.Log("up");
                    return 3;
                }
                else if (y + SlidingDistance < x && y < -x - SlidingDistance)
                {
                    if (currentVector == slideVector.down)
                    {
                        return 0;
                    }

                    Debug.Log("Down");
                    return 4;
                }

                timeCount = 0;
                touchFirst = touchSecond;
            }
            if (Input.touchCount == 0)
            {
                //滑动结束  
                currentVector = slideVector.nullVector;
            }
        }
        return 0;
    }

}
