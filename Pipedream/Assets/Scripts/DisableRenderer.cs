using UnityEngine;
using System.Collections;

public class DisableRenderer : MonoBehaviour
{
	void Awake ()
    {
        transform.FindChild("HyperParts").gameObject.SetActive(false);
	}
}
