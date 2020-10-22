using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class T_ShipVisualisationWindow : EditorWindow
{
    static Sprite shipSprite;
    static T_WeaponScriptable weapon;

    private List<float> projectileDistance = new List<float>();

    private float burstTime, attackTime=99;
    private float speedMultipiler = 0.194f; //210 étant la taille de la fenêtre et 1080 la résolution de l'écran de jeu, j'ai 210/1080 = 0.194
    private int currentBurst;
    private float refreshTime = 0.001f;
    private float lastRefresh;

    //[MenuItem("ComboMaker/Node window")]
    public static void Create(T_WeaponScriptable newWeap, Sprite newShip)
    {
        // Get existing open window or if none, make a new one:
        weapon = newWeap;
        shipSprite = newShip;
        T_ShipVisualisationWindow window = (T_ShipVisualisationWindow)GetWindow(typeof(T_ShipVisualisationWindow),false,weapon.nom);
        window.maxSize = new Vector2(210f, 210f);
        window.minSize = window.maxSize;
        window.Show();
    }

    public void OnGUI()
    {
        refreshTime = (float)EditorApplication.timeSinceStartup - lastRefresh;
        lastRefresh = (float)EditorApplication.timeSinceStartup;

        Vector2 spriteDef = new Vector2(60, 60);
        DrawSprite(new Rect(100 - spriteDef.x / 2, 100 - spriteDef.y / 2, spriteDef.x, spriteDef.y), shipSprite);
        Handles.BeginGUI();
        Handles.color = Color.red;

        List<Vector3> coordList = ProjectileDirections(weapon.lazerByBurst, weapon.angleBeetwenLazers);
        if (projectileDistance.Count != weapon.burstNumber)
        {
            ResetBurst();
        }

        attackTime += refreshTime;
        if (attackTime >= weapon.recoveryTime)
        {
            attackTime = 0;
            ResetProjectileDistance();
        }

        burstTime += refreshTime;
        if (burstTime >= weapon.burstDelay && currentBurst < weapon.burstNumber)
        {
            burstTime = 0;
            currentBurst++;
        }

        for (int i = 0; i < weapon.lazerByBurst; i++)
        {
            Handles.color = Color.red;
            Handles.DrawLine(new Vector3(100, 100), coordList[i] * -100 + new Vector3(100, 100));
            Handles.color = Color.yellow;
            for (int k = 0; k < currentBurst; k++)
            {
                Handles.DrawSolidDisc(new Vector3(100, 100) + coordList[i] * -100 * projectileDistance[k], Vector3.forward, 3);
            }
        }

        //Debug.Log(currentBurst + "  >  " + projectileDistance.Count);
        for (int k = 0; k < currentBurst; k++)
        {
            if (projectileDistance[k] > 1)
            {
                projectileDistance[k] = -2;
            }
            else if (projectileDistance[k] >= 0)
            {
                projectileDistance[k] += refreshTime * weapon.lazerSpeed * speedMultipiler;
                Debug.Log(speedMultipiler);
            }
        }
        Handles.EndGUI();
    }

    private void ResetBurst()
    {
        projectileDistance = new List<float>();
        for (int i = 0; i < weapon.burstNumber; i++)
        {
            projectileDistance.Add(0);
        }
        currentBurst = 0;
        burstTime = 0;
    }

    private void ResetProjectileDistance()
    {
        Debug.Log("New attack");
        for(int i = 0; i < projectileDistance.Count; i++)
        {
            projectileDistance[i]=0;
        }
        currentBurst = 0;
        burstTime = 0;
    }

    private void DrawSprite(Rect position, Sprite sprite)
    {
        // on récupère la taille du sprite, en pixels, dans le référentiel de la texture d'où il est issu
        Vector2 fullSize = new Vector2(sprite.texture.width, sprite.texture.height);
        Vector2 size = new Vector2(sprite.textureRect.width, sprite.textureRect.height);

        // on récupère les coordonnées du sprite au sein de la texture au format UV, c'est à dire entre 0 et 1
        Rect coords = sprite.textureRect;
        coords.x /= fullSize.x;
        coords.width /= fullSize.x;
        coords.y /= fullSize.y;
        coords.height /= fullSize.y;

        // quelle différence d'échelle de taille entre la texture réelle et l'espace qu'on a en GUI pour la dessiner ?
        Vector2 ratio;
        ratio.x = position.width / size.x;
        ratio.y = position.height / size.y;
        float minRatio = Mathf.Min(ratio.x, ratio.y);

        // on corrige la position/taille du rectangle où tracer la texture en GUI
        Vector2 center = position.center;
        position.width = size.x * minRatio;
        position.height = size.y * minRatio;
        position.center = center;

        // enfin, on dessine le morceau de texture correspondant au sprite
        GUI.DrawTextureWithTexCoords(position, sprite.texture, coords, true);
    }

    List<Vector3> ProjectileDirections(int nbProjectile, int angle)
    {
        List<Vector3> projectiles = new List<Vector3>();

        Vector2 newTraj = Vector2.up;
        Vector2 vecToAdd = newTraj;

        if(nbProjectile%2 == 0)
        {
            vecToAdd = Quaternion.Euler(0, 0, -angle / 2) * newTraj;
        }

        projectiles.Add(vecToAdd);

        for (int i = 2; i <= nbProjectile; i++)
        {

            if (i%2 == 0)
            {
                vecToAdd = Quaternion.Euler(0, 0, angle * (int)(i / 2)) * projectiles[0];
                
            }
            else
            {
                vecToAdd = Quaternion.Euler(0, 0, -angle*(int)(i/2)) * projectiles[0];
            }
            projectiles.Add(vecToAdd);
        }

        return new List<Vector3>(projectiles);
    }
}
