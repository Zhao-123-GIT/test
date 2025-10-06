using UnityEngine;

public class PipelineController : MonoBehaviour
{
    [Header("Animator 控制")]
    public Animator pipelineAnimator;
    public string triggerName = "Start";

    [Header("可自定义鼠标点击开关")]
    public bool useMouseClick = true;

    [Header("自动触发选项")]
    public bool autoTrigger = false; // 机器人靠近自动触发
    public float triggerDistance = 2f;
    public Transform robot;

    private bool isRunning = false;

    void Update()
    {
        // 自动触发
        if (autoTrigger && robot != null && !isRunning)
        {
            float dist = Vector3.Distance(transform.position, robot.position);
            if (dist <= triggerDistance)
            {
                StartPipeline();
            }
        }
    }

    void OnMouseDown()
    {
        if (useMouseClick)
        {
            StartPipeline();
        }
    }

    public void StartPipeline()
    {
        if (isRunning) return;
        isRunning = true;

        // Animator Trigger
        if (pipelineAnimator != null)
        {
            pipelineAnimator.SetTrigger(triggerName);
        }

        // 可扩展：添加自定义函数
        Debug.Log("流水线已启动: " + gameObject.name);
    }

    public void StopPipeline()
    {
        isRunning = false;
        // 可以加 Animator Stop 或自定义逻辑
    }
}
