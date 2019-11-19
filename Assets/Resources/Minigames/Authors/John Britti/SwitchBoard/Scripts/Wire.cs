using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{

    public LineRenderer line;
    public WireLead[] leads;
    [HideInInspector] public InputHole[] correctHoles;
    [SerializeField] private Vector3 lineOffset;
    [SerializeField] private int subdivisions;
    [SerializeField] private float springForce = 50;
    [SerializeField] private float springDistFactor = 0.6f;
    [SerializeField] GameObject springPrefab;
    private GameObject[] springs;
    // Start is called before the first frame update

    void Awake()
    {
        correctHoles = new InputHole[2];
    }
    void Start()
    {
        springs = new GameObject[subdivisions];
        line = GetComponent<LineRenderer>();
        leads = GetComponentsInChildren<WireLead>();
        for (int i = 0; i < subdivisions; i++) {
            Vector3 pos = Vector3.Lerp(leads[0].transform.position, leads[1].transform.position, 1f / (subdivisions + 1f) * i);
            springs[i] = Object.Instantiate(springPrefab, transform);
            springs[i].transform.position = pos;
        }
        for (int i = 0; i < subdivisions; i++) {
            ConfigurableJoint[] joints = springs[i].GetComponents<ConfigurableJoint>();
            SoftJointLimit lim = new SoftJointLimit();
            SoftJointLimitSpring sLim = joints[0].linearLimitSpring;
            sLim.spring = springForce;
            lim.limit = Vector3.Distance(leads[0].transform.position, leads[1].transform.position) / (subdivisions + 1f) * springDistFactor;
            joints[0].linearLimit = lim;
            joints[1].linearLimit = lim;
            joints[0].linearLimitSpring = sLim;
            joints[1].linearLimitSpring = sLim;
            if (i == 0) {
                joints[0].connectedBody = leads[0].GetComponent<Rigidbody>();
                joints[0].autoConfigureConnectedAnchor = false;
                joints[0].connectedAnchor = lineOffset;
            } else {
                joints[0].connectedBody = springs[i - 1].GetComponent<Rigidbody>();
            }
            if (i == subdivisions - 1) {
                joints[1].connectedBody = leads[1].GetComponent<Rigidbody>();
                joints[1].autoConfigureConnectedAnchor = false;
                joints[1].connectedAnchor = lineOffset;
                // joints[1].autoConfigureConnectedAnchor = true;
            } else {
                joints[1].connectedBody = springs[i + 1].GetComponent<Rigidbody>();
            }
        }
        line.positionCount = subdivisions + 2;
    }

    public void PositionSetDirect(int lead, Vector3 position) {
        leads[lead].transform.position = position;
        for (int i = 0; i < subdivisions; i++) {
            // springs[i].GetComponent<Rigidbody>().isKinematic = true;
            springs[i].transform.position = Vector3.Lerp(leads[0].transform.position, leads[1].transform.position, 1f / (subdivisions + 1f) * i);
            // springs[i].GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = new Vector3[subdivisions + 2];
        positions[0] = leads[0].Model.transform.position + lineOffset;
        positions[subdivisions + 1] = leads[1].Model.transform.position + lineOffset;
        for(int i = 0; i < subdivisions; i++) {
            positions[i + 1] = springs[i].transform.position;
        }
        line.SetPositions(positions);
    }

    public bool CorrectHoles() {
        return (leads[0].hole == correctHoles[0] && leads[1].hole == correctHoles[1]) || (leads[0].hole == correctHoles[1] && leads[1].hole == correctHoles[0]);
    }
}
