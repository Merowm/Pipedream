using UnityEngine;
using System.Collections;

public class StartNext : MonoBehaviour {

    // Start game level that comes after last played level
	
    Statistics st;
   
    void Start()
    {
        st = FindObjectOfType<Statistics>();
    }

    void OnMouseUpAsButton()
    {
        st.ResetScore();
        int level = st.GetCurrentLevel() + 1;
        Application.LoadLevel(st.GetLevelNameAsString(level));
    }
}
