using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SwitchBoardController : MonoBehaviour
{
    SwitchLight[] lights;
    InputHole[] holes;
    [HideInInspector] public Wire[] wires;
    int[] sourceHoles = {-1, -1, -1, -1};
    int[] targetHoles = {-1, -1, -1, -1};
    
    [ColorUsageAttribute(false,true)]
    public Color[] colors = new Color[4];
    public Material[] wireMaterials = new Material[4];

    public float lightDelay = 0.1f;

    public static SwitchBoardController instance;

    private void Start()
    {
        instance = this;
        lights = GetComponentsInChildren<SwitchLight>();
        holes = GetComponentsInChildren<InputHole>();
        wires = FindObjectsOfType<Wire>();

        for (int i = 0; i < sourceHoles.Length; i++) {
            int target = Random.Range(0, holes.Length);
            while (sourceHoles.Any(elem => elem == target)) {
                target = Random.Range(0, holes.Length);
            }
            sourceHoles[i] = target;
            wires[i].leads[0].hole = holes[target];
            wires[i].PositionSetDirect(0, holes[target].transform.position);
            wires[i].line.material = wireMaterials[i];
            // lights[target].SetLight(colors[i]);
        }

        for (int i = 0; i < targetHoles.Length; i++) {
            int target = Random.Range(0, holes.Length);
            while (targetHoles.Any(elem => elem == target) || sourceHoles.Any(elem => elem == target)) {
                target = Random.Range(0, holes.Length);
            }
            targetHoles[i] = target;
            wires[i].leads[1].hole = holes[target];
            wires[i].PositionSetDirect(1, holes[target].transform.position);
            // lights[target].SetLight(colors[i]);
        }

        sourceHoles = new[]{-1, -1, -1, -1};
        targetHoles = new[]{-1, -1, -1, -1};

        for (int i = 0; i < sourceHoles.Length; i++) {
            int target = Random.Range(0, holes.Length);
            while (sourceHoles.Any(elem => elem == target)) {
                target = Random.Range(0, holes.Length);
            }
            sourceHoles[i] = target;
            wires[i].correctHoles[0] = holes[target];
            // lights[target].SetLight(colors[i]);
        }

        for (int i = 0; i < targetHoles.Length; i++) {
            int target = Random.Range(0, holes.Length);
            while (targetHoles.Any(elem => elem == target) || sourceHoles.Any(elem => elem == target)) {
                target = Random.Range(0, holes.Length);
            }
            targetHoles[i] = target;
            wires[i].correctHoles[1] = holes[target];
            // lights[target].SetLight(colors[i]);
        }

        StartCoroutine(StartLights());
    }

    IEnumerator StartLights() {
        for(int i = 0; i < targetHoles.Length; i++) {
            lights[sourceHoles[i]].SetLight(colors[i]);
            lights[targetHoles[i]].SetLight(colors[i]);
            yield return new WaitForSeconds(lightDelay);
        }
    }
}
