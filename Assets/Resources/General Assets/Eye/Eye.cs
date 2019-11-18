using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {
    Material m;
    [Header("Look")]
    [SerializeField] private AnimationCurve lookCurve = null;
    [SerializeField] private float lookDuration = 0.3f;
    //[SerializeField] private float lookSpeed = 5;
    [SerializeField] private float lookMin = 0.2f;
    [SerializeField] private float lookMax = 0.8f;
    [SerializeField] private float lookWaitMin = 0.5f;
    [SerializeField] private float lookWaitMax = 3.5f;
    private float lookTimer;
    private bool movingLook;

    [Header("Blink")]
    [SerializeField] private AnimationCurve blinkCurve = null;
    [SerializeField] private float blinkDuration = 0.3f;
    [SerializeField] private float blinkWaitMin = 0.3f;
    [SerializeField] private float blinkWaitMax = 2.5f;
    private float blinkTimer;
    private bool blinking;

    [Header("Float")]
    [SerializeField] private Vector3 moveAmount = Vector3.zero;
    private Vector3 offset;
    [SerializeField] private Vector3 rotateAmount = Vector3.zero;
    Vector3 startPos;
    Vector3 startRot;

    // Start is called before the first frame update
    void Start() {
        lookTimer = Random.Range(lookWaitMin, lookWaitMax);
        blinkTimer = Random.Range(blinkWaitMin, blinkWaitMax);
        m = GetComponent<Renderer>().material;
        m.SetFloat("_EyelidBlink", blinkCurve.Evaluate(0));
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
        offset = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
    }

    // Update is called once per frame
    void Update() {
        lookTimer -= Time.deltaTime;
        blinkTimer -= Time.deltaTime;
        if (lookTimer < 0) {
            if (!movingLook) {
                StartCoroutine(MoveLook(new Vector2(Random.Range(lookMin, lookMax), Random.Range(lookMin, lookMax))));
            }
            lookTimer = Random.Range(lookWaitMin, lookWaitMax);
        }
        if (blinkTimer < 0) {
            if (!blinking) {
                StartCoroutine(Blink());
            }
            blinkTimer = Random.Range(blinkWaitMin, blinkWaitMax);
        }
        transform.localPosition = startPos + new Vector3(
            moveAmount.x * Mathf.Sin(Time.timeSinceLevelLoad + offset.x),
            moveAmount.y * Mathf.Cos(Time.timeSinceLevelLoad + offset.y),
            moveAmount.z * Mathf.Sin(Time.timeSinceLevelLoad + offset.z));
        transform.localEulerAngles = startRot + new Vector3(
            rotateAmount.x * Mathf.Sin(Time.timeSinceLevelLoad + offset.x),
            rotateAmount.y * Mathf.Cos(Time.timeSinceLevelLoad + offset.y),
            rotateAmount.z * Mathf.Sin(Time.timeSinceLevelLoad + offset.z));
    }

    private void OnDestroy() {
        Destroy(m);
    }

    private IEnumerator Blink() {
        blinking = true;
        float perc = 0;
        float start = Time.timeSinceLevelLoad;
        m.SetFloat("_EyelidBlink", blinkCurve.Evaluate(0));
        do {
            perc = (Time.timeSinceLevelLoad - start) / blinkDuration;
            m.SetFloat("_EyelidBlink", blinkCurve.Evaluate(perc));
            yield return null;
        } while (perc < 1);
        m.SetFloat("_EyelidBlink", blinkCurve.Evaluate(1));
        blinking = false;
    }
    private IEnumerator MoveLook(Vector2 endPos) {
        movingLook = true;
        float perc = 0;
        float start = Time.timeSinceLevelLoad;
        Vector4 startPos = m.GetVector("_IrisCenter");
        m.SetVector("_IrisCenter", Vector2.Lerp(startPos, endPos, lookCurve.Evaluate(0)));
        do {
            perc = (Time.timeSinceLevelLoad - start) / lookDuration;
            m.SetVector("_IrisCenter", Vector2.Lerp(startPos, endPos, lookCurve.Evaluate(perc)));
            yield return null;
        } while (perc < 1);
        m.SetVector("_IrisCenter", Vector2.Lerp(startPos, endPos, lookCurve.Evaluate(1)));
        movingLook = false;
    }
}