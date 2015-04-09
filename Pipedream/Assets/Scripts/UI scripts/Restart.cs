using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour
{

    Statistics st;
    // Use this for initialization
    void Start()
    {
        st = FindObjectOfType<Statistics>();
    }
    public void GoToLast()
    {
        st.ResetScore();
        int level = st.GetCurrentLevel();
        string levelname = st.GetLevelNameAsString(level);
        GameObject.FindWithTag("statistics").GetComponent<LoadScreen>().showLoader(levelname, false);
        Application.LoadLevel(levelname);
    }

    public void RestartLevel(){
        Time.timeScale = 1.0f;
        Application.LoadLevel(Application.loadedLevel);
    }
}
