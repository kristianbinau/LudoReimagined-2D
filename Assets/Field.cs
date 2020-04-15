using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour, IField
{
    public string Type { get; } = "Field";
    public GameObject Piece { get; set; } = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
