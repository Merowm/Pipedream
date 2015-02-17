﻿using UnityEngine;
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
        Application.LoadLevel(st.GetLevelNameAsString(level));
    }
}