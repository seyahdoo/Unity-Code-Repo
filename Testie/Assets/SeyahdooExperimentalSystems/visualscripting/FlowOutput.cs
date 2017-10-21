using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowOutput {

    public FlowInput input;

    public void Invoke() {
        input.Invoke();
    }

}
