using UnityEngine;
using System.Collections;

public class BodyPart
{

    Vector2 location_;
    Player.Direction facing_ = Player.Direction.NORTH;
    // Use this for initialization
    void Start()
    {

    }

    public void Init(Vector2 location)
    {
        location_ = location;
    }

    public Player.Direction Facing
    {
        get { return facing_; }
    }
    public Vector2 Location
    {
        get { return location_; }
    }
    public void ChangeDirection(Player.Direction changeTo)
    {
        if (changeTo == Player.Direction.SOUTH && facing_ == Player.Direction.NORTH) return;
        facing_ = changeTo;
    }

    public void Move(Vector2 newPos)
    {
        location_ = newPos;
    }

    public void Move(float diffX, float diffY)
    {
        if (diffX == 0 && diffY == 0) return;
        location_.x += diffX;
        location_.y += diffY;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
