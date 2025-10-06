using UnityEngine;

public class PipelineTriggerEnhanced : MonoBehaviour
{
    [Header("机器人对象")]
    public GameObject robot; // 拖入机器人对象
    public float triggerDistance = 2f; // 触发距离

    [Header("流水线对象")]
    public GameObject[] pipelines; // 多条流水线

    [Header("Animator 控制")]
    public bool useAnimator = true; // 是否使用 Animator
    public string animatorTriggerName = "Start"; // Animator Trigger 名称

    [Header("自定义脚本控制")]
    public bool useCustomScript = false; // 是否使用自定义脚本
    public string customMethodName = "StartConveyor"; // 自定义方法名

    [Header("触发方式")]
    public bool oneTimeTrigger = true; // 是否只触发一次
    private bool hasTriggered = false; // 内部状态

    void Update()
    {
        if (robot == null || pipelines.Length == 0) return;
        if (oneTimeTrigger && hasTriggered) return;

        // 计算机器人到触发器的距离
        float distance = Vector3.Distance(robot.transform.position, transform.position);

        if (distance <= triggerDistance)
        {
            TriggerPipelines();
            hasTriggered = true;
        }
    }

    void TriggerPipelines()
    {
        foreach (GameObject pipeline in pipelines)
        {
            if (pipeline == null) continue;

            // Animator 控制
            if (useAnimator)
            {
                Animator anim = pipeline.GetComponent<Animator>();
                if (anim != null)
                {
                    anim.SetTrigger(animatorTriggerName);
                }
            }

            // 自定义脚本控制
            if (useCustomScript)
            {
                MonoBehaviour script = pipeline.GetComponent<MonoBehaviour>();
                if (script != null)
                {
                    script.Invoke(customMethodName, 0f);
                }
            }
        }
    }
}
