using AI.Support.Abstractions;
using AI.UtilityBasedSystem.Abstractions;
using System;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2016-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// A Factor for evaluating an action based on the character's
    /// rank in the fighting arts style he is employing in the fight. 
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 02-07-2016
    /// </date>
    public class FightingStyleRankFactor : IFactor
    {
	    FightingStyleRank _rank;

	    #region Public properties

	    /// <summary>
	    /// Gets or sets fighting arts style the rank.
	    /// </summary>
	    /// <value>The rank.</value>
	    public FightingStyleRank Rank
	    {
		    get
		    {
			    return _rank;
		    }
		    set
		    {
			    _rank = value;
		    }
	    }

	    #endregion
	
	    #region Public constructors

	    /// <summary>
	    /// Initializes a new instance of the <see cref="FightingStyleRankFactor"/> class.
	    /// </summary>
	    public FightingStyleRankFactor()
	    {
		    _rank = null;
	    }

	    /// <summary>
	    /// Initializes a new instance of the <see cref="Factor"/> class.
	    /// </summary>
	    public FightingStyleRankFactor (FightingStyleRank rank)
	    {
		    if (rank == null)
		    {
			    throw new ArgumentNullException ("FightingStyleRank argument cannot be null.");
		    }

		    _rank = rank;
	    }

	    #endregion

	    #region Public methods

	    /// <summary>
	    /// Evaluate the specified action.
	    /// </summary>
	    /// <param name="action">Action.</param>
	    public Appraisal Evaluate (AIAction action)
	    {
		    if (_rank == null)
		    {
			    throw new ArgumentNullException ("FightingStyleRank argument cannot be null.");
		    }

		    if (action == null)
		    {
			    throw new ArgumentNullException ("Action argument cannot be null.");
		    }

		    // The appraisal's basic score is linearly dependent on the rank
		    // the character possesses in their fighting arts style.
		    Appraisal appraisal = new Appraisal (_rank.Rank);

		    return appraisal;
	    }

	    #endregion
	
    }
}