﻿using System.Collections;
using System.Collections.Generic;

using Genetic.Base;
using Genetic.Behaviour.DecisionNets;
using Genetic.Composite;
using Genetic.Traits.Base;
using Genetic.Traits.TraitGenes;
using JTools.Calc.ActiavationFunctions;
using JTools.Calc.Base;
using JTools.Calc.Lines;
using JTools.Calc.Vectors;
using JTools.Events;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject m_prefab;
	public DNABasedEvolutionManager<MindBodyDNA<DecisionNetCreature>> m_evolution;
	IntervalEventManager m_interval;
	Line2D m_goalLine = new Line2D(new Vector2(15,15), new Vector2(1,-0.5f));

	void Start () {
		m_evolution = new DNABasedEvolutionManager<MindBodyDNA<DecisionNetCreature>>(
			 new MindBodySpecies<DecisionNetCreature>(0,
			 	new TraitGenesSpecies(0, new HashSet<ETrait> {ETrait.SPEED}, 4, new Range<float>(0.25f, 1f), 4, new Range<float>(-0.5f, 0.5f)),
				new DecisionNetSpecies<DecisionNetCreature>( 0, DecisionNetCreature.getInputFactorys(), DecisionNetCreature.getOutputFactorys(), new Range<float>(0.8f, 1.2f) )
			 ), 0.1f, 100, (float p_fitness) => { return p_fitness * 0.9f; }, 1f 
		);
	
		for(int i = 0; i<20; i++){
			m_evolution.addRandom();
		}

		m_interval = new IntervalEventManager();

		m_interval.addListener(0.25f, () => {
			for(int i = 0; i<10; i++){
				spawn();
			}
	 	} );

		m_interval.addListener(0.1f, () => {
			m_goalLine.rotate(1f);
		});

	
	
	
	
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		m_interval.tick(Time.fixedDeltaTime);
		m_evolution.tick(Time.fixedDeltaTime);
	}

	void spawn(){
		GameObject obj = Instantiate(m_prefab, Vector3.zero, /*Quaternion.identity*/ Quaternion.Euler(0,0,Random.Range(0,360)));
		DecisionNetCreature cre = obj.GetComponent<DecisionNetCreature>();
		cre.Initialize(m_evolution.birth(), this, m_goalLine);
	}

	public void logDNA(MindBodyDNA<DecisionNetCreature> dna, float fitness){
		m_evolution.addDNA(dna, fitness);
	}
}


