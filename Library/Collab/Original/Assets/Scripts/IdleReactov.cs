using UnityEngine;
using UnityEngine.UI;

public class IdleReactov : Singleton<IdleReactov>
{
    public Text PowerText;
    public Text clickValueText;
    public double power;
    public double powerClickValue;

    public Text PowerPerSecText;
    public Text clickUpgrade1Text;
    public Text clickUpgrade2Text;
    public Text productionUpgrade1Text;
    public Text productionUpgrade2Text;

    public double PowerPerSecond;
    public double clickUpgrade1Cost;
    public int clickUpgrade1Level;

    public double productionUpgrade1Cost;
    public double productionUpgrade1Power;
    public int productionUpgrade1Level;


    //
    public double clickUpgrade2Cost;
    public int clickUpgrade2Level;
    //
    public double productionUpgrade2Cost;
    public double productionUpgrade2Power;
    public int productionUpgrade2Level;
    //Prestige
    public Text antimatterText;
    public Text antimatterBoostText;
    public Text antimatterToGetText;

    public double antimatter;
    public double antimatterBoost;
    public double antimatterToGet;

    // Tick time
    public float tickTime = 0.1F;

    // Use conduit systems
    public bool useConduits = false;
    // Use conductor systems
    public bool useConductor = true;



    //Load save
    public void Start()
    {
        Load();
    }

    public void Load()
    {
        power = double.Parse(PlayerPrefs.GetString("power", "0"));
        powerClickValue = double.Parse(PlayerPrefs.GetString("powerClickValue", "1"));
        clickUpgrade1Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade1Cost", "10"));
        clickUpgrade2Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade2Cost", "100"));
        productionUpgrade1Cost = double.Parse(PlayerPrefs.GetString("productionUpgrade1Cost", "25"));
        productionUpgrade2Cost = double.Parse(PlayerPrefs.GetString("productionUpgrade2Cost", "250"));
        productionUpgrade1Power = double.Parse(PlayerPrefs.GetString("productionUpgrade2Power", "1"));
        productionUpgrade2Power = double.Parse(PlayerPrefs.GetString("productionUpgrade2Power", "5"));

        antimatter = double.Parse(PlayerPrefs.GetString("antimatter", "0"));
        //Upgrades load
        clickUpgrade1Level = PlayerPrefs.GetInt("clickUpgrade1Level", 0);
        clickUpgrade2Level = PlayerPrefs.GetInt("clickUpgrade2Level", 0);
        productionUpgrade1Level = PlayerPrefs.GetInt("productionUpgrade1Level", 0);
        productionUpgrade2Level = PlayerPrefs.GetInt("productionUpgrade2Level", 0);

    }

    public void Save()
    {
        PlayerPrefs.SetString("power", power.ToString());
        PlayerPrefs.SetString("powerClickValue", powerClickValue.ToString());
        PlayerPrefs.SetString("clickUpgrade1Cost", clickUpgrade1Cost.ToString());
        PlayerPrefs.SetString("clickUpgrade2Cost", clickUpgrade2Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade1Cost", productionUpgrade1Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade2Cost", productionUpgrade2Cost.ToString());
        PlayerPrefs.SetString("productionUpgrade1Power", productionUpgrade1Power.ToString());
        PlayerPrefs.SetString("productionUpgrade2Power", productionUpgrade2Power.ToString());

        PlayerPrefs.SetString("antimatter", antimatter.ToString());
        //Upgrades saves
        PlayerPrefs.SetInt("clickUpgrade1Level", clickUpgrade1Level);
        PlayerPrefs.SetInt("clickUpgrade2Level", clickUpgrade2Level);
        PlayerPrefs.SetInt("productionUpgrade1Level", productionUpgrade1Level);
        PlayerPrefs.SetInt("productionUpgrade2Level", productionUpgrade2Level);

    }

