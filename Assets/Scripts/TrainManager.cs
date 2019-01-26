using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public class PassengeInTrain
    {
        public Block mBlock;
        public Seat mSeat;
    }
    public Vector2 StartPos;
    public Vector2 SeatSize;
    public int SeatRow, SeatColumn;
    public Seat[,] Seats;
    public GameObject SeatSprite;
    public Dictionary<Passenge, PassengeInTrain> passenges;

    // Start is called before the first frame update
    void Start()
    {
        passenges = new Dictionary<Passenge, PassengeInTrain>();
        Seats = new Seat[SeatColumn, SeatRow];
        for (int i=0;i< SeatColumn; i++)
        {
            for(int j = 0; j < SeatRow; j++)
            {
                GameObject seatobj = new GameObject("seat_" + i + "_" + j);
                seatobj.tag = "seat";
                seatobj.transform.SetParent(transform);
                seatobj.transform.localPosition = new Vector2(StartPos.x + SeatSize.x * i, StartPos.y + SeatSize.y * j);
                BoxCollider2D seatCollider = seatobj.AddComponent<BoxCollider2D>();
                seatCollider.size = SeatSize;
                Seat seat = seatobj.AddComponent<Seat>();
                seat.State = Seat.SeatState.Empty;
                seat.x = i;
                seat.y = j;
                Seats[i, j] = seat;
                GameObject seatSprite = Instantiate(SeatSprite);
                seatSprite.transform.SetParent(seatobj.transform);
                seatSprite.transform.localPosition = Vector2.zero;
            }
        }
    }

    public bool IsSeatEmpty(Passenge passenge,Block block,Seat seat,out Vector2 outputPos)
    {
        //List<Vector2> temp = new List<Vector2>(passenge.Blocks.Count);
        Vector2 sum = Vector2.zero;
        for(int i=0;i< passenge.Blocks.Count; i++)
        {
            int offsetX = passenge.Blocks[i].x - block.x;
            int offsetY = passenge.Blocks[i].y - block.y;
            int trainX = seat.x + offsetX;
            int trainY = seat.y + offsetY;
            if(trainX <0|| trainX>= SeatColumn|| trainY < 0 || trainY >= SeatRow)
            {
                outputPos = Vector2.zero;
                return false;
            }
            if(Seats[trainX, trainY].State != Seat.SeatState.Empty)
            {
                outputPos = Vector2.zero;
                return false;
            }
            sum += (Vector2)Seats[trainX, trainY].transform.position;
        }
        outputPos = new Vector2(sum.x / passenge.Blocks.Count, sum.y / passenge.Blocks.Count);
        return true;
    }

    public void AddPassenge(Passenge passenge,Block block, Seat seat)
    {
        for (int i = 0; i < passenge.Blocks.Count; i++)
        {
            int offsetX = passenge.Blocks[i].x - block.x;
            int offsetY = passenge.Blocks[i].y - block.y;
            int trainX = seat.x + offsetX;
            int trainY = seat.y + offsetY;
            Seats[trainX, trainY].State = Seat.SeatState.Passenge;
        }
        passenge.State = Passenge.PassengeState.Sitting;
        passenges.Add(passenge, new PassengeInTrain { mBlock = block, mSeat = seat });
    }

    public void RemovePassenge(Passenge passenge)
    {
        PassengeInTrain passengeInTrain = passenges[passenge];
        for (int i = 0; i < passenge.Blocks.Count; i++)
        {
            int offsetX = passenge.Blocks[i].x - passengeInTrain.mBlock.x;
            int offsetY = passenge.Blocks[i].y - passengeInTrain.mBlock.y;
            int trainX = passengeInTrain.mSeat.x + offsetX;
            int trainY = passengeInTrain.mSeat.y + offsetY;
            Seats[trainX, trainY].State = Seat.SeatState.Empty;
        }
        passenge.State = Passenge.PassengeState.Standing;
        passenges.Remove(passenge);
    }

    public bool TryMovePassenge(Passenge passenge, Block block, Seat seat, out Vector2 outputPos)
    {
        RemovePassenge(passenge);
        if(IsSeatEmpty(passenge, block, seat, out outputPos))
        {
            AddPassenge(passenge, block, seat);
            return true;
        }
        return false;
    }
}
