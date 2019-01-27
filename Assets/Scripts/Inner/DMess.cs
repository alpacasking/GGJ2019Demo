using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class DMess : MonoBehaviour {
    public Sprite[] background;
    public int col;
    public int raw;
    public bool showBlocked;

    // Update is called once per frame
    void Update() {

    }
    // 在编辑器发生改变的时候进行调用
    void OnValidate() {
        UnityEditor.EditorApplication.delayCall += () => {
            //Debug.Log("new col:" + col + " raw:" + raw);
            if (this && transform && transform.childCount != col * raw) {
                updateChildBlockCount();
            }
        };
        for (int i = 0; i < transform.childCount; ++i) {
            var chl = transform.GetChild(i);
            chl.GetComponent<DBlock>().showUpdateBlocked(showBlocked);
        }
    }
    private void updateChildBlockCount() {
        for (int i = transform.childCount; i < col * raw; ++i) {
            GameObject go = new GameObject("Block-" + i / col + "_" + i % col);
            var block = go.AddComponent<DBlock>();
            var img = go.AddComponent<UnityEngine.UI.Image>();
            if (background.Length > 0) {
                var idx = (i / col + i) % background.Length;
                img.sprite = background[idx];
            }
            go.transform.SetParent(this.transform);
        }
        for (int j = transform.childCount - 1; j >= col * raw; --j) {
            DestroyImmediate(transform.GetChild(j).gameObject);
        }
        for (int k = 0; k < transform.childCount; ++k) {
            transform.GetChild(k).name = "Block-" + k / col + "_" + k % col;
        }
    }
}
