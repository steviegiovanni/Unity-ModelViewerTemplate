using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWindow : MonoBehaviour {
    public Text TextObject;
    public float Width;
    public float Lines;
    public Camera TextCamera;
    public Vector3 TextCameraInitPos;
    public float ScrollSpeed = 1.0f;
    public float LinesPerUnit = 1.75f;
    public GameObject TextPanel;
    public GameObject ScrollUpButton;
    public GameObject ScrollDownButton;
    public GameObject Scroller;

    private float TotalLines;
    private float DeltaLines;

	// Use this for initialization
	void Start () {
        SetWindowSize(Width, Lines);
        TextCameraInitPos = TextCamera.transform.position;
	}
	
    public void SetWindowSize(float width, float height)
    {
        TextObject.rectTransform.sizeDelta = new Vector2(width, height);
        float adjustedScale = LinesPerUnit / Lines;
        TextObject.rectTransform.localScale = new Vector3(adjustedScale,adjustedScale,adjustedScale);
        Canvas.ForceUpdateCanvases();

        UpdateLooks();
    }

    public void UpdateLooks()
    {
        TotalLines = TextObject.cachedTextGenerator.lines.Count;
        DeltaLines = TotalLines - Lines;

        TextPanel.transform.localScale = new Vector3(Width / Lines, TextPanel.transform.localScale.y, TextPanel.transform.localScale.z);
        TextPanel.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(Width / Lines,1.0f);
        TextPanel.GetComponent<MeshRenderer>().sharedMaterial.mainTextureOffset = new Vector2((1 - Width/Lines)/2, 0);
        ScrollUpButton.transform.localPosition = new Vector3(-Width/Lines/2 - ScrollUpButton.transform.localScale.x/2,ScrollUpButton.transform.localPosition.y, ScrollUpButton.transform.localPosition.z);
        ScrollDownButton.transform.localPosition = new Vector3(-Width / Lines / 2 - ScrollDownButton.transform.localScale.x / 2, ScrollDownButton.transform.localPosition.y, ScrollDownButton.transform.localPosition.z);
        
        float ySpace = 1 - ScrollUpButton.transform.localScale.y - ScrollDownButton.transform.localScale.y;
        float scrollerYScale = ySpace;
        if (DeltaLines > 0)
            scrollerYScale = scrollerYScale / DeltaLines;
        Scroller.transform.localScale = new Vector3(Scroller.transform.localScale.x, scrollerYScale, Scroller.transform.localScale.z);
        Scroller.transform.localPosition = new Vector3(-Width / Lines / 2 - Scroller.transform.localScale.x / 2, ScrollUpButton.transform.localPosition.y - ScrollUpButton.transform.localScale.y / 2 - Scroller.transform.localScale.y/2, Scroller.transform.localPosition.z);


    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
            UpdateText("this is a very long text that is supposed to test whether the cachedtextgenerator line count is going to work or not in our simulation. but what about this one then? because i'm still not satisfied with the result.");

        if (Input.GetKey(KeyCode.UpArrow))
            ScrollUp();
        if (Input.GetKey(KeyCode.DownArrow))
            ScrollDown();
    }

    public void UpdateText(string text)
    {
        TextObject.text = text;
        Canvas.ForceUpdateCanvases();
        TextCamera.transform.position = TextCameraInitPos;
        Canvas.ForceUpdateCanvases();

        UpdateLooks();
    }

    public void ScrollDown()
    {
        if (DeltaLines <= 0) return;
        float minY = TextCameraInitPos.y - 2.0f / Lines * DeltaLines;
        if (TextCamera.transform.position.y <= minY) return;

        TextCamera.transform.position -= TextCamera.transform.up * ScrollSpeed * Time.deltaTime;
        if (TextCamera.transform.position.y < minY)
            TextCamera.transform.position = new Vector3(TextCamera.transform.position.x, minY, TextCamera.transform.position.z);

        float ySpace = Scroller.transform.localScale.y * (DeltaLines-1);
        Scroller.transform.localPosition = new Vector3(Scroller.transform.localPosition.x,
             ScrollUpButton.transform.localPosition.y - ScrollUpButton.transform.localScale.y / 2 - Scroller.transform.localScale.y / 2 - ySpace * (TextCameraInitPos.y - TextCamera.transform.position.y) / (TextCameraInitPos.y - minY)
            , Scroller.transform.localPosition.z);
    }

    public void ScrollUp()
    {
        if (TextCamera.transform.position.y >= TextCameraInitPos.y) return;

        TextCamera.transform.position += TextCamera.transform.up * ScrollSpeed * Time.deltaTime;
        if (TextCamera.transform.position.y > TextCameraInitPos.y) TextCamera.transform.position = TextCameraInitPos;

        float minY = TextCameraInitPos.y - 2.0f / Lines * DeltaLines;
        float ySpace = Scroller.transform.localScale.y* (DeltaLines - 1);
        Scroller.transform.localPosition = new Vector3(Scroller.transform.localPosition.x,
             ScrollUpButton.transform.localPosition.y - ScrollUpButton.transform.localScale.y / 2 - Scroller.transform.localScale.y / 2 - ySpace * (TextCameraInitPos.y - TextCamera.transform.position.y) / ( TextCameraInitPos.y - minY) 
            , Scroller.transform.localPosition.z);
    }
}
