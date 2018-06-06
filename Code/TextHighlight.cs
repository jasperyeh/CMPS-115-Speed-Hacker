using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHighlight : MonoBehaviour {

    public int charIndex;
    public Text textComp;
    public Canvas canvas;
    public GameObject obj;
    public GameObject highlight;
    public List<GameObject> highlightList;

    Text uiText;

    bool triggered;

    private void Start()
    {
        uiText = GetComponent<WriteText>().uiText;
    }

    private void Update()
    {
        // Sets UI text to be the last child so it always shows over highlighting
        textComp.transform.SetAsLastSibling();

        // Takes user keyboard input
        foreach (char c in Input.inputString)
        {
            // If input is backspace
            if (c == '\b')
            {
                if (charIndex >= 1)
                {
                    charIndex -= 1;
                    Destroy(highlightList[charIndex].gameObject);
                    highlightList.RemoveAt(charIndex);
                }
                else
                {
                    charIndex = 0;
                }
            }
            else
            {
                if (charIndex > 0 || charIndex <= textComp.text.Length)
                {
                    charIndex += 1;
                    PrintPos();
                }
            }
        }
    }

    // Creates a new text generator and populates it with what we are typing
    // Then we get the corners of each letter and get the middle position in 
    // order to place the highlighting evenly
    void PrintPos()
    {
        string text = textComp.text;
        
        TextGenerator textGen = new TextGenerator(text.Length);
        Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
        textGen.Populate(text, textComp.GetGenerationSettings(extents));

        int newLine = text.Substring(0, charIndex).Split('\n').Length - 1;
        int whiteSpace = text.Substring(0, charIndex).Split(' ').Length - 1;
        int indexOfTextQuad = (charIndex * 4) + (newLine * 4) - 4;

        if (indexOfTextQuad < textGen.vertexCount)
        {
            Vector3 avgPos = (textGen.verts[indexOfTextQuad].position + textGen.verts[indexOfTextQuad + 1].position)/2;
            float dist = Vector3.Distance(textGen.verts[indexOfTextQuad].position, textGen.verts[indexOfTextQuad + 1].position);
            PrintWorldPos(avgPos, dist);
        }
        else
        {
            Debug.LogError("Out of text bound");
        }
    }

    // Creates the position in the world space where the highlighter is instantiated
    // and then creates it and places it in the highlight array
    void PrintWorldPos(Vector3 testPoint, float dist)
    {
        Vector3 worldPos = new Vector3(textComp.transform.TransformPoint(testPoint).x, textComp.transform.position.y, textComp.transform.position.z);
        new GameObject("point").transform.position = worldPos;
        
        obj.transform.position = worldPos;
        GameObject img = Instantiate(highlight, canvas.transform);
        img.transform.position = worldPos;
        img.GetComponent<RectTransform>().sizeDelta = new Vector2(dist, 50);
        highlightList.Add(img);
    }
}

