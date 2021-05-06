using UnityEngine;
using UnityEngine.UI;

public class IdleReactov : Singleton<IdleReactov>
{
    public Text PowerText;
    public Text clickValueText;
    public double power;
    public double powerClickValue;

    public Text PowerPerSecText;

    public double PowerPerSecond;
    //Click power variables
    public double clickUpgrade1Cost;
    public int clickUpgrade1Level;
    public Text clickUpgrade1Text;
    public double clickUpgrade2Cost;
    public int clickUpgrade2Level;
    public Text clickUpgrade2Text;

    //Production  variables
    public double [] productionUpgradeCost = new double [6];
    public double [] productionUpgradePower = new double [6];
    public int [] productionUpgradeLevel = new int [6];
    public Text [] productionUpgradeText = new Text [6];
    public Text [] productionInfo = new Text [6];
    public Text [] BuyInfo = new Text [6];
    //Cooling variables
    public double[] coolingUpgradeCost = new double[4];
    public double[] coolingUpgradePower = new double[4];
    public int[] coolingUpgradeLevel = new int[4];
    public Text[] coolingUpgradeText = new Text[4];
    public Text[] coolingInfo = new Text[4];
    public Text[] coolingBuyInfo = new Text[4];


    //Prestige variables
    public Text antimatterText;
    public Text antimatterBoostText;
    public Text antimatterToGetText;

    public double antimatter;
    public double antimatterBoost;
    public double antimatterToGet;

    // Map size
    public int x_size = 15;
    public int y_size = 15;
    // Tick time
    public float tickTime = 0.1F;

    // Use conduit systems
    public bool useConduits = false;
    // Use conductor systems
    public bool useConductor = true;

    // Grid power statisctics
    private double [] PowerGeneratedOverLastSecond = new double[5];     // 0=T1, 1=T2, 2=T3, 3=T4, 4=T5                 DO NOT TOUCH, CALCULATIONS ONLY
    private double [] PowerGeneratedPerSecond = new double[6];           // 0=T1, 1=T2, 2=T3, 3=T4, 4=T5, 5=SumOfAll     TOUCH ME!!!


    
    public void Start()
    {
        Load();//Load save

        InvokeRepeating("TrackGridPower", 0, 1.0F);

    }

