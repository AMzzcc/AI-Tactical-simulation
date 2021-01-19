using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class MemoryTree : Conditional
{
    public Sensory sensory;
    public SensoryMemory memory;
    void Start()
    {
        memory = sensory.memory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
