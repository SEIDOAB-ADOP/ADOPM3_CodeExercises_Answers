using Seido.Utilities.SeedGenerator;

namespace Delegate3;

public enum CarColor { Brown, Red, Green, Burgundy }
public enum CarBrand { Boxcar, Ford, Jaguar, Honda }
public enum CarModel { Boxmodel, Mustang_GT, Golf, V70, XF, Civic }
public class Car
{
    public CarColor Color { get; init; }
    public CarBrand Brand { get; set; }
    public CarModel Model { get; init; }
    public int YearModel {get; init;}

    public Car Seed (SeedGenerator _seeder) => 
        new Car() {
            Color = _seeder.FromEnum<CarColor>(),
            Brand = _seeder.FromEnum<CarBrand>(),
            Model = _seeder.FromEnum<CarModel>(),
            YearModel = _seeder.Next(1970, 2024)
            };
}
