using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

	private Dictionary<string, UnityEvent> eventMap;

	void Init() {
		eventMap = new Dictionary<string, UnityEvent>();
	}

	public static string onPlayerDeath = "oPD";

	public static EventManager instance {
		get {
			if (!manager) {
				manager = FindObjectOfType(typeof(EventManager)) as EventManager;
				manager.Init();
			}

			return manager;
		}
	}

	public static void AddListener(string eventName, UnityAction listener) {
		UnityEvent e = null;

		if (!instance.eventMap.TryGetValue(eventName, out e)) {
			e = new UnityEvent();
			instance.eventMap.Add(eventName, e);
		}

		e.AddListener(listener);
	}

	public static void RemoveListener(string eventName, UnityAction listener) {
		UnityEvent e = null;

		if (instance.eventMap.TryGetValue(eventName, out e)) {
			e.RemoveListener(listener);
		}
	}

	public static void TriggerEvent(string eventName) {
		UnityEvent e = null;

		if (instance.eventMap.TryGetValue(eventName, out e)) {
			e.Invoke();
		}
	}

	private static EventManager manager;
}
