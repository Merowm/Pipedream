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
    void OnMouseUpAsButton()
    {
        st.ResetScore();
        Application.LoadLevel(st.GetLastLevelNameAsString());
    }
}
