using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    private int Length;
    private GameObject Piece = null;
    private Piece PieceScript = null;
    private GameObject PieceInField = null;
    private Piece PieceInFieldScript = null;

    protected string LandingType;
    protected int LandingNum;

    public Move(GameObject Pce, int Lngth = 0)
    {
        Piece = Pce;
        PieceScript = Pce.GetComponent<Piece>();
        Length = Lngth;

        //Maybe Switch
        if (PieceScript.PositionType == null)
        {
            MovePieceHome();
        }
        else if (PieceScript.PositionType == "Field")
        {
            MovePieceFromField();
        }
        else if (PieceScript.PositionType == "Home")
        {
            MovePieceFromHome();
        }
    }

    private void MovePieceFromField() //Piece is in field
    {
        MakeMoveValid();
        PieceInField = DataManager.Data[LandingType][LandingNum].GetComponent<Field>().Piece;
        PieceInFieldScript = PieceInField != null ? PieceInField.GetComponent<Piece>() : null;



        if (PieceInField == null) //Empty
        {
            DataManager.Data[PieceScript.GetPositionType()][PieceScript.GetPositionNum()].GetComponent<Field>().Piece = null; //Remove old PieceLocation
            
            PieceInField = Piece;
            Piece.transform.position = DataManager.Data[LandingType][LandingNum].transform.position;
            Piece.GetComponent<Piece>().SetPosition(LandingType, LandingNum);
        }
        else if (PieceInFieldScript.PlayerId == PieceScript.PlayerId) //Own Piece is there
        {
            Piece.GetComponent<Piece>().SendHome();
        }
        else //Opponent is there
        {
            DataManager.Data[PieceScript.GetPositionType()][PieceScript.GetPositionNum()].GetComponent<Field>().Piece = null; //Remove old PieceLocation

            PieceInFieldScript.SendHome();
            PieceInField = Piece;
            Piece.transform.position = DataManager.Data[LandingType][LandingNum].transform.position;
            Piece.GetComponent<Piece>().SetPosition(LandingType, LandingNum);
        }

        DataManager.Data[LandingType][LandingNum].GetComponent<Field>().Piece = PieceInField;
    }

    private void MovePieceFromHome() //Move Piece from home to field
    {
        LandingType = "Field";

        if (Length == 6)
        {
            switch (PieceScript.PlayerId)
            {
                case 1:
                    LandingNum = 26;
                    break;
                case 2:
                    LandingNum = 13;
                    break;
                case 3:
                    LandingNum = 39;
                    break;
                case 4:
                    LandingNum = 0;
                    break;
            }
            Piece.transform.position = DataManager.Data[LandingType][LandingNum].transform.position;
            Piece.GetComponent<Piece>().SetPosition(LandingType, LandingNum);
        }
    }

    private void MovePieceHome() //Move Piece home
    {
        if (PieceScript.PositionType != null)
        {
            DataManager.Data[PieceScript.GetPositionType()][PieceScript.GetPositionNum()].GetComponent<Home>().Piece = null;
        }

        LandingType = "Home";
        LandingNum = CheckHome();
        DataManager.Data[LandingType][LandingNum].GetComponent<Home>().Piece = Piece;
        Piece.transform.position = DataManager.Data[LandingType][LandingNum].transform.position;
        PieceScript.SetPosition(LandingType, LandingNum);
    }

    /*
     * Check if a move is valid
     * Should work by sending a integer as a "code".
     * Code 0 - Valid
     * Code 1 - ArrayOutOfBounds
     * 
     */
    private void MakeMoveValid()
    {
        LandingType = PieceScript.GetPositionType();
        LandingNum = PieceScript.GetPositionNum() + Length;

        if (LandingType == "Field" && LandingNum > 52) //ArrayOutOfBounds
        {
            LandingNum = (PieceScript.GetPositionNum() + Length) - 52;
        }
    }

    public int CheckHome()
    {
        int StartInt = 0;

        switch (PieceScript.PlayerId)
        {
            case 2:
                StartInt = 4;
                break;
            case 3:
                StartInt = 8;
                break;
            case 4:
                StartInt = 12;
                break;
        }
        for (int i = StartInt;StartInt+4 > i ; i++)
        {
            if (DataManager.Data["Home"][i].GetComponent<Home>().Piece == null)
            {
                return i;
            }
        }
        throw new InvalidOperationException();
    }
}
