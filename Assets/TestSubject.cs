using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSubject : Subject
{
    // Start is called before the first frame update
    void Start()
    {
        TestObserver t = new TestObserver();
        TestObserver2 t2 = new TestObserver2();
        TestEvent te = new TestEvent(Test.EEEEEE);

        AddObserver(t);
        AddObserver(t2);

        Notify(te);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
