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

    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = cristalsNumber + " / 5";
        if(cristalsNumber == 1)
        {
            // increment the step
            step += scaleSpeed * Time.deltaTime;
            objScale = aCurve.Evaluate(step);
            _transform.localScale = new Vector2(objScale, objScale);

            if(step >= 1)
            {
                //set the step to 0 and start over
                step = 0;
            }

            if(timeCount < delayTime)
            {
                timeCount += Time.deltaTime;
                //return;
            }
            else
            {
                step = 1;
            }
            
        }
        
    }
}
