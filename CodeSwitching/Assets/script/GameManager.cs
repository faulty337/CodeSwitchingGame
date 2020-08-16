using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static string _ID;
    private static string _Game;
    private static int _Subject;
    private static int _Level;
    private static string _Lan_1;
    private static string _Lan_2;
    private static int _state;

    public static string ID
    {
        get;
        set;
    }

    public static string Game
    {
        get;
        set;
    }

    public static string Subject
    {
        get;
        set;
    }

    public static int Level
    {
        get;
        set;
    }

    public static string Lan_1
    {
        get;
        set;
    }
    public static string Lan_2
    {
        get;
        set;
    }
    public static int state
    {
        get;
        set;
    }
}
