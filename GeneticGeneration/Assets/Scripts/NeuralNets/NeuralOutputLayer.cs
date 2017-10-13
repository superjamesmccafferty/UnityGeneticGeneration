﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Calc;

public class NeuralOutputLayer : IRecievable {
 
 //Has outputs and activation functions
  NeuralOutput[] m_outputs;
  DActivationFunction[] m_activators;

  //Create with activators, must have same number of activators to outputs
  public NeuralOutputLayer(NeuralOutput[] p_outputs, DActivationFunction[] p_activators){
    m_outputs = (NeuralOutput[])p_outputs.Clone();

    if(p_activators.Length != p_outputs.Length){
      Debug.LogError("OUTPUT WRONG NUMBER ACTIVATORS");
    }

    m_activators = (DActivationFunction[])p_activators.Clone();
  }

  //Number of outputs
  public int count()
  {
    return m_outputs.Length;
  }

  //Recieve propagation and activate
  public void recievePropagation(Matrix p_propagation)
  {
    Matrix prop = p_propagation.clone();
    
    Debug.Log("Ouput Preactivation \n" + prop);
    prop.activate(m_activators);
    Debug.Log("Ouput Activation \n" + prop);

    //Complete Output
    for(int i = 0; i<m_outputs.Length; i++){
      m_outputs[i].output(prop.get(i, 0));
    }
  }

  public SNeuralOutputDNA[] dnaify(){
    SNeuralOutputDNA[] outputs = new SNeuralOutputDNA[m_outputs.Length];

    for(int i = 0; i<m_outputs.Length;i++){
      outputs[i].m_output = m_outputs[i].dnaify();
      outputs[i].m_activate = m_activators[i];
    }

    return outputs;
  }

  public DActivationFunction[] getActivators(){
    return m_activators;
  }

}
