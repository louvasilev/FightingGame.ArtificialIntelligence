using AI.Dependencies;
using AI.Support.Abstractions;
using AI.UtilityBasedSystem.Abstractions;
using System;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// A factor for evaluating actions based on the status of the
    /// body part(s) they are performed with.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 05-26-2016
    /// </date>
    public class AttackerBodyPartFactor : IFactor
    {
	    // The initial score of the action being evaluated.
	    private float _initialScore;

	    #region Public constructors

	    /// <summary>
	    /// Initializes a new instance of the <see cref="AttackerBodyPartFactor"/> class.
	    /// </summary>
	    public AttackerBodyPartFactor()
	    {
		    _initialScore = 1.0f;
	    }

	    #endregion

	    #region Public methods

	    /// <summary>
	    /// Evaluate the specified action.
	    /// </summary>
	    /// <param name="action">Action.</param>
	    public Appraisal Evaluate (AIAction action)
	    {
		    if (action == null)
		    {
			    throw new ArgumentNullException ("Action argument cannot be null.");
		    }

		    TechniqueProficiency proficiency = ((FightingTechniqueAction)action).Proficiency;

		    if (proficiency == null)
		    {
			    throw new ArgumentNullException ("FightingTechniqueAction variable cannot be null.");
		    }

		    BodyPart bodyPart = proficiency.TechniqueBodyPart;

		    if (bodyPart == null)
		    {
			    throw new ArgumentNullException ("BodyPart variable cannot be null.");
		    }

		    float damage = bodyPart.DamagePercentage;

		    Appraisal appraisal = new Appraisal ();

		    if (damage <= 5)
		    {
			    appraisal.BasicScore = _initialScore;
		    }
		    else if (damage <= 35)
		    {
			    appraisal.BasicScore = _initialScore - damage / 100;
		    }
		    else if (damage <= 85)
		    {
			    appraisal.BasicScore = (_initialScore - damage / 100) / TacticalMind.BODY_PART_DAMAGE_COEFFICIENT;
		    }
		    else
		    {
			    appraisal.BasicScore = 0.0f;
		    }

		    return appraisal;
	    }

	    #endregion

    }
}