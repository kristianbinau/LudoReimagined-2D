using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int PlayerId;
    public string PositionType ;
    private int PositionNum;

    // Start is called before the first frame update
    void Start()
    {
        SendHome();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        new Move(gameObject, 6);
    }

    public string GetPositionType()
    {
        return PositionType;
    }

    public int GetPositionNum()
    {
        return PositionNum;
    }

    public void SetPosition(string Type, int Num)
    {
        PositionType = Type;
        PositionNum = Num;
    }

    // Sends Piece to Home.
    public void SendHome() // Needs to be shortened.
    {
        new Move(gameObject);
    }
}
