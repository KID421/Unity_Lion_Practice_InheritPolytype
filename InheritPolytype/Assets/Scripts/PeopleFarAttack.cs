using UnityEngine;

public class PeopleFarAttack : PeopleTrack
{
    [Header("停止距離"), Range(1, 10)]
    public float stop = 3f;
    [Header("子彈")]
    public GameObject bullet;
    [Header("冷卻"), Range(0.1f, 3f)]
    public float cd = 1.5f;

    private float timer;

    protected override void Start()
    {
        agent.stoppingDistance = stop;                  // 代理器.停止距離 = 停止距離
        target = GameObject.Find("殭屍").transform;      // 目標 = 殭屍
    }

    protected override void Track()
    {
        agent.SetDestination(target.position);
        transform.LookAt(target);                       // 變形.看著(目標)

        if (agent.remainingDistance <= stop) Attack();  // 如果 代理器.距離 < 停止距離 就 攻擊
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        timer += Time.deltaTime;        // 計時器 累加 時間

        // 如果 計時器 >= 冷卻時間
        if (timer >= cd)
        {
            timer = 0;                  // 計時器歸零
            ani.SetTrigger("攻擊");                                                                                                     // 攻擊動畫
            GameObject temp = Instantiate(bullet, transform.position + transform.forward + transform.up, transform.rotation);           // 生成子彈
            Rigidbody rig = temp.AddComponent<Rigidbody>();                                                                             // 添加元件
            rig.AddForce(transform.forward * 1500);                                                                                     // 子彈添加推力
        }
    }
}
