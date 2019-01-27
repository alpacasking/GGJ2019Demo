using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DCarriage : DMess, IPointerClickHandler {
    void Update() {

    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!DInputManager.me.moveOne)
            return;

        DInputManager.me.DropPassanger();
        //DPassenger.selectPassenger = null;
    }
}
