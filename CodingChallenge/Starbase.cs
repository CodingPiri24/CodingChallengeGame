namespace CodingChallenge;

public class Starbase
{
    private int maximum_defence_strength {get; set;}
    private int max_health {get; set;}
    private int current_health {get; set;}
    private int current_defence_strength {get; set;}
    private List<StarShip> dockedShips {get; set;}
    private bool isDisabled {get; set;}
    private Sector CurrentStarBaseSector {get; set;}
    
    private Fleet FleetLocationOfStarbase {get; set;}
    

    public Starbase(int max_health, int current_health, List<StarShip> dockedShips,Sector starbaseSector,Fleet fleet)
    {
        this.dockedShips = dockedShips;
        this.max_health = max_health;
        this.current_health = current_health;
        this.max_health = max_health;
        this.CurrentStarBaseSector = starbaseSector;
        this.FleetLocationOfStarbase = fleet;
    }

    public int getCurrentDefenceStrength()
    {
        int calc1 = maximum_defence_strength * (current_health / max_health);
        int calc2 = dockedShips.Count * (dockedShips.Count / current_defence_strength);
        return calc1 + calc2;
    }

    public Sector getCurrentSector()
    {
        return CurrentStarBaseSector;
    }

    public Fleet getFleet()
    {
        return FleetLocationOfStarbase;
    }

    public void take_Damage_From_Opponent(int damage)
    {
        this.current_health -= damage;
        if (this.current_health < 0)
        {
            isDisabled = true;
            foreach (var dockedShip in dockedShips)
            {
                dockedShip.isDisabled = true;
            }
        }
    }

    public void adddockStarShip(StarShip starshipToDock)
    {
        dockedShips.Add(starshipToDock);
    }

 
}