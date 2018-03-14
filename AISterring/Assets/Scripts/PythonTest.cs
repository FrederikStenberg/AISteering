using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IronPython.Hosting;

public class PythonTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var engine = Python.CreateEngine();
        var scope = engine.CreateScope();

        string code = "str = 'Hello world!'";

        var source = engine.CreateScriptSourceFromString(code);
        source.Execute(scope);

        Debug.Log(scope.GetVariable<string>("str"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