    public void Update()
    {
        antimatterToGet = (150 * System.Math.Sqrt(power / 1e8));
        antimatterBoost = (antimatter * 0.01) + 1;

        
        antimatterToGetText.text = "Prestige:\n+" + System.Math.Floor(antimatterToGet).ToString("F0") + " Antimatter";
        antimatterText.text = "Antimatter: " + System.Math.Floor(antimatter).ToString("F0");
        antimatterBoostText.text = antimatterBoost.ToString("F2") + "x boost";
       


        PowerPerSecond = ((productionUpgrade1Power * productionUpgrade1Level) + (productionUpgrade2Power * productionUpgrade2Level)) * antimatterBoost;

        if (powerClickValue > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(powerClickValue))));
            var mantissa = (powerClickValue / System.Math.Pow(10, exponent));
            clickValueText.text = "Click\n+" + mantissa.ToString("F2") + "e" + exponent + " Power";
        }
        else
            clickValueText.text = "Click\n+" + powerClickValue.ToString("F0") + " Power";

        if (power > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(power))));
            var mantissa = (power / System.Math.Pow(10, exponent));
            PowerText.text = "Power: " + mantissa.ToString("F2") + "e" + exponent;
        }
        else
            PowerText.text = "Power: " + power.ToString("F0");




        PowerPerSecText.text = PowerPerSecond.ToString("F0") + " Power/s";
        //Production Active
        //Upgrade 1
        string clickUpgrade1CostString;
        if (clickUpgrade1Cost > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(clickUpgrade1Cost))));
            var mantissa = (clickUpgrade1Cost / System.Math.Pow(10, exponent));
            clickUpgrade1CostString = mantissa.ToString("F2") + "e" + exponent;
        }
        else
            clickUpgrade1CostString = clickUpgrade1Cost.ToString("F0");
        string clickUpgrade1LevelString;
        if (clickUpgrade1Level > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(clickUpgrade1Level))));
            var mantissa = (clickUpgrade1Level / System.Math.Pow(10, exponent));
            clickUpgrade1LevelString = mantissa.ToString("F2") + "e" + exponent;
        }
        else
            clickUpgrade1LevelString = clickUpgrade1Level.ToString("F0");
        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1CostString + " power\nAmout: +1 Click\nLevel: " + clickUpgrade1Level;
        //Upgrade 2
        clickUpgrade2Text.text = "Click Upgrade 2\nCost: " + clickUpgrade2Cost.ToString("F0") + " power\nAmout: +5 Click\nLevel: " + clickUpgrade2Level;

        //Production Passive
        productionUpgrade1Text.text = "Production Upgrade 1\nCost: " + productionUpgrade1Cost.ToString("F0") + " power\nAmout:" + (productionUpgrade1Power * antimatterBoost).ToString("F2") + " Click/s\nLevel: " + productionUpgrade1Level;
        productionUpgrade2Text.text = "Production Upgrade 2\nCost: " + productionUpgrade2Cost.ToString("F0") + " power\nAmout: +" + (productionUpgrade2Power * antimatterBoost).ToString("F2") + " Click/s\nLevel: " + productionUpgrade2Level;

        power += PowerPerSecond * Time.deltaTime;

        Save();
    }

    //Prestige
    public void Prestige()
    {
        if (power > 1000)
        {
            power = 0;
            powerClickValue = 1;
            clickUpgrade1Cost = 10;
            clickUpgrade2Cost = 100;
            productionUpgrade1Cost = 25;
            productionUpgrade2Cost = 250;
            productionUpgrade1Power = 1;
            productionUpgrade2Power = 5;

            clickUpgrade1Level = 0;
            clickUpgrade2Level = 0;
            productionUpgrade1Level = 0;
            productionUpgrade2Level = 0;

            antimatter += antimatterToGet;
            
        }

    }




    //Button
    public void Click()
    {
        
        power += powerClickValue;
    }

    public void BuyClickUpgrade1()
    {
        if (power >= clickUpgrade1Cost)
        {
            clickUpgrade1Level++;
            power -= clickUpgrade1Cost;
            clickUpgrade1Cost *= 1.07;
            powerClickValue++;
        }
    }

    public void BuyClickUpgrade2()
    {
        if (power >= clickUpgrade2Cost)
        {
            clickUpgrade2Level++;
            power -= clickUpgrade2Cost;
            clickUpgrade2Cost *= 1.09;
            powerClickValue += 5;
            
        }
    }
    public void BuyProductionUpgrade1()
    {
        if (power >= productionUpgrade1Cost)
        {
            productionUpgrade1Level++;
            power -= productionUpgrade1Cost;
            productionUpgrade1Cost *= 1.07;
            
        }
    }
    public void BuyProductionUpgrade2()
    {
        if (power >= productionUpgrade2Cost)
        {
            productionUpgrade2Level++;
            power -= productionUpgrade2Cost;
            productionUpgrade2Cost *= 1.07;
            
        }
    }


    public void AddPower(double gain)
    {
        power += gain * antimatterBoost;
    }
}
