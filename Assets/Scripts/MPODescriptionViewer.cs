using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelViewer;

public class MPODescriptionViewer : MonoBehaviour {
    /// <summary>
    /// the multipartsobject we're going to query this object to get the node info once we get a hover event
    /// </summary>
    [SerializeField]
    private MultiPartsObject _mpo;
    public TextWindow _textWindow;

    // Use this for initialization
    void Start () {
        _mpo.OnSelectEvent.AddListener(ShowDescription);
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

    public void ShowDescription(Node node)
    {
        _textWindow.UpdateText(node.Description);
    }
}
