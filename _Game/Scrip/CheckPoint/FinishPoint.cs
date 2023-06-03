using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            int level = PlayerPrefs.GetInt("Level",0);
            int maxLevel = PlayerPrefs.GetInt("MaxLevel",0);
            
            if (level == maxLevel){
                PlayerPrefs.SetInt("MaxLevel",maxLevel+1);
                PlayerPrefs.Save();
            }
            GamePlay.Instance.OpenUI(GameState.End);
        }
    }
    
}
