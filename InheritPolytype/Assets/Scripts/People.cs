using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 人類：移動速度、代理器、動畫控制器、死亡
/// </summary>
public class People : MonoBehaviour
{
    [Header("移動速度"), Range(0, 10)]
    public float speed = 1.5f;

    /// <summary>
    /// 導覽代理器
    /// </summary>
    protected NavMeshAgent agent;

    /// <summary>
    /// 動畫控制器
    /// </summary>
    protected Animator ani;

    protected bool dead;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();       // 取得元件<代理器>
        ani = GetComponent<Animator>();             // 取得元件<動畫控制器>

        agent.speed = speed;                        // 代理器.速度 = 速度
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Dead()
    {
        if (dead) return;
        dead = true;
        ani.SetTrigger("死亡");         // 動畫控制器.設定觸發("死亡")
        agent.isStopped = true;         // 停止導覽
        agent.velocity = Vector3.zero;
        agent.enabled = false;
        Destroy(gameObject, 1.5f);      // 刪除(遊戲物件，秒數)
    }
}
