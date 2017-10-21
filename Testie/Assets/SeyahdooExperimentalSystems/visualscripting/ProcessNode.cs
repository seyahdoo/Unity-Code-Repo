using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessNode : BaseNode  {

    FlowInput inputflow;
    FlowOutput outputflow;

    BaseVarible[] inputVaribles;
    BaseVarible[] outputVaribles;


    virtual internal void Process() { }

    void Done()
    {
        outputflow.Invoke();
    }

}
