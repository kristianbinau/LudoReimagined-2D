using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Players = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        DataManager.Init();
        CreatePlayer(1);
        CreatePlayer(2);
        CreatePlayer(3);
        CreatePlayer(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameLoop()
    {

    }

    private void CreatePlayer(int PlayerNum)
    {
        GameObject Player = new GameObject("player" + PlayerNum, typeof(SpriteRenderer));
        Player.AddComponent<HumanPlayer>().Create(PlayerNum);
        Player.GetComponent<SpriteRenderer>().sprite = Resources.Load("player", typeof(Sprite)) as Sprite;
        Player.GetComponent<SpriteRenderer>().color = Player.GetComponent<HumanPlayer>().PColor;
        Player.GetComponent<Transform>().position = GameObject.Find("FieldPlayer" + PlayerNum).GetComponent<Transform>().position;
        Player.GetComponent<Transform>().localScale = new Vector3(0.1855f, 0.1855f, 0.1855f);
        Player.GetComponent<Renderer>().sortingOrder = 10;
        Players.Add(Player);
    }
}
