using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState {Play = 0, End = 1, Pause = 2, Menu = 0, Level = 1, SelectPlayers = 2}
public class GamePlay : MonoBehaviour
{
    private static GamePlay instance;
    public static GamePlay Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<GamePlay>();
            }
            return instance;
        }
    }
    [SerializeField] GameObject[] panels;

    [SerializeField] Text fruitText;
    [SerializeField] Text fruitEndText;
    [SerializeField] Text rankText;
    [SerializeField] Image hp;
    private float hpCurrent;
    int level;
    // Start is called before the first frame update
    void Start()
    {
        OpenUI(GameState.Play);
        level = PlayerPrefs.GetInt("Level",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CloseAll(){
        foreach (var item in panels)
        {
            item.SetActive(false);
        }
    }
    public void OpenUI(GameState state){
        CloseAll();
        panels[(int)state].SetActive(true);
    }
    public void NextButton(){
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Level",level+1);
        PlayerPrefs.Save();
        if(SceneManager.sceneCountInBuildSettings-1 > level){
            SceneManager.LoadScene(level+1);
        }
    }
    public void RetryButton(){
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }
    public void MenuButton(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void PauseButton(){
        Time.timeScale = 0;
        OpenUI(GameState.Pause);
    }
    public void ResumeButton(){
        Time.timeScale = 1;
        OpenUI(GameState.Play);
    }
    public void ExitButton(){
        Time.timeScale = 1;
        Application.Quit();
    }
    public void SetFruit(int fruit){
        fruitText.text = fruit.ToString();
        fruitEndText.text = fruit.ToString();
    }
    public void SetRank(int rank){
        PlayerPrefs.SetInt("Rank",rank);
        PlayerPrefs.Save();
        if(rank >= 60){
            rankText.text = "S";
        }else if(rank >= 50){
            rankText.text = "A";
        }else if(rank >= 40){
            rankText.text = "B";
        }else if(rank <= 30){
            rankText.text = "C";
        }
        
    }
    public void SetHp(float hp){
        hpCurrent = hp;
        this.hp.fillAmount = hpCurrent/3.0f;
    }
}
