﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourGenoType: IMutatable, IRandomizable{
	VTreeNode<IBehaviourNode> phenotype(VTreeNode<IBehaviourNode> p_parent, BehaviourTree p_tree);
}

