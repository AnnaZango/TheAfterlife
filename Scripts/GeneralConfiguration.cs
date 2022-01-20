using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneralConfiguration 
{
    //Life (brains)
    private static int currentLives = 10;
    private static int maxLives = 10;
    private static int minLives = 0;
    private static int numBrainsPickup = 1;

    //Projectiles (bones)
    private static int numCurrentProjectiles = 25;
    private static int numMaxProjectiles = 25;
    private static int numProjectilePickup = 5;
    
    //Coins
    private static int numCurrentCoins = 0;
    private static int numCoinsPickup = 5;

    //Hurting elements & Boss projectile
    private static int damageHurtingElements = 10;
    private static int damageProjectileBoss = 3;


    public static int GetCurrentNumLives()
    {
        return currentLives;
    }
    public static void SetCurrentNumLives(int numLivesToSet)
    {
        currentLives = numLivesToSet;
    }
    public static int GetNumMaxLives()
    {
        return maxLives;
    }
    public static int GetNumMinLives()
    {
        return minLives;
    }
    public static int GetNumBrainsPickup()
    {
        return numBrainsPickup;
    }

    public static int GetNumProjectiles()
    {
        return numCurrentProjectiles;
    }
    public static void SetNumProjectiles(int numProjectilesToSet)
    {
        numCurrentProjectiles = numProjectilesToSet;       
    }
    public static int GetNumMaxProjectiles()
    {
        return numMaxProjectiles;
    }
    public static int GetNumProjectilesPickup()
    {
        return numProjectilePickup;
    }

    public static int GetNumCoins()
    {
        return numCurrentCoins;
    }
    public static void SetNumCoins(int numCoinsToSet)
    {
        numCurrentCoins = numCoinsToSet;
    }
    public static int GetNumCoinsPickup()
    {
        return numCoinsPickup;
    }

    public static int GetDamageHurtingElements()
    {
        return damageHurtingElements;
    }
    public static int GetDamageProjectileBoss()
    {
        return damageProjectileBoss;
    }
}
