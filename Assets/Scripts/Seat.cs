using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : Block {
    public enum SeatState {
        Empty,
        Passenge,
        Barrier,
    }
    public SeatState State;

    void OnValidate() {
        if (State == SeatState.Empty) {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 255, 128);
        } else if (State == SeatState.Barrier) {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 245, 0);
        }
    }
}