    public void Load() //Loading sesion
    {
        
        power = double.Parse(PlayerPrefs.GetString("power", "0"));
        powerClickValue = double.Parse(PlayerPrefs.GetString("powerClickValue", "1"));
        clickUpgrade1Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade1Cost", "10"));
        clickUpgrade2Cost = double.Parse(PlayerPrefs.GetString("clickUpgrade2Cost", "100"));
        productionUpgradeCost[0] = double.Parse(PlayerPrefs.GetString("productionUpgradeCost[0]", "25"));
        productionUpgradeCost[1] = double.Parse(PlayerPrefs.GetString("productionUpgradeCost[1]", "250"));
        productionUpgradeCost[2] = double.Parse(PlayerPrefs.GetString("productionUpgradeCost[2]", "3750"));
        productionUpgradeCost[3] = double.Parse(PlayerPrefs.GetString("productionUpgradeCost[3]", "75000"));
        productionUpgradeCost[4] = double.Parse(PlayerPrefs.GetString("productionUpgradeCost[4]", "150000"));
        productionUpgradePower[0] = double.Parse(PlayerPrefs.GetString("productionUpgradePower[0]", "1"));
        productionUpgradePower[1] = double.Parse(PlayerPrefs.GetString("productionUpgradePower[1]", "5"));
        productionUpgradePower[2] = double.Parse(PlayerPrefs.GetString("productionUpgradePower[2]", "50"));
        productionUpgradePower[3] = double.Parse(PlayerPrefs.GetString("productionUpgradePower[3]", "100"));
        productionUpgradePower[4] = double.Parse(PlayerPrefs.GetString("productionUpgradePower[4]", "250"));
        coolingUpgradeCost[0] = double.Parse(PlayerPrefs.GetString("coolingUpgradeCost[0]", "1000"));
        coolingUpgradeCost[1] = double.Parse(PlayerPrefs.GetString("coolingUpgradeCost[1]", "4000"));
        coolingUpgradeCost[2] = double.Parse(PlayerPrefs.GetString("coolingUpgradeCost[2]", "16000"));
        coolingUpgradeCost[3] = double.Parse(PlayerPrefs.GetString("coolingUpgradeCost[3]", "64000"));
        coolingUpgradePower[0] = double.Parse(PlayerPrefs.GetString("coolingUpgradePower[0]", "2"));
        coolingUpgradePower[1] = double.Parse(PlayerPrefs.GetString("coolingUpgradePower[1]", "8"));
        coolingUpgradePower[2] = double.Parse(PlayerPrefs.GetString("coolingUpgradePower[2]", "32"));
        coolingUpgradePower[3] = double.Parse(PlayerPrefs.GetString("coolingUpgradePower[3]", "128"));

        antimatter = double.Parse(PlayerPrefs.GetString("antimatter", "0"));
        //Upgrades load
        clickUpgrade1Level = PlayerPrefs.GetInt("clickUpgrade1Level", 0);
        clickUpgrade2Level = PlayerPrefs.GetInt("clickUpgrade2Level", 0);
        productionUpgradeLevel[0] = PlayerPrefs.GetInt("productionUpgradeLevel[0]", 0);
        productionUpgradeLevel[1] = PlayerPrefs.GetInt("productionUpgradeLevel[1]", 0);
        productionUpgradeLevel[2] = PlayerPrefs.GetInt("productionUpgradeLevel[2]", 0);
        productionUpgradeLevel[3] = PlayerPrefs.GetInt("productionUpgradeLevel[3]", 0);
        productionUpgradeLevel[4] = PlayerPrefs.GetInt("productionUpgradeLevel[4]", 0);
        coolingUpgradeLevel[0] = PlayerPrefs.GetInt("coolingUpgradeLevel[0]", 0);
        coolingUpgradeLevel[1] = PlayerPrefs.GetInt("coolingUpgradeLevel[1]", 0);
        coolingUpgradeLevel[2] = PlayerPrefs.GetInt("coolingUpgradeLevel[2]", 0);
        coolingUpgradeLevel[3] = PlayerPrefs.GetInt("coolingUpgradeLevel[3]", 0);
        
    }

    public void Save() //Saving sesion
    {
        PlayerPrefs.SetString("BuildingsTileList.Instance.GetPlacedBuildings()", BuildingsTileList.Instance.GetPlacedBuildings().ToString());
        PlayerPrefs.SetString("power", power.ToString());
        PlayerPrefs.SetString("powerClickValue", powerClickValue.ToString());
        PlayerPrefs.SetString("clickUpgrade1Cost", clickUpgrade1Cost.ToString());
        PlayerPrefs.SetString("clickUpgrade2Cost", clickUpgrade2Cost.ToString());
        PlayerPrefs.SetString("productionUpgradeCost[0]", productionUpgradeCost[0].ToString());
        PlayerPrefs.SetString("productionUpgradeCost[1]", productionUpgradeCost[1].ToString());
        PlayerPrefs.SetString("productionUpgradeCost[2]", productionUpgradeCost[2].ToString());
        PlayerPrefs.SetString("productionUpgradeCost[3]", productionUpgradeCost[3].ToString());
        PlayerPrefs.SetString("productionUpgradeCost[4]", productionUpgradeCost[4].ToString());
        PlayerPrefs.SetString("productionUpgradePower[0]", productionUpgradePower[0].ToString());
        PlayerPrefs.SetString("productionUpgradePower[1]", productionUpgradePower[1].ToString());
        PlayerPrefs.SetString("productionUpgradePower[2]", productionUpgradePower[2].ToString());
        PlayerPrefs.SetString("productionUpgradePower[3]", productionUpgradePower[3].ToString());
        PlayerPrefs.SetString("productionUpgradePower[4]", productionUpgradePower[4].ToString());

        PlayerPrefs.SetString("antimatter", antimatter.ToString());
        PlayerPrefs.SetInt("clickUpgrade1Level", clickUpgrade1Level);
        PlayerPrefs.SetInt("clickUpgrade2Level", clickUpgrade2Level);
        PlayerPrefs.SetInt("productionUpgradeLevel[0]", productionUpgradeLevel[0]);
        PlayerPrefs.SetInt("productionUpgradeLevel[1]", productionUpgradeLevel[1]);
        PlayerPrefs.SetInt("productionUpgradeLevel[2]", productionUpgradeLevel[2]);
        PlayerPrefs.SetInt("productionUpgradeLevel[3]", productionUpgradeLevel[3]);
        PlayerPrefs.SetInt("productionUpgradeLevel[4]", productionUpgradeLevel[4]);

    }

