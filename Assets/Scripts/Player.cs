using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player
{
    public enum Direction { NORTH, EAST, SOUTH, WEST };
    Direction facing_ = Direction.NORTH;
    List<BodyPart> bodyParts_ = new List<BodyPart>();
    bool changeQueued = false;
    Direction queuedDirection;
    Color myColour = Color.cyan;
    // Use this for initialization
    void Start()
    {

    }

    public List<BodyPart> SnakeParts
    {
        get { return bodyParts_; }
    }

    public int Length
    {
        get { return bodyParts_.Count; }
    }
    public Color Colour
    {
        get { return myColour; }
    }

    public void Init()
    {
        bodyParts_.Clear();
        changeQueued = false;
        queuedDirection = Direction.NORTH;
        AddBodyPart(new Vector2(13, 13));
        AddBodyPart(new Vector2(13, 12));
        AddBodyPart(new Vector2(13, 11));
        myColour.r = PlayerPrefs.GetFloat("PlayerR",0);
        myColour.g = PlayerPrefs.GetFloat("PlayerG",1);
        myColour.b = PlayerPrefs.GetFloat("PlayerB",1);
    }
    public void CosumeFruit()
    {
        Vector2 headPos = bodyParts_[bodyParts_.Count - 1].Location;
        AddBodyPart(headPos);
    }
    void AddBodyPart(Vector2 location)
    {
        BodyPart newPart = new BodyPart();
        newPart.Init(location);
        bodyParts_.Add(newPart);
    }


    public void ChangeDirection(Direction changeTo)
    {
        if (changeTo == Direction.SOUTH && facing_ == Direction.NORTH) return;
        if (changeTo == Direction.NORTH && facing_ == Direction.SOUTH) return;
        if (changeTo == Direction.EAST && facing_ == Direction.WEST) return;
        if (changeTo == Direction.WEST && facing_ == Direction.EAST) return;
        if (changeQueued) return;
        else
        {
            queuedDirection = changeTo;
            changeQueued = true;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        Vector2 headPos = bodyParts_[0].Location;
        if (changeQueued)
        {
            facing_ = queuedDirection;
            bodyParts_[0].ChangeDirection(queuedDirection);
            changeQueued = false;
        }

        switch (bodyParts_[0].Facing)
        {
            case Direction.NORTH:
                {
                    bodyParts_[0].Move(0, 1f);
                    break;
                }
            case Direction.SOUTH:
                {
                    bodyParts_[0].Move(0, -1f);
                    break;
                }
            case Direction.EAST:
                {
                    bodyParts_[0].Move(1f, 0);
                    break;
                }
            case Direction.WEST:
                {
                    bodyParts_[0].Move(-1f, 0);
                    break;
                }
        }

        bodyParts_[bodyParts_.Count - 1].Move(headPos);
        bodyParts_.Insert(1, bodyParts_[bodyParts_.Count - 1]);
        bodyParts_.RemoveAt(bodyParts_.Count - 1);

    }
}
