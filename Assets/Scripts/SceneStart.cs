using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart : MonoBehaviour
{
    public AudioSource click;

    // Start is called before the first frame update
    void Start()
    {
        click.Play();
    }
}
