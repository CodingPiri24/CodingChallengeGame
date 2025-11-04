namespace CodingChallenge;

public  class Fleet{

    public  List<StarShip> activeStarShips {get; set;}
    public  List<Starbase> starbases {get; set;}
    
    public  String nameOfPlayer_FleetBelongsTo;

    public Fleet(String playernamer,List<Starbase> starbases ,List<StarShip> starships){
        this.starbases = starbases;
        this.activeStarShips = starships;
        this.nameOfPlayer_FleetBelongsTo = playernamer;
    }

    public void MobiliseToSector(Sector sector){
        
        foreach (var activeStarShip in activeStarShips)
        {
            activeStarShip.MoveTo(sector);
        }
    }

    public void AttackTarget(StarShip target){
        
        foreach (var activeStarShip in activeStarShips)
        {
            activeStarShip.attackStarShip(target);
        }
    }
}