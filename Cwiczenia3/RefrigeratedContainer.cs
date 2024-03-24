namespace Cwiczenia3;

public class RefrigeratedContainer : AbstractContainer
{
    public string ProductType { get; private set; }
    public double Temperature { get; private set; }
    
    private static int _numOfConteiners = 1;
    private static  Dictionary<string, double> _productTemperatureRequirements = new Dictionary<string, double>
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18 },
        { "Fish" , 2},
        { "Meat" , -15},
        {"Ice cream", -18},
        {"Frozen pizza", -30},
        {"Cheese", 7.2},
        {"Sausages", 5},
        {"Butter", 20.5},
        {"Eggs", 19}
    };

    public RefrigeratedContainer(int height, double tareWeight, int depth, double maxPayload, string productType, double temperature) : base(height, tareWeight, depth, maxPayload)
    {
        if (!_productTemperatureRequirements.ContainsKey(productType))
        {
            throw new ArgumentException("Nieprawidłowy typ produktu dla kontenera chłodniczego.");
        }
        if (temperature < _productTemperatureRequirements[productType])
        {
            throw new ArgumentException("Temperatura jest zbyt niska dla określonego typu produktu.");
        }
        ProductType = productType;
        Temperature = temperature;
        SerialNumber += "-C-" + _numOfConteiners.ToString();
        _numOfConteiners++;
    }
    public void SetTemperature(double newTemperature)
    {
        // Перевірка, чи не нижча нова температура від дозволеної для цього типу продукту
        if (!_productTemperatureRequirements.ContainsKey(ProductType))
        {
            throw new ArgumentException("Nie rozpoznano typu produktu.");
        }
        if (newTemperature < _productTemperatureRequirements[ProductType])
        {
            throw new ArgumentException($"Temperatura nie może być niższa niż wymagane minimum dla {ProductType}.");
        }

        Temperature = newTemperature;
    }

    public override string ToString()
    {
        return base.ToString() +
               "\n Rodzaj produktu : " + ProductType +
               "\n Temperatura : " + Temperature;
    }
}