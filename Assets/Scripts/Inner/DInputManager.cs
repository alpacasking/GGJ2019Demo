using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.PointerEventData;

public class DInputManager : MonoBehaviour, IPointerClickHandler {
    public static DInputManager me;
    private DPassenger selected;
    public DPassenger selectPassenger {
        get { return selected; }
        set {
            Debug.Log(" passager set to [" + value + "]");
            selected = value;
        }
    }

    private void Start() {
        if (!me)
            me = this;
    }

    public GameObject moveOne;
    public void OnPassengerClicked(PointerEventData eventData, DPassenger psg) {
        if (!selected && psg.IsClicked(eventData)) {
            selected = psg;
            this.moveOne = GameObject.Instantiate(selected.gameObject);
            moveOne.transform.parent = this.transform;
            selected.gameObject.SetActive(false);
            moveOne.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        } else {
            DropPassanger();
        }
    }
    public void DropPassanger() {
        if (selected) {
            selected.gameObject.SetActive(true);
            selected = null;
            Destroy(moveOne);
            moveOne = null;
        }
    }

    void Update() {
        //if (selectPassenger) {

        //} else {
        //    // 未选取旅客
        //    if (Input.GetMouseButtonDown(0)) {
        //        // 左击事件
        //        var curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        // 尝试选取当前旅客
        //    }
        //}
        //// 已经选取一个旅客
        //// 移动旅客
        //// 左击事件
        //// 尝试放下旅客，或者归位旅客
        //// 右击事件
        //// 旋转旅客
        //// 移动或者预览旅客放置位置
        if (moveOne) {
            moveOne.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == InputButton.Right) {
            if (me.moveOne) {
                me.moveOne.GetComponent<DPassenger>().Rotate();
            }
        }
    }
}
