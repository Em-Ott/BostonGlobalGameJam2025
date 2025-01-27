using System;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Characters : MonoBehaviour
{
    private int money;
    public String type;
    private Boolean cat;
    private Boolean rat;
    private Boolean cow; 
    
    void Start() {
        cat = true;
        rat = false;
        cow = false;
    }
    // Update is called once per frame
    void Update()
    {
        money = ManagerScript.Instance.money;
        if (money >= 50) {
            rat = true;
            Debug.Log(rat);
        }

        if (money >= 200) {
            cow = true;
            Debug.Log(cow);
        }
        
        if (type.Equals("rat") && rat) {
            transform.position = new Vector2(-2.909f, -0.689f);
        }

        if (type.Equals("cow") && cow) {
            transform.position = new Vector2(-4.27f, 0.1f);
        }
    }

    public Boolean hasRat() {
        return rat;
    }

    public Boolean hasCow() {
        return cow;
    }
}
