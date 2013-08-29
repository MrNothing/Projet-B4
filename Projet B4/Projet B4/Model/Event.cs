using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotonB4
{
    //events trigger units, spawn rewards or entities, display npc messages, activate other events.
    public enum EventType
    {
        doNothing, moveEntityTo, makeEntitySayMsg, castMakeEntityCastSpell, SpawnEntity, rewardAllParticipants, killEntity, destroyInvokedEntities,
    }

    public enum EventObjectiveType
    {
        doNothing, moveTo, talkTo, killEntity, killInvokedEntity,
    }

    public enum EventStatus
    {
        idle, started, paused, canceled
    }

    public class Event
    {
        public float range; //the range for the event to be visible by the player, it also is the range to start the event if no player was around
        public float duration;
        public Vector3 location;
        public Entity entity; //the entity for the event
        public Entity[] entitiesToInvoke;
        public float xpReward;
        public float goldReward;
        public int randomRewardLevel = 0; //if 0, no reward
        public EventType eventName;
        public EventObjectiveType objective;
        public string eventDescription;

        public EventStatus status = EventStatus.idle;

        public Event(float _duration, EventType _eventName, EventObjectiveType _objective)
        {
            duration = _duration;
            eventName = _eventName;
            objective = _objective;
        }
    }
}
