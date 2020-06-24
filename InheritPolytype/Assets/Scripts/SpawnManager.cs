using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("生成的物件：0 警察、1 殭屍、2 屁孩、3 男孩、 4 女孩")]
    public GameObject[] people;
    [Header("生成的最大數量")]
    public int[] peopleCount = { 4, 20, 7, 7, 7 };

    public int[] peopleTotal = new int[5];
    public int[] peopleLeft = new int[5];

    public Text textTip;

    public Transform final;

    private void Start()
    {
        Spawn();

        UpdateText();
    }

    public void UpdateText(int index = 0, int lose = 0)
    {
        peopleLeft[index] -= lose;

        textTip.text =
            $"警察：{peopleTotal[0]} | 剩餘：{peopleLeft[0]}\n" +
            $"殭屍：{peopleTotal[1]} | 剩餘：{peopleLeft[1]}\n" +
            $"屁孩：{peopleTotal[2]} | 剩餘：{peopleLeft[2]}\n" +
            $"男孩：{peopleTotal[3]} | 剩餘：{peopleLeft[3]}\n" +
            $"女孩：{peopleTotal[4]} | 剩餘：{peopleLeft[4]}";

        if (!final.gameObject.activeInHierarchy && (peopleLeft[0] == 0 || peopleLeft[1] == 0)) StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);

        final.gameObject.SetActive(true);
        final.Find("標題").GetComponent<Text>().text = peopleLeft[0] == 0 ? "殭屍獲勝啦!!!" : "人類獲勝啦!!!";

        for (int i = 0; i < final.childCount; i++) final.GetChild(i).gameObject.SetActive(false);

        for (int i = 0; i < final.childCount; i++)
        {
            final.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Spawn()
    {
        for (int i = 0; i < people.Length; i++)
        {
            int r = Random.Range(1, peopleCount[i]);
            peopleTotal[i] = r;
            peopleLeft[i] = r;

            for (int j = 0; j < r; j++)
            {
                Instantiate(people[i], RandomPointInNav(12), Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// 隨機生成在網格內
    /// </summary>
    /// <param name="radius">半徑</param>
    /// <returns>在網格內的座標</returns>
    private Vector3 RandomPointInNav(float radius)
    {
        Vector3 pointRan = Random.insideUnitSphere * radius + transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(pointRan, out hit, radius, 1);
        return hit.position;
    }
}
