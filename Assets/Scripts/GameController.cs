using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public static GameController Instance { get { return _instance; } }

    public ResultView Result;

    public bool IsRunning { get; set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        IsRunning = true;
    }

    public Stack stack;
    public RessourceManager ressource;
    public EnemySpawner Spawner;
    public GameObject Reload;

    public void ToggleReload(bool b)
    {
        if(b != Reload.activeInHierarchy)
        {
            Reload.SetActive(b);
        }
    }

    public void Start()
    {
        ressource=RessourceManager.Instance;
        stack = Stack.Instance;
        ressource.Initiate();
        stack.Initiate();
        Spawner.Intitialize();
    }

    public void Update()
    {
        ressource.RessourceUpdate();
        stack.StackUpdate();
    }

    public void GameOver()
    {        AudioController.Instance.PlaySound(AudioController.Sounds.Dead);
        Result.ShowResult(false);
        IsRunning = false;
    }

    public void Win()
    {        AudioController.Instance.PlaySound(AudioController.Sounds.GameOver);
        Result.ShowResult(true);
        IsRunning = false;
    }
}
