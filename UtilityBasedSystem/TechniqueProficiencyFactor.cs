using AI.Dependencies;
using AI.Support.Abstractions;
using AI.UtilityBasedSystem.Abstractions;
using System;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 05-15-2016
    /// </date>
    public class TechniqueProficiencyFactor : IFactor 
    {

	    #region Public constructors

	    /// <summary>
	    /// Initializes a new instance of the <see cref="TechniqueProficiencyFactor"/> class.
	    /// </summary>
	    public TechniqueProficiencyFactor()
	    {
	    }

	    #endregion

	    #region Public methods

	    /// <summary>
	    /// Evaluate the specified action in terms of the technique proficiency corresponding
	    /// to it. In other words, measure the worth (to the character) of the action depending
	    /// on how skilled he is at the fighting technique tied to the action.
	    /// </summary>
	    /// <remarks>>
	    /// This assumes the given action is of type FightingTechniqueAction.
	    /// </remarks>
	    /// <param name="action">The Action to evaluate.</param>
	    /// <returns>
	    /// An Appraisal instance containing the utility measure of the given action.
	    /// </returns>
	    public Appraisal Evaluate (AIAction action)
	    {
		    if (action == null)
		    {
			    throw new ArgumentNullException ("Action argument cannot be null.");
		    }

		    TechniqueProficiency proficiency = ((FightingTechniqueAction)action).Proficiency;

		    // The appraisal's basic score is linearly dependent on the proficiency
		    // of the character in the technique corresponding to the given action.
		    Appraisal appraisal = new Appraisal (proficiency.Proficiency);

		    return appraisal;
	    }

	    #endregion

    }
}