    public void Update() //Main void
    {
        //Antimatter for prestige system
        antimatterToGet = (150 * System.Math.Sqrt(power / 1e8));
        antimatterBoost = (antimatter * 0.01) + 1;
        antimatterToGetText.text = "Prestige:\n+" + System.Math.Floor(antimatterToGet).ToString("F0") + " Antimatter";
        antimatterText.text = "Antimatter: " + System.Math.Floor(antimatter).ToString("F0");
        antimatterBoostText.text = antimatterBoost.ToString("F2") + "x boost";
       

        //Power per second
        PowerPerSecond = PowerGeneratedPerSecond[5];

        //Converting numbers in to shorter type of writing
        
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

        if (PowerPerSecond > 1000)
        {
            var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(PowerPerSecond))));
            var mantissa = (PowerPerSecond / System.Math.Pow(10, exponent));
            PowerPerSecText.text = "Power/s: " + mantissa.ToString("F2") + "e" + exponent;
        }
        else
            PowerPerSecText.text = "Power/s: " + PowerPerSecond.ToString("F0");

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
        
        string[] productionUpgradeCostString = new string [6];
        for (int x = 0;x<5; x++)
        {
            if (productionUpgradeCost[x] > 1000)
            {
                var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(productionUpgradeCost[x]))));
                var mantissa = (productionUpgradeCost[x] / System.Math.Pow(10, exponent));
                productionUpgradeCostString[x] = mantissa.ToString("F2") + "e" + exponent;
            }
            else
                productionUpgradeCostString[x] = productionUpgradeCost[x].ToString("F0");
        }
        string[] coolingUpgradeCostString = new string[4];
        for (int x = 0; x < 4; x++)
        {
            if (coolingUpgradeCost[x] > 1000)
            {
                var exponent = (System.Math.Floor(System.Math.Log10(System.Math.Abs(coolingUpgradeCost[x]))));
                var mantissa = (coolingUpgradeCost[x] / System.Math.Pow(10, exponent));
                coolingUpgradeCostString[x] = mantissa.ToString("F2") + "e" + exponent;
            }
            else
                coolingUpgradeCostString[x] = coolingUpgradeCost[x].ToString("F0");
        }

        //Click upgrade
        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1CostString + " power\nAmout: +1 Click\nLevel: " + clickUpgrade1Level;
        clickUpgrade2Text.text = "Click Upgrade 2\nCost: " + clickUpgrade2Cost.ToString("F0") + " power\nAmout: +5 Click\nLevel: " + clickUpgrade2Level;

        //Production Passive Info on Buttons
        //Tier 1

        productionUpgradeText[0].text = "Production Upgrade 1\nCost: " + productionUpgradeCostString[0] + " power\nAmout:" + (productionUpgradePower[0] * antimatterBoost).ToString("F2") + " Power/s\nLevel: " + productionUpgradeLevel[0];
        productionInfo[0].text = "Tier 1  power production:\n" + (PowerGeneratedPerSecond[0]).ToString("F0") + " Power/s";
        BuyInfo[0].text = "Reactor Tier 1\n Power & Heat:" + productionUpgradePower[0] * (productionUpgradeLevel[0] + 1) + "\n Cost: 250" ;
        //Tier 2
        productionUpgradeText[1].text = "Production Upgrade 2\nCost: " + productionUpgradeCostString[1] + " power\nAmout: +" + (productionUpgradePower[1] * antimatterBoost).ToString("F2") + " Power/s\nLevel: " + productionUpgradeLevel[1];
        productionInfo[1].text = "Tier 2  power production:\n" + (PowerGeneratedPerSecond[1]).ToString("F0") + " Power/s";
        BuyInfo[1].text = "Reactor Tier 2\n Power & Heat:" + productionUpgradePower[1] * (productionUpgradeLevel[1] + 1) + "\n Cost: 2500";
        //Tier 3
        productionUpgradeText[2].text = "Production Upgrade 3\nCost: " + productionUpgradeCostString[2] + " power\nAmout: +" + (productionUpgradePower[2] * antimatterBoost).ToString("F2") + " Power/s\nLevel: " + productionUpgradeLevel[2];
        productionInfo[2].text = "Tier 3  power production:\n" + (PowerGeneratedPerSecond[2]).ToString("F0") + " Power/s";
        BuyInfo[2].text = "Reactor Tier 3\n Power & Heat:" + productionUpgradePower[2] * (productionUpgradeLevel[2] + 1) + "\n Cost: 37500";
        //Tier 4
        productionUpgradeText[3].text = "Production Upgrade 4\nCost: " + productionUpgradeCostString[3] + " power\nAmout: +" + (productionUpgradePower[3] * antimatterBoost).ToString("F2") + " Power/s\nLevel: " + productionUpgradeLevel[3];
        productionInfo[3].text = "Tier 4  power production:\n" + (PowerGeneratedPerSecond[3]).ToString("F0") + " Power/s";
        BuyInfo[3].text = "Reactor Tier 4\n Power & Heat:" + productionUpgradePower[3] * (productionUpgradeLevel[3] + 1) + "\n Cost: 750000";
        //Tier 5
        productionUpgradeText[4].text = "Production Upgrade 5\nCost: " + productionUpgradeCostString[4] + " power\nAmout: +" + (productionUpgradePower[4] * antimatterBoost).ToString("F2") + " Power/s\nLevel: " + productionUpgradeLevel[4];
        productionInfo[4].text = "Tier 5  power production:\n" + (PowerGeneratedPerSecond[4]).ToString("F0") + " Power/s";
        BuyInfo[4].text = "Reactor Tier 5\n Power & Heat:" + productionUpgradePower[4] * (productionUpgradeLevel[4] + 1) + "\n Cost: 1500000";

        //Cooling info Buttons
        //Tier 1
        coolingUpgradeText[0].text = "Production Upgrade 1\nCost: " + coolingUpgradeCostString[0] + " power\nAmout:" + (coolingUpgradePower[0] * antimatterBoost).ToString("F2") + " Power/s\nLevel: " + coolingUpgradeLevel[0];
        coolingInfo[0].text = "Tier 1  cooling production:\n- " + (PowerGeneratedPerSecond[0]).ToString("F0") + " Heat/s";
        coolingBuyInfo[0].text = "Cooler Tier 1\n Heat:-" + coolingUpgradePower[0] * (coolingUpgradeLevel[0] + 1) + "\n Cost: 1000";
        //Tier 2
        coolingUpgradeText[1].text = "Production Upgrade 2\nCost: " + coolingUpgradeCostString[1] + " power\nAmout:" + (coolingUpgradePower[1] * antimatterBoost).ToString("F2") + " Power/s\nLevel: " + coolingUpgradeLevel[1];
        coolingInfo[1].text = "Tier 2  cooling production:\n- " + (PowerGeneratedPerSecond[1]).ToString("F0") + " Heat/s";
        coolingBuyInfo[1].text = "Cooler Tier 2\n Heat:-" + coolingUpgradePower[1] * (coolingUpgradeLevel[1] + 1) + "\n Cost: 4000";
        //Tier 3
        coolingUpgradeText[2].text = "Production Upgrade 3\nCost: " + coolingUpgradeCostString[2] + " power\nAmout:" + (coolingUpgradePower[2] * antimatterBoost).ToString("F2") + " Power/s\nLevel: " + coolingUpgradeLevel[2];
        coolingInfo[2].text = "Tier 3  cooling production:\n- " + (PowerGeneratedPerSecond[2]).ToString("F0") + " Heat/s";
        coolingBuyInfo[2].text = "Cooler Tier 3\n Heat:-" + coolingUpgradePower[2] * (coolingUpgradeLevel[2] + 1) + "\n Cost: 16000";
        //Tier 4
        coolingUpgradeText[3].text = "Production Upgrade 4\nCost: " + coolingUpgradeCostString[3] + " power\nAmout:" + (coolingUpgradePower[3] * antimatterBoost).ToString("F2") + " Power/s\nLevel: " + coolingUpgradeLevel[3];
        coolingInfo[3].text = "Tier 4  cooling production:\n -" + (PowerGeneratedPerSecond[3]).ToString("F0") + " Heat/s";
        coolingBuyInfo[3].text = "Cooler Tier 4\n Heat:-" + coolingUpgradePower[3] * (coolingUpgradeLevel[3] + 1) + "\n Cost: 64000";







        //Increase of power (money)
        power += PowerPerSecond * Time.deltaTime;
        //Save of session
        Save();
    }

    //Reset everything for Prestige
    public void Prestige()
    {
        if (power > 1000)
        {
            power = 0;
            powerClickValue = 1;
            clickUpgrade1Cost = 10;
            clickUpgrade2Cost = 100;
            productionUpgradeCost[0] = 25;
            productionUpgradeCost[1] = 250;
            productionUpgradeCost[2] = 3750;
            productionUpgradeCost[3] = 75000;
            productionUpgradeCost[4] = 150000;
            productionUpgradePower[0] = 1;
            productionUpgradePower[1] = 5;
            productionUpgradePower[2] = 50;
            productionUpgradePower[3] = 100;
            productionUpgradePower[4] = 250;
            clickUpgrade1Level = 0;
            clickUpgrade2Level = 0;
            productionUpgradeLevel[0] = 0;
            productionUpgradeLevel[1] = 0;
            productionUpgradeLevel[2] = 0;
            productionUpgradeLevel[3] = 0;
            productionUpgradeLevel[4] = 0;
            //Cooling
            coolingUpgradeCost[0] = 1000;
            coolingUpgradeCost[1] = 4000;
            coolingUpgradeCost[2] = 16000;
            coolingUpgradeCost[3] = 64000;
            coolingUpgradePower[0] = 2;
            coolingUpgradePower[1] = 8;
            coolingUpgradePower[2] = 32;
            coolingUpgradePower[3] = 128;
            coolingUpgradeLevel[0] = 0;
            coolingUpgradeLevel[1] = 0;
            coolingUpgradeLevel[2] = 0;
            coolingUpgradeLevel[3] = 0;
            antimatter += antimatterToGet;
        }

    }




    //Buttons to Upgrade things
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
    public void BuyProductionUpgrade(int x)
    {
        if (power >= productionUpgradeCost[x])
        {
            productionUpgradeLevel[x]++;
            power -= productionUpgradeCost[x];
            productionUpgradeCost[x] *= (1.50+(x*0.1));
            
        }
    }
    public void BuyCoolingUpgrade(int x)
    {
        if (power >= coolingUpgradeCost[x])
        {
            coolingUpgradeLevel[x]++;
            power -= coolingUpgradeCost[x];
            coolingUpgradeCost[x] *= (1.50 + (x * 0.1));

        }
    }

    public void AddPower(double gain, int tier)
    {
        power += gain * antimatterBoost;
        PowerGeneratedOverLastSecond[tier] += gain * antimatterBoost;
    }

    private void TrackGridPower()
    {
        PowerGeneratedPerSecond[5] = 0;
        for (int i=0; i<5; i++)
        {
            PowerGeneratedPerSecond[i] = PowerGeneratedOverLastSecond[i];
            PowerGeneratedOverLastSecond[i] = 0;
            PowerGeneratedPerSecond[5] += PowerGeneratedPerSecond[i];
        }
    }

    public bool CheckIfYouAreBroke(double price)                    //Checks if you can afford to buy building
    {
        if (power > price)
        {
            power -= price;
            return true;
        }
        return false;
    }
}
