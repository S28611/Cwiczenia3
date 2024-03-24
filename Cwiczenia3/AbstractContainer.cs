namespace Cwiczenia3;

public class AbstractContainer
{
    public double CargoMass { get; protected set; }
    public int Height { get; private set; }
    public double TareWeight { get; private set; } // Вага порожнього контейнера
    public int Depth { get; private set; }
    public string SerialNumber { get; protected set; }
    public double MaxPayload { get; private set; } // Максимальна вантажопідйомність

    protected AbstractContainer(int height, double tareWeight, int depth, double maxPayload)
    {
        Height = height;
        TareWeight = tareWeight;
        Depth = depth;
        MaxPayload = maxPayload;
        SerialNumber = "KON";
    }

    public virtual void LoadCargo(double mass)
    {
        if (CargoMass + mass > MaxPayload)
            throw new OverfillException("Masa ładunku przekracza maksymalną pojemność kontenera.");
        
        CargoMass += mass;
    }

    public double GetMass()
    {
        return CargoMass + TareWeight;
    }
    // Метод для вивантаження вантажу
    public virtual void UnloadCargo()
    {
        CargoMass = 0;
    }

    public override string ToString()
    {
        return "Kontener z numerem seryjnym " + SerialNumber +
               "\n Masa Ładunku : " + CargoMass + 
               "\n Waga wlasna : " + TareWeight +
               "\n Masksymalna ładowność : " + MaxPayload;
    }

    public override int GetHashCode()
    {
        return SerialNumber.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        // Безпечне приведення типу
        AbstractContainer other = (AbstractContainer)obj;

        // Порівняння серійних номерів
        return this.SerialNumber == other.SerialNumber;
    }
}