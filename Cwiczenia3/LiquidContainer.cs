namespace Cwiczenia3;
public class LiquidContainer : AbstractContainer, IHazardNotifier
{
    public bool IsHazardous { get; private set; }
    private static int _numOfConteiners = 1;

    public LiquidContainer(int height, double tareWeight, int depth, double maxPayload, bool isHazardous) : base(height, tareWeight, depth, maxPayload)
    {
        IsHazardous = isHazardous;
        SerialNumber += "-L-" + _numOfConteiners.ToString();
        _numOfConteiners++;
    }

    public override void LoadCargo(double mass)
    {
        double maxAllowedMass = IsHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;
        if (mass > maxAllowedMass)
        {
            NotifyHazard();
            // throw new OverfillException("Attempting to load hazardous material beyond safe capacity.");
        }
        base.LoadCargo(mass);
    }

    public void NotifyHazard()
    {
        // Тут можна додати логіку оповіщення про небезпеку,
        // наприклад, вивести повідомлення в консоль або залогувати його.
        Console.WriteLine("Uwaga. Kontener z numerem seryjnym " + SerialNumber + ": wykryto naruszenie przepisów dotyczących obsługi materiałów niebezpiecznych.");
    }

    public override string ToString()
    {
        return base.ToString() +
               "\n Niebiezpieczny ładunek : " + IsHazardous;
    }
}
