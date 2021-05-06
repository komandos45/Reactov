using UnityEngine;
using UnityEngine.UI;

public class IdleReactov : MonoBehaviour
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
    public int productionUpgrade1Level;
    //
    public double clickUpgrade2Cost;
    public int clickUpgrade2Level;
    //
    public double productionUpgrade2Cost;
    public double productionUpgrade2Power;
    public int productionUpgrade2Level;


    public void Start()
    {
        powerClickValue = 1;
        clickUpgrade1Cost = 10;
        clickUpgrade2Cost = 100;
        productionUpgrade1Cost = 25;
        productionUpgrade2Cost = 250;
        productionUpgrade2Power = 5;
    }

    public void Update()
    {
        PowerPerSecond = productionUpgrade1Level + (productionUpgrade2Power * productionUpgrade2Level);

        clickValueText.text = "Click\n" + powerClickValue + " Power";
        PowerText.text = "Power: " + power.ToString("F0");
        PowerPerSecText.text = PowerPerSecond.ToString("F0") + " Power/s";

        clickUpgrade1Text.text = "Click Upgrade 1\nCost: " + clickUpgrade1Cost.ToString("F0") + " power\nAmout: +1 Click\nLevel: " + clickUpgrade1Level;
        clickUpgrade2Text.text = "Click Upgrade 2\nCost: " + clickUpgrade2Cost.ToString("F0") + " power\nAmout: +5 Click\nLevel: " + clickUpgrade2Level;
       
        //Production Passive
        productionUpgrade1Text.text = "Production Upgrade 1\nCost: " + productionUpgrade1Cost.ToString("F0") + " power\nAmout: +1 Click/s\nLevel: " + productionUpgrade1Level;
        productionUpgrade2Text.text = "Production Upgrade 2\nCost: " + productionUpgrade2Cost.ToString("F0") + " power\nAmout: +" + productionUpgrade2Power + " Click/s\nLevel: " + productionUpgrade2Level;

        power += PowerPerSecond * Time.deltaTime;
    
    
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

}
