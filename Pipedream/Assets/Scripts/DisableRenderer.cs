using UnityEngine;
using System.Collections;

public class DisableRenderer : MonoBehaviour
{
	void Awake ()
    {
        transform.FindChild("HyperPart_x10").gameObject.SetActive(false);
	}
}
