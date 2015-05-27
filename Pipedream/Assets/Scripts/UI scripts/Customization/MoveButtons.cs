using UnityEngine;
using System.Collections.Generic;

public class MoveButtons : MonoBehaviour
{
    public List<GameObject> buttons;
    public List<Vector3> newPositions;
    public List<Vector3> originalPositions;

    void Awake ()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            originalPositions.Add(buttons[i].transform.localPosition);
        }
    }

	public void Relocate ()
    {
        //If buttons are at their original position move them to their new position and vice versa
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].transform.localPosition == originalPositions[i])
            {
                buttons[i].transform.localPosition = newPositions[i];
            }
            else
            {
                buttons[i].transform.localPosition = originalPositions[i];
            }
        }
	}
}
