using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoSingleton<GameManager>
{
    #region ��������
    public PoolManager PoolManager { get { return poolManager; } }
    public CamManager CamManager { get { return camManager; } }
    public Transform Player { get { return player; } }
    #endregion
    [SerializeField] private UpGradeSystem upGradeSystem;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private CamManager camManager;
    [SerializeField] private MainCoin mainCoin;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private Transform player;

    [SerializeField] private User user;
    [SerializeField] private CoinUI coinUI;
    [SerializeField] private bool isMenu = false;
    public bool IsMenu { get { return isMenu; } }
    bool isTuto = false;
    public CoinUI CoinUI { get { return coinUI; } }
    public User currentUser { get { return user; } }
    public bool IsTuto { get { return isTuto; } }

    public bool isAttackTuto = false;
    public Transform PlayerSpawnPos;
    public GameObject Sasin;
    public GameObject Gisa;
    //public Transform EnemyPos;

    private void Awake()
    {
        Time.timeScale = 1;
        LoadUser();
        SaveUser();
    }
    public void Start()
    {
        if (isMenu) return;
        Search();
    }
    public void Update()
    {
        if (isMenu) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseManager.On();
        }
    }
    public void Tuto()
    {
        if (!isAttackTuto)
        {
            DialogManager.ShowDialog(0, TutoSpawn);
            isTuto = true;
        }
    }
    public bool Purchase(int price)
    {
        if (user.coin > price)
        {
            user.coin -= price;
            SaveUser();
            if (mainCoin)
            {
                mainCoin.UpdateCoinUI();
            }
            return true;
        }
        else
        {
            SaveUser();
            return false;
        }
    }
    public void Search()
    {
        try
        {
            poolManager = FindObjectOfType<PoolManager>();
            camManager = FindObjectOfType<CamManager>();
            upGradeSystem = FindObjectOfType<UpGradeSystem>();
            player = FindObjectOfType<Player>().transform;
            //player.GetComponent<Player>().SetAnim(user.playerIndex);
            player.GetComponent<Player>().SetPet(user.petIndex);
            upGradeSystem.SetBuff(player.GetComponent<Player>(),FindObjectOfType<CoinUI>());
            print($"���������� \"FindObjectOfType\" �۵��Ϸ� Succes : {Time.time}");
        }
        catch
        {
            try
            {
                mainCoin = FindObjectOfType<MainCoin>();
                print($"���������� \"FindObjectOfType\" �۵��Ϸ� Succes : {Time.time} [MenuScene]");
            }
            catch
            {
                print("����!");
            }
            print($"Main���̶� \"FindObjectOfType\" ���� �Ұ� ERROR : {Time.time}");
        }
        try
        {
            pauseManager = FindObjectOfType<PauseManager>();
        }
        catch
        {
            print($"Tuto���̶� \"FindObjectOfType\" ���� �Ұ� ERROR : {Time.time}");
        }
    }
    public void AddCoin()
    {
        coinUI.AddCoin(10000);
        user.coin += 10000;
        SaveUser();
    }
    [ContextMenu("�����ϱ�")]
    public void SaveUser()
    {
        print("����");
        string jsonData = JsonUtility.ToJson(user, true);
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("�ҷ�����")]
    public void LoadUser()
    {
        print("�ҷ�����");
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        string jsonData = File.ReadAllText(path);
        user = JsonUtility.FromJson<User>(jsonData);
        coinUI.AddCoin(0);
    }
    private void OnDestroy()
    {
        SaveUser();
    }
    private void OnApplicationQuit()
    {
        SaveUser();
    }
    public void TutoSpawn()
    {
        Sasin.SetActive(true);
        Gisa.SetActive(true);
        isAttackTuto = true;
    }
    public void SpawnEnd()
    {
        SceneManager.LoadScene(0);
    }
    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
