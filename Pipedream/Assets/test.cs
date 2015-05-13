using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    public new Color32 color = new Color32();

    public Material mat;
    public ParticleSystem partSys;

	void Awake ()
    {
       // mat = transform.GetComponent<MeshRenderer>().material;
       // partSys = transform.GetChild(0).transform.GetComponent<ParticleSystem>();
	}

	void Update ()
    {
        //Color of the particles, which is a modification of the color of the material
        Color32 modifiedColor = new Color32();

        int R = color.r;
        int G = color.g;
        int B = color.b;

        //Red is dominant
        if (R > G && R > B)
        {
            modifiedColor = new Color32((byte)R,
                                        (byte)(R * 0.4f),
                                        (byte)B,
                                        (byte)255);
        }
        //Green is dominant
        else if (G > R && G > B)
        {
            modifiedColor = new Color32((byte)R,
                                        (byte)G,
                                        (byte)(G * 0.4f),
                                        (byte)255);
        }
        //Blue is dominant
        else if (B > R && B > G)
        {
            modifiedColor = new Color32((byte)(B * 0.4f),
                                        (byte)G,
                                        (byte)B,
                                        (byte)255);
        }
        //All colors are the same
        else
        {
            modifiedColor = new Color32((byte)R,
                                        (byte)G,
                                        (byte)B,
                                        (byte)255);
        }



        mat.color = new Color32((byte)color.r,(byte)color.g,(byte)color.b,(byte)mat.color.a);
        //partSys.startColor = new Color32((byte)color.r,(byte)color.g,(byte)color.b,(byte)255);
        partSys.startColor = modifiedColor;
	}
}
