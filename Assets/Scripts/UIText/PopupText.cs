using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{
    [Range(0.25f, 5f)]
    public float scaleSpeed = 0.25f;
    public AnimationCurve aCurve;
    private Transform _transform;
    private float step;
    private float objScale;

    float timeCount;
    public float delayTime = 2f;

    public static int cristalsNumber;
    public Text displayText;

    void Awake()
    {
        displayText = GetComponent<Text>();
        cristalsNumber = 0;
    }

    // Use this for initialization
    void Start()
    {
        // set the starting scale/transform to whatever scale the gameobject has gamestart
        _transform = this.transform;

        enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
           // increment the step
            step += scaleSpeed * Time.deltaTime;
            objScale = aCurve.Evaluate(step);
            _transform.localScale = new Vector2(objScale, objScale);

            if(timeCount < delayTime)
            {
                timeCount += Time.deltaTime;
            }
            else
            {
                enabled = false;
            }
    }

    public void UpdateText(int numberSwitchValue)
    {
        cristalsNumber += numberSwitchValue;
        displayText.text = cristalsNumber + " / 5";
        step = 0;
        timeCount = 0;

        enabled = true;
    }
}
