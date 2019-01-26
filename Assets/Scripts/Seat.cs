using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat:Block
{
    public enum SeatState
    {
        Empty,
        Passenge,
    }
    public SeatState State;
}
