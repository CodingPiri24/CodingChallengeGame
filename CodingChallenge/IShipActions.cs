namespace CodingChallenge;

public interface IShipActions
{
    public interface IShipActions
    {
        void dock(StarShip starShip,Starbase starbase);
        
        void undock(StarShip starShip,Starbase starbase);

        void attackStarShip(StarShip attackerShip ,StarShip starShipToAttack );

        void repairStarShip(StarShip starShipToRepair);
        
    }
}