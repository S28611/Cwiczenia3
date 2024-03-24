namespace Cwiczenia3;

public class GasContainer : AbstractContainer,IHazardNotifier
{
    private static int _numOfConteiners = 1;
    public double Pressure  { get;  set; }
    public GasContainer(int height, double tareWeight, int depth, double maxPayload, double pressure) : base(height, tareWeight, depth, maxPayload)
    {
        SerialNumber += "-G-" + _numOfConteiners.ToString();
        _numOfConteiners++;
        Pressure = pressure;
    }

    public override void UnloadCargo()
    {
        if (CargoMass > 0)
        {
            CargoMass = CargoMass * 0.05;
        }
    }

    public void NotifyHazard()
    {
        Console.WriteLine("Uwaga. Kontener z numerem seryjnym " + SerialNumber + ": wykryto naruszenie przepisów dotyczących obsługi materiałów niebezpiecznych.");
    }

    public override string ToString()
    {
        return base.ToString() +
               "\n Ciśnienie : " + Pressure;
    }
}