using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DBlock : MonoBehaviour {
    public int x, y;
    public bool blocked; // 是否锁定 占位
    void OnValidate() {
        bool toshow = this.transform.parent &&
            this.transform.parent.GetComponent<DMess>()
            && this.transform.parent.GetComponent<DMess>().showBlocked;
        this.showUpdateBlocked(toshow);
    }
    public void showUpdateBlocked(bool show) {
        var img = this.GetComponent<UnityEngine.UI.Image>();
        if (img == null)
            return;
        img.color = (show && blocked) ? new Color(255, 0, 0, 128) : new Color(255, 255, 255, 255);
    }

    private DMess pmess;
    public DMess ParentMess {
        get {
            if (pmess)
                return pmess;
            pmess = transform.parent.GetComponent<DMess>();
            return pmess;
        }
    }

    private void Start() {
        x = (int)this.transform.localPosition.x / 62;
        y = (int)this.transform.localPosition.y / 62;
    }

}
