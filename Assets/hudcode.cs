using UnityEngine;
using LitJson;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;


public class hudcode : MonoBehaviour {
	
	float showTime = 4;
	float curShowTime;
	bool rect = false;
	bool isShown;
	bool isAnimIn = false;
	bool isAnimOut = false;
	float opacity = 0;
	JsonData data = null;
	float default_opacity = 1;
	Texture2D imgtext;
	string message = "";
	Vector3 OFFSET = new Vector3 (-.5f, 1, 2);
	public WWW www;
	public WWW www2;
	public string json = null;
	GameObject man;
	Texture2D healthbar;
	Texture2D totalhealth;
	Texture2D shark;
	Texture2D rgb_texture;
	GUIStyle fground;
	GUIStyle bground;
	GUIStyle generic_style;
	GUIStyle empty;

	Rect sharkrect;
	
	// Use this for initialization
	void Start () {
		sharkrect = new Rect (Screen.width - 510, 290, 80, 60);
		empty = new GUIStyle ();
		empty.alignment = TextAnchor.MiddleLeft;
		empty.fontSize = 25;
		shark = (Texture2D)Resources.Load ("shark");
		Color color1 = Color.yellow;
		Color color2 = new Color (0f, 0f, 0f, 1.0f);
		healthbar = new Texture2D (400, 50);
		totalhealth = new Texture2D (400, 50);
		int i, j;
		for (i = 0; i < 400; i++) {
			for (j = 0; j < 50; j++) {
				healthbar.SetPixel (i, j, color1);
			}
		}
		healthbar.Apply ();
		for (i = 0; i < 400; i++) {
			for (j = 0; j < 50; j++) {
				totalhealth.SetPixel (i, j, color2);
			}
		}
		totalhealth.Apply ();
		bground = new GUIStyle ();
		fground = new GUIStyle ();
		fground.normal.background = healthbar;
		bground.normal.background = totalhealth;
		man = GameObject.Find("MAN"); 
		opacity = 0;
		imgtext = new Texture2D(20, 20, TextureFormat.DXT5, false);
		www = new WWW("http://www.rezgo.com/wp-content/uploads/2011/01/facebook-logo-128.png.png");
		www2 = new WWW ("http://ironmanvr.herokuapp.com/notification");

		int w = 275;
		int h = 100;
		rgb_texture = new Texture2D(w, h);
		Color rgb_color = new Color(0f, 0f, 0f, 0.8f);
		Color transp = new Color(15.0f, 15.0f, 15.0f, 0.0f);
		int radius = 7;
		for(i = 0;i<w;i++)
		{
			for(j = 0;j<h;j++)
			{
				
				int inum = Math.Min (i, w - i);
				int jnum = Math.Min (j, h - j);
				if (inum <= radius && jnum <= radius){
					if (Math.Pow ((double)radius - inum, 2.0) + Math.Pow ((double)radius - jnum, 2.0) < Math.Pow((double)radius,2.0)) {
						rgb_texture.SetPixel (i, j, rgb_color);
					} else {
						rgb_texture.SetPixel (i, j, transp);
					}
				} else {
					rgb_texture.SetPixel (i, j, rgb_color);
				}
			}
		}
		rgb_texture.Apply();

	}
	
	// Update is called once per frame
	void Update () {
		if (www2.isDone) {
			if (json == "" || json == null) {
				Debug.Log("first");
				json = www2.text;
				data = JsonMapper.ToObject(www2.text);
				Debug.Log ((string)data ["message"]);
			} else if (json != www2.text) {
				data = JsonMapper.ToObject(www2.text);
				json = www2.text;
				www = new WWW((string)data["icon"]);
				newMessage ((string)data ["message"]);
				
				// wait until the download is done
				// assign the downloaded image to the main texture of the object
			}
			www2 = new WWW ("http://www.ironmanvr.com/notification");
			
		}
		if (www.isDone) {
			www.LoadImageIntoTexture(imgtext);
		}
		
		if (isAnimIn) {
			opacity += Time.deltaTime;
			
			if ( opacity >= 1.0f ) {
				isShown = true;
				isAnimIn = false;
				opacity = 1;
				curShowTime = showTime;
			}
		}
		if (isShown) {
			curShowTime-= Time.deltaTime;
			if ( curShowTime <= 0 ) {
				isAnimOut = true;
				isShown = false;
				rect = false;
			}
		}
		if ( isAnimOut ) {
			opacity -= Time.deltaTime;
			if ( opacity <= 0f ) {
				isAnimOut = false;
				opacity = 0;
				rect = false;
			}
		}
		
	}
	
