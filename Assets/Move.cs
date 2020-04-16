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

        if (Length == 0)
        {
            MovePieceHome();
        }
        else
        {
            switch (PieceScript.GetPositionType())
            {
                case "Field":
                    MovePieceFromField();
                    break;
                case "Home":
                    MovePieceFromHome();
                    break;
            }
        }
    }
    
    public int CheckHome() // Check which home is the next avaliable
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
        for (int i = StartInt; StartInt + 4 > i; i++)
        {
            if (DataManager.Data["Home"][i].GetComponent<Home>().Piece == null)
            {
                return i;
            }
        }
        throw new InvalidOperationException();
    }

    public int CheckFinish()// Check which finish is the next avaliable
    {
        int StartInt = 0;

        switch (PieceScript.PlayerId)
        {
            case 2:
                StartInt = 5;
                break;
            case 3:
                StartInt = 10;
                break;
            case 4:
                StartInt = 15;
                break;
        }

        for (int i = StartInt; StartInt + 5 > i; i++)
        {
            if (DataManager.Data["Finish"][i].GetComponent<Finish>().Piece == null)
            {
                return i;
            }
        }
        throw new InvalidOperationException();
    }

    private void MovePieceFromField() //Move Piece in field
    {
        LandingType = PieceScript.GetPositionType();
        LandingNum = PieceScript.GetPositionNum() + Length;
        MakeMoveValid();
        bool Finished = HandleFinish();
        if (Finished == false)
        {
            PieceInField = DataManager.Data[LandingType][LandingNum].GetComponent<Field>().Piece;

            if (PieceInField != null) //Empty
            {
                PieceInFieldScript = PieceInField.GetComponent<Piece>();
                if (PieceInFieldScript.StandingOnProtected)
                {
                    PieceScript.SendHome();
                    return;
                }
                PieceInFieldScript.SendHome();
            }

            DataManager.Data[PieceScript.GetPositionType()][PieceScript.GetPositionNum()].GetComponent<Field>().Piece = null; //Remove old PieceLocation

            Piece.transform.position = DataManager.Data[LandingType][LandingNum].transform.position;
            PieceScript.SetPosition(LandingType, LandingNum);

            DataManager.Data[LandingType][LandingNum].GetComponent<Field>().Piece = Piece;
        }
    }

    private void MovePieceFromHome() //Move Piece from Home to Field
    {
        LandingType = PieceScript.GetPositionType();
        LandingNum = PieceScript.GetPositionNum();
        DataManager.Data[LandingType][LandingNum].GetComponent<Home>().Piece = null;
        PieceScript.StandingOnProtected = true;
        LandingType = "Field";

        if (Length == 6)
        {
            switch (PieceScript.PlayerId)
            {
                case 1:
                    LandingNum = 9;
                    break;
                case 2:
                    LandingNum = 48;
                    break;
                case 3:
                    LandingNum = 22;
                    break;
                case 4:
                    LandingNum = 35;
                    break;
            }

            Piece.transform.position = DataManager.Data[LandingType][LandingNum].transform.position;
            PieceScript.SetPosition(LandingType, LandingNum);
            DataManager.Data[LandingType][LandingNum].GetComponent<Field>().Piece = Piece;
        }
    }

    private void MovePieceHome() //Move Piece to Home
    {
        if (PieceScript.GetPositionType() != null)
        {
            DataManager.Data[PieceScript.GetPositionType()][PieceScript.GetPositionNum()].GetComponent<Field>().Piece = null;
        }

        LandingType = "Home";
        LandingNum = CheckHome();
        DataManager.Data[LandingType][LandingNum].GetComponent<Home>().Piece = Piece;
        Piece.transform.position = DataManager.Data[LandingType][LandingNum].transform.position;
        PieceScript.SetPosition(LandingType, LandingNum);
    }

    private void MakeMoveValid() // Check if a move is valid
    {
        PieceScript.StandingOnProtected = false;
        if (LandingType == "Field" && LandingNum > 51) //ArrayOutOfBounds
        {
            LandingNum = (PieceScript.GetPositionNum() + Length) - 52;
        }
    }

    private bool HandleFinish()
    {
        int FinishField;
        switch (PieceScript.PlayerId)
        {
            case 1:
                FinishField = 7;
                break;
            case 2:
                FinishField = 46;
                break;
            case 3:
                FinishField = 20;
                break;
            case 4:
                FinishField = 33;
                break;
            default:
                throw new InvalidOperationException();
        }
        if (FinishField > PieceScript.GetPositionNum() && FinishField <= LandingNum)
        {
            LandingType = "Finish";
            LandingNum = CheckFinish();
            DataManager.Data[LandingType][LandingNum].GetComponent<Finish>().Piece = Piece;
            Piece.transform.position = DataManager.Data[LandingType][LandingNum].transform.position;
            PieceScript.SetPosition(LandingType, LandingNum);
            return true;
        }
        return false;
    }
}
