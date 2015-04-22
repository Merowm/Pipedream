using UnityEngine;
using System.Collections.Generic;

public class MoveButtons : MonoBehaviour
{
    public List<GameObject> buttons;
    public List<Vector3> newPositions;
    public List<Vector3> originalPositions;

    private bool relocated = false;

    void Awake ()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            originalPositions.Add(buttons[i].transform.localPosition);
        }
    }

	public void Relocate ()
    {
        if (!relocated)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].transform.localPosition = newPositions[i];
            }
            relocated = true;
        }
        else
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].transform.localPosition = originalPositions[i];
            }
            relocated = false;
        }
        transform.Rotate(new Vector3(0,0,180));
	}
}
