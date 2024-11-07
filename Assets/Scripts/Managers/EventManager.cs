using UnityEngine;

namespace SS.FallUp.Event
{
    public class EventManager : MonoBehaviour
    {
        public EventController OnPlayerDeathEvent {  get; private set; }

        public EventManager()
        {
            OnPlayerDeathEvent = new EventController();
        }
    }
}
