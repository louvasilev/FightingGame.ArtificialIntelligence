
using AI.Support.Abstractions;
using UnityEngine;

namespace AI.Support
{
    /// Copyright © 2016 Lubomir Vasilev - All rights reserved.
    ///
    /// <summary>
    /// Represents an action, such as a fighting arts technique, taken
    /// during a turn in a fight. That will be useful in keeping track
    /// of what actions the AI controlled character performed during the
    /// fight, which in turn will be instrumental in deciding what action
    /// to take next.
    /// </summary>
    /// 
    /// <author>
    /// Lou Vasilev
    /// </author>
    /// <date>
    /// 05-29-2016
    /// </date>
    public class TurnAction : ScriptableObject
    {
        // TODO: those should probably be private fields
        // with corresponding public properties.
        public int turn = 0;
        public AIAction action = null;
    }
}