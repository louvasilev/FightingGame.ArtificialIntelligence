using AI.Dependencies;
using AI.Dependencies.Abstractions;
using AI.Dependencies.Enumerations;
using AI.Support.Abstractions;
using AI.UtilityBasedSystem.Abstractions;
using System;
using System.Collections.Generic;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// A Factor for evaluating an action based on the character's
    /// physical condition. Specifically, based on the general rule
    /// that punches take less energy to perform than kicks, as
    /// physical condition deteriorates under certain threshold,
    /// punches will be favored over kicks.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 05-19-2016
    /// </date>
    public class PhysicalConditionFactor : IFactor
    {
	    // The initial score of the action being evaluated.
	    private float _initialScore;
	    // The threshold (as a percentage of the maximum physical
	    // condition) under which techniques which require less
	    // energy to perfrom will start being favored.
	    private float _modificationThreshold;

	    #region Public constructors

	    /// <summary>
	    /// Initializes a new instance of the <see cref="PhysicalConditionFactor"/> class.
	    /// </summary>
	    public PhysicalConditionFactor()
	    {
		    _initialScore = 1.0f;
		    _modificationThreshold = 0.75f;
	    }

	    #endregion

	    #region Public methods

	    /// <summary>
	    /// Evaluate the specified action.
	    /// </summary>
	    /// <param name="action">Action.</param>
	    public Appraisal Evaluate(AIAction action)
	    {
		    if (action == null)
		    {
			    throw new ArgumentNullException ("Action argument cannot be null.");
		    }

		    List<ICondition> conditions = TacticalMind.Input.FighterConditions;
		    if (conditions == null)
		    {
			    throw new ArgumentNullException ("ICondition variable cannot be null.");
		    }

		    if (conditions.Count < 1)
		    {
			    throw new ArgumentNullException ("ICondition variable cannot be of length 0.");
		    }

		    // TODO: conditions should be stored per character and there should
		    // probably be a more elegant way to access them.
		    ICondition physicalCondition = conditions [3];

		    TechniqueProficiency proficiency = ((FightingTechniqueAction)action).Proficiency;

		    if (proficiency == null)
		    {
			    throw new ArgumentNullException ("FightingTechniqueAction variable cannot be null.");
		    }

		    float endurance = ((PhysicalCondition)physicalCondition).Condition;
		    Appraisal appraisal = new Appraisal ();

		    // If the current endurance is above the pre-defined
		    // threshold then the basic score will be equal to the
		    // initial score. Otherwise the score will be reduced or
		    // increased by the result of dividing the endurance by
		    // the maximum endurance, depending on whether it's a punch
		    // or a kick.
		    if (endurance >= _modificationThreshold * TacticalMind.MAX_PHYSICAL_CONDITION_VALUE)
		    {
			    appraisal.BasicScore = _initialScore;
		    }
		    else if (proficiency.TechniqueBodyPart.PartType == BodyPartType.LeftArm ||
		             proficiency.TechniqueBodyPart.PartType == BodyPartType.RightArm)
		    {
			    appraisal.BasicScore = _initialScore - endurance / TacticalMind.MAX_PHYSICAL_CONDITION_VALUE;
		    }
		    else if (proficiency.TechniqueBodyPart.PartType == BodyPartType.LeftLeg ||
		             proficiency.TechniqueBodyPart.PartType == BodyPartType.RightLeg)
		    {
			    appraisal.BasicScore = _initialScore + endurance / TacticalMind.MAX_PHYSICAL_CONDITION_VALUE;
		    }
		    else
		    {
			    appraisal.BasicScore = _initialScore;
		    }

		    return appraisal;
	    }

	    #endregion

    }
}