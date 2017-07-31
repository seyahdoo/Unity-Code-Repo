using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowInput {

    public void Invoke() {

        if (invoked != null) invoked();
    }
    
    public delegate void VoidDelegate();
    public event VoidDelegate invoked;

}
