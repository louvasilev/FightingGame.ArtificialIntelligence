
using AI.Dependencies;

namespace AI.UtilityBasedSystem
{
    /// Copyright © 2015-2019 Lou Vasilev - All rights reserved.
    ///
    /// <summary>
    /// Fighting style rank.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 03-27-2016
    /// </date>
    public class FightingStyleRank
    {
	    private float _rank;
	    private Character _character;
	    private FightingArtsStyle _fightingArtsStyle;

	    #region Public properties

	    /// <summary>
	    /// Gets or sets the rank.
	    /// </summary>
	    /// <value>The rank.</value>
	    public float Rank
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

	    /// <summary>
	    /// Gets or sets the character.
	    /// </summary>
	    /// <value>The character.</value>
	    public Character TheCharacter
	    {
		    get
		    {
			    return _character;
		    }
		    set
		    {
			    _character = value;
		    }
	    }

	    /// <summary>
	    /// Gets or sets the fighting style.
	    /// </summary>
	    /// <value>The fighting style.</value>
	    public FightingArtsStyle FightingStyle
	    {
		    get
		    {
			    return _fightingArtsStyle;
		    }
		    set
		    {
			    _fightingArtsStyle = value;
		    }
	    }

	    #endregion

	    #region Public constructors

	    /// <summary>
	    /// Initializes a new instance of the <see cref="FightingStyleRank"/> class.
	    /// </summary>
	    public FightingStyleRank()
	    {
	    }

	    #endregion
    }
}