using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

/// <summary>
/// Represents a single currency
/// </summary>
[Serializable]
public class Currency
{
    public CurrencyType currencyType;
    public int amount;
    private PlayerCurrency.CurrenyChangedEvent valueChanged;
    public void Subscribe(PlayerCurrency.CurrenyChangedEvent subscriber)
    {
        valueChanged += subscriber;
    }

    internal void AddAmount(int amountToAdd)
    {
        amount += amountToAdd;
        if (valueChanged != null)
            valueChanged(amount);
    }
}

[Serializable]
public enum CurrencyType
{
    [XmlEnum("1")]
    Gold,
    [XmlEnum("2")]
    Gems
};