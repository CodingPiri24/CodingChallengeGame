namespace CodingChallenge;

public class StarShip : IShipActions
{
    public String name { get; set; }
    public Fleet fleet { get; set; }
    public Sector sectorStarShipIsLocatedIn { get; set; }
    public Starbase starBaseDockedIn { get; set; }

    public int maxHealthOfStarShip { get; set; }
    public int maxAttackStrength { get; set; }
    public int maxCrew { get; set; }
    public int maxDefenceStrength { get; set; }

    private List<StarShip> dockedShips { get; set; }

    public int currentHealth { get; set; }
    public int currentCrew { get; set; }
    public int numberof_skipActionsRemaining { get; set; }

    public bool isDisabled { get; set; }
    public bool isDocked { get; set; }

    public int currentAttackStrength { get; set; }
    public int currentDefenceStrength { get; set; }

    public StarShip(int currentDefenceStrength, string name, Fleet fleet, Sector sectorStarShipIsLocatedIn,
        Starbase starBaseDockedIn, int maxHealthOfStarShip, int maxAttackStrength, int maxCrew, int maxDefenceStrength,
        int currentHealth, int currentCrew)
    {
        this.name = name;
        this.fleet = fleet;
        this.sectorStarShipIsLocatedIn = sectorStarShipIsLocatedIn;
        this.starBaseDockedIn = starBaseDockedIn;
        this.maxHealthOfStarShip = maxHealthOfStarShip;
        this.maxAttackStrength = maxAttackStrength;
        this.maxCrew = maxCrew;
        this.maxDefenceStrength = maxDefenceStrength;
        this.currentHealth = currentHealth;
        this.currentCrew = currentCrew;
        this.currentDefenceStrength = currentDefenceStrength;
    }

    public bool validateAttackOnTarget(StarShip attackerShip, StarShip targetShip)
    {
        if (attackerShip.fleet != targetShip.fleet &
            (targetShip.sectorStarShipIsLocatedIn == attackerShip.sectorStarShipIsLocatedIn))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void attackStarShip(StarShip starShipToAttack)
    {
        if (validateAttackOnTarget(this, starShipToAttack) && !isDocked)
        {
            int calc1 = this.currentAttackStrength - starShipToAttack.currentDefenceStrength;
            int damageToPerform = calc1 > 5 ? calc1 : 5;

            starShipToAttack.currentHealth -= damageToPerform;
            int damageToCurrentCrewOfTarget = starShipToAttack.currentCrew -
                                              (starShipToAttack.currentCrew * damageToPerform) /
                                              starShipToAttack.currentDefenceStrength;
            if (damageToCurrentCrewOfTarget >= 1)
            {
                starShipToAttack.currentCrew = damageToCurrentCrewOfTarget;
            }
        }
    }

    public void take_Damage_From_Opponent(int damage)
    {
        if (numberof_skipActionsRemaining > 0)
        {
            numberof_skipActionsRemaining -= 1;
            return;
        }

        currentHealth -= damage;
    }

    public void repairStarShip(StarShip starShipToRepair)
    {
        if (starShipToRepair.isDocked)
        {
            int numberOfActionsToSkipWhileRepiar = 0;

            int currenthealthOfStarShipToRepair = starShipToRepair.currentHealth;
            int currentHealthPercentageOfMaxHealth = (starShipToRepair.maxHealthOfStarShip -
                                                      currenthealthOfStarShipToRepair /
                                                      starShipToRepair.maxHealthOfStarShip) * 100;
            if (currentHealthPercentageOfMaxHealth < 25)
            {
                numberOfActionsToSkipWhileRepiar = 4;
            }
            else if (currentHealthPercentageOfMaxHealth < 50)
            {
                numberOfActionsToSkipWhileRepiar = 3;
            }
            else if (currentHealthPercentageOfMaxHealth < 75)
            {
                numberOfActionsToSkipWhileRepiar = 2;
            }
            else if (currentHealthPercentageOfMaxHealth < 100)
            {
                numberOfActionsToSkipWhileRepiar = 1;
            }

            starShipToRepair.numberof_skipActionsRemaining = numberOfActionsToSkipWhileRepiar;
        }
    }


    public void dock(StarShip starShipToDock, List<Starbase> starbases)
    {
        Sector sectorForStarShipToDock = null;
        Starbase starbasForStarShipToDock = null;
        bool isDockedInAnotherStarbases = false;

        if (numberof_skipActionsRemaining > 0)
        {
            numberof_skipActionsRemaining -= 1;
            return;
        }


        foreach (var starbase in starbases)
        {
            if (starbase.getCurrentSector() == starShipToDock.sectorStarShipIsLocatedIn)
            {
                sectorForStarShipToDock = starbase.getCurrentSector();
                starbasForStarShipToDock = starbase;
            }

            if (starbase.getFleet().activeStarShips.Contains(starShipToDock))
            {
                isDockedInAnotherStarbases = true;
            }
        }


        if (sectorForStarShipToDock != null && starbases != null && !isDockedInAnotherStarbases)
        {
            if (starbasForStarShipToDock.getFleet() == this.fleet)
            {
                starShipToDock.isDocked = true;
                starbasForStarShipToDock.adddockStarShip(starShipToDock);
            }
        }
    }

    public void undock(StarShip starShipToUndock)
    {
        if (numberof_skipActionsRemaining > 0)
        {
            numberof_skipActionsRemaining -= 1;
            return;
        }

        starShipToUndock.numberof_skipActionsRemaining = 0;
        starShipToUndock.isDocked = false;
    }

    public void MoveTo(Sector sector)
    {
        if (!isDocked)
        {
            if (numberof_skipActionsRemaining > 0)
            {
                numberof_skipActionsRemaining -= 1;
                return;
            }

            this.sectorStarShipIsLocatedIn = sector;
        }
    }

    public int getCurrentAttackStrenght()
    {
        return (maxAttackStrength * (currentHealth / maxHealthOfStarShip));
    }

    public int getCurrentDefenceStrenght()
    {
        return (maxDefenceStrength * ((currentHealth + currentCrew) / (maxHealthOfStarShip + maxCrew)));
    }
}