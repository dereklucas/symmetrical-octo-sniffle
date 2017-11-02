using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayers : MonoBehaviour {
	public GameObject player;
	private List<GameObject> players = new List<GameObject>();

  private int joystickId;
  private string joystickName;

	void Update () {
		// Look for new joysticks

    var key = CheckForKey();
    if (key != KeyCode.None) {
			if (key == KeyCode.W) {
				createPlayer("Keyboard 1");
			} else if (key == KeyCode.UpArrow) {
				createPlayer("Keyboard 2");
			} else {
	      // Convert Joystick3Button42 to 3
	      try {
	        joystickId = int.Parse(key.ToString().Replace("Joystick", "").Split(new string[] { "Button" }, System.StringSplitOptions.RemoveEmptyEntries)[0]);
	        joystickName = Input.GetJoystickNames()[joystickId - 1];

					createPlayer("Joystick " + joystickId);

	        Debug.Log("#" + joystickId + " - " + joystickName);
	      } catch (System.Exception) { }
			}
    }
	}

	private void createPlayer(string id) {
		if (players.Exists(x => x.name == id)) {
			return;
		}

		Debug.Log("New Player:" + id);
		GameObject newPlayer = Instantiate(player);
		newPlayer.name = id;

    PlayerControl control = newPlayer.GetComponent<PlayerControl>();
		control.controller = id;

		players.Add(newPlayer);
	}

  private KeyCode CheckForKey(string id = "") {
    foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode))) {

      // Get "JoystickNButtonX" keys but not "JoystickButtonX" keys
      if ((k.ToString().Contains("Joystick" + id) && !k.ToString().Contains("JoystickButton")) ||
					k.ToString() == "W" || k.ToString() == "UpArrow") {
        if (Input.GetKeyDown(k)) {
          return k;
        }
      }
    }
    return KeyCode.None;
  }
}
