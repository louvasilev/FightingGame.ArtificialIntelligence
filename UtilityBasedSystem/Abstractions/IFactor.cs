
using AI.Support.Abstractions;

namespace AI.UtilityBasedSystem.Abstractions
{
    /// Copyright © 2016 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// Represent a generic Factor. A Factor is an atomic piece of
    /// logic that evaluates one aspect of the game situation.
    /// </summary>
    /// <remarks>
    /// Factors generate Appraisals.
    /// </remarks>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 02-07-2016
    /// </date>
    /// <credit>
    /// Kevin Dill (http://www.gdcvault.com/play/1012410/Improving-AI-Decision-Modeling-Through)
    /// </credit>
    public interface IFactor
    {
	    /// <summary>
	    /// Evaluate the specified action.
	    /// </summary>
	    /// <param name="action">Action.</param>
	    Appraisal Evaluate (AIAction action);
    }
}