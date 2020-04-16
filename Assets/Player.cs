using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public int PId;
    public Color32 PColor;
    public List<GameObject> Pieces = new List<GameObject>();

    public void Create(int PlayerId) // Set Player Values
    {
        int IndexToStartAt = 0;
        switch (PlayerId)
        {
            case 1: //Green
                PId = PlayerId;
                PColor = new Color32(162, 255, 0, 255);
                IndexToStartAt = 0;
                break;
            case 2: //Yellow
                PId = PlayerId;
                PColor = new Color32(254, 255, 0, 255);
                IndexToStartAt = 4;
                break;
            case 3: //Red
                PId = PlayerId;
                PColor = new Color32(255, 25, 0, 255);
                IndexToStartAt = 8;
                break;
            case 4: //Blue
                PId = PlayerId;
                PColor = new Color32(0, 149, 255, 255); ;
                IndexToStartAt = 12;
                break;
        }

        for (int I = IndexToStartAt;I < (IndexToStartAt + 4) ;I++)
        {
            CreatePiece(I);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePiece(int Number) // Create Piece GameObject
    {
        GameObject Piece = new GameObject("Piece" + Number, typeof(SpriteRenderer));
        Piece.AddComponent<Piece>().PlayerId = PId;
        Piece.GetComponent<SpriteRenderer>().sprite = Resources.Load("piece", typeof(Sprite)) as Sprite;
        Piece.GetComponent<SpriteRenderer>().color = PColor;
        Piece.GetComponent<Transform>().localScale = new Vector3(0.05351029f, 0.05351029f, 0.05351029f);
        Piece.GetComponent<Renderer>().sortingOrder = 10;
        Piece.AddComponent<BoxCollider2D>();
        Pieces.Add(Piece);
    }
}
