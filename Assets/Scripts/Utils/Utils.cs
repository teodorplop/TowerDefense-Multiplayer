using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class Utils
{
    public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {
        float result = Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
        if (result < 0)
            result += 360;
        return result;
    }

    public static string SignatureF(float value)
    {
        if (value > Mathf.Epsilon)
            return "+";
        if (value < -Mathf.Epsilon)
            return "-";
        return string.Empty;
    }

    public static string SignatureI(int value)
    {
        return SignatureF((float)value);
    }

    public static void Swap(ref object First, ref object Second)
    {
        object temp = First;
        First = Second;
        Second = temp;
    }

	public static T GetEnumValue<T>(string valueName) {
		foreach (T value in Enum.GetValues(typeof(T))) {
			if (value.ToString() == valueName) {
				return value;
			}
		}

		throw new UnityException("Value could not be found");
	}

	/*public static void ShowUnitHudText(GameObject unit, System.Object message, Color color, float duration) {
		GameObject hudText = UnityEngine.Object.Instantiate(Resources.Load("UI/HudText"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		
		Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		Camera uiCamera = GameObject.Find("GUI").transform.FindChild("Camera").GetComponent<Camera>();

		hudText.transform.parent = GameObject.Find("GUI").transform;
		
		Vector3 unitScreenPosition = mainCamera.WorldToScreenPoint(unit.transform.position);
		Vector3 hudPosition = uiCamera.ScreenToWorldPoint(unitScreenPosition);
		hudPosition.z = 0;
		hudText.transform.position = hudPosition;
		hudText.GetComponent<UIWidget>().SetAnchor(unit);

		hudText.GetComponent<HUDText>().Add(message, color, duration);
	}*/

    public static string LoadTextStandalone(string fileName)
    {
        string path = Path.Combine("StreamingAssets", fileName);
        path = Path.Combine(Application.dataPath, path);
        StreamReader streamReader = new StreamReader(path);
        string streamString = streamReader.ReadToEnd();
        return streamString;
    }
}

public static class Collections
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

	public static void Swap<T>(this IList<T> list, int x, int y) {
		T aux = list[x];
		list[x] = list[y];
		list[y] = aux;
	}
}

[Serializable]
public class Pair<T, U> {
	[SerializeField]
	public T first;
	[SerializeField]
	public U second;

	public Pair(T first, U second) {
		this.first = first;
		this.second = second;
	}
}

[Serializable]
public class Circle {
	[SerializeField]
	public Vector3 center;
	[SerializeField]
	public float radius;

	public Circle(Vector3 center, float radius) {
		this.center = center;
		this.radius = radius;
	}

    public Vector3 GetRandomInteriorPoint(float dispersion) {
        var point = new Vector3(UnityEngine.Random.Range(0,dispersion * radius),0,0);
        return center + Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), Vector3.forward) * point;
    }
}