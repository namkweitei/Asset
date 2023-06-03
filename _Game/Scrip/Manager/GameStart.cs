using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    private static GameStart instance;
    public static GameStart Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<GameStart>();
            }
            return instance;
        }
    }
    private int level;
    private int maxLevel;
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject[] players;
    [SerializeField] GameObject[] imglock;
    [SerializeField] GameObject[] textlock;
    private int currentPlayer;
    
    // Start is called before the first frame update
    public void Start()
    {
#if UNITY_EDITOR
        maxLevel = 0;
        PlayerPrefs.SetInt("MaxLevel",maxLevel);
        PlayerPrefs.Save();
#endif
        OpenUI(GameState.Menu);
        maxLevel = PlayerPrefs.GetInt("MaxLevel",0);
        if(maxLevel > SceneManager.sceneCountInBuildSettings-1){
            maxLevel = SceneManager.sceneCountInBuildSettings-1;
            PlayerPrefs.SetInt("MaxLevel",maxLevel);
            PlayerPrefs.Save();
        }
        if(maxLevel == 0){
            maxLevel = 1;
            PlayerPrefs.SetInt("MaxLevel",maxLevel);
            PlayerPrefs.Save();
        }
        for(int i = 0 ; i < maxLevel-1 ; i++){
            imglock[i].SetActive(false);
            textlock[i].SetActive(true);
        }
        currentPlayer = PlayerPrefs.GetInt("Player",0);
    }
    private void Update() {

    }
    public void SetLv(int lv){ 
        maxLevel = PlayerPrefs.GetInt("MaxLevel",0);
        if(lv<=maxLevel){
            level = lv ;
            PlayerPrefs.SetInt("Level",level);
            PlayerPrefs.Save();
            SceneManager.LoadScene(level);
        }
        
    }
    public void SelectPlayers(bool isLeft){
        foreach (var item in players)
        {
            item.SetActive(false);
        }
        if(isLeft){
            currentPlayer -=1;
            if(currentPlayer >= 0 && currentPlayer < players.Length){
                players[currentPlayer].SetActive(true);
            }else if(currentPlayer == 0){
                currentPlayer = players.Length-1;
                players[currentPlayer].SetActive(true);
            }
        }else{
            currentPlayer +=1;
            if(currentPlayer >= 0 && currentPlayer < players.Length){
                players[currentPlayer].SetActive(true);
            }else if(currentPlayer == players.Length){
                currentPlayer = 0;
                players[currentPlayer].SetActive(true);
            }
        }
    }
    public void SelectButton(){
        PlayerPrefs.SetInt("Player",currentPlayer);
        PlayerPrefs.Save();
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

    public void PlayButton(){
        OpenUI(GameState.Level);
    }
    public void ExitButton(){
        Application.Quit();
    }
    public void BackButton(){
        OpenUI(GameState.Menu);
    }
    public void SelectPlayersButton(){
        foreach (var item in players)
        {
            item.SetActive(false);
        }
        players[currentPlayer].SetActive(true);
        OpenUI(GameState.SelectPlayers);
    }
    public void UnLock(){
        if(level == maxLevel){
        maxLevel +=1;
        PlayerPrefs.SetInt("MaxLevel",maxLevel);
        PlayerPrefs.Save();
        }
    }
}
