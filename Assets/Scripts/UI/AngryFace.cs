using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class AngryFace : MonoBehaviour
{
    public Sprite Sad;
    public Sprite Happy;
    public Sprite Indifferent;
    public Sprite Angry;
    public Sprite Irretated;

    public Image face;

    private List<Sprite> faces;
    // Start is called before the first frame update
    void Start()
    {
        faces = new List<Sprite> {Sad, Irretated, Angry, Indifferent, Happy};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Set new face.
    /// </summary>
    /// <param name="i">Which face in list</param>
    void SetNewFace(int i)
    {
        if (i >= faces.Count)
        {
            face.sprite = faces.Last();
        } 
        else if (i < 0)
        {
            face.sprite = faces[0];
        }
        else
        {
            face.sprite = faces[i];
        }
    }
}
