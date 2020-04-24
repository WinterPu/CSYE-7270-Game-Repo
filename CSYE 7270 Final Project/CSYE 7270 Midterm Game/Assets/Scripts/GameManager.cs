using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static float GroundYVal = -2.22f;
    public static GameManager Instance { get; private set; }
    public delegate void MethodDelegate();

    public Player player;
    public TextMeshProUGUI txtResult;

    private bool hasResult = false;
    private bool isWin =false;


    public Image imgGrenade;
    public TextMeshProUGUI txtGrenade;

    void Awake()
    {
        Time.timeScale = 1f;
        Instance = this;
        txtResult.text = "";
        isWin = false;
        hasResult = false;
    }
    // Start is called before the first frame update

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        if (!hasResult)
        {
            ReportFinishingTheGame();

            imgGrenade.gameObject.SetActive(false);
            txtGrenade.gameObject.SetActive(false);
            hasResult = true;
            txtResult.text = "You Die!\n Mission Failed";
            Animator playerAnimator = player.gameObject.GetComponent<Animator>();
            playerAnimator.SetBool("Dead", true);
            MethodDelegate method = ReloadScene;
            ExecMethodAfterWaitForSeconds(2, method);
        }
    }

    public bool CheckGameIsWin()
    {
        return isWin;
    }
    public void WinGame()
    {
        if (!hasResult)
        {
            ReportFinishingTheGame();
            hasResult = true;
            isWin = true;
            txtResult.text = "Good Job Agent!\n You Completed The Mission Perfectly!";
            MethodDelegate method = ReloadScene;
            FreezeTimeAndExecMethodAfterWaitForSeconds(2, method);


        }
    }

    private void ReloadScene()
    {
       
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void FreezeTimeAndExecMethodAfterWaitForSeconds(int time, MethodDelegate method =null)
    {
        StartCoroutine(FreezeTimeWaitForSecond(time, method));
    }
    private IEnumerator FreezeTimeWaitForSecond(int time, MethodDelegate method=null)
    {
        Time.timeScale = 0.0f;
        float pauseEndTime = Time.realtimeSinceStartup + time;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        method?.Invoke();
    }


    public void ExecMethodAfterWaitForSeconds(int time, MethodDelegate method)
    {
        StartCoroutine(WaitForSecondsIEnumerator(time, method));
    }
    IEnumerator WaitForSecondsIEnumerator(int time, MethodDelegate method)
    {
        yield return new WaitForSeconds(time);
        method();
    }



    // Unity Analytics
    public void ReportFinishingTheGame()
    {
        Dictionary<string, object> parameters
            = new Dictionary<string, object>();
        parameters.Add("Progression", 2);
        AnalyticsEvent.Custom("Progression", parameters);
    }
}