	void newMessage(string newmess) {
		int i = 0;
		int split = 40;
		message = newmess;
		//		while (i < newmess.Length) {
		//			if (i + split >= newmess.Length) {
		//				message = message + newmess.Substring (i);
		//				i += split;
		//			} else {
		//				int count = 0;
		//				int newsplit = split;
		//				while (newmess [i+newsplit] != ' ' && newsplit > 0) {
		//					newsplit--;
		//					count++;
		//					if (count == 40) {
		//						newsplit += 40;
		//						break;
		//					}
		//				}
		//				message = message + newmess.Substring (i, newsplit) + "\n";
		//				i += newsplit;
		//			}
		//
		//		}
		Debug.Log (message);
		isAnimIn = true;
		rect = true;
	}
	void OnGUI(){

		if (rect && www.isDone ) {
			Render_Colored_Rectangle (370, 330, (string)data ["app"], message);
			//Render_Colored_Rectangle(Screen.currentResolution.width-370-275, 330, (string)data ["app"], message);
			//(string)data["app"] + " : " + (string)data["message"]
		}

		manmove mandata = man.GetComponent<manmove> ();
		Sharks_Killed (mandata.kills);
		//Render_Health (mandata.shootingLeft / manmove.totalShooting);

	}
	void Render_Colored_Rectangle(int x, int y, string app, string message){
		//		byte[] bitmapData = Convert.FromBase64String(encoded);
		//		string foo = "";
		//		int a;
		//		for (a = 0; a < bitmapData.Length; a++) {
		//			foo += a;
		//		}
		// System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
		// Bitmap bitImage = new Bitmap(streamBitmap);
		GUI.skin.box.normal.background = rgb_texture;
		generic_style = new GUIStyle(GUI.skin.box);
		generic_style.wordWrap = true;
		generic_style.alignment = TextAnchor.MiddleLeft;
		generic_style.fontStyle = FontStyle.Bold;
		
		Debug.Log (message);
		Debug.Log (app);
		//		GUI.BeginGroup (new Rect (x, y, w, h));
		GUI.Box(new Rect (x,330, 275, 100), new GUIContent(app + ":\n\n" + message, imgtext), generic_style);
		//		GUI.Box (new Rect (170, 20, 250, 100), app + "\n" + message );
		//		GUI.EndGroup ();
	}
	void Render_Health(float health){
		
		int x1 = (int)Math.Floor (400 * health);
		Debug.Log (x1);
		int x2 = 400;

		

		int xpos = 150;
		int ypos = 250;

		GUI.BeginGroup (new Rect (xpos, ypos, x2,15));
		
		// Draw the background image
		GUI.Box (new Rect (0, 0, x2 ,15), totalhealth, bground);
		
		// Create a second Group which will be clipped
		// We want to clip the image and not scale it, which is why we need the second Group
		GUI.BeginGroup (new Rect (0, 0, x1, 15));
		
		// Draw the foreground image
		GUI.Box (new Rect (0, 0, x2, 15), healthbar, fground);
		
		// End both Groups
		GUI.EndGroup ();
		
		GUI.EndGroup ();
	}
	void Sharks_Killed(int killed){
		//GUI.Box (new Rect (Screen.width - 500, 200, 80, 60), new GUIContent ("= " + killed,shark), empty);
		GUI.Box (sharkrect, new GUIContent ("= " + killed,shark), empty);
	}
}