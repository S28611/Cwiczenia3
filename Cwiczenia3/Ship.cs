namespace Cwiczenia3;

public class Ship
{
    public List<AbstractContainer> Containers { get; private set; }
    public double MaxSpeed;
    public int MaxContainerLoad { get; private set; }
    public double MaxMassLoad { get; private set; }

    public Ship(List<AbstractContainer> containers, double maxSpeed, int maxContainerLoad, double maxMassLoad)
    {
        Containers = containers;
        MaxSpeed = maxSpeed;
        MaxContainerLoad = maxContainerLoad;
        MaxMassLoad = maxMassLoad;
    }

    public Ship(double maxSpeed, int maxContainerLoad, double max)
    {
        MaxSpeed = maxSpeed;
        MaxContainerLoad = maxContainerLoad;
        MaxMassLoad = max;
        Containers = new List<AbstractContainer>();
    }

    private double CountSelfMassLoad()
    {
        double res = 0;
        
        foreach (AbstractContainer container in Containers)
        {
            res += container.GetMass();
        }
        
        return res;
    }

    public void AddContainer(AbstractContainer newContainer)
    {
        if (Containers.Count >= MaxContainerLoad)
        {
            Console.WriteLine("Statek osiągnął maksymalną pojemność kontenerów.");
            return;
        }

        if (CountSelfMassLoad() + newContainer.GetMass() > MaxMassLoad)
        {
            Console.WriteLine("Dodanie kontenera przekroczyłoby maksymalną masę ładunku statku.");
            return;
        }
        Containers.Add(newContainer);
    }

    public void AddContainersList(List<AbstractContainer> containersToLoad)
    {
        if (Containers.Count + containersToLoad.Count > MaxContainerLoad)
        {
            Console.WriteLine("Statek osiągnął maksymalną pojemność kontenerów.");
            return;
        }

        double massToLoad = 0;
        foreach (AbstractContainer container in containersToLoad)
        {
            massToLoad += container.GetMass();
        }

        if (CountSelfMassLoad() + massToLoad > MaxMassLoad)
        {
            Console.WriteLine("Statek osiągnął maksymalną masę ładunku.");
            return;
        }
        Containers.AddRange(containersToLoad);
    }
    
    public void RemoveContainer(AbstractContainer containerToRemove)
    {
        if (!Containers.Contains(containerToRemove))
        {
            Console.WriteLine("Nie ma takiego kontenera na statku.");
            return;
        }
        
        Containers.Remove(containerToRemove);
        
    }

    public bool SwitchContainer(string serialNumber, AbstractContainer containerToLoad)
    {
        var index = Containers.FindIndex(c => c.SerialNumber == serialNumber);
        if (index < 0)
        {
            Console.WriteLine("Nie znaleziono kontenera o numerze seryjnym: " + serialNumber);
            return false;
        }

        if (CountSelfMassLoad() - Containers[index].GetMass() + containerToLoad.GetMass() > MaxMassLoad)
        {
            Console.WriteLine("Zmiana kontenera przekroczyłaby maksymalną masę ładunku statku.");
            return false;
        }

        Containers[index] = containerToLoad;
        return true;
    }
    public bool TransferContainer(Ship targetShip, AbstractContainer containerToTransfer)
    {
        // Перевірка, чи контейнер існує на поточному кораблі
        if (!Containers.Contains(containerToTransfer))
        {
            Console.WriteLine("Nie ma takiego kontenera na statku.");
            return false;
        }

        // Перевірка на максимальне навантаження цільового корабля
        if (targetShip.MaxContainerLoad < targetShip.Containers.Count + 1)
        {
            Console.WriteLine("Statek docelowy osiągnął maksymalną pojemność kontenerów.");
            return false;
        }

        // Перевірка на максимальну вагу цільового корабля
        if (targetShip.CountSelfMassLoad() + containerToTransfer.GetMass() > targetShip.MaxMassLoad)
        {
            Console.WriteLine("Transfer kontenera przekroczyłby maksymalną masę ładunku statku docelowego.");
            return false;
        }

        // Видалення контейнера з поточного корабля і додавання його на цільовий
        Containers.Remove(containerToTransfer);
        targetShip.Containers.Add(containerToTransfer);

        return true;
    }
    public bool TransferContainer(Ship targetShip, string serialNumber)
    {
        var index = Containers.FindIndex(c => c.SerialNumber == serialNumber);
        if (index < 0)
        {
            Console.WriteLine("Nie znaleziono kontenera o numerze seryjnym: " + serialNumber);
            return false;
        }
        
        // Перевірка на максимальне навантаження цільового корабля
        if (targetShip.MaxContainerLoad < targetShip.Containers.Count + 1)
        {
            Console.WriteLine("Statek docelowy osiągnął maksymalną pojemność kontenerów.");
            return false;
        }

        // Перевірка на максимальну вагу цільового корабля
        if (targetShip.CountSelfMassLoad() + Containers[index].GetMass() > targetShip.MaxMassLoad)
        {
            Console.WriteLine("Transfer kontenera przekroczyłby maksymalną masę ładunku statku docelowego.");
            return false;
        }

        // Видалення контейнера з поточного корабля і додавання його на цільовий
        targetShip.Containers.Add(Containers[index]);
        Containers.Remove(Containers[index]);

        return true;
    }

    public override string ToString()
    {
        string allContainersString = "";
        foreach (AbstractContainer container in Containers)
        {
            allContainersString += container.SerialNumber + "\n";
        }
        return "Statek z:" +
               "\n maksymalną liczbą kontenerów " + MaxContainerLoad +
               "\n maksymalną wagą kontenerów " + MaxMassLoad +
               "\n i kontenerami :\n" + allContainersString;
    }
}