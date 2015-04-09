using UnityEngine;
using System.Collections;

public class StartNext : MonoBehaviour {

    // Start game level that comes after last played level
	
    Statistics st;
   
    void Start()
    {
        st = FindObjectOfType<Statistics>();
    }

    public void ChangeToNextScene()
    {
        st.ResetScore();
        int level = st.GetCurrentLevel() + 1;
        string levelname = st.GetLevelNameAsString(level);
        GameObject.FindWithTag("statistics").GetComponent<LoadScreen>().showLoader(levelname, false);
        Application.LoadLevel(levelname);
    }
}
