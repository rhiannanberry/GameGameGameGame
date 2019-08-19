using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnStateEnter<T> {
    T OnStateEnter();
}

public interface IOnStateExit<T> {
    T OnStateExit();
}
