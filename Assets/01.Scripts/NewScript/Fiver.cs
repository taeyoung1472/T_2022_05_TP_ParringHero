using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fiver : MonoBehaviour
{
    [SerializeField] private List<GameObject> alpabet = new List<GameObject>(); // parrying¾ËÆÄºª
    [SerializeField] private GameObject _fiverCol;
    [SerializeField] public GameObject alpabetParent;
    private MoveBackground[] _moveBackground;
    private int count = 0;
    private Player _player;
    private List<GameObject> displayedAlpabet = new List<GameObject>();

    private void Awake()
    {
        _moveBackground = FindObjectsOfType<MoveBackground>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnAlpa();
        }
    }
    public void SpawnAlpa()
    {
        GameObject obj = Instantiate(alpabet[count], transform.position, Quaternion.identity);
        obj.SetActive(true);
        obj.transform.SetParent(alpabetParent.transform);
        displayedAlpabet.Add(obj);
        count++;
        if (count >= 8)
        {
            StartCoroutine(FiverTime());
        }
    }
    IEnumerator FiverTime()
    {
        StartFevering();
        yield return new WaitForSeconds(5f);
        EndFiver();
    }
    public void EndFiver()
    {
        foreach (MoveBackground mb in _moveBackground)
        {
            mb.speed = mb.orignspeed;
        }
        EnemyBase.staticSpeed = 1f;
        _fiverCol.SetActive(false);
        _player.IsInvincibility = false;
        count = 0;


        for (int i = 0; i < 8; i++)
        {
            Destroy(displayedAlpabet[i]);
        }

        displayedAlpabet.Clear();
    }
    public void StartFevering()
    {
        foreach (MoveBackground mb in _moveBackground)
        {
            mb.speed *= 2.5f;
        }
        EnemyBase.staticSpeed = 2.5f;
        _player.IsInvincibility = true;
        _fiverCol.SetActive(true);
    }
}