using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.PointerEventData;

public class DPassenger : DMess, IPointerClickHandler {

    public void Rotate() {
        Debug.Log(" [" + this.gameObject.name + "]" + "rotate");
        List<GameObject> childs = new List<GameObject>();
        for (int i = 0; i < this.transform.childCount; ++i) {
            var child = transform.GetChild(i);
            if (child.GetComponent<DBlock>())
                childs.Add(child.gameObject);
        }
        //  原来的顺序 0,1,2,3; 4,5,6,7; 现在的顺序 3,7,11,15; 2,6,10,14;
        int idx = 0;
        for (int x = col - 1; x >= 0; --x) {
            for (int y = 0; y < this.raw; ++y) {
                int oldidx = y * col + x;
                childs[oldidx].gameObject.transform.SetSiblingIndex(idx++);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == InputButton.Right) {
            DInputManager.me.OnPointerClick(eventData);
        }
        if (eventData.button == InputButton.Left) {
            DInputManager.me.OnPassengerClicked(eventData, this);
        }
    }

    public bool IsClicked(PointerEventData eventData) {
        var x = (int)(eventData.position.x - transform.position.x) / 62;
        var y = (int)(eventData.position.y - transform.position.y) / 62;
        y = this.raw - y - 1;// 世界坐标左下角是原点
        var idx = y * col + x;

        //Debug.Log("pos:" + eventData.position + " x,y:" + x + "," + y + "idx:" + idx);
        if (idx < 0 || idx >= transform.childCount) {
            return false;
        }
        return transform.GetChild(idx).GetComponent<DBlock>().blocked;
    }
}
