using Cwiczenia3;

class Program
{
    static void Main()
    {
        var ships = new List<Ship>(); // Список кораблів
        var containers = new List<AbstractContainer>(); // Список контейнерів

        while (true)
        {
            Console.WriteLine("Lista kontenerowców:");
            if (ships.Count == 0)
            {
                Console.WriteLine("Brak");
            }
            else
            {
                foreach (var ship in ships)
                {
                    Console.WriteLine(ships.IndexOf(ship) + " : " + ship);
                }
            }

            Console.WriteLine("\nLista kontenerów:");
            if (containers.Count == 0)
            {
                Console.WriteLine("Brak");
            }
            else
            {
                foreach (var container in containers)
                {
                    Console.WriteLine(containers.IndexOf(container) + ": " + container);
                }
            }

            Console.WriteLine("\nMożliwe akcje:");
            Console.WriteLine("1. Dodaj kontenerowiec");
            Console.WriteLine("2. Dodaj kontener");
            Console.WriteLine("3. Wyświetl kontenerowiec");
            Console.WriteLine("4. Wyświetl kontener");
            Console.WriteLine("5. Załaduj kontener");
            Console.WriteLine("6. Rozładuj kontener ");
            Console.WriteLine("7. Załaduj kontener na kontenerowiec");
            Console.WriteLine("8. Załaduj liste kontenerów na kontenerowiec");
            Console.WriteLine("9. Usun kontener ze statku");
            Console.WriteLine("10. Usun kontener z listy");
            Console.WriteLine("11. Wymiana kontenera");
            Console.WriteLine("12. Przeniść kontener na inny statek");
            // інші опції...
            Console.WriteLine("0. Wyjście");

            Console.Write("\nWybierz akcję: ");
            string action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    AddShip(ships);
                    break;
                case "2":
                    AddContainer(containers);
                    break;
                case "3":
                    DisplayShip(ships);
                    break;
                case "4":
                    DisplayContainer(containers);
                    break;
                case "5":
                    LoadContainer(containers);
                    break;
                case "6":
                    UnloadContainer(containers);
                    break;
                case "7":
                    LoadContainerOnShip(ships, containers);
                    break;
                case "8":
                    LoadMultipleContainersOnShip(ships, containers);
                    break;
                case "9":
                    RemoveContainerFromShip(ships);
                    break;
                case "10":
                    RemoveContainerFromList(containers);
                    break;
                case "11":
                    ExchangeContainerOnShip(ships, containers);
                    break;
                case "12":
                    TransferContainerToAnotherShip(ships);
                    break;
                case "0":
                    Console.WriteLine("Zakończenie pracy programu.");
                    return;
                default:
                    Console.WriteLine("Nieznana akcja.");
                    break;
            }
        }
    }

    // Методи для виконання операцій
    private static void AddShip(List<Ship> ships)
    {
        Console.WriteLine("Podaj maksymalną prędkość kontenerowca:");
        double maxSpeed = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną liczbę kontenerów:");
        int maxContainerLoad = int.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną masę ładunku w tonach:");
        double maxMassLoad = double.Parse(Console.ReadLine());

        Ship newShip = new Ship(maxSpeed, maxContainerLoad, maxMassLoad);
        ships.Add(newShip);

        Console.WriteLine("Kontenerowiec dodany pomyślnie.");
    }
    private static void AddContainer(List<AbstractContainer> containers)
    {
        Console.WriteLine("Wybierz typ kontenera: 1 - Liquid, 2 - Gas, 3 - Refrigerated");
        string containerType = Console.ReadLine();
        AbstractContainer newContainer = null;

        Console.WriteLine("Podaj wysokość:");
        int height = int.Parse(Console.ReadLine());

        Console.WriteLine("Podaj ciężar własny:");
        double tareWeight = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj głębokość:");
        int depth = int.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną ładowność:");
        double maxPayload = double.Parse(Console.ReadLine());

        // Stworzenie odpowiedniego kontenera w zależności od typu
        switch (containerType)
        {
            case "1":
                Console.WriteLine("Czy ładunek jest niebezpieczny? (true/false)");
                bool isHazardous = bool.Parse(Console.ReadLine());
                newContainer = new LiquidContainer(height, tareWeight, depth, maxPayload, isHazardous);
                break;
            case "2":
                Console.WriteLine("Podaj ciśnienie:");
                double pressure = double.Parse(Console.ReadLine());
                newContainer = new GasContainer(height, tareWeight, depth, maxPayload, pressure);
                break;
            case "3":
                Console.WriteLine("Podaj typ produktu:");
                string productType = Console.ReadLine();
                Console.WriteLine("Podaj temperaturę:");
                double temperature = double.Parse(Console.ReadLine());
                newContainer = new RefrigeratedContainer(height, tareWeight, depth, maxPayload, productType, temperature);
                break;
            default:
                Console.WriteLine("Nieznany typ kontenera.");
                return;
        }

        containers.Add(newContainer);
        Console.WriteLine("Kontener dodany pomyślnie.");
    }
    private static void DisplayShip(List<Ship> ships)
    {
        Console.WriteLine("Podaj numer indeksu kontenerowca do wyświetlenia:");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index < ships.Count)
        {
            Console.WriteLine(ships[index]);
        }
        else
        {
            Console.WriteLine("Nieprawidłowy indeks.");
        }
    }

    private static void DisplayContainer(List<AbstractContainer> containers)
    {
        Console.WriteLine("Podaj numer indeksu kontenera do wyświetlenia:");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index < containers.Count)
        {
            Console.WriteLine(containers[index]);
        }
        else
        {
            Console.WriteLine("Nieprawidłowy indeks.");
        }
    }
    private static void LoadContainer(List<AbstractContainer> containers)
    {
        Console.WriteLine("Podaj numer indeksu kontenera, który chcesz załadować:");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index < containers.Count)
        {
            Console.WriteLine("Podaj masę ładunku do załadowania:");
            double mass;
            if (double.TryParse(Console.ReadLine(), out mass))
            {
                try
                {
                    containers[index].LoadCargo(mass);
                    Console.WriteLine("Kontener został załadowany.");
                }
                catch (OverfillException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowa masa.");
            }
        }
        else
        {
            Console.WriteLine("Nieprawidłowy indeks kontenera.");
        }
    }
    private static void UnloadContainer(List<AbstractContainer> containers)
    {
        Console.WriteLine("Podaj numer indeksu kontenera do rozładowania:");
        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index < containers.Count)
        {
            containers[index].UnloadCargo();
            Console.WriteLine("Kontener został rozładowany.");
        }
        else
        {
            Console.WriteLine("Nieprawidłowy indeks kontenera.");
        }
    }
    private static void LoadContainerOnShip(List<Ship> ships, List<AbstractContainer> containers)
    {
        Console.WriteLine("Podaj indeks kontenerowca:");
        int shipIndex = int.Parse(Console.ReadLine());
        if (shipIndex < 0 || shipIndex >= ships.Count)
        {
            Console.WriteLine("Nieprawidłowy indeks kontenerowca.");
            return;
        }

        Console.WriteLine("Podaj indeks kontenera do załadowania:");
        int containerIndex = int.Parse(Console.ReadLine());
        if (containerIndex < 0 || containerIndex >= containers.Count)
        {
            Console.WriteLine("Nieprawidłowy indeks kontenera.");
            return;
        }

        Ship ship = ships[shipIndex];
        AbstractContainer container = containers[containerIndex];

        try
        {
            ship.AddContainer(container);
            Console.WriteLine("Kontener został załadowany na kontenerowiec.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private static void LoadMultipleContainersOnShip(List<Ship> ships, List<AbstractContainer> containers)
    {
        Console.WriteLine("Podaj indeks kontenerowca:");
        int shipIndex = int.Parse(Console.ReadLine());
        if (shipIndex < 0 || shipIndex >= ships.Count)
        {
            Console.WriteLine("Nieprawidłowy indeks kontenerowca.");
            return;
        }

        Console.WriteLine("Podaj indeksy kontenerów do załadowania, oddzielone przecinkami:");
        string[] containerIndices = Console.ReadLine().Split(',');
        List<AbstractContainer> containersToLoad = new List<AbstractContainer>();
    
        foreach (var indexString in containerIndices)
        {
            if (int.TryParse(indexString, out int containerIndex) && containerIndex >= 0 && containerIndex < containers.Count)
            {
                containersToLoad.Add(containers[containerIndex]);
            }
            else
            {
                Console.WriteLine($"Nieprawidłowy indeks kontenera: {indexString}");
                return;
            }
        }

        Ship ship = ships[shipIndex];

        try
        {
            ship.AddContainersList(containersToLoad);
            Console.WriteLine("Lista kontenerów została załadowana na kontenerowiec.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private static void RemoveContainerFromShip(List<Ship> ships)
    {
        Console.WriteLine("Podaj indeks kontenerowca:");
        int shipIndex = int.Parse(Console.ReadLine());
        if (shipIndex < 0 || shipIndex >= ships.Count)
        {
            Console.WriteLine("Nieprawidłowy indeks kontenerowca.");
            return;
        }

        Ship ship = ships[shipIndex];
        Console.WriteLine("Podaj numer seryjny kontenera do usunięcia:");
        string serialNumber = Console.ReadLine();

        var containerToRemove = ship.Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (containerToRemove != null)
        {
            ship.RemoveContainer(containerToRemove);
            Console.WriteLine("Kontener został usunięty z kontenerowca.");
        }
        else
        {
            Console.WriteLine("Nie znaleziono kontenera o podanym numerze seryjnym na tym statku.");
        }
    }
    private static void RemoveContainerFromList(List<AbstractContainer> containers)
    {
        Console.WriteLine("Podaj indeks kontenera do usunięcia:");
        int index = int.Parse(Console.ReadLine());
        if (index < 0 || index >= containers.Count)
        {
            Console.WriteLine("Nieprawidłowy indeks kontenera.");
            return;
        }

        containers.RemoveAt(index);
        Console.WriteLine("Kontener został usunięty z listy.");
    }
    private static void ExchangeContainerOnShip(List<Ship> ships, List<AbstractContainer> containers)
    {
        Console.WriteLine("Podaj indeks kontenerowca:");
        int shipIndex = int.Parse(Console.ReadLine());
        if (shipIndex < 0 || shipIndex >= ships.Count)
        {
            Console.WriteLine("Nieprawidłowy indeks kontenerowca.");
            return;
        }

        Console.WriteLine("Podaj numer seryjny kontenera do wymiany:");
        string serialNumber = Console.ReadLine();

        Console.WriteLine("Podaj indeks nowego kontenera:");
        int newContainerIndex = int.Parse(Console.ReadLine());
        if (newContainerIndex < 0 || newContainerIndex >= containers.Count)
        {
            Console.WriteLine("Nieprawidłowy indeks kontenera.");
            return;
        }

        if (ships[shipIndex].SwitchContainer(serialNumber, containers[newContainerIndex]))
        {
            Console.WriteLine("Kontener został wymieniony pomyślnie.");
        }
    }
    private static void TransferContainerToAnotherShip(List<Ship> ships)
    {
        Console.WriteLine("Podaj indeks kontenerowca, z którego chcesz przenieść kontener:");
        int sourceShipIndex = int.Parse(Console.ReadLine());
        if (sourceShipIndex < 0 || sourceShipIndex >= ships.Count)
        {
            Console.WriteLine("Nieprawidłowy indeks kontenerowca.");
            return;
        }

        Console.WriteLine("Podaj indeks kontenerowca, do którego chcesz przenieść kontener:");
        int targetShipIndex = int.Parse(Console.ReadLine());
        if (targetShipIndex < 0 || targetShipIndex >= ships.Count)
        {
            Console.WriteLine("Nieprawidłowy indeks kontenerowca docelowego.");
            return;
        }

        Console.WriteLine("Podaj numer seryjny kontenera do przeniesienia:");
        string serialNumber = Console.ReadLine();

        AbstractContainer containerToTransfer = ships[sourceShipIndex].Containers
            .FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (containerToTransfer == null)
        {
            Console.WriteLine("Nie znaleziono kontenera o podanym numerze seryjnym na statku źródłowym.");
            return;
        }

        if (ships[sourceShipIndex].TransferContainer(ships[targetShipIndex], containerToTransfer))
        {
            Console.WriteLine("Kontener został przeniesiony na inny statek.");
        }
    }



}
