
namespace AI.Support.Abstractions
{
    /// Copyright © 2016-2019 Lubomir Vasilev - All rights reserved.
    ///
    /// <summary>
    /// An Action is one of all the possible things the AI controlled character can
    /// do at that point in the game. Usually an Action is a Fighting Arts technique,
    /// but it can also be other things the character could do during a fight, such
    /// as move forward or backward, jump, crouch, taunt the opponent, meditate,
    /// heal himself, etc (to keep things simple, initially actions will be limited
    /// to Fighting Arts techniques).
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 02-07-2016
    /// </date>
    public abstract class AIAction
    {
        /// <summary>
        /// Determines whether this instance is identical to the specified action.
        /// </summary>
        /// <returns><c>true</c> if this instance is identical to the specified action; otherwise, <c>false</c>.</returns>
        /// <param name="action">The Action to compare this one to.</param>
        public abstract bool IsIdenticalTo(AIAction action);
    }
}