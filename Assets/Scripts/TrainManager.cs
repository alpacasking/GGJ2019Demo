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
        var seats = gameObject.GetComponentsInChildren<Seat>();
        for (int i = 0; i < seats.Length; i++)
        {
            Seats[seats[i].x, seats[i].y] = seats[i];
            var oColor = Seats[seats[i].x, seats[i].y].GetComponentInChildren<SpriteRenderer>().color;
            oColor.a = 0.2f;
            Seats[seats[i].x, seats[i].y].GetComponentInChildren<SpriteRenderer>().color = oColor;
        }
        //for (int i=0;i< SeatColumn; i++)
        //{
        //    for(int j = 0; j < SeatRow; j++)
        //    {
        //        GameObject seatobj = new GameObject("seat_" + i + "_" + j);
        //        seatobj.tag = "seat";
        //        seatobj.transform.SetParent(transform);
        //        seatobj.transform.localPosition = new Vector2(StartPos.x + SeatSize.x * i, StartPos.y + SeatSize.y * j);
        //        BoxCollider2D seatCollider = seatobj.AddComponent<BoxCollider2D>();
        //        seatobj.transform.localScale = new Vector3(SeatSize.x, SeatSize.y,1);
        //        Seat seat = seatobj.AddComponent<Seat>();
        //        seat.State = Seat.SeatState.Empty;
        //        seat.x = i;
        //        seat.y = j;
        //        Seats[i, j] = seat;
        //        GameObject seatSprite = Instantiate(SeatSprite);
        //        seatSprite.transform.SetParent(seatobj.transform);
        //        seatSprite.transform.localPosition = Vector2.zero;
        //    }
        //}
    }

    public bool IsSeatEmpty(Passenge passenge,Block block,Seat seat,out Vector2 outputPos)
    {
        //List<Vector2> temp = new List<Vector2>(passenge.Blocks.Count);
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
            if (Seats[trainX, trainY].State != Seat.SeatState.Empty)
            {
                outputPos = Vector2.zero;
                return false;
            }
        }
        outputPos = new Vector2(seat.transform.position.x+passenge.transform.position.x-block.transform.position.x,
        seat.transform.position.y+passenge.transform.position.y - block.transform.position.y);
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
            Seats[trainX, trainY].GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
        passenge.State = Passenge.PassengeState.Sitting;
        passenges.Add(passenge, new PassengeInTrain { mBlock = new Block {x = block.x,y = block.y }, mSeat = seat });
    }

    public void RemovePassenge(Passenge passenge ,bool useCache = false)
    {
        PassengeInTrain passengeInTrain = passenges[passenge];
        var tempBlocks = useCache ? passenge.BlocksCache : passenge.Blocks;
        Debug.Log("tempBlocks:"+tempBlocks.Count);
        for (int i = 0; i < tempBlocks.Count; i++)
        {
            int offsetX = tempBlocks[i].x - passengeInTrain.mBlock.x;
            int offsetY = tempBlocks[i].y - passengeInTrain.mBlock.y;
            int trainX = passengeInTrain.mSeat.x + offsetX;
            int trainY = passengeInTrain.mSeat.y + offsetY;
            Seats[trainX, trainY].State = Seat.SeatState.Empty;
            Seats[trainX, trainY].GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        }
        passenge.State = Passenge.PassengeState.Standing;
        passenges.Remove(passenge);
    }

    public bool TryMovePassenge(Passenge passenge, Block block, Seat seat, out Vector2 outputPos)
    {
        PassengeInTrain passengeInTrain = passenges[passenge];
        RemovePassenge(passenge,true);
        if(IsSeatEmpty(passenge, block, seat, out outputPos))
        {
            AddPassenge(passenge, block, seat);
            return true;
        }
        else
        {
            AddPassenge(passenge, passengeInTrain.mBlock, passengeInTrain.mSeat);
        }
        return false;
    }
}
